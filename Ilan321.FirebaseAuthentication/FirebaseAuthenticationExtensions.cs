using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ilan321.FirebaseAuthentication
{
    public static class FirebaseAuthenticationDefaults
    {
        public const string SchemeName = "FirebaseAuth";
    }

    public static class FirebaseAuthenticationExtensions
    {
        /// <summary>
        /// Registers the <see cref="FirebaseAuthenticationHandler"/> and scheme for use in the Authentication middleware.
        /// <para>To set the <see cref="FirebaseAuthenticationHandler"/> as the default handler, pass <see cref="FirebaseAuthenticationDefaults.SchemeName"/> to the AddAuthentication() call.</para>
        /// </summary>
        /// <param name="initFbApp">Whether the <see cref="FirebaseApp"/> should be initialized as well. Pass false if you initialize the app yourself.</param>
        /// <param name="options">Optionally pass an options configuration action to configure the handler.</param>
        public static AuthenticationBuilder AddFirebaseAuthentication(this AuthenticationBuilder authBuilder, bool initFbApp = true, Action<FirebaseAuthenticationOptions> options = null)
        {
            if (initFbApp)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.GetApplicationDefault()
                });
            }

            var opt = options ?? (_ => { });

            authBuilder.AddScheme<FirebaseAuthenticationOptions, FirebaseAuthenticationHandler>(FirebaseAuthenticationDefaults.SchemeName, opt);

            return authBuilder;
        }
    }
}
