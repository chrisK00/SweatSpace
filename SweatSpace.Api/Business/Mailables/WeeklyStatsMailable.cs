using Coravel.Mailer.Mail;

namespace SweatSpace.Api.Business.Mailables
{
    public class WeeklyStatsMailable : Mailable<WeeklyStatsModel>
    {
        private readonly WeeklyStatsModel _weeklyStatsModel;

        public WeeklyStatsMailable(WeeklyStatsModel weeklyStatsModel)
        {
            _weeklyStatsModel = weeklyStatsModel;
        }

        public override void Build()
        {
            //TODO
            // change from to a real mail 
            To(_weeklyStatsModel.Email)
                .From("chris@gmail.com")
                .Subject(_weeklyStatsModel.Title)
                .Html(_weeklyStatsModel.Content);
        }
    }
}
