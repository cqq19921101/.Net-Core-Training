using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SmartPark.EntityFrameworkCore.DbMigrator.Models
{
    public partial class AccesscontrolrecordsContext : DbContext
    {
        public AccesscontrolrecordsContext()
        {
        }

        public AccesscontrolrecordsContext(DbContextOptions<AccesscontrolrecordsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SpCursorHistory> SpCursorHistories { get; set; }
        public virtual DbSet<SpFaceCapture> SpFaceCaptures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.170.3.103;Database=Access-control-records;uid=access;pwd=access");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<SpCursorHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SP_CursorHistory");

                entity.Property(e => e.CreateTime).HasMaxLength(50);

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.CursorDecrypt).HasMaxLength(50);

                entity.Property(e => e.CursorTag).HasMaxLength(50);

                entity.Property(e => e.Tkey).HasColumnName("TKEY");
            });

            modelBuilder.Entity<SpFaceCapture>(entity =>
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
