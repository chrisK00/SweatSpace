namespace SweatSpace.Api.Persistence.Entities
{
    public class WorkoutExercise : BaseOwnedEntity
    {
        public Exercise Exercise { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public bool IsCompleted { get; set; }
    }
}