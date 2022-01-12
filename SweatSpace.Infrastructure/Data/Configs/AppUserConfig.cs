using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Core.Entities;

namespace SweatSpace.Infrastructure.Data.Configs
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
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