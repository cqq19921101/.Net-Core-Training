using System;
using System.Collections.Generic;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class Server
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public DateTime LastHeartbeat { get; set; }
    }
}
