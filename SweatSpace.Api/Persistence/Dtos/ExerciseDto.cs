using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Dtos
{
    public class ExerciseDto
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
