using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Tests.Setup;

/// <summary>
/// Factory for executing integration api tests using DbConnection.
/// </summary>
/// <typeparam name="TStartup">A type in the entry point assembly of the application.</typeparam>
public class DbConnectionFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
  readonly IConfigurationRoot _configuration;

  public DbConnectionFactory(IConfigurationRoot configuration)
  {
    _configuration = configuration;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      // Deleting the current database connection, if there is one
      var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnection));

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }

      services.AddScoped<IDbConnection>(_ =>
      {
        var connection = _configuration.GetConnectionString("DefaultConnection");
        return new SqlConnection(connection);
      });

      // Add any additional mocks or services needed for testing
      // ...
    });
  }
}