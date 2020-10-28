using System;
using System.Collections.Generic;
using System.Text;

namespace QtechAgilityFramework.ConsulClientExtend
{
    public interface IConsulDispatcher
    {
        /// <summary>
        /// 负载均衡获取地址
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        string ChooseAddress(string ServiceName);
    }
}
