using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebAPI.Authentication.Infrastructure.Logging;
using WebAPI.Authentication.Infrastructure.Setup;

namespace WebAPI.Authentication
{
  public class Program
  {
    /// <summary>
    /// Program initialize.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static void Main(string[] args)
    {
      // Выбор настройкм логирования
      LogSet.MinimumLevelSetup();

      try
      {
        Log.Warning("Authentication server run");
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        #pragma warning disable 4014
        RoleManager.Initialize(scope.ServiceProvider);
        #pragma warning restore 4014
        
        Log.Warning("RoleManager initialize");

        host.Run();
      }
      catch (Exception exception)
      {
        Log.Fatal(exception, "Authentication server failed");
        throw;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    /// <summary>
    /// Create web host.
    /// </summary>
    /// <param name="args"></param>
    /// <returns>web host</returns>
    private static IHostBuilder CreateHostBuilder(string[] args) => Host
      .CreateDefaultBuilder(args)
      .UseDefaultServiceProvider(opts => opts.ValidateScopes = false) // needed for mediatr DI.
      .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
      .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json", false, true))
      .UseSerilog();
  }
}
