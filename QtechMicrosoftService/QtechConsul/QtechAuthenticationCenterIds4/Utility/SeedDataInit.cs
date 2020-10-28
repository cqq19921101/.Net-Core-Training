using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QtechAuthenticationCenterIds4.DataInit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QtechAuthenticationCenterIds4.Utility
{
    /// <summary>
    /// 初始化 种子数据
    /// </summary>
    public static class SeedDataInit
    {
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="service"></param>
        /// <param name="conncetionString"></param>
        public static void InitSeedData(this IServiceCollection service, string conncetionString)
        {
            var migtationAssembly = typeof(SeedDataInit).GetTypeInfo().Assembly.GetName().Name;

            service.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(conncetionString,
                    sql => sql.MigrationsAssembly(migtationAssembly));
            });
            service.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(conncetionString,
                    sql => sql.MigrationsAssembly(migtationAssembly));
            });

            var serviceProvider = service.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
                var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                InitCustomSeedData(context);
            }
        }


        private static void InitCustomSeedData(IConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in PasswordInitConfig.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.ApiResources.Any())
            {
                foreach (var api in PasswordInitConfig.GetApiResources())
                    context.ApiResources.Add(api.ToEntity());
                context.SaveChanges();
            }
        }
    }
}
