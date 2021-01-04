using System;
using System.Collections.Generic;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class Counter
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
