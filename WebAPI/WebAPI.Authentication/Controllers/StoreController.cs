using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Queries;

namespace WebAPI.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoreController : BaseController
{
  public StoreController(IMediator mediator) : base(mediator)
  {}

  /// <remarks>
  /// Sample request:
  /// GET -> ../store/GetAllProducts
  /// </remarks>
  [AllowAnonymous]
  [HttpGet("GetAllProducts")]
  public async Task<IActionResult> GetAllProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
  {
    var request = new GetProductsListQuery
    {
      PageNumber = pageNumber,
      PageSize = pageSize
    };
    return Ok(await Mediator.Send(request));
  }
}
