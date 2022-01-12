using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Core.Entities;
using SweatSpace.Infrastructure.Data.Configs;

namespace SweatSpace.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new AppUserConfig().Configure(builder.Entity<AppUser>());
            new WorkoutConfig().Configure(builder.Entity<Workout>());
            new WorkoutExerciseConfig().Configure(builder.Entity<WorkoutExercise>());
            new ExerciseConfig().Configure(builder.Entity<Exercise>());

            // override identity default
            builder.Entity<AppRole>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role).HasForeignKey(r => r.RoleId)
                .IsRequired();
        }
    }
}