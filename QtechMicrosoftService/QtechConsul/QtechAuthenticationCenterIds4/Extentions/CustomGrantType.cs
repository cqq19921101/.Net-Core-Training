using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using QtechAuthenticationCenterIds4.Interface;

namespace QtechAuthenticationCenterIds4.Extentions
{

    /// <summary>
    /// 定制 扩展 校验方式
    /// </summary>
    public class CustomGrantType : IExtensionGrantValidator
    {
        public string GrantType => "qianqianchen";

        private readonly IUserService _iUserService;
        private readonly ILogger _logger;

        public CustomGrantType(IUserService iUserService, ILogger logger)
        {
            this._iUserService = iUserService;
            this._logger = logger;
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            _logger.LogInformation("*********** Authentication Start ****************");
            var Id_Name = context.Request.Raw.Get("Id_Name");
            var Id_Password = context.Request.Raw.Get("Id_Password");

            if (string.IsNullOrEmpty(Id_Name) || string.IsNullOrEmpty(Id_Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }
            var result = this._iUserService.Login(Id_Name, Id_Password);
            if (result == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogError("************** Authentication Failed *****************");
            }
            else
            {
                _logger.LogInformation($"*********** Success ! Name:{Id_Name}  Password : {Id_Password} ***************");
                context.Result = new GrantValidationResult(
                             subject: result.UId.ToString(),
                             authenticationMethod: GrantType,
                             claims: result.Claims);
            }
            return Task.CompletedTask;
        }
    }
}
