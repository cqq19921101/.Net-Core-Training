using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Domain.Configurations
{
    public class AccessTokenConfig
    {

        /// <summary>
        /// POST请求，根据code得到access_token
        /// </summary>
        public static string API_AccessToken = "http://localhost:3333/access_token";//鉴权授权服务器

        /// <summary>
        /// Client ID
        /// </summary>
        public static string API_User = AppSettings.AccessToken.API_User;

        /// <summary>
        /// Client ID
        /// </summary>
        public static int UserId = AppSettings.AccessToken.UserId;


        /// <summary>
        /// Client ID
        /// </summary>
        public static string Client_ID = AppSettings.AccessToken.Client_ID;

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string Client_Secret = AppSettings.AccessToken.Client_Secret;

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public static string Redirect_Url = AppSettings.AccessToken.Redirect_Url;
    }
}
