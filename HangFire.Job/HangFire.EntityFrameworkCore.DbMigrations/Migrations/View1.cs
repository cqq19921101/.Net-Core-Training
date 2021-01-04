using System;
using System.Collections.Generic;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class View1
    {
        public long Id { get; set; }
        public long? StateId { get; set; }
        public string StateName { get; set; }
        public string InvocationData { get; set; }
        public string Arguments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
