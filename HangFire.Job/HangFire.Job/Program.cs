using System.Threading.Tasks;
using HangFire.Domain.Configuration;
using HangFire.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HangFire.Job
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                      .UseLog4Net()
                      .ConfigureWebHostDefaults(builder =>
                      {
                          builder.UseIISIntegration()
                                 .ConfigureKestrel(options =>
                                 {
                                     options.AddServerHeader = false;
                                 })
                                 .UseUrls($"http://*:{Appsettings.ListenPort}")
                                 .UseStartup<Startup>();
                      }).UseAutofac().Build().RunAsync();
        }
    }
}
