using System;

namespace WebAPI.Authentication.UseCases.Dto
{
  public class ProfileDto
  {
    public ProfileDto(string fullName, string email, string userName, DateTime dateCreated, string role)
    {
      FullName = fullName;
      Email = email;
      UserName = userName;
      DateCreated = dateCreated;
      Role = role;
    }

    public string FullName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateTime DateCreated { get; set; }
    public string Role { get; set; }
    public string? Token { get; set; }
  }
}
