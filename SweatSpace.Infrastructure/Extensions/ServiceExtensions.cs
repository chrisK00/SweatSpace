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
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2,PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
[assembly: InternalsVisibleToAttribute("SweatSpace.Tests")]
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
