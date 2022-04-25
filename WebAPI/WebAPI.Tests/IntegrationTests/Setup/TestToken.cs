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
  public static async Task<string> GenerateJwtToken(IServiceProvider serviceProvider)
  {
    using (var scope = serviceProvider.CreateScope())
    {
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
      var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();
      var jwtProvider = new JwtProvider(jwtOptions, userManager);
      var registerDto = TestModels.TestRegisterDto;

      var testUser = new User
      {
        UserName = registerDto.UserName,
        Email = registerDto.Email,
        AcceptTerms = true
      };

      var result = await userManager.CreateAsync(testUser, TestModels.TestRegisterDto.Password);

      if (!result.Succeeded)
      {
        throw new Exception("Failed to create test user");
      }

      await userManager.AddToRoleAsync(testUser, "Customer");

      return await jwtProvider.GenerateToken(testUser);
    }
  }
}
