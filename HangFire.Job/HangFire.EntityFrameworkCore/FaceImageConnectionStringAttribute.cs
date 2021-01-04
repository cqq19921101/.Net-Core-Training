using HangFire.Domain.Configurations;
using Volo.Abp.Data;

namespace HangFire.EntityFrameworkCore
{
    public class FaceImageConnectionStringAttribute : ConnectionStringNameAttribute
    {
        private static readonly string db = AppSettings.FaceImageEnableDb;

        public FaceImageConnectionStringAttribute(string name = "") : base(db)
        {
            Name = string.IsNullOrEmpty(name) ? db : name;
        }

        public new string Name { get; }

    }
}
