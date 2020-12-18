using HangFire.EntityFrameworkCore.Module;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace HangFire.EntityFrameworkCore.DBContext
{
    [HangFireConnectionString]
    public class HangFireDBContext : AbpDbContext<HangFireDBContext>
    {
        public HangFireDBContext(DbContextOptions<HangFireDBContext> options) : base(options)
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
