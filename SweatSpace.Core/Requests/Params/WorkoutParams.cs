namespace SweatSpace.Core.Requests.Params
{
    public class WorkoutParams : PaginationParams
    {
        public int UserId { get; set; }
        public string FilterBy { get; init; }
    }
}