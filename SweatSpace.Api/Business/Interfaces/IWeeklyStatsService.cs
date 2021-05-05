using SweatSpace.Api.Persistence.Helpers;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWeeklyStatsService
    {
        string GetWeeklyWorkoutStats(WeeklyStatsUserModel weeklyStatsUserModel);
        void ResetWeeklyWorkoutStats(WeeklyStatsUserModel weeklyStatsUserModel);
    }
}
