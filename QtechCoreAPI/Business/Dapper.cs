using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QtechCoreAPI
{
    public static class DapperFactory
    {
        //FP库
        public const string DbConnString21= "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = qtrac1-vip.qtech.com)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = qtrac2-vip.qtech.com)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = qtrac3-vip.qtech.com)(PORT = 1521))(LOAD_BALANCE = yes)(CONNECT_TIMEOUT=3)(TRANSPORT_CONNECT_TIMEOUT=3)(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = sqtdb)(FAILOVER_MODE =(TYPE = SELECT)(METHOD = BASIC)(RETRIES = 180)(DELAY = 5))));User Id=ckmes;Password=qt123mes;Min Pool Size=4;Max Pool Size=1000;Connection Timeout=5;Decr Pool Size=4";
        public static async Task<IDbConnection> Crate21OracleConnection()
        {
            var connection = new OracleConnection(DbConnString21);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }

        //备份库
        private const string DbConnString40 = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.170.1.40)(PORT = 1521))(CONNECT_TIMEOUT=3)(TRANSPORT_CONNECT_TIMEOUT=3)(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = QTHISDB)));User Id=ckmes;Password=qt123mes;Min Pool Size=1;Max Pool Size=1000;Connection Timeout=30;Decr Pool Size=4";
        public static async Task<IDbConnection> CrateOracle40Connection()
        {
            var connection = new OracleConnection(DbConnString40);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
        //新库
        private const string DbConnString57 = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.170.1.57)(PORT = 1521))(CONNECT_TIMEOUT=3)(TRANSPORT_CONNECT_TIMEOUT=3)(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = QTSOFT)));User Id=ckmes;Password=qt#it2012;Min Pool Size=5;Max Pool Size=1000;Connection Timeout=30;Decr Pool Size=4";
        public static async Task<IDbConnection> Crate57OracleConnection()
        {
            var connection = new OracleConnection(DbConnString57);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
        //测试库
        private const string DbConnString32 = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.175.3.2)(PORT = 1521))(CONNECT_TIMEOUT=3)(TRANSPORT_CONNECT_TIMEOUT=3)(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = inadb)));User Id=ckmes;Password=qt123mes;Min Pool Size=5;Max Pool Size=3000;Connection Timeout=30;Decr Pool Size=4";
        public static async Task<OracleConnection> CrateOracle32Connection()
        {
            var connection = new OracleConnection(DbConnString32);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
        public static IDbConnection Crate21OracleConnectionPool()
        {
            var connection = new OracleConnection(DbConnString21);
            connection.Open();
            return connection;
        }
        public static IDbConnection Crate57OracleConnectionPool()
        {
            var connection = new OracleConnection(DbConnString57);
            connection.Open();
            return connection;
        }
        //报表库
        private const string DbConnString36= "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST = 10.170.1.36) (PORT=1521)))(CONNECT_DATA=(SID=qtreport)));User Id=ckmes; Password=qt123mes";
        public static async Task<OracleConnection> Crate36OracleConnection()
        {
            var connection = new OracleConnection(DbConnString36);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
        public static string GetWhereIn(List<string> list, string columnName, out Dictionary<string, object> arguments)
        {
            arguments = new Dictionary<string, object>();
            if (list == null || list.Count == 0) return string.Empty;

            var pageSizes = 1000;

            // 计算参数分页页数
            var totalPages = list.Count % pageSizes == 0 ?
                list.Count / pageSizes :
                list.Count / pageSizes + 1;

            var sqlResultContainer = new List<string>();

            for (var pageIndex = 0; pageIndex < totalPages; pageIndex++)
            {
                var skipCount = pageIndex * pageSizes;
                var listGroup = list.Skip(skipCount).Take(pageSizes).ToList();
                if (listGroup.Count == 0) continue;

                sqlResultContainer.Add($"{columnName} IN :p{pageIndex}");
                arguments.Add($"p{pageIndex}", listGroup);
            }

            var sqlResult = "(" + string.Join(" OR ", sqlResultContainer) + ")";

            return sqlResult;
        }
    }
}
