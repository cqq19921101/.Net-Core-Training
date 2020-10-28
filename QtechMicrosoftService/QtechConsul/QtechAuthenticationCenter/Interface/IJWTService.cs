using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QtechAuthenticationCenter.Interface
{
    public interface IJWTService
    {
        string GetToken(string UserName);
    }
}
