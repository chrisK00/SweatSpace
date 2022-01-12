using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Core.Entities;

namespace SweatSpace.Infrastructure.Data.Configs
{
    public class ExerciseConfig : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .HasMany<WorkoutExercise>()
                .WithOne(x => x.Exercise)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}