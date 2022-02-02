using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.UseCases.Models.Input;

/// <summary>
/// Dynamic input model for user credential data.
/// </summary>
/// <remarks>
/// This model includes attributes to enforce validation rules and ensure data integrity.
/// Provides an additional layer of protection against potential attacks or bugs in client code.
/// </remarks>
public class CredentialDto
{
  [EmailAddress] 
  public string? Email { get; set; }

  [DataType(DataType.Password)] 
  public string? Password { get; set; }
}
