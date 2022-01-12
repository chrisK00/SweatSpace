using System;
using System.Collections.Generic;

namespace SweatSpace.Core.Responses
{
    public class WorkoutResponse
    {
        public int Id { get; init; }

        public string Name { get; init; }
        public int TimesCompletedThisWeek { get; init; }
        public bool IsCompleted { get; init; }

        public int Rating { get; init; }
        public DateTime? Date { get; init; }

        public int AppUserId { get; init; }
        public IEnumerable<ExerciseResponse> Exercises { get; init; } = new List<ExerciseResponse>();
    }
}