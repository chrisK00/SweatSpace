using System.Threading.Tasks;
using Coravel.Invocable;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Mailables;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Business.Invocables
{
    public class SendWeeklyStats : IInvocable
    {
        private readonly IMailer _mailer;
        private readonly ILogger<SendWeeklyStats> _logger;
        private readonly IUserService _userService;
        private readonly IStatsService _statsService;
        private readonly IUnitOfWork _unitOfWork;

        public SendWeeklyStats(IMailer mailer, ILogger<SendWeeklyStats> logger, IUserService userService,
            IStatsService statsService, IUnitOfWork unitOfWork)
        {
            _mailer = mailer;
            _logger = logger;
            _userService = userService;
            _statsService = statsService;
            _unitOfWork = unitOfWork;
        }

        public async Task Invoke()
        {
            foreach (var member in await _userService.GetMembersAsync())
            {
                var statsMailable = new WeeklyStatsMailable(new WeeklyStatsModel
                {
                    Email = member.Email,
                    Title = "Your weekly stats has arrived from SweatSpace",
                    Content = _statsService.GetWeeklyMemberStats(member)
                });

                await _mailer.SendAsync(statsMailable);               
                _statsService.ResetWeeklyMemberStats(member);
                await _unitOfWork.SaveAllAsync();
            }
            _logger.LogInformation($"Weekly mails sent");
        }
    }
}