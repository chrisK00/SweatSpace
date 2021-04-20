using System.Collections.Generic;

namespace SweatSpace.Api.Persistence.Dtos
{
    public class MemberDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<WorkoutDto> Workouts { get; set; } = new List<WorkoutDto>();
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}