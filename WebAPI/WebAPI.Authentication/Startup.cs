using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
      var connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AuthDbContext>(op =>
        op.UseSqlServer(connectionString ?? throw new InvalidOperationException()));
      #endregion
      
      #region Dependency Injection
      // services.AddHttpContextAccessor().AddSingleton<IHttpService, HttpService>();
      services.AddControllers();
      services.AddTransient<IProductRepository>(_ => new ProductRepository(connectionString!));
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
        options.AddPolicy("ApiCorsPolicy", builder =>
        {
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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
      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
      app.UseRouting();
      app.UseCors("ApiCorsPolicy");
      app.UseCookiePolicy(new CookiePolicyOptions
      {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always
      });
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseCookiePolicy();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
