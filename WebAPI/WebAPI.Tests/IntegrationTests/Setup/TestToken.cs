using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure.Options;
using WebAPI.Authentication.Infrastructure.Providers;
using WebAPI.Tests.UnitTests;

namespace WebAPI.Tests.IntegrationTests.Setup;

public class TestToken
{
  public static async Task<string> GenerateJwtToken(IServiceProvider serviceProvider, string userName = null)
  {
    using (var scope = serviceProvider.CreateScope())
    {
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
      var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();
      var jwtProvider = new JwtProvider(jwtOptions, userManager);

      User user;
      if (string.IsNullOrEmpty(userName))
      {
        var registerDto = TestModels.TestRegisterDto;
        user = await userManager.FindByNameAsync(registerDto.UserName);

        if (user == null)
        {
          user = new User
          {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            AcceptTerms = true
          };

          var result = await userManager.CreateAsync(user, registerDto.Password);

          if (!result.Succeeded)
          {
            throw new Exception("Failed to create test user, because user already created");
          }

          await userManager.AddToRoleAsync(user, "Customer");
        }
      }
      else
      {
        user = await userManager.FindByNameAsync(userName);

        if (user == null)
        {
          throw new Exception("User not found");
        }

        var roles = await userManager.GetRolesAsync(user);
        if (!roles.Contains("Customer"))
        {
          await userManager.AddToRoleAsync(user, "Customer");
        }
      }

      var jwt = jwtOptions.Value;
      var existingToken = await userManager.GetAuthenticationTokenAsync(user, jwt.Issuer, jwt.Audience);

      if (existingToken != null)
      {
        return existingToken;
      }

      return await jwtProvider.GenerateToken(user);
    }
  }
}
