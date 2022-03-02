using Microsoft.AspNetCore.Http;

namespace WebAPI.Authentication.UseCases.Models.Output;

public class SuccessResponse : ServerResponse
{
  public SuccessResponse(string message, dynamic? dataSet)
  {
    Code = StatusCodes.Status200OK;
    Success = true;
    Message = message;
    DataSet = dataSet;
  }
}
