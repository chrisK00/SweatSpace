using System.Collections.Generic;

namespace SweatSpace.Api.Persistence.Responses
{
    public class MemberResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<WorkoutResponse> Workouts { get; set; } = new List<WorkoutResponse>();
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}