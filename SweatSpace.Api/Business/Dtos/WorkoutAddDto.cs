using System;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Dtos
{
    public class WorkoutAddDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }
        public int AppUserId { get; set; }
    }
}
