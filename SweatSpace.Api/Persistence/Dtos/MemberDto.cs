using System.Collections.Generic;

namespace SweatSpace.Api.Persistence.Dtos
{
    public class MemberDto
    {
        public string UserName { get; set; }
        public ICollection<WorkoutDto> Workouts { get; set; } = new List<WorkoutDto>();
    }
}
