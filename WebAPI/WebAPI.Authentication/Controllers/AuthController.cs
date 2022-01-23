using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Tranfers.Input;
using WebAPI.Authentication.UseCases.Tranfers.Output;

namespace WebAPI.Authentication.Controllers
{
  [ApiController]
  [Route("api/[controller]")] // http://localhost:5000/api
  public class AuthController : BaseController
  {
    /// <remarks>
    /// Sample request:
    /// GET -> ../auth/GetAllUsers.
    /// </remarks>
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable>> GetUserList()
    {
      var request = new GetUserListQuery();
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// POST -> ../auth/RegisterUser.
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("SignUp")]
    public async Task<ActionResult<ServerResponse>> SignUp([FromBody] RegisterUserDto registerData)
    {
      var request = new RegisterUserCommand() {RegisterUserDto = registerData};
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// GET -> ../auth/GetAuthProfile.
    /// </remarks>
    [Authorize(Roles = "Customer")]
    [HttpGet("GetAuthProfile")]
    public async Task<IActionResult> GetAuthProfile()
    {
      var request = new GetAuthProfileQuery(HttpContext);
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// POST -> ../auth/SignIn.
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("SignIn")]
    public async Task<ActionResult<ServerResponse>> SignIn([FromBody] LoginDto login)
    {
      var request = new SignInCommand() {LoginDto = login};
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Request example:
    /// POST -> ../auth/Logout.
    /// </remarks>
    [Authorize(Roles = "Customer")]
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
      var request = new LogoutUserCommand(HttpContext);
      return Ok(await Mediator.Send(request));
    }

    /// <remarks>
    /// Sample request:
    /// POST -> ..http://localhost:5000/api/auth/ForgotPassword.
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] AccountDto account)
    {
      var request = new ForgotPasswordCommand() {AccountDto = account};
      return Ok(await Mediator.Send(request));
    }
  }
}
