namespace SweatSpace.Api.Persistence.Responses
{
    public class ExerciseResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Sets { get; init; }
        public int Reps { get; init; }
        public bool IsCompleted { get; init; }
    }
}
