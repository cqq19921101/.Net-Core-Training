using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.Domain.Shared.Module
{
    [DependsOn(typeof(AbpIdentityDomainSharedModule))]
    public class HangFireDoaminSharedModule : AbpModule
    {

    }
}
