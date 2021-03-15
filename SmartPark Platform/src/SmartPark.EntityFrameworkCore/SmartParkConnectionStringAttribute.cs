using Volo.Abp.Data;

namespace SmartPark.EntityFrameworkCore
{
    public class SmartParkConnectionStringAttribute : ConnectionStringNameAttribute
    {
        private static readonly string db = "SqlServer";

        public SmartParkConnectionStringAttribute(string name = "") : base(db)
        {
            Name = string.IsNullOrEmpty(name) ? db : name;
        }

        public new string Name { get; }
    }
}
