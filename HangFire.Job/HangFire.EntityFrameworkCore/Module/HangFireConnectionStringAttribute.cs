using HangFire.Domain.Configuration;
using Volo.Abp.Data;

namespace HangFire.EntityFrameworkCore.Module
{
    /// <summary>
    /// HangFireConnectionStringAttribute
    /// </summary>
    public class HangFireConnectionStringAttribute: ConnectionStringNameAttribute
    {
        private static readonly string db = Appsettings.EnableDb;

        public HangFireConnectionStringAttribute(string name = "") : base(db)
        {
            Name = string.IsNullOrEmpty(name) ? db : name;
        }

        public new string Name { get; }

    }
}
