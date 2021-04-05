using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}