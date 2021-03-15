using System;
using System.Collections.Generic;

#nullable disable

namespace SmartPark.EntityFrameworkCore.DbMigrator.Models
{
    public partial class SpCursorHistory
    {
        public Guid Tkey { get; set; }
        public string CursorTag { get; set; }
        public string CursorDecrypt { get; set; }
        public string CreateUser { get; set; }
        public string CreateTime { get; set; }
    }
}
