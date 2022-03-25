using AutoFixture;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SweatSpace.Api;
using SweatSpace.Core.Entities;
using SweatSpace.Infrastructure.Data;
using System.Linq;

namespace SweatSpace.Tests.Integration.Setup
{
    public class WebApiFactory : WebApplicationFactory<Startup>
    {
        public static int UserId { get; private set; }
        public static int WorkoutId { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DataContext>));
                if (descriptor != null) services.Remove(descriptor);

                var connection = new SqliteConnection("Filename=:memory:");
                connection.Open();

                using var context = new DataContext(new DbContextOptionsBuilder<DataContext>().UseSqlite(connection).Options);
                Seed(context);

                services.AddDbContext<DataContext>(opt => opt.UseSqlite(connection));

                services.AddAuthentication(FakeAuthHandler.AuthType)
                    .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>(FakeAuthHandler.AuthType, _ => { });
            });
        }

        private static void Seed(DataContext context)
        {
            context.Database.EnsureCreated();
            var admin = new AppUser { UserName = "admin", Email = "admin@gmail.com" };
            var user = new AppUser { UserName = "user", Email = "user@gmail.com" };
            context.AddRange(admin, user);
            context.SaveChanges();

            UserId = admin.Id;

            var workouts = new Fixture().Build<Workout>()
                .Without(w => w.Id).Without(w => w.UsersThatLiked).Without(w => w.Exercises)
                .CreateMany().ToArray();

            for (int i = 0; i < workouts.Length; i++)
            {
                workouts[i].AppUserId = i % 2 == 0 ? admin.Id : user.Id;
            }

            context.AddRange(workouts);
            context.SaveChanges();
            WorkoutId = workouts.First(x => x.AppUserId == admin.Id).Id;
        }
    }
}
