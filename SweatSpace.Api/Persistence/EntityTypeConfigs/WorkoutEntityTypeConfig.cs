using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.EntityTypeConfigs
{
    public class WorkoutEntityTypeConfig : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder
             .Property(x => x.Name)
             .IsRequired().HasMaxLength(255);
        }
    }
}
