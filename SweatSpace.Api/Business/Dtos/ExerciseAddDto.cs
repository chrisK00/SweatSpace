using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Api.Business.Dtos
{
    public class ExerciseAddDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
