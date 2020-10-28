using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QtechAuthDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region IdentityServer4 - Client 客户端模式
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";//IdentityServer4的地址 拿公钥
            //        options.ApiName = "UserApi";//匹配对应的Api
            //        options.RequireHttpsMetadata = false;
            //    });

            ////自定义授权策略
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "client_eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("client_eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion

            #region IdentityServer4 - Password 密码模式
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:7200";
                    options.ApiName = "TestApi";
                    options.RequireHttpsMetadata = false;
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("eMailPolicy",
                    policyBuilder => policyBuilder
                    .RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "eMail")
                    && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            });
            #endregion

            #region  IdentityServer4 - 隐藏模式
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion

            #region  IdentityServer4 - 混合模式
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});

            #endregion

            #region  IdentityServer4 - 授权码模式
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//中间件 启用鉴权 IdentityServer4

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
