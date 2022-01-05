using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Authentication.Domain.Entities;

namespace WebAPI.Authentication.DataAccess
{
  public class AuthDbContext : IdentityDbContext<User, IdentityRole, string>
  {
    public AuthDbContext(DbContextOptions options) : base(options)
    {}
  }
}
