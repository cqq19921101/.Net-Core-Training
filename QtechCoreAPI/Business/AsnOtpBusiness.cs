using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace QtechCoreAPI
{
    public static class AsnOtpBusiness
    {
        private static IMemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        public static async Task<DataTable> Get09SnPallet(string asn)
        {
            DataTable table = new DataTable("MyTable");
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT   VR_CLASS, VR_VALUE, END_VALUE,VR_DESC  FROM   B_PARAMETER_INI a WHERE   PRG_NAME = 'FP箱号比对' AND END_VALUE = :asn group by VR_CLASS, VR_VALUE, END_VALUE,VR_DESC order by VR_CLASS";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("asn", asn);
                using (var reader = await conn.ExecuteReaderAsync(executeSql, dynamicParams).ConfigureAwait(false))
                {
                    table.Load(reader);
                }
                return table;
            }
        }
        public static async Task<DataTable> GetCartonModuleid(string palletNo)
        {
            DataTable table = new DataTable("MyTable");
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" select c.moduleid,c.OTP_TIME from T_PALLET2CARTON a inner join  TEST_SNCODE_TRAY2SN b on to_char(A.CARTON_NO)=B.CARTON_NO and B.IS_VALID=1 --and b.testresult='OK'
                                        inner join r_otpcheck_log c on B.SNCODE=C.SNCODE and c.is_valid=1 and c.message='OK'
                                        where PALLET_NO = :pallet";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("pallet", palletNo);
                using (var reader = await conn.ExecuteReaderAsync(executeSql, dynamicParams).ConfigureAwait(false))
                {
                    table.Load(reader);
                }
                return table;
            }
        }
        public static async Task<int> UpdatePalletStatus(List<string> palletNo)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" update T_PALLET2CARTON set EATTRIBUTE2='1' where PALLET_NO in :palletNo";
                return await conn.ExecuteAsync(executeSql,new { palletNo }).ConfigureAwait(false);
            }
        }
        public static async Task<int> RollBackPalletStatus(List<string> palletNo)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" update T_PALLET2CARTON set EATTRIBUTE2='' where PALLET_NO in :palletNo";
                return await conn.ExecuteAsync(executeSql, new { palletNo }).ConfigureAwait(false);
            }
        }
        public static async Task<int> GetPalletStatus(string palletNo)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" select count(1) from T_PALLET2CARTON where EATTRIBUTE2='1' and PALLET_NO=:pallet";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("pallet", palletNo);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
    }
}
