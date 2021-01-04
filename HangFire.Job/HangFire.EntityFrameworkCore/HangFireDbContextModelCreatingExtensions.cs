using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using static HangFire.Domain.Shared.HangFireDbConsts;

namespace HangFire.EntityFrameworkCore
{
    public static class HangFireDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity("HangFire.Domain.FaceImage.FaceImageApi", b =>
            {
                b.Property<string>("EmpNumber")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("EmpName")
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasMaxLength(50);

                b.Property<string>("JDate")
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasMaxLength(50);

                b.Property<string>("CategoryName")
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasMaxLength(50);

                b.Property<string>("CategoryName")
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasMaxLength(50);

                b.Property<string>("DisplayName")
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasMaxLength(50);

                b.HasKey("EmpNumber");

                b.ToView("v_smartpark_emp");
            });
        }
    }
}