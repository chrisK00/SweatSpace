using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.EntityTypeConfigs
{
    public class AppUserEntityTypeConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .HasMany(x => x.LikedWorkouts)
                .WithMany(x => x.UsersThatLiked);

            builder
                .HasMany(x => x.Workouts)
                .WithOne().HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            //need to override identity default
            builder
                .HasMany(r => r.Roles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
        }
    }
}
