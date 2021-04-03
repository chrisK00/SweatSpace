using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SweatSpace.Api.Business.Profiles;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(UserProfiles).Assembly);

            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(config.GetConnectionString("Default")));
            return services;
        }
    }
}
