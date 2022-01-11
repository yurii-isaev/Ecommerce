using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Authentication.UseCases.Tranfers.Output;

public class InternalServerErrorResponse : ActionResult
{
  public string Message { get; set; }

  public InternalServerErrorResponse(string message)
  {
    Message = message;
  }

  public override void ExecuteResult(ActionContext context)
  {
    var objectResult = new ObjectResult(new
    {
      Code = StatusCodes.Status500InternalServerError,
      IsValid = false,
      Message
    })
    {
      StatusCode = StatusCodes.Status500InternalServerError
    };

    objectResult.ExecuteResult(context);
  }
}
