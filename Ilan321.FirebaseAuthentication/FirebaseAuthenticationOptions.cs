using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ilan321.FirebaseAuthentication
{
    public class FirebaseAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Gets or sets the name of the HTTP header that contains the Firebase access token.
        /// <para>Defaults to `FirebaseToken`.</para>
        /// </summary>
        public string TokenHeaderName { get; set; }

        /// <summary>
        /// Gets or sets whether to add Firebase's claims to the <see cref="System.Security.Claims.ClaimsPrincipal"/>.
        /// <para>Defaults to true.</para>
        /// </summary>
        public bool AddFirebaseClaims { get; set; }

        public FirebaseAuthenticationOptions()
        {
            TokenHeaderName = "FirebaseToken";
            AddFirebaseClaims = true;
        }
    }
}
