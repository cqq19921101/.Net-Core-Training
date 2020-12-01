using HangFire.BackgroundJobs.Module;
using HangFire.Common.Base;
using HangFire.Common.Extensions;
using HangFire.Domain.Configuration;
using HangFire.Job.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HangFire.Job.Mouble
{
    [DependsOn(
       typeof(AbpAspNetCoreMvcModule),
       typeof(AbpAutofacModule),
       typeof(HangFireBackgroundJobsModule)
     )]

    public class HangFireHostingMouble : AbpModule
    {
        /// <summary>
        /// 重写 ConfigureServices
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute
                                                                    && attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));

                // 移除 AbpExceptionFilter
                options.Filters.Remove(filterMetadata);

                // 添加自己实现的 ExceptionHandlerMiddleware
                options.Filters.Add(typeof(ExceptionHandlerMiddleware));
            });

            // 跨域配置
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // 路由配置
            context.Services.AddRouting(options =>
            {
                // 设置URL为小写
                options.LowercaseUrls = true;
                // 在生成的URL后面添加斜杠
                options.AppendTrailingSlash = true;
            });

            // 身份验证之JWT
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           // 是否验证颁发者
                           ValidateIssuer = true,
                           // 是否验证访问群体
                           ValidateAudience = true,
                           // 是否验证生存期
                           ValidateLifetime = true,
                           // 验证Token的时间偏移量
                           ClockSkew = TimeSpan.FromSeconds(30),
                           // 是否验证安全密钥
                           ValidateIssuerSigningKey = true,
                           // 访问群体
                           ValidAudience = Appsettings.JWT.Domain,
                           // 颁发者
                           ValidIssuer = Appsettings.JWT.Domain,
                           // 安全密钥
                           IssuerSigningKey = new SymmetricSecurityKey(Appsettings.JWT.SecurityKey.GetBytes())
                       };

                       // 应用程序提供的对象，用于处理承载引发的事件，身份验证处理程序
                       options.Events = new JwtBearerEvents
                       {
                           OnChallenge = async context =>
                           {
                               // 跳过默认的处理逻辑，返回下面的模型数据
                               context.HandleResponse();

                               context.Response.ContentType = "application/json;charset=utf-8";
                               context.Response.StatusCode = StatusCodes.Status200OK;

                               var result = new ServiceResult();
                               result.IsFailed("UnAuthorized");

                               await context.Response.WriteAsync(result.ToJson());
                           }
                       };
                   });

            // 认证授权
            context.Services.AddAuthorization();

            // Http请求
            context.Services.AddHttpClient();

        }

        /// <summary>
        /// 重写 OnApplicationInitialization
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            //开发环境
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 使用HSTS的中间件，该中间件添加了严格传输安全头
            app.UseHsts();

            // 转发将标头代理到当前请求，配合 Nginx 使用，获取用户真实IP
            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            // 路由
            app.UseRouting();

            // 跨域
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            // 自己的异常处理中间件
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // 身份验证
            app.UseAuthentication();

            // 认证授权
            app.UseAuthorization();

            // HTTP => HTTPS
            //app.UseHttpsRedirection();

            // 路由映射
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
