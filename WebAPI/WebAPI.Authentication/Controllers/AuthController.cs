using System.Collections;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Authentication.UseCases.Dto;
using WebAPI.Authentication.UseCases.Requests;

namespace WebAPI.Authentication.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private IMediator _mediator = null!;

    protected IMediator Mediator => _mediator ??= HttpContext
      .RequestServices
      .GetRequiredService<IMediator>();

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

    /// <summary>
    /// Register new user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /auth/RegisterUser.
    /// </remarks>
    /// <param name="registerUser">RegisterUserDto.</param>
    /// <returns>Response model.</returns>
    /// <response code="200">Success.</response>
    [HttpPost("RegisterUser")]
    [AllowAnonymous]
    public async Task<ActionResult<Response>> RegisterUser([FromBody] RegisterUserDto registerUser)
    {
      var request = new RegisterUserCommand() { RegisterUserDto = registerUser };
      return Ok(await Mediator.Send(request));
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
    public async Task<ActionResult<Response>> SignIn([FromBody] LoginDto login)
    {
      var request = new SignInCommand() { LoginDto = login };
      return Ok(await Mediator.Send(request));
    }
  }
}
