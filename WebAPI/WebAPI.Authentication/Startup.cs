using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebAPI.Authentication.DataAccess;
using WebAPI.Authentication.DataAccess.Repositories;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure.Extensions;
using WebAPI.Authentication.Infrastructure.Options;
using WebAPI.Authentication.UseCases.Contracts;

namespace WebAPI.Authentication
{
  public class Startup
  {
    private IConfiguration Configuration { get; }
    
    public Startup(IConfiguration config) => Configuration = config;

    public void ConfigureServices(IServiceCollection services)
    {
      #region DataBase Connection
      var connection = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AuthDbContext>(op => op.UseSqlServer(connection ?? throw new InvalidOperationException()));
      #endregion
      
      #region Dependency Injection
      //services.AddScoped<IOptions<JwtOptions>, JwtOptions>();
      services.AddHttpContextAccessor();
      services.AddControllers();
      services.AddSingleton<IMediator, Mediator>();
      services.AddTransient<IProductRepository>(_ => new ProductRepository(connection!));
      #endregion

      #region Role Identity
      services.AddIdentity<User, IdentityRole>(options =>
        {
          options.Password.RequireDigit = true;
          options.Password.RequiredLength = 5;
          options.Password.RequireUppercase = true;
          options.Lockout.MaxFailedAccessAttempts = 6;
          options.User.RequireUniqueEmail = true;
          options.SignIn.RequireConfirmedAccount = true;
          options.SignIn.RequireConfirmedEmail = false;
        })
        .AddRoles<IdentityRole>()
        .AddRoleManager<RoleManager<IdentityRole>>()
        .AddSignInManager<SignInManager<User>>()
        .AddEntityFrameworkStores<AuthDbContext>();
      #endregion

      #region Authentication JWT
      services.AddApiAuthentication(Configuration);
      #endregion

      #region Configuration Options
      services
        .Configure<JwtOptions>(Configuration.GetSection("JwtOptions"))
        .Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
      #endregion
      
      #region CORS
      services.AddCors(options =>
      {
        options.AddPolicy("AllowSpecificOrigin",
          builder =>
          {
            builder.WithOrigins("http://localhost:8080") // Указываем конкретный источник
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Разрешаем куки
          });
      });
      #endregion

      #region Assembly
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Startup).Assembly));
      #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // обязателен для правильной работы маршрутизации запросов в вашем ASP.NET Core приложении
      app.UseRouting();

      // Middleware для логирования информации о запросах
      app.UseSerilogRequestLogging();

      // Проверка политики файлов cookie
      app.UseCookiePolicy();

      // Обработка аутентификации и авторизации
      app.UseAuthentication();
      app.UseAuthorization();

      // Обработка CORS
      app.UseCors("AllowSpecificOrigin");

      // Завершение конфигурации маршрутов
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
