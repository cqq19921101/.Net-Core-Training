using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HangFire.EntityFrameworkCore
{
    [FaceImageConnectionString]
    public class FaceImageDBContext : AbpDbContext<FaceImageDBContext>
    {
        public FaceImageDBContext(DbContextOptions<FaceImageDBContext> options) : base(options)
        {
        }

        #region Dbset
        public DbSet<Domain.FaceImage.FaceImageApi> FaceImageApi { get; set; }
        #endregion

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
