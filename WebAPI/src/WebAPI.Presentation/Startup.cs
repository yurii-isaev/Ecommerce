using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using WebAPI.Domain.Core.Entities;
using WebAPI.Domain.Core.Interfaces;
using WebAPI.Infrastructure.Data.Persistence.Context;
using WebAPI.Infrastructure.Data.Repositories.Implementations;

namespace WebAPI.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region JSON Serializer

            services.AddControllersWithViews()
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver());

            #endregion

            #region Enable application context
            
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region Dependency injection
            
            services.AddScoped<ICrudRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();
            
            #endregion

            #region Role Identity
            
            services.AddIdentity<User,IdentityRole>(opts =>
                {
                    opts.Password.RequireDigit = true;
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireUppercase = true;
                    opts.Lockout.MaxFailedAccessAttempts = 6;
                    opts.User.RequireUniqueEmail = true;
                    opts.SignIn.RequireConfirmedAccount = true;
                    opts.SignIn.RequireConfirmedEmail = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            
            #endregion

            #region Swagger
            
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebAPI.Authentication",
                    Description = "An ASP.NET Core Web API for managing API documentation"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });
            
            #endregion
            
            #region Enable logging

            services.AddLogging(loggingBuilder =>
            {
                // Enable logging.
                loggingBuilder.AddConsole()
                    // Display sql commands.
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                // Display output IDE.
                loggingBuilder.AddDebug();
            });

            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // This middleware is used to returns static files and short-circuits further request processing.   
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
                RequestPath = "/Photos"
            });

            app.UseRouting();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            // Enable CORS.
            app.UseCors(opts => opts
                .WithOrigins("http://localhost:4200", "http://localhost:9876")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}