using Meowv.Blog.Domain.AccessControl.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace SmartPark.EntityFrameworkCore
{
    public static class SmartParkDbContextModelCreatingExtensions
    {
        public static void SmartParkConfigure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            #region Build FaceCapture
            builder.Entity<FaceCapture>(entity =>
            {
                entity.HasKey(e => e.Tkey)
                    .HasName("PK_SP_FACECAPTURE")
                    .IsClustered(false);

                entity.ToTable("SP_FaceCapture");

                entity.Property(e => e.Tkey)
                    .ValueGeneratedNever()
                    .HasColumnName("TKEY");

                entity.Property(e => e.Age)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("age");

                entity.Property(e => e.CameraPosition)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("camera_position");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Confidence).HasColumnName("confidence");

                entity.Property(e => e.CreateDate).HasMaxLength(100);

                entity.Property(e => e.EventType)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("event_type");

                entity.Property(e => e.ExtraId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("extra_id");

                entity.Property(e => e.Fmp)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("fmp");

                entity.Property(e => e.FmpError)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("fmp_error");

                entity.Property(e => e.Gender)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("gender");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PassType)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("pass_type");

                entity.Property(e => e.Photo)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Quality).HasColumnName("quality");

                entity.Property(e => e.ScreenId)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("screen_id");

                entity.Property(e => e.SubjectId)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("subject_id");

                entity.Property(e => e.SubjectPhoto)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("subject_photo");

                entity.Property(e => e.SubjectType)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("subject_type");

                entity.Property(e => e.Temperature)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("temperature");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("uuid");

                entity.Property(e => e.VerificationMode)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("verification_mode");
            });

            #endregion

        }

    }
}
