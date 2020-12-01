using Hangfire;
using HangFire.Domain.Configuration;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;
using Hangfire.SQLite;
using Hangfire.Dashboard;
using HangFire.Common.Consts;
using Hangfire.SqlServer;
using Hangfire.Dashboard.BasicAuthorization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace HangFire.BackgroundJobs.Module
{
    /// <summary>
    /// Reference Abp Hangfire Frame
    /// </summary>
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule))]

    public class HangFireBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(config =>
            {
                var tablePrefix = HangFireConsts.DbTablePrefix + "hangfire";

                switch (Appsettings.EnableDb)
                {
                    //case "MySql":
                    //    config.UseStorage(
                    //        new MySqlStorage(AppSettings.ConnectionStrings,
                    //        new MySqlStorageOptions
                    //        {
                    //            TablePrefix = tablePrefix
                    //        }));
                    //    break;

                    case "Sqlite":
                        config.UseSQLiteStorage(Appsettings.ConnectionStrings, new SQLiteStorageOptions
                        {
                            SchemaName = tablePrefix
                        });
                        break;

                    case "SqlServer":
                        config.UseSqlServerStorage(Appsettings.ConnectionStrings, new SqlServerStorageOptions
                        {
                            SchemaName = tablePrefix
                        });
                        break;
                }
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new []
                        {
                            new BasicAuthAuthorizationUser
                            {
                                Login = Appsettings.Hangfire.Login,
                                PasswordClear =  Appsettings.Hangfire.Password
                            }
                        }
                    })
                },
                DashboardTitle = "任务调度中心"
            });

            //Schedule Job
            var service = context.ServiceProvider;

            //service.UseWallpaperJob();
            //service.UseHotNewsJob();
            //service.UsePuppeteerTestJob();
        }
    }
}
