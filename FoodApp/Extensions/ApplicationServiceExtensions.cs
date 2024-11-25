using FluentValidation;
using FluentValidation.AspNetCore;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Helper;
using FoodApp.Repository.Interface;
using FoodApp.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjectManagementSystem.Data.Context;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FoodApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            
            services.AddMediatRServices();

            services.AddSwaggerServices()
                    .AddFluentValidationConfig();

            services.AddHttpContextAccessor();

            services.AddAuthConfig(configuration);

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
            });

            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UserState>();
            services.AddScoped<RequestParameters>();
            services.AddScoped<ControllerParameters>();
            services.AddTransient<EmailSenderHelper>();

            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }

        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food-Management-System-API", Version = "v1" });

                c.SchemaFilter<EnumSchemaFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            });

            return services;
        }

        private static IServiceCollection AddAuthConfig(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddOptions<JwtOptions>()
                    .BindConfiguration(JwtOptions.SectionName)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = jwtSettings?.Issuer,
                      ValidAudience = jwtSettings?.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                      ClockSkew = TimeSpan.Zero
                  };
              });

            return services;
        }

        private static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

            return services;
        }

    }
}
