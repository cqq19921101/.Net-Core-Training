using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Hangfire.SqlServer;
using HangFire.Application.Contracts;
using HangFire.Domain.Configurations;
using HangFire.Domain.Shared;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace HangFire.BackgroundJobs
{
    [DependsOn(
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(HangFireApplicationContractsModule)
    )]
    public class HangFireBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(config =>
            {
                var tablePrefix = HangFireConsts.DbTablePrefix + "hangfire";

                switch (AppSettings.EnableDb)
                {
                    case "MySql":
                        config.UseStorage(
                            new MySqlStorage(AppSettings.ConnectionStrings,
                            new MySqlStorageOptions
                            {
                                TablePrefix = tablePrefix
                            }));
                        break;
                    case "SqlServer":
                        config.UseSqlServerStorage(AppSettings.ConnectionStrings, new SqlServerStorageOptions
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
                                Login = AppSettings.Hangfire.Login,
                                PasswordClear =  AppSettings.Hangfire.Password
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

            //service.UseFaceImageApiJob_GetNewEmployee();//同步新入职员工
            //service.UseFaceImageApiJob_GetResignedEmployee();//同步离职员工
            //service.UseFaceImageApiJob_GetUpdatedEmployee();//同步更新过资料的员工


            //service.UseFaceImageApiJob_GetAllEmployee();//同步全厂员工

            #endregion

            #region ExportTask Schedule Job

            #endregion
        }
    }
}