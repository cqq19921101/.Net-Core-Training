using System;
using System.Collections.Generic;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class Set
    {
        public string Key { get; set; }
        public double Score { get; set; }
        public string Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
