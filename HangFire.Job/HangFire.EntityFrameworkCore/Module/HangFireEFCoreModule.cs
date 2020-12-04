using HangFire.Domain.Configuration;
using HangFire.Domain.Module;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace HangFire.EntityFrameworkCore.Module
{
    [DependsOn(
    typeof(HangFireDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    //typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpEntityFrameworkCoreSqliteModule)
)]

    public class HangFireEFCoreModule : AbpModule
    {
        /// <summary>
        /// 重写 ConfigureServices
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HangFireDBContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                switch (Appsettings.EnableDb)
                {

                    case "SqlServer":
                        options.UseSqlServer();
                        break;

                    case "Sqlite":
                        options.UseSqlite();
                        break;
                }
            });
        }
    }
}
