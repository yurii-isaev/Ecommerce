using System;
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
using WebAPI.Authentication.UseCases.Mapping;
using WebAPI.Authentication.UseCases.Models.Input;
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
      var connection = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AuthDbContext>(op => op.UseSqlServer(connection ?? throw new InvalidOperationException()));
      #endregion
      
      #region Dependency Injection
      //services.AddScoped<IOptions<JwtOptions>, JwtOptions>();
      services.AddHttpContextAccessor();
      services.AddControllers();
      services.AddTransient<IMediator, Mediator>();
      services.AddTransient<IEmailService, EmailService>();
      services.AddTransient<IOrderRepository>(_ => new OrderRepository(connection!));
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
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders(); // Configure default token support
      #endregion

      #region Authentication
      services.AddJwtAuthentication(Configuration);
      #endregion

      #region Configuration Options
      services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"))
              .Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
      #endregion
      
      #region CORS
      services.AddCors(options =>
      {
        options.AddPolicy("AllowSpecificOrigin",
          builder =>
          {
            builder
              // Specify a specific source.
              .WithOrigins("http://localhost:5602") 
              // Allows the use of any HTTP methods in requests to your server.
              .AllowAnyMethod()
              // Allows any HTTP headers in requests to be sent to your server.
              .AllowAnyHeader()
              // Allows the transfer of credentials (e.g. cookies).
              .AllowCredentials(); 
          });
      });
      #endregion

      #region Mapper
      services.AddAutoMapper(config =>
        { 
          config.CreateMap<OrderDto, Order>().ReverseMap();
          config.CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();
          config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
          config.CreateMap<OrderCardPaymentDto, OrderCardPayment>().ReverseMap();
          config.CreateMap<LayerDto, Layer>().ReverseMap();
          config.CreateMap<RegisterDto, User>().ReverseMap();
        }
      );
      #endregion
       
      #region Assembly
      services.AddAutoMapper(typeof(MappingProfile));
      services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Startup).Assembly));
      #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // Required for request routing to work properly.
      app.UseRouting();

      // Middleware for logging information about requests.
      app.UseSerilogRequestLogging();

      // Checking the Cookie files policy.
      app.UseCookiePolicy();

      // Authentication and Authorization processing.
      app.UseAuthentication();
      app.UseAuthorization();

      // CORS processing.
      app.UseCors("AllowSpecificOrigin");

      // Completing Route Configuration.
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
