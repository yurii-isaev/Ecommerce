using System;

namespace WebAPI.Authentication.UseCases.Tranfers
{
  public class ProfileDto
  {
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public string Role { get; set; }
    
    // public string? Token { get; set; }
    // public string FullName { get; set; }

    public ProfileDto(Guid id, string userName, DateTime dateCreated, string email, string role)
    {
      Id = id;
      UserName = userName;
      DateCreated = dateCreated;
      Email = email;
      Role = role;
    }
  }
}
