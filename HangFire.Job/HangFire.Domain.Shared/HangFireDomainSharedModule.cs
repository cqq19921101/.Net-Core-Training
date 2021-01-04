using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.Domain.Shared
{
    [DependsOn(typeof(AbpIdentityDomainSharedModule))]
    public class HangFireDomainSharedModule : AbpModule
    {

    }
}