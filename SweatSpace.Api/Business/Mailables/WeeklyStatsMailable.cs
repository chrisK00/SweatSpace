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
            To(_weeklyStatsModel.Mail)
                .From("Dr.me")
                .Subject(_weeklyStatsModel.Title)
                .Html(_weeklyStatsModel.Content);
        }
    }
}
