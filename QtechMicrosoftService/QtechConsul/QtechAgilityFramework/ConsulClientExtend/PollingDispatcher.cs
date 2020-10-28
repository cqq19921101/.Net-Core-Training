using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtechAgilityFramework.ConsulClientExtend
{
    public class PollingDispatcher : IConsulDispatcher
    {


        private static int _iTotalCount = 0;

        private static int iTotalCount
        {
            get
            {
                return _iTotalCount;
            }
            set
            {
                _iTotalCount = value >= Int32.MaxValue ? 0 : value;
            }
            
        }
        /// <summary>
        /// 负载均衡获取地址
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        public string ChooseAddress(string ServiceName)
        {
            AgentService agentService = null;
            try
            {
                ConsulClient client = new ConsulClient(c => //通过Consul集群 查找可用的Consul Agent地址
                {
                    c.Address = new Uri("http://localhost:8500");//测试Consul地址
                    c.Datacenter = "dc1";
                });

                var ResponseService = client.Agent.Services().Result.Response;//找出已经注册的全部服务

                //循环 打印在命令行测试看看
                foreach (var item in ResponseService)
                {
                    var key = item.Key;
                    var service = item.Value;
                    Console.WriteLine($"{service.Address}--{service.Port}--{service.Service}");
                }

                //轮询 从已经注册服务中挑一个
                var serviceDictionary = ResponseService.Where(s => s.Value.Service.Equals(ServiceName, StringComparison.OrdinalIgnoreCase)).ToArray();
                {
                    int index = iTotalCount++ % serviceDictionary.Length;//取余 找索引
                    agentService = serviceDictionary[index].Value;
                }
                return $"{agentService.Address}:{agentService.Port}";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
