using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server;

namespace QtechCoreAPI
{
    public class RegisterService : IHostedService
    {
        private readonly ILogger<RegisterService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IServer _server;
        public RegisterService(ILogger<RegisterService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory, IHostApplicationLifetime lifetime,IServer server)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _lifetime = lifetime;
            _server = server;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStarted.Register(() => OnStarted(cancellationToken));
            using (_ = DapperFactory.Crate21OracleConnectionPool())
            {
            }
            using (_ = DapperFactory.Crate57OracleConnectionPool())
            {
            }
            return Task.CompletedTask;
        }
        private void OnStarted(CancellationToken cancellationToken)
        {
            string registerServer = _configuration.GetSection("RegisterServer").Value;
            if (!string.IsNullOrEmpty(registerServer))
            {
                string RegisterServiceName = _configuration.GetSection("RegisterServiceName").Value;
                if (string.IsNullOrEmpty(RegisterServiceName))
                    throw new ArgumentNullException("Args RegisterServiceName is not null.");
                string urls = _configuration.GetSection("urls").Value;
                if (string.IsNullOrEmpty(urls))
                    throw new ArgumentNullException("Args urls is not null.");
                using (var client = _httpClientFactory.CreateClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    ApUrl apUrl = new ApUrl();
                    apUrl.ServiceName = RegisterServiceName;
                    apUrl.Url = urls.Contains("*") ? urls.Replace("*", GetLocalIP()) : urls;
                    using (var stringContext = new StringContent(JsonSerializer.Serialize(apUrl), Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage responseMessage = client.PostAsync(registerServer, stringContext, cancellationToken).Result;
                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            _logger.LogError($"{registerServer} Register Fail");
                        }
                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            _logger.LogInformation($"{registerServer} Register Success");
                        }
                    }
                }
            }           
            _logger.LogInformation("tcp connection onstarted event triggered");
        }
        public string GetLocalIpAddress()
        {
            UnicastIPAddressInformation mostSuitableIp = null;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus != OperationalStatus.Up)
                    continue;
                var properties = network.GetIPProperties();
                if (properties.GatewayAddresses.Count == 0)
                    continue;
                foreach (var address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    if (IPAddress.IsLoopback(address.Address))
                        continue;
                    if (!address.IsDnsEligible)
                    {
                        if (mostSuitableIp == null)
                            mostSuitableIp = address;
                        continue;
                    }
                    // The best IP is the IP got from DHCP server  
                    if (address.PrefixOrigin != PrefixOrigin.Dhcp)
                    {
                        if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
                            mostSuitableIp = address;
                        continue;
                    }
                    return address.Address.ToString();
                }
            }
            return mostSuitableIp != null
                ? mostSuitableIp.Address.ToString()
                : "";
        }
        public string GetLocalIP() 
        {
            string ip = string.Empty;
            var firstUpInterface = NetworkInterface.GetAllNetworkInterfaces()
    .OrderByDescending(c => c.Speed)
    .FirstOrDefault(c => c.NetworkInterfaceType != NetworkInterfaceType.Loopback && c.OperationalStatus == OperationalStatus.Up);
            if (firstUpInterface != null)
            {
                var props = firstUpInterface.GetIPProperties();
                // get first IPV4 address assigned to this interface
                var firstIpV4Address = props.UnicastAddresses
                    .Where(c => c.Address.AddressFamily == AddressFamily.InterNetwork)
                    .Select(c => c.Address)
                    .FirstOrDefault();
                ip = firstIpV4Address.ToString();
            }
            return ip;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
