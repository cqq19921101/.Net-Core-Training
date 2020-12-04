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
        }

    }
}
