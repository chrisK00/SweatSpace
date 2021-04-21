using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;

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

            builder.Entity<WorkoutExercise>().HasOne<AppUser>()
                .WithMany().HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>().HasMany(x => x.LikedWorkouts)
                .WithMany(x => x.UsersThatLiked);

            builder.Entity<AppUser>().HasMany(x => x.Workouts).WithOne()
                .HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);

            //need to override identity default
            builder.Entity<AppUser>().HasMany(r => r.Roles).WithOne(u => u.User)
                .HasForeignKey(u => u.UserId).IsRequired();
            builder.Entity<AppRole>().HasMany(u => u.Users).WithOne(r => r.Role)
               .HasForeignKey(r => r.RoleId).IsRequired();
        }
    }
}