using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddAutoMapper(typeof(UserProfiles).Assembly);
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(config.GetConnectionString("Default")));
            return services;
        }

        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
               .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}