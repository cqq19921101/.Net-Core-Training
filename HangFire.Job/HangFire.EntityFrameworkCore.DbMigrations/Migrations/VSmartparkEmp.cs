using System;
using System.Collections.Generic;

namespace HangFire.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class VSmartparkEmp
    {
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
        public DateTime? Jdate { get; set; }
        public DateTime? Ldate { get; set; }
        public string DeptName01 { get; set; }
        public string DeptName02 { get; set; }
        public string DeptName { get; set; }
        public string Sector { get; set; }
        public string Workshop { get; set; }
        public string Line { get; set; }
        public byte[] FileData { get; set; }
        public DateTime? Utime { get; set; }
    }
}
