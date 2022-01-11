using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Tranfers;
using WebAPI.Authentication.UseCases.Tranfers.Output;

namespace WebAPI.Authentication.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : BaseController
  {
    /// <summary>
    /// Get all user from database.   
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /auth/GetAllUsers.
    /// </remarks>
    /// <returns>User list.</returns>
    /// <response code="200">Success.</response>
    // [Authorize(Roles = "Admin")]
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable>> GetUserList()
    {
      var request = new GetUserListQuery();
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Request example:
    /// GET http://localhost:5000/api/auth/RegisterUser
    /// </remarks>
    [HttpPost("RegisterUser")]
    [AllowAnonymous]
    public async Task<ActionResult<ServerResponse>> RegisterUser([FromBody] RegisterUserDto userData)
    {
      var request = new RegisterUserCommand() {RegisterUserDto = userData};
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Request example:
    /// GET http://localhost:5000/api/auth/CheckAuthentication
    /// </remarks>
    [HttpGet]
    [Authorize(Roles = "Customer")]
    [Route("CheckAuthentication")]
    public IActionResult CheckAuthentication()
    {
      try
      {
        // Проверяем, аутентифицирован ли пользователь
        if (User.Identity is ClaimsIdentity claimsIdentity && User.Identity.IsAuthenticated)
        {
          // Создаем список необходимых утверждений
          var requiredClaims = new List<(string Type, string Value)>
          {
            (ClaimTypes.NameIdentifier, claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value)!,
            (ClaimTypes.Name, claimsIdentity.FindFirst(ClaimTypes.Name)?.Value)!,
            (ClaimTypes.Email, claimsIdentity.FindFirst(ClaimTypes.Email)?.Value)!,
            ("jti", claimsIdentity.FindFirst("jti")?.Value)!,
            (ClaimTypes.Role, claimsIdentity.FindFirst(ClaimTypes.Role)?.Value)!
          };

          // Проверяем, что все утверждения присутствуют и не пустые
          if (requiredClaims.All(c => !string.IsNullOrEmpty(c.Value)))
          {
            // Извлекаем значения утверждений
            var userId = requiredClaims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userName = requiredClaims.First(c => c.Type == ClaimTypes.Name).Value;
            var userEmail = requiredClaims.First(c => c.Type == ClaimTypes.Email).Value;
            var userRole = requiredClaims.First(c => c.Type == ClaimTypes.Role).Value;

            // Формируем объект профиля для ответа
            var profile = new ProfileDto
            {
              Id = Guid.Parse(userId),
              UserName = userName,
              Email = userEmail,
              Role = userRole
            };

            // Возвращаем успешный ответ с профилем пользователя
            return Ok(new ServerResponse
            {
              Code = ResponseCode.Ok,
              IsValid = true,
              Message = "User is authenticated",
              DataSet = new {profile}
            });
          }
        }

        return Unauthorized(new ServerResponse
        {
          Code = ResponseCode.Unauthorized,
          IsValid = false,
          Message = "User is not authenticated or invalid authentication data"
        });
      }
      catch (Exception ex)
      {
        return new InternalServerErrorResponse("Internal server error: " + ex.Message);
      }
    }

    /// <summary>
    /// Sign in App.  
    /// </summary>
    /// /// <remarks>
    /// Sample request:
    /// POST /auth/SignIn.
    /// </remarks>
    /// <param name="login">LoginDto.</param>
    /// <returns>Response model.</returns>
    // [AllowAnonymous]
    [HttpPost("SignIn")]
    [AllowAnonymous]
    public async Task<ActionResult<ServerResponse>> SignIn([FromBody] LoginDto login)
    {
      var request = new SignInCommand() {LoginDto = login};
      return Ok(await Mediator.Send(request));
    }
  }
}
