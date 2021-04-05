using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Persistence.Business
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasMany(x => x.Workouts).WithMany(x => x.Users);
            modelBuilder.Entity<AppUser>().HasMany(x => x.LikedWorkouts).WithMany(x => x.UsersThatLiked);

            base.OnModelCreating(modelBuilder);
        }
    }
}
