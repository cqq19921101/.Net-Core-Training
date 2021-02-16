using HangFire.Domain.Configurations;
using HangFire.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using static HangFire.Domain.Shared.HangFireConsts;


using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using IGeekFan.AspNetCore.Knife4jUI;

namespace HangFire.Swagger
{
    public static class HangFireSwaggerExtensions
    {
        /// <summary>
        /// 当前API版本，从appsettings.json获取
        /// </summary>
        private static readonly string version = $"Version : {AppSettings.Version}";

        /// <summary>
        /// Swagger描述信息
        /// </summary>
        private static readonly string description = @"<b>Hangfire</b>：<a target=""_blank"" href=""/hangfire"">任务调度中心</a><code>Powered by .NET Core 3.1</code>";
     
        /// <summary>
        /// Swagger分组信息，将进行遍历使用
        /// </summary>
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v1,
                Name = "人脸库接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "人脸库接口_",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v2,
                Name = "测试接口2",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "测试接口2",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v3,
                Name = "测试接口3",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "测试接口3",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v4,
                Name = "JWT授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "JWT授权接口",
                    Description = description
                }
            }
        };

        /// <summary>
        /// AddSwagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                // 遍历并应用Swagger分组信息
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo);
                });

                options.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/HangFire.Swagger.xml"),true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/HangFire.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/HangFire.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/HangFire.Application.Contracts.xml"));


                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();


                // 应用Controller的API文档描述信息
                options.DocumentFilter<SwaggerDocumentFilter>();
            });
        }

        /// <summary>
        /// UseSwaggerUI
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseKnife4UI(options =>
            {
                //options.RoutePrefix = ""; // serve the UI at root
                //options.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
                // 遍历分组信息，生成Json
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });

                // 模型的默认扩展深度，设置为 -1 完全隐藏模型
                options.DefaultModelsExpandDepth(-1);
                // API文档仅展开标记
                options.DocExpansion(IGeekFan.AspNetCore.Knife4jUI.DocExpansion.List);
                // API前缀设置为空
                options.RoutePrefix = string.Empty;
                // API页面Title
                options.DocumentTitle = "接口文档";

            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapSwagger("{documentName}/api-docs");
            //});

            //app.UseSwaggerUI(options =>
            //{
            //    // 遍历分组信息，生成Json
            //    ApiInfos.ForEach(x =>
            //    {
            //        options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
            //    });

            //    // 模型的默认扩展深度，设置为 -1 完全隐藏模型
            //    options.DefaultModelsExpandDepth(-1);
            //    // API文档仅展开标记
            //    options.DocExpansion(DocExpansion.List);
            //    // API前缀设置为空
            //    options.RoutePrefix = string.Empty;
            //    // API页面Title
            //    options.DocumentTitle = "接口文档";
            //});
        }

        internal class SwaggerApiInfo
        {
            /// <summary>
            /// URL前缀
            /// </summary>
            public string UrlPrefix { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/>
            /// </summary>
            public OpenApiInfo OpenApiInfo { get; set; }
        }
    }
}