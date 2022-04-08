namespace WebAPI.Authentication.UseCases.Models.Output;

public class ServerResponse
{
  public int Code { get; set; }
  public string? Message { get; set; }
  public bool Success { get; set; }
  public dynamic? Set { get; set; }
}
