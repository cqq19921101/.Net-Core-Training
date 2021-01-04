using HangFire.Domain.Configurations;
using Volo.Abp.Data;

namespace HangFire.EntityFrameworkCore
{
    public class ConnectionStringAttribute : ConnectionStringNameAttribute
    {
        private static readonly string db = AppSettings.EnableDb;

        public ConnectionStringAttribute(string name = "") : base(db)
        {
            Name = string.IsNullOrEmpty(name) ? db : name;
        }

        public new string Name { get; }
    }
}