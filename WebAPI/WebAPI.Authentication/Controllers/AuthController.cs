using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Tranfers;
using WebAPI.Authentication.UseCases.Tranfers.Output;

namespace WebAPI.Authentication.Controllers
{
  [ApiController]
  [Route("api/[controller]")] // http://localhost:5000/api
  public class AuthController : BaseController
  {
    /// <remarks>
    /// Sample request:
    /// GET  ../auth/GetAllUsers.
    /// </remarks>
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable>> GetUserList()
    {
      var request = new GetUserListQuery();
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// POST  ../auth/RegisterUser.
    /// </remarks>
    [HttpPost("RegisterUser")]
    [AllowAnonymous]
    public async Task<ActionResult<ServerResponse>> RegisterUser([FromBody] RegisterUserDto userData)
    {
      var request = new RegisterUserCommand() {RegisterUserDto = userData};
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// GET  ../auth/CheckAuthentication.
    /// </remarks>
    [Authorize(Roles = "Customer")]
    [HttpGet("GetAuthProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAuthProfile()
    {
      var request = new GetAuthProfileQuery(HttpContext);
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// POST  ../auth/SignIn.
    /// </remarks>
    [HttpPost("SignIn")]
    [AllowAnonymous]
    public async Task<ActionResult<ServerResponse>> SignIn([FromBody] LoginDto login)
    {
      var request = new SignInCommand() {LoginDto = login};
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Request example:
    /// POST  ../auth/Logout.
    /// </remarks>
    [HttpPost("Logout")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Logout()
    {
      var request = new LogoutUserCommand(HttpContext);
      return Ok(await Mediator.Send(request));
    }
  }
}
