using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class HRDB_QTContext : DbContext
    {
        public HRDB_QTContext()
        {
        }

        public HRDB_QTContext(DbContextOptions<HRDB_QTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VSmartparkEmp> VSmartparkEmp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=10.170.1.29;Database=HRDB_QT;uid=smartpark;pwd=smartpark");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VSmartparkEmp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_smartpark_emp");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DeptName01).HasMaxLength(20);

                entity.Property(e => e.DeptName02).HasMaxLength(20);

                entity.Property(e => e.EmpName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FileData).HasColumnType("image");

                entity.Property(e => e.Jdate)
                    .HasColumnName("JDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ldate)
                    .HasColumnName("LDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Line)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Sector).HasMaxLength(20);

                entity.Property(e => e.Utime)
                    .HasColumnName("UTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Workshop).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
