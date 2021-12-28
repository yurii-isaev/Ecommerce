namespace WebAPI.Authentication.UseCases.Dto;

public class ResponseModel
{
   public ResponseCode? Code { get; set; }
   public bool IsValid { get; set; }
   public string? Message { get; set; }
   public dynamic? DataSet { get; set; }

   public ResponseModel()
   {}

   public ResponseModel(ResponseCode сode, string responseMessage, object dataSet)
   {
      Code = сode;
      Message = responseMessage;
      DataSet = dataSet;
   }

   public ResponseModel(ResponseCode сode, bool isValid, string responseMessage, object dataSet)
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
