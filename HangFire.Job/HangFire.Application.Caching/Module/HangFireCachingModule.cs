using HangFire.Domain.Configuration;
using HangFire.Domain.Module;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace HangFire.Application.Caching.Module
{
    [DependsOn(typeof(AbpCachingModule),typeof(HangFireDomainModule))]
    public class HangFireCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Redis Cache IsOpen
            if (Appsettings.Caching.IsOpen)
            {
                context.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Appsettings.Caching.RedisConnectionString;
                });

                var csredis = new CSRedis.CSRedisClient(Appsettings.Caching.RedisConnectionString);
                RedisHelper.Initialization(csredis);

                context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
            }
        }

    }
}
