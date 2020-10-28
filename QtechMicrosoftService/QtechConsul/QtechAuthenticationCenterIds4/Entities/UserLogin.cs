using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QtechAuthenticationCenterIds4.Entities
{
    /// <summary>
    /// UserLogin Entity
    /// </summary>
    public class UserLogin
    {
        public int UId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }//用户信息

    }
}
