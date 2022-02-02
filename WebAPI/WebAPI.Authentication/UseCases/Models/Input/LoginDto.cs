using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.UseCases.Models.Input;

/// <summary>
/// Struct input model of user login.
/// </summary>
/// <remarks>
/// This model includes attributes to enforce validation rules and ensure data integrity.
/// Provides an additional layer of protection against potential attacks or bugs in client code.
/// </remarks>
public class LoginDto
{
  [EmailAddress] 
  public string Email { get; set; } = null!;

  [DataType(DataType.Password)] 
  public string Password { get; set; } = null!;
}
