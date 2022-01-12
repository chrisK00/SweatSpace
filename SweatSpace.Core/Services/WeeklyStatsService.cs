using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Core.Interfaces.Services;

namespace SweatSpace.Core.Services
{
    public class WeeklyStatsService : IWeeklyStatsService
    {
        public string GetWeeklyWorkoutStats(WeeklyStatsUserModel weeklyStatsUserModel)
        {
            int amountOfCompletedWorkouts = 0;
            foreach (var workout in weeklyStatsUserModel.Workouts)
            {
                amountOfCompletedWorkouts += workout.TimesCompletedThisWeek;
            }

            return amountOfCompletedWorkouts > 0 ?
                $"<strong>{amountOfCompletedWorkouts}</strong> workouts completed this week" :
                "<strong>You have no completed workouts this week.</strong> <p>Not liking your workouts? Check out others!</p>";
        }

        public void ResetWeeklyWorkoutStats(WeeklyStatsUserModel weeklyStatsUserModel)
        {
            foreach (var workout in weeklyStatsUserModel.Workouts)
            {
                workout.TimesCompletedThisWeek = 0;
            }
        }
    }
}
