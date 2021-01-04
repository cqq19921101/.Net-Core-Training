using HangFire.Common.Base;
using System;
using System.Threading.Tasks;

namespace HangFires.Application.Caching.Authorize
{
    public interface IAuthorizeCacheService
    {
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetAccessTokenAsync(string code, Func<Task<ServiceResult<string>>> factory);

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(string access_token, Func<Task<ServiceResult<string>>> factory);
    }
}