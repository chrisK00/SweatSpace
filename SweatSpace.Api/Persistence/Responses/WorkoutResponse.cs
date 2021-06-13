using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Responses
{
    public class WorkoutResponse
    {
        public int Id { get; init; }

        [Required, MaxLength(255)]
        public string Name { get; init; }
        public int TimesCompletedThisWeek { get; init; }
        public bool IsCompleted { get; init; }

        [Range(0, 5)]
        public int Rating { get; init; }
        public DateTime? Date { get; init; }

        public int AppUserId { get; init; }
        public IEnumerable<ExerciseResponse> Exercises { get; init; } = new List<ExerciseResponse>();
    }
}