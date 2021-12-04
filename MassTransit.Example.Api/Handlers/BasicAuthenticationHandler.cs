using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Api.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header was not found.");
            }

            var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authenticationHeaderValue.Parameter == null)
            {
                return AuthenticateResult.Fail("Authentication header's parameter was null.");
            }

            var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
            var credentials = Encoding.UTF8.GetString(bytes);

            Logger.LogInformation(credentials);

            return AuthenticateResult.Fail("Need to implement");
        }
    }
}
