using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Persistence.Entities
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}