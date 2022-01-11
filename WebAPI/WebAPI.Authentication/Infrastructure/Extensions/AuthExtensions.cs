using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication.Infrastructure.Options;

namespace WebAPI.Authentication.Infrastructure.Extensions
{
  public static class AuthExtensions
  {
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

      services.AddAuthentication(options => 
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
          options.RequireHttpsMetadata = true;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Secret!)),
          };

          options.Events = new JwtBearerEvents
          {
            OnMessageReceived = context =>
            {
              context.Token = context.Request.Cookies["user-cookies"];
              return Task.CompletedTask;
            }
          };
        });

      // services.AddAuthorization(options =>
      // {
      //   options.AddPolicy("AdminPolicy", policy =>
      //   {
      //     policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
      //     policy.RequireRole("Admin");
      //   });
      // });
    }
  }
}
