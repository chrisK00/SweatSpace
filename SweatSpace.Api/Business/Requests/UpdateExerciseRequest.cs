namespace SweatSpace.Api.Business.Requests
{
    public class UpdateExerciseRequest
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public bool IsCompleted { get; set; }
    }
}
