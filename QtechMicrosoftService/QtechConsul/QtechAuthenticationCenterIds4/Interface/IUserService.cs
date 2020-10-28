using QtechAuthenticationCenterIds4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QtechAuthenticationCenterIds4.Interface
{
    /// <summary>
    /// UserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserLogin Login(string userName, string password);
    }
}
