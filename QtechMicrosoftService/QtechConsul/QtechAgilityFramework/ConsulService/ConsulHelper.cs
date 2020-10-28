using Consul;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QtechAgilityFramework.ConsulService
{
    /// <summary>
    /// ConsulHelper   Http模式
    /// </summary>
    public static class ConsulHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task UseConsul(this IApplicationBuilder app,ConsulConfigModel ConsulService,HealthConfigModel HealthService )
        {
            string ip = HealthService.IP;
            int port = HealthService.Port;

            using (ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{ConsulService.ip}:{ConsulService.port}");
                c.Datacenter = "dc1";
            }))
            {
                //服务注册
                await client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = "grpcService" + ip + ":" + port,//唯一的
                    Name = HealthService.GroupName,//组名称-Group
                    Address = ip,
                    Port = port,
                    Tags = HealthService.Tag,//权重
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(12),//每隔多长时间检查
                        HTTP = $"http://{ip}:{HealthService.CheckPort}/Health",//HealthCheck地址
                        Timeout = TimeSpan.FromSeconds(5),//未响应 超时时间
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)//隔多长时间取消注册
                    }
                    
                });
                Console.WriteLine($"http://{ip}:{port}注册成功!");
            };
        }

    }
}
