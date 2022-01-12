using SweatSpace.Api.Persistence.Helpers;

namespace SweatSpace.Core.Interfaces.Services
{
    public interface IWeeklyStatsService
    {
        string GetWeeklyWorkoutStats(WeeklyStatsUserModel weeklyStatsUserModel);
        void ResetWeeklyWorkoutStats(WeeklyStatsUserModel weeklyStatsUserModel);
    }
}
