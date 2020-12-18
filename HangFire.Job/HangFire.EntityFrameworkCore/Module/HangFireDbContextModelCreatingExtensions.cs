using HangFire.Domain.FaceImage;
using HangFire.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace HangFire.EntityFrameworkCore.Module
{
    public static class HangFireDbContextModelCreatingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            //builder.Entity<FaceImageApi>(b =>
            //{
            //    b.ToTable(HangFireConsts.DbTablePrefix + DbTableName.FaceImageApi);
            //    b.HasKey(x => x.Id);
            //    b.Property(x => x.Id).ValueGeneratedOnAdd();
            //    b.Property(x => x.EmpNumber).HasMaxLength(200).IsRequired();
            //    b.Property(x => x.EmpName).HasMaxLength(100).IsRequired();
            //    b.Property(x => x.JDate).HasColumnType("datetime").IsRequired();
            //    b.Property(x => x.LDate).HasColumnType("datetime").IsRequired();
            //    b.Property(x => x.FileData).HasColumnType("byte").IsRequired();
            //});
        }

    }
}
