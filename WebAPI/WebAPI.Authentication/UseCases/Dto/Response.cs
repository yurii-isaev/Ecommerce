namespace WebAPI.Authentication.UseCases.Dto;

public class Response
{
  public ResponseCode? Code { get; set; }
  public bool IsValid { get; set; }
  public string? Message { get; set; }
  public dynamic? DataSet { get; set; }

  public Response()
  {}

  public Response(ResponseCode сode, string responseMessage, object dataSet)
  {
    Code = сode;
    Message = responseMessage;
    DataSet = dataSet;
  }

  public Response(ResponseCode сode, bool isValid, string responseMessage, object dataSet)
  {
    Code = сode;
    IsValid = isValid;
    Message = responseMessage;
    DataSet = dataSet;
  }
}


public enum ResponseCode
{
  Ok = 200,
  Error = 500
}

