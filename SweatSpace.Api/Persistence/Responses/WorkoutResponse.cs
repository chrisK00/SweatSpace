using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Responses
{
    public class WorkoutResponse
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
        public int TimesCompletedThisWeek { get; set; }
        public bool IsCompleted { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
        public DateTime? Date { get; set; }

        public int AppUserId { get; set; }
        public ICollection<ExerciseResponse> Exercises { get; set; } = new List<ExerciseResponse>();
    }
}