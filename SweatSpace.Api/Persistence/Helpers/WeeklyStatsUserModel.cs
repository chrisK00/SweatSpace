using System.Collections.Generic;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Helpers
{
    public class WeeklyStatsUserModel
    {
        public string Email { get; set; }
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
