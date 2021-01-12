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
        }
    }
}