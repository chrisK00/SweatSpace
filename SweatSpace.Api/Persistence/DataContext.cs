using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.EntityTypeConfigs;

namespace SweatSpace.Persistence.Business
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

            builder.Entity<Exercise>()
                .Property(x => x.Name)
                .IsRequired().HasMaxLength(255);

            new AppUserEntityTypeConfig().Configure(builder.Entity<AppUser>());
            new WorkoutEntityTypeConfig().Configure(builder.Entity<Workout>());
            new WorkoutExerciseEntityTypeConfig().Configure(builder.Entity<WorkoutExercise>());

            // override identity default
            builder.Entity<AppRole>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role).HasForeignKey(r => r.RoleId)
                .IsRequired();
        }
    }
}