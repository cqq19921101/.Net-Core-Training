﻿using System;
using System.Collections.Generic;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class AggregatedCounter
    {
        public string Key { get; set; }
        public long Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}