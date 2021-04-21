using System.Text;
using Coravel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Invocables;
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
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });
            return services;
        }
    }
}