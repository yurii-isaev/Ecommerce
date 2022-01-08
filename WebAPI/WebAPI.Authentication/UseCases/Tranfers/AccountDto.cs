using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.UseCases.Tranfers;

public class AccountDto
{
   [EmailAddress]
   public string? Email { get; set; }

   [DataType(DataType.Password)]
   public string? Password { get; set; }

   public string? ResetPasswordUri { get; set; }
}
