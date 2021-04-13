using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Entities
{
    public class Exercise
    {
        private string _name;

        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get => _name; set => _name = value.ToLower(); }
    }
}