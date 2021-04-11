using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Api.Business.Dtos
{
    public class WorkoutAddDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
