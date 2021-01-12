using HangFire.Domain.Configurations;
using System;

namespace HangFire.Common.AccessToken
{
    public class AuthorizeRequest
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string Client_ID = AccessTokenConfig.Client_ID;

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public string Redirect_Url = AccessTokenConfig.Redirect_Url;

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Scope
        /// </summary>
        public string Scope { get; set; } = "user,public_repo";
    }
}