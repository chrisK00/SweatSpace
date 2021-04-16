using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Persistence.Business
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasMany(x => x.LikedWorkouts).WithMany(x => x.UsersThatLiked);

            builder.Entity<AppUser>().HasMany(x => x.Workouts).WithOne()
                .HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}