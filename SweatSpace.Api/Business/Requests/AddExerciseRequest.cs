using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class AddExerciseRequest
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public int AppUserId { get; set; }
    }
}
