using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HangFire.EntityFrameworkCore
{
    [ConnectionString]
    public class HangFireDbContext : AbpDbContext<HangFireDbContext>
    {
        public HangFireDbContext(DbContextOptions<HangFireDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configure();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}