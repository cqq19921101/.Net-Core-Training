using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HangFire.EntityFrameworkCore.DbMigrations.EntityFrameworkCore
{
    public class HangFireMigrationsDbContext : AbpDbContext<HangFireMigrationsDbContext>
    {
        public HangFireMigrationsDbContext(DbContextOptions<HangFireMigrationsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configure();
        }
    }
}