using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;

namespace WebAPI.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
  public OrderController(IMediator mediator) : base(mediator)
  {}
  
  /// <remarks>
  /// Sample request:
  /// POST -> ../order/CreateOrder
  /// </remarks>
  [Authorize(Roles = "Customer")]
  [HttpPost("CreateOrder")]
  public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
  {
    var request = new CreateOrderCommand() {OrderDto = order};
    return Ok(await Mediator.Send(request));
  }

  /// <remarks>
  /// Sample request:
  /// GET -> ../order/GetOrderList/{userId}
  /// </remarks>
  [Authorize(Roles = "Customer")]
  [HttpGet("GetOrderList/{userId}")]
  public async Task<IActionResult> GetOrderList(string userId)
  {
    var request = new GetOrderListQuery() {UserId = userId};
    return Ok(await Mediator.Send(request));
  }
  
  /// <remarks>
  /// Sample request:
  /// DELETE -> ../order/DeleteOrder/{orderId}
  /// </remarks>
  [Authorize(Roles = "Customer")]
  [HttpDelete("DeleteOrder/{orderId}")]
  public async Task<IActionResult> DeleteOrder(string orderId)
  {
    var request = new DeleteOrderCommand() {OrderId = orderId};
    return Ok(await Mediator.Send(request));
  }
}
