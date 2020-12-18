using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Hangfire.SQLite;
using Hangfire.SqlServer;
using HangFire.Domain.Configuration;
using HangFire.Domain.Shared;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace HangFire.BackgroundJobs.Module
{
    /// <summary>
    /// Reference Abp Hangfire Frame
    /// </summary>
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule))]

    public class HangFireBackgroundJobsModule : AbpModule
    {
        /// <summary>
        /// Configure Ioc Container
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(config =>
            {
                var tablePrefix = HangFireConsts.DbTablePrefix + "hangfire";

                switch (Appsettings.EnableDb)
                {
                    case "MySql":
                        config.UseStorage(
                            new MySqlStorage(Appsettings.ConnectionStrings,
                            new MySqlStorageOptions
                            {
                                TablePrefix = tablePrefix
                            }));
                        break;

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

            //Add Service Schedule Job
            var service = context.ServiceProvider;

            //service.UseTestMailJob();//Test SendMail

            #region FaceImageApi Schedule Job

            service.UseFaceImageApiJob_GetNewEmployee();//同步新入职员工
            //service.UseFaceImageApiJob_GetResignedEmployee();//同步离职员工
            //service.UseFaceImageApiJob_GetUpdatedEmployee();//同步更新过资料的员工


            //service.UseFaceImageApiJob_GetAllEmployee();//同步全厂员工

            #endregion

            #region ExportTask Schedule Job

            #endregion
        }
    }
}
