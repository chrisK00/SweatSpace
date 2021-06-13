using System.Collections.Generic;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Helpers
{
    public class WeeklyStatsUserModel
    {
        public string Email { get; init; }
        public ICollection<Workout> Workouts { get; init; } = new List<Workout>();
    }
}
