using Hangfire;
using HangFire.Domain.Configuration;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;
using Hangfire.SQLite;
using Hangfire.SqlServer;
using Hangfire.Dashboard.BasicAuthorization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using HangFire.Domain.Shared;

namespace HangFire.BackgroundJobs.Module
{
    /// <summary>
    /// Reference Abp Hangfire Frame
    /// </summary>
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule),typeof(AbpAspNetCoreMvcModule))]

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
                        //string con = Appsettings.ConnectionStrings;
                        config.UseSQLiteStorage(Appsettings.ConnectionStrings, new SQLiteStorageOptions
                        {
                            SchemaName = tablePrefix
                        });
                        break;
                    case "SqlServer":
                        string con = Appsettings.ConnectionStrings;
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

            //Add HangFire Service
            app.UseHangfireServer();

            //User HangFire Dashboard
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

            //Add Schedule Job Service
            var service = context.ServiceProvider;

            service.UseFaceImageApiJob();//FaceImageApi Schedule Job
        }
    }
}
