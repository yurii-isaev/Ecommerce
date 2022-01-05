using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Authentication.Controllers
{
  public abstract class BaseController : ControllerBase
  {
    protected IMediator Mediator => HttpContext
      .RequestServices
      .GetRequiredService<IMediator>();
  }
}
