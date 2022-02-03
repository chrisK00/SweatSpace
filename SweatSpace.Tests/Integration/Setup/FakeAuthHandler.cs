using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SweatSpace.Tests.Integration.Setup
{
    public class FakeAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string AuthType = "IntegrationTest";

        public FakeAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
          ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
          : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, WebApiFactory.UserId.ToString()), };

            var identity = new ClaimsIdentity(claims, AuthType);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthType);
            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}

