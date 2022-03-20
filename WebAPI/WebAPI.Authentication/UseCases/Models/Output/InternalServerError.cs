using Microsoft.AspNetCore.Http;

namespace WebAPI.Authentication.UseCases.Models.Output;

public class InternalServerError : ServerResponse
{
  public InternalServerError(string message)
  {
    Code = StatusCodes.Status500InternalServerError;
    Message = message;
  }
}
