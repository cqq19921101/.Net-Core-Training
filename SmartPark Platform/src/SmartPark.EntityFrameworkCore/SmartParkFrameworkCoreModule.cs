using Meowv.Blog;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace SmartPark.EntityFrameworkCore
{
    [DependsOn(
        typeof(MeowvBlogCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule)
)]
    public class SmartParkFrameworkCoreModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SmartParkDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });


            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
        }
    }
}
