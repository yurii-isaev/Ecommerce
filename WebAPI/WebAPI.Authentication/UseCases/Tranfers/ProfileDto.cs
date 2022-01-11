using System;

namespace WebAPI.Authentication.UseCases.Tranfers
{
  public class ProfileDto
  {
    public Guid Id { get; set; }
    
    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime DateCreated { get; set; }
    
    public string Role { get; set; } = null!;

    // public string? Token { get; set; }
    // public string FullName { get; set; }

    public ProfileDto()
    {}

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
