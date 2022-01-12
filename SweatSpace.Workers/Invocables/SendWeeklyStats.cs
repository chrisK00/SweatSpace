using Coravel.Invocable;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.Extensions.Logging;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Interfaces.Services;
using SweatSpace.Core.Responses;
using SweatSpace.Workers.Mailables;
using System.Threading.Tasks;

namespace SweatSpace.Workers.Invocables
{
    public class SendWeeklyStats : IInvocable
    {
        private readonly IMailer _mailer;
        private readonly ILogger<SendWeeklyStats> _logger;
        private readonly IUserService _userService;
        private readonly IWeeklyStatsService _statsService;
        private readonly IUnitOfWork _unitOfWork;

        public SendWeeklyStats(IMailer mailer, ILogger<SendWeeklyStats> logger, IUserService userService,
            IWeeklyStatsService statsService, IUnitOfWork unitOfWork)
        {
            _mailer = mailer;
            _logger = logger;
            _userService = userService;
            _statsService = statsService;
            _unitOfWork = unitOfWork;
        }

        public async Task Invoke()
        {
            foreach (var user in await _userService.GetWeeklyStatsUserModels())
            {
                var statsMailable = new WeeklyStatsMailable(new WeeklyStatsResponse
                {
                    Email = user.Email,
                    Title = "Your weekly stats has arrived from SweatSpace",
                    Content = _statsService.GetWeeklyWorkoutStats(user)
                });

                await _mailer.SendAsync(statsMailable);
                _statsService.ResetWeeklyWorkoutStats(user);
                await _unitOfWork.SaveAllAsync();
            }

            _logger.LogInformation($"Weekly mails sent");
        }
    }
}