using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Api.Persistence.Dtos
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public bool IsCompleted { get; set; }
    }
}
