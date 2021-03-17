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
    public static class RDSoftBusiness
    {
        private static IMemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        /// <summary>
        /// 检查机型是否存在
        /// </summary>
        /// <param name="Rcard"></param>
        /// <returns></returns>
        public static async Task<int> GetPartSpec(string PartSpec)
        {
            int count;
            string cacheKeys = $"GetPartSpec_{PartSpec}";
            if (!_memoryCache.TryGetValue<int>(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT COUNT(1) FROM I_MATERIAL WHERE PART_SPEC=:PARTSPEC";
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { PARTSPEC = PartSpec }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            return count;
        }
        public static async Task<int> UpdateSoftClassify(SoftClassify softClassify)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string checkResultSql = @"UPDATE T_SOFT_CLASSIFY SET MD5=:MD5, PART_SPEC=:PartSpec, STATION=:Station, SWSTATUS=:SwStatus,UDATE=SYSDATE WHERE CHECK_NUMBER=:CheckNumber";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("CheckNumber", softClassify.CheckNumber);
                checkResultParams.Add("MD5", softClassify.MD5);
                checkResultParams.Add("PartSpec", softClassify.ProductName);
                checkResultParams.Add("Station", softClassify.Station);
                checkResultParams.Add("SwStatus", softClassify.SwStatus);
                return await conn.ExecuteAsync(checkResultSql, checkResultParams).ConfigureAwait(false);
            }
        }
        public static async Task<int> SaveSoftClassify(SoftClassify softClassify)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string checkResultSql = @"INSERT INTO T_SOFT_CLASSIFY(CHECK_NUMBER, MD5, PART_SPEC, STATION, SWSTATUS, MDATE) VALUES (:CheckNumber,:MD5,:PartSpec,:Station,:SwStatus,sysdate)";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("CheckNumber", softClassify.CheckNumber);
                checkResultParams.Add("MD5", softClassify.MD5);
                checkResultParams.Add("PartSpec", softClassify.ProductName);
                checkResultParams.Add("Station", softClassify.Station);
                checkResultParams.Add("SwStatus", softClassify.SwStatus);
                return await conn.ExecuteAsync(checkResultSql, checkResultParams).ConfigureAwait(false);
            }
        }
        public static async Task<SoftClassify> GetSoftClassify(string CheckNumber)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT CHECK_NUMBER AS CHECKNUMBER,MD5,PART_SPEC AS PRODUCTNAME,STATION,SWSTATUS FROM T_SOFT_CLASSIFY WHERE CHECK_NUMBER=:CheckNumber";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("CheckNumber", CheckNumber);
                return await conn.QueryFirstOrDefaultAsync<SoftClassify>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 流程卡获取机种和客户
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <returns></returns>
        public static async Task<PartSpecClientModel> GetCustomerByRcard(string Rcard)
        {
            PartSpecClientModel partSpec;
            string cacheKeys = $"GetCustomerByRcard_{Rcard}";
            if (!_memoryCache.TryGetValue(cacheKeys, out partSpec))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT   B.CUSTOMER,B.PART_SPEC AS PARTSPEC FROM T_WIP A INNER JOIN I_CLIENT_MODEL B ON A.ITEM_CODE = B.ITEM_CODE WHERE  A.RCARD = :rcard AND ROWNUM = 1";
                    partSpec = await conn.QueryFirstOrDefaultAsync<PartSpecClientModel>(executeSql, new { rcard = Rcard }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, partSpec, TimeSpan.FromMinutes(3));
                }
            }
            return partSpec;
        }
        /// <summary>
        /// 二维码获取机种和客户
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <returns></returns>
        public static async Task<PartSpecClientModel> GetCustomerBySn(string Sn)
        {
            PartSpecClientModel partSpec;
            string cacheKeys = $"GetCustomerBySn_{Sn}";
            if (!_memoryCache.TryGetValue(cacheKeys, out partSpec))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT  c.moduleid as MaterialCode ,B.CUSTOMER, B.PART_SPEC AS PARTSPEC
                                              FROM         test_msr c
                                                        INNER JOIN
                                                           T_WIP A
                                                        ON TO_CHAR (C.RCARD) = A.RCARD
                                                     INNER JOIN
                                                        I_CLIENT_MODEL B
                                                     ON A.ITEM_CODE = B.ITEM_CODE
                                             WHERE   C.SNCODE = :SnCode AND ROWNUM = 1";
                    partSpec = await conn.QueryFirstOrDefaultAsync<PartSpecClientModel>(executeSql, new { SnCode = Sn }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, partSpec, TimeSpan.FromMinutes(3));
                }
            }
            return partSpec;
        }
        public static async Task<IEnumerable<RcardTestData>> GetFPModuleidByRcard(string rcard)
        {
            using (IDbConnection conn = await DapperFactory.Crate36OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT A.MODULEID,A.OPCODE FROM TEST_FP_DATA A INNER JOIN (
                                    SELECT MODULEID,MAX(ID) AS ID FROM TEST_FP_DATA  R  WHERE RCARD=:RCARD AND RESULT='OK' AND IS_VALID=1
                                    GROUP BY MODULEID) B ON A.ID=B.ID";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RCARD", rcard);
                return await conn.QueryAsync<RcardTestData>(executeSql, dynamicParams, commandTimeout: 60).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<RcardTestData>> GetModuleidByRcard(string rcard)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT A.MODULEID,A.OPCODE FROM TEST_MOD_DATA A INNER JOIN (
                                    SELECT MODULEID,MAX(ID) AS ID FROM TEST_MOD_DATA  R  WHERE RCARD=:RCARD AND RESULT='OK' AND IS_VALID=1
                                    GROUP BY MODULEID) B ON A.ID=B.ID ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RCARD", rcard);
                return await conn.QueryAsync<RcardTestData>(executeSql, dynamicParams, commandTimeout: 60).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestDataDetail>> GetFPDataDateilByRcard(List<string> rcard)
        {
            Dictionary<string, object> arguments;
            using (IDbConnection conn = await DapperFactory.Crate36OracleConnection().ConfigureAwait(false))
            {
                string executeSql = $@"SELECT   A.MODULEID,
                                            A.OPCODE,
                                            A.BARCODE,
                                            A.TESTTIME,
                                            B.TESTITEM,
                                            B.TESTSUBITEM,
                                            B.SUBITEMTESTVALUE
                                    FROM      TEST_FP_DATA A
                                            INNER JOIN
                                            TEST_FP_DATA_DETAIL B
                                            ON A.ID = B.ID
                                            WHERE {DapperFactory.GetWhereIn(rcard, "A.RCARD", out arguments)} and A.result='OK'";
                return await conn.QueryAsync<TestDataDetail>(executeSql, arguments, commandTimeout: 60).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestDataDetail>> GetFPDataDateilByModuleud(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate36OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   A.MODULEID,
                                            A.OPCODE,
                                            A.BARCODE,
                                            A.TESTTIME,
                                            B.TESTITEM,
                                            B.TESTSUBITEM,
                                            B.SUBITEMTESTVALUE
                                    FROM      TEST_FP_DATA A
                                            INNER JOIN
                                            TEST_FP_DATA_DETAIL B
                                            ON A.ID = B.ID
                                            WHERE A.MODULEID=:moduleid and A.result='OK'";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                return await conn.QueryAsync<TestDataDetail>(executeSql, dynamicParams, commandTimeout: 60).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestDataDetail>> GetDataDateilByModuleud(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   A.MODULEID,
                                            A.OPCODE,
                                            A.BARCODE,
                                            A.TESTTIME,
                                            B.TESTITEM,
                                            B.TESTSUBITEM,
                                            B.SUBITEMTESTVALUE
                                    FROM      TEST_MOD_DATA A
                                            INNER JOIN
                                            TEST_MOD_DATA_DETAIL B
                                            ON A.ID = B.ID
                                            WHERE A.MODULEID=:moduleid and A.result='OK'";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                return await conn.QueryAsync<TestDataDetail>(executeSql, dynamicParams, commandTimeout: 60).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestDataDetail>> GetDataDateilByRcard(List<string> rcard)
        {
            Dictionary<string, object> arguments;
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = $@"SELECT   A.MODULEID,
                                            A.OPCODE,
                                            A.BARCODE,
                                            A.TESTTIME,
                                            B.TESTITEM,
                                            B.TESTSUBITEM,
                                            B.SUBITEMTESTVALUE
                                    FROM      TEST_MOD_DATA A
                                            INNER JOIN
                                            TEST_MOD_DATA_DETAIL B
                                            ON A.ID = B.ID
                                            WHERE {DapperFactory.GetWhereIn(rcard, "A.RCARD", out arguments)}  and A.result='OK'";
                return await conn.QueryAsync<TestDataDetail>(executeSql, arguments, commandTimeout: 60).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 非FP最后工序测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static async Task<TestModData> GetFPFinalData57(string moduleid, string partSpec, string opCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_MOD_DATA WHERE MODULEID = :MODULEID AND IS_VALID=1 AND PART_SPEC=:PARTSPEC AND OPCODE=:OPCODE ORDER BY TESTTIME DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                dynamicParams.Add("OPCODE", opCode);
                return await conn.QueryFirstOrDefaultAsync<TestModData>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 非FP最后工序测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static async Task<TestModData> GetFPFinalData57(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_MOD_DATA WHERE MODULEID = :MODULEID AND IS_VALID=1 AND PART_SPEC=:PARTSPEC ORDER BY TESTTIME DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<TestModData>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        /// <summary>
        ///FP最后工序测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static async Task<TestModData> GetFPFinalData21(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND IS_VALID=1 AND PART_SPEC=:PARTSPEC ORDER BY TESTTIME DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<TestModData>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        /// <summary>
        ///获取机型Bom
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static async Task<DataTable> GetBom21(string partSpec)
        {
            DataTable table = new DataTable("MyTable");
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   ID,
                                            STEPS,
                                            REPLACE_CPN,
                                            UPN,
                                            SOURCE,
                                            NAME,
                                            SPEC,
                                            UNIT,
                                            QTY,
                                            POSITION,
                                            STATUS,
                                            ID1,
                                            MDATE,
                                            MUSER,
                                            VERSION
                                    FROM   AA_BOM
                                    WHERE   ID = (SELECT   PART_CODE
                                                    FROM   I_MATERIAL
                                                WHERE   PART_SPEC = :partSpec AND ROWNUM = 1)
                                            AND IS_DO = 1";
                using (IDataReader reader = conn.ExecuteReader(executeSql, new { partSpec }))
                {
                    table.Load(reader);
                }
                return table;
            }
        }
    }
}
