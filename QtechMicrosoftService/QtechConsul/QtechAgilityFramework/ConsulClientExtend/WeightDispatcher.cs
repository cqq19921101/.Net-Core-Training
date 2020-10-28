using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtechAgilityFramework.ConsulClientExtend
{
    /// <summary>
    /// 权重性  轮询
    /// </summary>
    public class WeightDispatcher : IConsulDispatcher
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
        public string ChooseAddress(string serviceName)
        {
            ConsulClient client = new ConsulClient(c =>//TODO:根据集群找可用Consul Agent地址
            {
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "dc1";
            });
            var response = client.Agent.Services().Result.Response;

            //打印
            foreach (var item in response)
            {
                Console.WriteLine("***************************************");
                Console.WriteLine(item.Key);
                var service = item.Value;
                Console.WriteLine($"{service.Address}--{service.Port}--{service.Service}");
                Console.WriteLine("***************************************");
            }

            AgentService agentService = null;

            var serviceDictionary = response.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).ToArray();
            //加权轮询
            var serviceDictionaryNew = new List<AgentService>();
            foreach (var service in serviceDictionary)
            {
                serviceDictionaryNew.AddRange(Enumerable.Repeat(service.Value, int.TryParse(service.Value.Tags?[0], out int iWeight) ? 1 : iWeight));
            }
            int index = new Random(DateTime.Now.Millisecond).Next(0, int.MaxValue) % serviceDictionaryNew.Count;
            agentService = serviceDictionaryNew[index];

            return $"{agentService.Address}:{agentService.Port}";
        }
    }
}
