using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Responses;

namespace SweatSpace.Api.Business.Services
{
    public class WeeklyStatsService : IWeeklyStatsService
    {
        public string GetWeeklyMemberResponseStats(MemberResponse memberDto)
        {
            int amountOfCompletedWorkouts = 0;
            foreach (var workout in memberDto.Workouts)
            {
                amountOfCompletedWorkouts += workout.TimesCompletedThisWeek;
            }

            return amountOfCompletedWorkouts > 0 ?
                $"<strong>{amountOfCompletedWorkouts}</strong> workouts completed this week" :
                "<strong>You have no completed workouts this week.</strong> <p>Not liking your workouts? Check out others!</p>";
        }

        public void ResetWeeklyMemberResponseStats(MemberResponse memberDto)
        {
            foreach (var workout in memberDto.Workouts)
            {
                workout.TimesCompletedThisWeek = 0;
            }
        }
    }
}
