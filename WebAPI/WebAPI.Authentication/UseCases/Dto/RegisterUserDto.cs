using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Mapping;

namespace WebAPI.Authentication.UseCases.Dto;

public class RegisterUserDto : IMapFrom<User>
{
  public string? FullName { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
}
