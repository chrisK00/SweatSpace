using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class AddExerciseRequest
    {
        [Required, MaxLength(255)]
        public string Name { get; init; }

        public int Sets { get; init; }
        public int Reps { get; init; }
        public int AppUserId { get; set; }
    }
}
