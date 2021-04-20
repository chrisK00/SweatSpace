using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SweatSpace.Api.Persistence.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> Roles { get; set; } = new List<AppUserRole>();
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        public ICollection<Workout> LikedWorkouts { get; set; } = new List<Workout>();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}