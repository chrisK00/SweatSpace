using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SweatSpace.Api.Persistence.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppRole> Roles { get; set; } = new List<AppRole>();
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        public ICollection<Workout> LikedWorkouts { get; set; } = new List<Workout>();
    }
}