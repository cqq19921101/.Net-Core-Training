using HangFire.Application.Caching;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.Application
{
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(HangFireApplicationCachingModule)
    )]
    public class HangFireApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HangFireApplicationModule>(validate: true);
                options.AddProfile<HangFireAutoMapperProfile>(validate: true);
            });
        }
    }
}