using System.ComponentModel.DataAnnotations;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Mapping;

namespace WebAPI.Authentication.UseCases.Models.Input;

/// <summary>
/// Struct input model of user registration.
/// </summary>
/// <remarks>
/// This model includes attributes to enforce validation rules and ensure data integrity.
/// Provides an additional layer of protection against potential attacks or bugs in client code.
/// </remarks>
public class RegisterDto : IMapFrom<User>
{
  public string UserName { get; set; } = null!;

  [EmailAddress] 
  public string Email { get; set; } = null!;

  [DataType(DataType.Password)] 
  public string Password { get; set; } = null!;

  public bool AcceptTerms { get; set; }
}
