using SweatSpace.Api.Persistence.Responses;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWeeklyStatsService
    {
        string GetWeeklyMemberResponseStats(MemberResponse memberResponse);
        void ResetWeeklyMemberResponseStats(MemberResponse memberResponse);
    }
}
