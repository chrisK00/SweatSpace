using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Mailables;

namespace SweatSpace.Api.Business.Invocables
{
    public class SendWeeklyStats : IInvocable
    {
        private readonly IMailer _mailer;
        private readonly ILogger<SendWeeklyStats> _logger;
        private readonly IWorkoutService _workoutService;

        public SendWeeklyStats(IMailer mailer, ILogger<SendWeeklyStats> logger, IWorkoutService workoutService)
        {
            _mailer = mailer;
            _logger = logger;
            _workoutService = workoutService;
        }

        public async Task Invoke()
        {
            var workout = await _workoutService.GetWorkoutDtoAsync(1);
            var mailable = new WeeklyStatsMailable(new WeeklyStatsModel
            {
                Title = workout.Name,
                Content = workout.Name,
                Mail = workout.Name
            });

            try
            {
                await _mailer.SendAsync(mailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "but why");
            }
        }
    }
}