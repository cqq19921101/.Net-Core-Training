using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QtechAuthenticationCenterIds4.DataInit;
using QtechAuthenticationCenterIds4.Extentions;
using QtechAuthenticationCenterIds4.Interface;
using QtechAuthenticationCenterIds4.Utility;

namespace QtechAuthenticationCenterIds4
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

            //services.AddControllers();
            services.AddControllersWithViews();
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");

            #region Store EntityFrameWork Core --- DataBase Migration 创建数据库表
            /* Add Migration 数据结构迁移 数据库中自动创建数据表
            Import-Module C:\Users\qianqian.chen\.nuget\packages\microsoft.entityframeworkcore.tools\3.1.6\tools\EntityFrameworkCore.psd1
            Get-Verb
            add-migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb 
            add-migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            */

            //services.AddIdentityServer()
            //    .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = b =>
            //            b.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly("QtechAuthenticationCenterIds4"));
            //    })
            //.AddOperationalStore(opt =>
            //{
            //    opt.ConfigureDbContext = b =>
            //        b.UseSqlServer(connectionString,
            //            sql => sql.MigrationsAssembly("QtechAuthenticationCenterIds4"));
            //});

            #endregion

            #region 初始化种子数据 自定义认证策略
            services.InitSeedData(connectionString);
            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString);
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString);
                    };
                })
                //.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                //.AddProfileService<CustomProfileService>();
                .AddExtensionGrantValidator<CustomGrantType>();
            #endregion

            #region 注册服务类
            services.AddTransient<IUserService, UserService>();//User Service
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            //访问wwwroot文件夹下的静态文件 防止未加载对应的js,css
            app.UseStaticFiles(
                new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
                }
                );

            app.UseIdentityServer();//IdentityServer4中间件配置 

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //配置路由
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}");
            });
        }

    }
}
