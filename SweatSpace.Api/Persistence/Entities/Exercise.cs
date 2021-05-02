namespace SweatSpace.Api.Persistence.Entities
{
    public class Exercise
    {
        private string _name;
        public int Id { get; set; }
        public string Name { get => _name; set => _name = value.ToLower(); }
    }
}