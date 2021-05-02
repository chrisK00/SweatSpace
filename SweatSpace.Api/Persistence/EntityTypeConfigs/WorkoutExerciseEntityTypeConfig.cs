using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.EntityTypeConfigs
{
    public class WorkoutExerciseEntityTypeConfig : IEntityTypeConfiguration<WorkoutExercise>
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
