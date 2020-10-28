using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QtechConsul.Utility
{
    public static class ConsulHelper
    {
        public static void ConsulRegist(this IConfiguration configuration)
        {
            string ip = configuration["ip"];
            int port = int.Parse(configuration["port"]);
            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "dc1";
            });

            //注册
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "Service" + ip + ":" + port.ToString(),
                Name = "QtechTestApiService",//组
                Address = ip,
                Port = port,
                Tags = new string[] { weight.ToString()},//权重   
                //心跳检测
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),//每隔多少时间进行一次心跳检测
                    HTTP = $"http://{ip}:{port}/Api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5),//设置5秒超时
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)//20秒之后 取消注册
                }
            });
        }
    }
}
