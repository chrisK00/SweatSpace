using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Interfaces.Services;
using SweatSpace.Infrastructure.Data;
using SweatSpace.Infrastructure.Repos;
using SweatSpace.Infrastructure.Services;

namespace SweatSpace.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IWorkoutRepo, WorkoutRepo>();
            services.AddScoped<IExerciseRepo, ExerciseRepo>();
            services.AddScoped<IExerciseReadRepo, ExerciseReadRepo>();
            services.AddScoped<IUserReadRepo, UserReadRepo>();
            services.AddScoped<IWorkoutReadRepo, WorkoutReadRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOwnedAuthService, OwnedAuthService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IShuffleService, ShuffleService>();

            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(config.GetConnectionString("Default")));

            services.AddIdentity();
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddSignInManager<SignInManager<AppUser>>()
               .AddRoleValidator<RoleValidator<AppRole>>()
               .AddEntityFrameworkStores<DataContext>();
        }
    }
}
