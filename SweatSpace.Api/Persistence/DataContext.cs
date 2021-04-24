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

            builder.Entity<WorkoutExercise>().HasOne<AppUser>()
                .WithMany().HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);

            new AppUserEntityTypeConfig().Configure(builder.Entity<AppUser>());
            // override identity default
            builder.Entity<AppRole>().HasMany(u => u.Users).WithOne(r => r.Role)
               .HasForeignKey(r => r.RoleId).IsRequired();
        }
    }
}