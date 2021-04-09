using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Services;
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
            services.AddScoped<ITokenService, TokenService>();

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
               .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}