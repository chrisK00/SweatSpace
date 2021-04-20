using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWeeklyStatsService
    {
        string GetWeeklyMemberStats(MemberDto memberDto);
        void ResetWeeklyMemberStats(MemberDto memberDto);
    }
}
