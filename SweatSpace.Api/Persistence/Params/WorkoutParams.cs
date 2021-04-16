namespace SweatSpace.Api.Persistence.Params
{
    public class WorkoutParams : PaginationParams
    {
        public int UserId { get; set; }
        public bool MyWorkouts { get; set; }
    }
}