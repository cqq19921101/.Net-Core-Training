using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace HangFire.EntityFrameworkCore
{
    public static class FaceImageDbContextModelCreatingExtensions
    {
        public static void FaceImageConfigure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            #region FaceImageApi View V_Smartpark_emp
            builder.Entity<Domain.FaceImage.FaceImageApi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_smartpark_emp");

                entity.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

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
            #endregion
        }

    }
}
