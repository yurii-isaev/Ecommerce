using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.UseCases.Requests.Queries;

namespace WebAPI.Authentication.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StoreController : BaseController
  {
    /// <remarks>
    /// Пример запроса:
    /// GET http://localhost:5000/api/store/GetAllProducts
    /// </remarks>
    [
      // Authorize(Roles = "Customer"),
      // ProducesResponseType(StatusCodes.Status401Unauthorized)
      HttpGet("GetAllProducts"),
      ProducesResponseType(StatusCodes.Status200OK),
      ProducesResponseType(StatusCodes.Status500InternalServerError),
    ]
    public async Task<JsonResult> GetAllProducts()
    {
      var request = new GetProductsListQuery();
      var products = await Mediator.Send(request);
      return new JsonResult(products);
    }
  }
}

