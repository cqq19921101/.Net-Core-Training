using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace QtechCoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddHealthChecks();
            if (!CurrentEnvironment.IsDevelopment())
                services.AddHostedService<RegisterService>();
            services.Configure<FormOptions>(options =>
            {
                //options.ValueCountLimit = 2000; // 2000 items max
                //options.ValueLengthLimit = 1024 * 100; // 100MB max len form data
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseHealthChecks("/Health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.MapWhen(context => context.Request.Path.Value.ToString().Contains("/api/FP/GetWipCount",StringComparison.OrdinalIgnoreCase), MonitorThreadCountConfigure);
        }

        /// <summary>
        /// 配置分支管道 处理url中包含特定方法名的请求
        /// </summary>
        /// <param name="app"></param>
        private static void MonitorThreadCountConfigure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke().ConfigureAwait(false);
            });
            //app.Run(async (context) => { await context.Response.WriteAsync("End"); });
        }
    }   
}
