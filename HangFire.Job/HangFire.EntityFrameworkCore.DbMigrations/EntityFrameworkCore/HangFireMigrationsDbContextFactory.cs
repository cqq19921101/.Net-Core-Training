using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HangFire.EntityFrameworkCore.DbMigrations.EntityFrameworkCore
{
    public class HangFireMigrationsDbContextFactory : IDesignTimeDbContextFactory<HangFireMigrationsDbContext>
    {
        public HangFireMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var EnableDb = configuration["ConnectionStrings:Enable"];

            var builder = new DbContextOptionsBuilder<HangFireMigrationsDbContext>();

            switch (EnableDb)
            {
                case "MySql":
                    builder.UseMySql(configuration.GetConnectionString(EnableDb));
                    break;

                case "SqlServer":
                    builder.UseSqlServer(configuration.GetConnectionString(EnableDb));
                    break;
            }

            return new HangFireMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}