using HangFire.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;

namespace HangFire.EntityFrameworkCore.DBContext
{
    public class FaceImageConnectionStringAttribute : ConnectionStringNameAttribute
    {
        private static readonly string db = Appsettings.FaceImageEnableDb;

        public FaceImageConnectionStringAttribute(string name = "") : base(db)
        {
            Name = string.IsNullOrEmpty(name) ? db : name;
        }

        public new string Name { get; }

    }
}
