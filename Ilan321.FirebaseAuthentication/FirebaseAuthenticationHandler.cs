using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Ilan321.FirebaseAuthentication
{
    public class FirebaseAuthenticationHandler : AuthenticationHandler<FirebaseAuthenticationOptions>
    {
        private readonly ILogger<FirebaseAuthenticationHandler> Log;

        public FirebaseAuthenticationHandler(
            IOptionsMonitor<FirebaseAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            ILogger<FirebaseAuthenticationHandler> log) : base(options, logger, encoder, clock)
        {
            Log = log;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
            {
                Log.LogInformation("No firebase token header found, returning NoResult..");

                return AuthenticateResult.NoResult();
            }

            var headerValue = Request.Headers[Options.TokenHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(headerValue))
            {
                Log.LogInformation("Firebase token header has no value, returning NoResult..");

                return AuthenticateResult.NoResult();
            }

            Log.LogTrace($"Checking if token `{headerValue}` is valid..");

            var auth = FirebaseAuth.DefaultInstance;

            try
            {
                var result = await auth.VerifyIdTokenAsync(headerValue);

                Log.LogInformation($"Token check complete for uid `{result.Uid}`");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Uid)
                };

                if (Options.AddFirebaseClaims)
                {
                    claims.AddRange(result.Claims.Select(f => new Claim(f.Key, f.Value.ToString())));
                }

                var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, $"Error while verifying ID token..");

                return AuthenticateResult.Fail(ex);
            }
        }
    }
}
