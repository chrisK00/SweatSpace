using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Business.Services
{
    public class StatsService : IStatsService
    {
        public string GetWeeklyMemberStats(MemberDto memberDto)
        {
            int amountOfCompletedWorkouts = 0;
            foreach (var workout in memberDto.Workouts)
            {
                amountOfCompletedWorkouts += workout.TimesCompletedThisWeek;
            }

            return amountOfCompletedWorkouts > 0 ?
                $"You have completed {amountOfCompletedWorkouts} workouts this week!" :
                "You have no completed workouts this week. Not liking your workouts? Check out others!";
        }

        public void ResetWeeklyMemberStats(MemberDto memberDto)
        {
            foreach (var workout in memberDto.Workouts)
            {
                workout.TimesCompletedThisWeek = 0;
            }
        }
    }
}
