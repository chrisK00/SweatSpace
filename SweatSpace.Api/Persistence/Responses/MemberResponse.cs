using System.Collections.Generic;

namespace SweatSpace.Api.Persistence.Responses
{
    public class MemberResponse
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public IEnumerable<WorkoutResponse> Workouts { get; init; } = new List<WorkoutResponse>();
        public IEnumerable<string> Roles { get; init; } = new List<string>();
    }
}