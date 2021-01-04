using HangFire.Application;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.HttpApi
{
    [DependsOn(
        typeof(AbpIdentityHttpApiModule),
        typeof(HangFireApplicationModule)
    )]
    public class HangFireHttpApiModule : AbpModule
    {

    }
}