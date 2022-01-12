using Coravel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SweatSpace.Api.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SweatSpace.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors();
            services.AddMemoryCache();
            services.AddScheduler();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(opt =>
                 {
                     opt.TokenValidationParameters = new TokenValidationParameters
                     {
                         //validate the created token is correct
                         ValidateIssuerSigningKey = true,
                         //our key to validate against the incoming
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                         ValidateAudience = false,
                         ValidateIssuer = false
                     };
                 });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyConstants.AdminPolicy, policy => policy.RequireRole("Admin"));
            });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SweatSpace.Api", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SweatSpace.Api.xml"));

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT bearer authorization header",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string>() }
                });
            });

            return services;
        }
    }
}