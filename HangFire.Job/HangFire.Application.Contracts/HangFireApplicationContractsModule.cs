using HangFire.Domain;
using Volo.Abp.Modularity;

namespace HangFire.Application.Contracts
{
    [DependsOn(
        typeof(HangFireDomainModule)
    )]
    public class HangFireApplicationContractsModule : AbpModule
    {

    }
}