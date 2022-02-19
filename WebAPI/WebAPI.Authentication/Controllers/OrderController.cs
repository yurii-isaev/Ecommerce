using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Requests.Commands;

namespace WebAPI.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
  /// <remarks>
  /// Sample request:
  /// POST -> ../auth/RegisterUser.
  /// </remarks>
  [HttpPost("CreateUserOrder")]
  public async Task<IActionResult> CreateUserOrder([FromBody] OrderDto order)
  {
    var request = new OrderCommand() {OrderDto = order};
    return Ok(await Mediator.Send(request));
  }
}
