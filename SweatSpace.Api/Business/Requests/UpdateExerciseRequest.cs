namespace SweatSpace.Api.Business.Requests
{
    public class UpdateExerciseRequest
    {
        public int Id { get; init; }
        public int Sets { get; init; }
        public int Reps { get; init; }
        public bool IsCompleted { get; init; }
    }
}
