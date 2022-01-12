using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Core.Entities;

namespace SweatSpace.Infrastructure.Data.Configs
{
    public class WorkoutConfig : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder
             .Property(x => x.Name)
             .IsRequired().HasMaxLength(255);
        }
    }
}