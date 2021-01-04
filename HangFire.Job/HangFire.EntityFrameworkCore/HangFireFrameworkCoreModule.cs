using HangFire.Domain;
using HangFire.Domain.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace HangFire.EntityFrameworkCore
{
    [DependsOn(
        typeof(HangFireDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule)
    )]
    public class HangFireFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HangFireDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            context.Services.AddAbpDbContext<FaceImageDBContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });


            Configure<AbpDbContextOptions>(options =>
            {
                switch (AppSettings.EnableDb)
                {
                    case "MySql":
                        options.UseMySQL();
                        break;

                    case "SqlServer":
                        options.UseSqlServer();
                        break;

                }

                switch (AppSettings.FaceImageEnableDb)
                {
                    case "HRSqlServer":
                        options.UseSqlServer();
                        break;
                }
            });
        }
    }
}