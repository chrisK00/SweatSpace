using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Core.Entities;

namespace SweatSpace.Infrastructure.Data.Configs
{
    public class WorkoutExerciseConfig : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder
                .HasOne<AppUser>()
                .WithMany().HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}