namespace SweatSpace.Core.Requests.Params
{
    public class ExerciseParams : PaginationParams
    {
        private string _name;

        public string Name { get => _name; init => _name = value.ToLower(); }
    }
}