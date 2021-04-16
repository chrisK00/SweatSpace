namespace SweatSpace.Api.Persistence.Params
{
    public class WorkoutParams : PaginationParams
    {
        public int UserId { get; set; }
        public string FilterBy { get; set; }
    }
}