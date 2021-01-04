using HangFire.Domain.Shared;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace HangFire.Domain
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(HangFireDomainSharedModule)
    )]
    public class HangFireDomainModule : AbpModule
    {

    }
}