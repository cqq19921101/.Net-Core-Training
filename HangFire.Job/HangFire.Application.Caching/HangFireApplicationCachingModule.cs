using HangFire.Application.Contracts;
using HangFire.Application.EventBus;
using HangFire.Domain;
using HangFire.Domain.Configurations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace HangFire.Application.Caching
{
    [DependsOn(
        typeof(AbpCachingModule),
        typeof(HangFireDomainModule),
        typeof(HangFireApplicationContractsModule),
        typeof(HangFireApplicationEventBusModule)
    )]
    public class HangFireApplicationCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            if (AppSettings.Caching.IsOpen)
            {
                context.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppSettings.Caching.RedisConnectionString;
                });

                var csredis = new CSRedis.CSRedisClient(AppSettings.Caching.RedisConnectionString);
                RedisHelper.Initialization(csredis);

                context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
            }
        }
    }
}