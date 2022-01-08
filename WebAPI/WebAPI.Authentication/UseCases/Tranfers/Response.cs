namespace WebAPI.Authentication.UseCases.Tranfers;

public class Response
{
  public ResponseCode? Code { get; set; }
  public bool IsValid { get; set; }
  public string? Message { get; set; }
  public dynamic? DataSet { get; set; }

  public Response()
  {}

  public Response(ResponseCode сode, string message, object dataSet)
  {
    Code    = сode;
    Message = message;
    DataSet = dataSet;
  }

  public Response(ResponseCode сode, bool isValid, string message, object dataSet)
  {
    Code    = сode;
    IsValid = isValid;
    Message = message;
    DataSet = dataSet;
  }
}

public enum ResponseCode
{
  Ok = 200,
  Error = 500
}
