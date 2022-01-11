using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Authentication.Domain.Entities;

namespace WebAPI.Authentication.Infrastructure.Setup
{
  public static class RoleManager
  {
    public static async Task Initialize(IServiceProvider provider)
    {
      foreach (var roleName in RoleNames.AllRoles)
      {
        await CreateRole(provider, roleName);
      }

      await CreateUserRoles(provider);
    }

    /// <summary>
    /// Create a role if not exists.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="roleName"></param>
    static async Task CreateRole(IServiceProvider provider, string roleName)
    {
      var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

      var roleExists = roleManager.RoleExistsAsync(roleName);
      roleExists.Wait();

      if (!roleExists.Result)
      {
        await roleManager.CreateAsync(new IdentityRole(roleName));
      }
      else
      {
        throw new Exception();
      }
    }

    /// <summary>
    /// Create a user of roles on email confirmation.
    /// </summary>
    /// <param name="provider"></param>
    static async Task CreateUserRoles(IServiceProvider provider)
    {
      // Create users.
      var manager = provider.GetRequiredService<UserManager<User>>();
      var empResult = await manager.CreateAsync((User) DefaultUsers.Customer, "User123!");
      var managerResult = await manager.CreateAsync((User) DefaultUsers.Manager, "User123!");
      var adminResult = await manager.CreateAsync((User) DefaultUsers.Administrator, "User123!");

      // Create user roles based on email confirmation.
      if (empResult.Succeeded || managerResult.Succeeded || adminResult.Succeeded)
      {
        var empUser = await manager.FindByEmailAsync(DefaultUsers.Customer.Email);
        var managerUser = await manager.FindByEmailAsync(DefaultUsers.Manager.Email);
        var adminUser = await manager.FindByEmailAsync(DefaultUsers.Administrator.Email);
        await manager.AddToRoleAsync(empUser, RoleNames.Customer);
        await manager.AddToRoleAsync(managerUser, RoleNames.Manager);
        await manager.AddToRoleAsync(adminUser, RoleNames.Administrator);
      }
      else
      {
        throw new Exception();
      }
    }
  }
}
