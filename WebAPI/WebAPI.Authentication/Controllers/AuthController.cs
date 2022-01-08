using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Tranfers;

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
    public async Task<ActionResult<Response>> RegisterUser([FromBody] RegisterUserDto userData)
    {
      var request = new RegisterUserCommand() {RegisterUserDto = userData};
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
      var request = new SignInCommand() {LoginDto = login};
      return Ok(await Mediator.Send(request));
    }
  }
}
