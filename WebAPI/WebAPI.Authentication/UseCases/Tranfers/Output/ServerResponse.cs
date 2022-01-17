namespace WebAPI.Authentication.UseCases.Tranfers.Output;

public class ServerResponse
{
  public ResponseCode? Code { get; set; }
  public bool Success { get; set; }
  public string? Message { get; set; }
  public dynamic? DataSet { get; set; }

  public ServerResponse()
  {}

  public ServerResponse(ResponseCode сode, string message, object dataSet)
  {
    Code    = сode;
    Message = message;
    DataSet = dataSet;
  }

  public ServerResponse(ResponseCode сode, bool success, string message, object dataSet)
  {
    Code    = сode;
    Success = success;
    Message = message;
    DataSet = dataSet;
  }
}
