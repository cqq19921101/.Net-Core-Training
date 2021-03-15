using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartPark.EntityFrameworkCore
{
    [SmartParkConnectionString]
    public class SmartParkDbContext : AbpDbContext<SmartParkDbContext>
    {
        public SmartParkDbContext(DbContextOptions<SmartParkDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SmartParkConfigure();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}