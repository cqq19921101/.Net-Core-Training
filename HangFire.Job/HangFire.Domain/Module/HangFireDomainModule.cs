using HangFire.Domain.Shared.Module;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.Domain.Module
{
    [DependsOn(typeof(AbpIdentityDomainModule),typeof(HangFireDoaminSharedModule))]
    public class HangFireDomainModule  : AbpModule
    {

    }
}
