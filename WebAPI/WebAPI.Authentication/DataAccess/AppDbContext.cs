using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Authentication.Domain.Entities;

namespace WebAPI.Authentication.DataAccess
{
   public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
   {
      public AppDbContext(DbContextOptions options) : base(options)
      {}
   }
}
