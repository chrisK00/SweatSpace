using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Entities
{
    public class Workout : BaseOwnedEntity
    {
        public string Name { get; set; }

        public bool IsCompleted { get; set; }
        public int TimesCompletedThisWeek { get; set; }

        public int Rating { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
        public ICollection<AppUser> UsersThatLiked { get; set; } = new List<AppUser>();
    }
}