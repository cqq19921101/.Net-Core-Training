using QtechAuthenticationCenterIds4.Entities;
using QtechAuthenticationCenterIds4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QtechAuthenticationCenterIds4.Utility
{
    /// <summary>
    /// 用户 Service
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns>UserLogin</returns>
        public UserLogin Login(string UserName,string Password)
        {
            //动态去数据库中取 并验证
            return new UserLogin()
            {
                UId = 99999,
                UserName = UserName,
                Password = Password,
                Claims = new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Administrator"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Cqq"),
                        new Claim("eMail","19921101@qq.com")
                    }
            };

        }
    }
}
