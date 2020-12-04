using HangFire.Domain.FaceImage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;


namespace HangFire.EntityFrameworkCore.Module
{
    [HangFireConnectionStringAttribute]
    public class HangFireDBContext : AbpDbContext<HangFireDBContext>
    {
        public HangFireDBContext(DbContextOptions<HangFireDBContext> options) : base(options)
        {
        }

        #region DbSet
        /// <summary>
        /// FaceImage SourceData
        /// </summary>
        public DbSet<v_smartpark_emp> v_smartpark_emp { get; set; }
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
