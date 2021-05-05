namespace SweatSpace.Api.Persistence.Responses
{
    public class ExerciseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public bool IsCompleted { get; set; }
    }
}
