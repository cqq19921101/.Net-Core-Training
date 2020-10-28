using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using QtechAuthenticationCenterIds4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QtechAuthenticationCenterIds4.Extentions
{
    /// <summary>
    /// 自定义用户校验
    /// </summary>
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _iUsersServices;

        public CustomResourceOwnerPasswordValidator(IUserService userService)
        {
            _iUsersServices = userService;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = this._iUsersServices.Login(context.UserName, context.UserName);
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }
            else
            {
                context.Result = new GrantValidationResult(
                        user.UId.ToString(),
                        OidcConstants.AuthenticationMethods.Password,
                        DateTime.UtcNow,
                        user.Claims);
            }
            return Task.CompletedTask;
        }
    }
}
