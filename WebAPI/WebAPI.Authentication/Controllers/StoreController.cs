using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Queries;

namespace WebAPI.Authentication.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StoreController : BaseController
  {
    [
      Authorize(Roles = "Customer"),
      HttpGet("all-products"),
      ProducesResponseType(StatusCodes.Status200OK),
      ProducesResponseType(StatusCodes.Status401Unauthorized)
    ]
    public async Task<ActionResult<IEnumerable>> GetAllProducts()
    {
      var request = new GetProductsListQuery();
      return Ok(await Mediator.Send(request));
    }
  }
}

