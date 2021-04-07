using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Entities
{
    public class Workout
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public ICollection<AppUser> UsersThatLiked { get; set; } = new List<AppUser>();
    }
}