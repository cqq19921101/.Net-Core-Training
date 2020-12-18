using HangFire.Application.Caching.Module;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.Application.Mdoule
{
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(HangFireCachingModule)
    )]
    public class HangFireApplicationModule : AbpModule
    {
        /// <summary>
        /// Configure Ioc Container
        /// </summary>
        /// <param name="context"></param>
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
