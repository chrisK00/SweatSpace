using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Mailables;
using SweatSpace.Api.Business.Services;

namespace SweatSpace.Api.Business.Invocables
{
    public class SendWeeklyStats : IInvocable
    {
        private readonly IMailer _mailer;
        private readonly ILogger<SendWeeklyStats> _logger;
        private readonly IUserService _userService;
        private readonly IStatsService _statsService;

        public SendWeeklyStats(IMailer mailer, ILogger<SendWeeklyStats> logger, IUserService userService,
            IStatsService statsService)
        {
            _mailer = mailer;
            _logger = logger;
            _userService = userService;
            _statsService = statsService;
        }

        public async Task Invoke()
        {
            foreach (var member in await _userService.GetMembers())
            {
                var statsMailable = new WeeklyStatsMailable(new WeeklyStatsModel
                {
                    Email = member.Email,
                    Title = "Your weekly stats has arrived from SweatSpace",
                    Content = _statsService.GetWeeklyMemberStats(member)
                });

                try
                {
                    await _mailer.SendAsync(statsMailable);
                    _statsService.ResetWeeklyMemberStats(member);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }


        }
    }
}