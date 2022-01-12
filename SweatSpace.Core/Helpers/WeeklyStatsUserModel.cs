using SweatSpace.Core.Entities;
using System.Collections.Generic;

namespace SweatSpace.Api.Persistence.Helpers
{
    public class WeeklyStatsUserModel
    {
        public string Email { get; init; }
        public ICollection<Workout> Workouts { get; init; } = new List<Workout>();
    }
}
