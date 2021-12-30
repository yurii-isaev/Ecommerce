using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Authentication.Infrastructure.Setup
{
  public static class DefaultUsers
  {
    public static readonly IdentityUser Сustomer = new IdentityUser
    {
      UserName = "Сustomer",
      Email = "Сustomere@test.ru",
      EmailConfirmed = true
    };

    public static readonly IdentityUser Manager = new IdentityUser
    {
      UserName = "Manager",
      Email = "Manager@test.ru",
      EmailConfirmed = true
    };

    public static readonly IdentityUser Administrator = new IdentityUser
    {
      UserName = "Administrator",
      Email = "Admin@test.ru",
      EmailConfirmed = true
    };

    public static IEnumerable<IdentityUser> AllUsers
    {
      get
      {
        yield return Сustomer;
        yield return Manager;
        yield return Administrator;
      }
    }
  }
}
