﻿using HangFire.Common.Base;
using System.Threading.Tasks;

namespace HangFire.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetAccessTokenAsync(string code);

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(string access_token);

        /// <summary>
        /// 验证Token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ServiceResult> VerifyToken(string token);
    }
}