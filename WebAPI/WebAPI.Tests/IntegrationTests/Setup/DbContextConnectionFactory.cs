using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPI.Authentication;
using WebAPI.Authentication.DataAccess;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Tests.IntegrationTests;

/// <summary>
/// Factory for executing integration api tests using DbContext.
/// </summary>
/// <typeparam name="TStartup">A type in the entry point assembly of the application.</typeparam>
public class DbContextConnectionFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var descriptor = services.SingleOrDefault(desc => desc.ServiceType == typeof(DbContextOptions<AuthDbContext>));

      if (descriptor != null) services.Remove(descriptor);

      var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

      services
        .AddDbContext<AuthDbContext>(opt => opt.UseInMemoryDatabase("EmployeeTestDB").UseInternalServiceProvider(serviceProvider))
        .AddControllers()
        .AddApplicationPart(typeof(Startup).Assembly);

      var provider = services.BuildServiceProvider();

      using (var scope = provider.CreateScope())
      {
        using (var appContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>())
        {
          var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbContextConnectionFactory<TStartup>>>();

          try
          {
            appContext.Database.EnsureCreated();
          }
          catch (Exception ex)
          {
            logger.LogError(ex.Message, Messages.ErrorMessage);
            throw;
          }
        }
      }
    });
  }
}
