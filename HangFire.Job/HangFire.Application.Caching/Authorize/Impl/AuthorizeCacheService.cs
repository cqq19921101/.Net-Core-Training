using HangFire.Application.Caching;
using HangFire.Common.Base;
using HangFire.Common.Extensions;
using System;
using System.Threading.Tasks;
using static HangFire.Domain.Shared.HangFireConsts;

namespace HangFires.Application.Caching.Authorize.Impl
{
    public class AuthorizeCacheService : CachingServiceBase, IAuthorizeCacheService
    {
        private const string Authorize_Prefix = CachePrefix.Authorize;

        private const string KEY_GetAccessToken = Authorize_Prefix + ":GetAccessToken-{0}";
        private const string KEY_GenerateToken = Authorize_Prefix + ":GenerateToken-{0}";

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code, Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetAccessToken.FormatWith(code), factory, CacheStrategy.FIVE_MINUTES);
        }

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GenerateTokenAsync(string access_token, Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GenerateToken.FormatWith(access_token), factory, CacheStrategy.HALF_HOURS);
        }
    }
}