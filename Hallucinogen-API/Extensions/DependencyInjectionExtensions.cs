using System;
using System.Text;
using Hallucinogen_API.Data;
using Hallucinogen_API.Data.Entities;
using Hallucinogen_API.Mappers;
using Hallucinogen_API.Mappers.Implementations;
using Hallucinogen_API.Repositories;
using Hallucinogen_API.Repositories.Implementations;
using Hallucinogen_API.Services;
using Hallucinogen_API.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;


namespace Hallucinogen_API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<UserEntity>(options => { options.SignIn.RequireConfirmedEmail = true; })
                .AddEntityFrameworkStores<HallucinogenDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }
        
        public static IServiceCollection AddRepositoriesLayer(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddDbContext<HallucinogenDbContext>(options =>
            {
                options.UseNpgsql(configuration["DbConnectionString"]);
            });

            services.AddScoped<IPostRepository, PostRepository>();
            
            return services;
        }
        
        public static IServiceCollection AddMappersLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserMapper, UserMapper>()
                .AddScoped<IPostMapper, PostMapper>()
                .AddScoped<IPostCommentMapper, PostCommentMapper>();
            
            return services;
        }

        public static IServiceCollection AddHelpersLayer(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddServicesLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IPostService, PostService>();
            
            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                    };
                });
            return services;
        }
        
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(configuration["Swagger:AppVersion"], new Info()
                {
                    Title = configuration["Swagger:AppName"],
                    Description = configuration["Swagger:AppDescription"]
                });
            });
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json",
                    $"{configuration["Swagger:AppName"]} {configuration["Swagger:AppVersion"]}");
            });
            return app;
        }
    }
}
