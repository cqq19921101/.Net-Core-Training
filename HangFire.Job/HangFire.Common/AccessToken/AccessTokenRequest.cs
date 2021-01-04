using HangFire.Domain.Configurations;

namespace HangFire.Common.AccessToken
{
    public class AccessTokenRequest
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string Client_ID = AccessTokenConfig.Client_ID;

        /// <summary>
        /// Client Secret
        /// </summary>
        public string Client_Secret = AccessTokenConfig.Client_Secret;

        /// <summary>
        /// 调用API_Authorize获取到的Code值
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public string Redirect_Uri = AccessTokenConfig.Redirect_Url;

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
    }
}