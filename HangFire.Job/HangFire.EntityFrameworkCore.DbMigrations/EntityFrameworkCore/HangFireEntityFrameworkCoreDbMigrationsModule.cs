using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace HangFire.EntityFrameworkCore.DbMigrations.EntityFrameworkCore
{
    [DependsOn(
        typeof(HangFireFrameworkCoreModule)
        )]
    public class HangFireEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HangFireMigrationsDbContext>();
        }
    }
}