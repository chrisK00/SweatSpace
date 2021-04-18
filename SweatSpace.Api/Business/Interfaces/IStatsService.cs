using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IStatsService
    {
        string GetWeeklyMemberStats(MemberDto memberDto);
        void ResetWeeklyMemberStats(MemberDto memberDto);
    }
}
