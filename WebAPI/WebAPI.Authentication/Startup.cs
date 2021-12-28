using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication.DataAccess;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Services;

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
      services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString ?? throw new InvalidOperationException()));

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
        .AddEntityFrameworkStores<AppDbContext>();

      #endregion

      #region Authentication JWT

      services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
      services.AddAuthentication(options =>
        {
          options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
          var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"] ??
                                            throw new InvalidOperationException());
          var issuer = Configuration["JwtConfig:Issuer"];
          var audience = Configuration["JwtConfig:Audience"];

          options.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            RequireExpirationTime = true,
          };
          options.SaveToken = true;
          options.RequireHttpsMetadata = true;
        });

      // services.AddDefaultIdentity<IdentityUser>(opts => 
      //         opts.SignIn.RequireConfirmedAccount = true)
      //     .AddEntityFrameworkStores<AppDbContext>();

      #endregion

      #region CORS

      services.AddCors(options => options
        .AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
      );

      #endregion

      #region Dependency Injection

      services.AddHttpContextAccessor().AddSingleton<IHttpService, HttpService>();
      services.AddControllers();
      services.AddAuthorization();

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
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseCookiePolicy();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
