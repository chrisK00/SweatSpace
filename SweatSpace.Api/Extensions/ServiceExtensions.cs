using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Coravel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Invocables;
using SweatSpace.Api.Business.Services;
using SweatSpace.Api.Helpers;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Profiles;
using SweatSpace.Api.Persistence.Repos;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWorkoutRepo, WorkoutRepo>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IExerciseRepo, ExerciseRepo>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IShuffleService, ShuffleService>();
            services.AddScoped<IWeeklyStatsService, WeeklyStatsService>();
            services.AddScoped<IOwnedAuthService, OwnedAuthService>();

            services.AddScheduler();
            services.AddTransient<SendWeeklyStats>();
            services.AddMailer(config);
            services.AddAutoMapper(typeof(UserProfiles).Assembly);
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(config.GetConnectionString("Default")));
            return services;
        }

        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration config)
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

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddSignInManager<SignInManager<AppUser>>()
               .AddRoleValidator<RoleValidator<AppRole>>()
               .AddEntityFrameworkStores<DataContext>();
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