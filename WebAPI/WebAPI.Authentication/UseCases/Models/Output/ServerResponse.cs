namespace WebAPI.Authentication.UseCases.Models.Output;

public class ServerResponse
{
  public int Code { get; set; }
  public bool Success { get; set; }
  public string? Message { get; set; }
  public dynamic? DataSet { get; set; }

  public ServerResponse()
  {}

  public ServerResponse(int сode, string message, object dataSet)
  {
    Code    = сode;
    Message = message;
    DataSet = dataSet;
  }

  public ServerResponse(int сode, bool success, string message, object dataSet)
  {
    Code    = сode;
    Success = success;
    Message = message;
    DataSet = dataSet;
  }
}
