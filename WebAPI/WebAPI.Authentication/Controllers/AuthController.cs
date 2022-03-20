using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;

namespace WebAPI.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")] // http://localhost:5000/api
public class AuthController : BaseController
{
  /// <remarks>
  /// Sample request:
  /// GET -> ../auth/GetAllUsers.
  /// </remarks>
  [Authorize(Roles = "Admin")]
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
  public async Task<IActionResult> SignUp([FromBody] RegisterDto register)
  {
    var request = new RegisterCommand() {RegisterDto = register};
    return Ok(await Mediator.Send(request));
  }

  /// <remarks>
  /// Sample request:
  /// GET -> ../auth/GetAuthProfile.
  /// + Bearer Token in headers
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
  public async Task<IActionResult> SignIn([FromBody] LoginDto login)
  {
    var request = new SignInCommand() {LoginDto = login};
    return Ok(await Mediator.Send(request));
  }

  /// <remarks>
  /// Request example:
  /// POST -> ../auth/Logout.
  /// + Bearer Token in headers
  /// </remarks>
  [Authorize(Roles = "Customer")]
  [HttpPost("Logout")]
  public async Task<IActionResult> Logout()
  {
    var request = new LogoutCommand(HttpContext);
    return Ok(await Mediator.Send(request));
  }

  /// <remarks>
  /// Sample request:
  /// POST -> ../auth/ForgotPassword.
  /// </remarks>
  [AllowAnonymous]
  [HttpPost("ForgotPassword")]
  public async Task<IActionResult> ForgotPassword([FromBody] CredentialDto credential)
  {
    var request = new ForgotPasswordCommand() {CredentialDto = credential};
    return Ok(await Mediator.Send(request));
  }

  /// <remarks>
  /// Sample request:
  /// POST -> ../auth/ChangePassword.
  /// </remarks>
  [AllowAnonymous]
  [HttpPost("ChangePassword")]
  public async Task<IActionResult> ChangePassword([FromBody] CredentialDto credential)
  {
    var request = new ChangePasswordCommand() {CredentialDto = credential};
    return Ok(await Mediator.Send(request));
  }
}
