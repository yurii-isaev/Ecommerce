using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.UseCases.Tranfers.Input;

public class LoginDto
{
  public string? UserName { get; set; }

   [Required, EmailAddress]
   public string? Email { get; set; }

   [Required, DataType(DataType.Password)]
   public string? Password { get; set; }
}
