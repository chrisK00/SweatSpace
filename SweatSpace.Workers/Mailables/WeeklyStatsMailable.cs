using Coravel.Mailer.Mail;
using SweatSpace.Core.Responses;

namespace SweatSpace.Workers.Mailables
{
    public class WeeklyStatsMailable : Mailable<WeeklyStatsResponse>
    {
        private readonly WeeklyStatsResponse _weeklyStatsResponse;

        public WeeklyStatsMailable(WeeklyStatsResponse weeklyStatsResponse)
        {
            _weeklyStatsResponse = weeklyStatsResponse;
        }

        public override void Build()
        {
            //TODO
            // change from to a real mail
            To(_weeklyStatsResponse.Email)
                .From("chris@gmail.com")
                .Subject(_weeklyStatsResponse.Title)
                .Html(_weeklyStatsResponse.Content);
        }
    }
}