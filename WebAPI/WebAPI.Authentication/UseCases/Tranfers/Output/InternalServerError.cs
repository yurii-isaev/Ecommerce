using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Authentication.UseCases.Tranfers.Output;

public class InternalServerError : ServerResponse
{
  public new string Message { get; set; }

  public InternalServerError(string message)
  {
    Message = message;
  }

  public void ExecuteResult(ActionContext context)
  {
    var objectResult = new ObjectResult(new
    {
      Code = StatusCodes.Status500InternalServerError,
      Success = false,
      Message
    })
    {
      StatusCode = StatusCodes.Status500InternalServerError
    };

    objectResult.ExecuteResult(context);
  }
}
