using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using QtechResponse.Dto.MesApi.Params;
using QtechResponse.Dto.MesApi;

namespace QtechCoreAPI.Business
{
    public static class OVCheckBussiness
    {
        private static IMemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        public static async Task<string> GetSysDate()
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT SYSDATE FROM DUAL ";
                return await conn.QueryFirstOrDefaultAsync<string>(executeSql).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 是否属于特殊工单
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetSortWO(string WoCode)
        {
            int count;
            string cacheKeys = $"GetSortWO_{WoCode}";
            if (!_memoryCache.TryGetValue<int>(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"
                                    SELECT   COUNT (1)
                                    FROM   I_WO
                                    WHERE   WO_CODE = :WoCode
                                    AND WORKSHOP_CODE LIKE '%SORT%'";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("WoCode", WoCode);
                    count = await conn.QuerySingleAsync<int>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            return count;
        }

        /// <summary>
        /// 获取模组绑定二维码
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<SnPackResult> GetSnPackByModuleid(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT A.SNCODE,B.IS_VALID FROM TEST_MSR A LEFT JOIN TEST_SNCODE_CHECKRESULT B ON TO_CHAR(A.SNCODE)=B.SNCODE AND B.IS_VALID = 1 AND B.TESTRESULT = 'OK' WHERE MODULEID = :MODULEID ";
                return await conn.QueryFirstOrDefaultAsync<SnPackResult>(executeSql, new { MODULEID = moduleid }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取扫描结果
        /// </summary>
        /// <param name="snCode"></param>
        /// <returns></returns>
        public static async Task<int> GetPackResult(string snCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT COUNT(*) FROM TEST_SNCODE_CHECKRESULT WHERE SNCODE = :SNCODE AND IS_VALID = 1 AND TESTRESULT = 'OK' ";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { SNCODE = snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 检查流程卡机型是否存在
        /// </summary>
        /// <param name="Rcard"></param>
        /// <returns></returns>
        public static async Task<RcardPanelPartSpec> GetRcardPanelPartSpec(string Rcard)
        {
            RcardPanelPartSpec rcardPanel;
            string cacheKeys = $"PANEL_{Rcard}";
            if (!_memoryCache.TryGetValue<RcardPanelPartSpec>(cacheKeys, out rcardPanel))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT A.RCARD,B.WO_CODE,C.PART_SPEC,D.CUSTOMER FROM B_WO_PANEL A LEFT JOIN T_WIP B ON A.RCARD=B.RCARD LEFT JOIN I_MATERIAL C ON B.ITEM_CODE=C.PART_CODE  LEFT JOIN  I_CLIENT_MODEL D ON C.PART_CODE = D.ITEM_CODE WHERE A.RCARD = :RCARD";
                    rcardPanel = await conn.QueryFirstOrDefaultAsync<RcardPanelPartSpec>(executeSql, new { RCARD = Rcard }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, rcardPanel, TimeSpan.FromMinutes(10));
                }
            }
            return rcardPanel;
        }
        /// <summary>
        /// 检查流程卡是否有在制
        /// </summary>
        /// <param name="Rcard"></param>
        /// <returns></returns>
        public static async Task<int> GetWip(string Rcard)
        {
            int count;
            string cacheKeys = $"WIP_{Rcard}";
            if (!_memoryCache.TryGetValue(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"    SELECT    COUNT (1)
                                        FROM   t_wip
                                       WHERE   rcard = :Rcard ";
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { Rcard }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            return count;
        }
        /// <summary>
        /// 工单获取机型
        /// </summary>
        /// <param name="woCode"></param>
        /// <returns></returns>
        public static async Task<string> GetPartSpec(string woCode)
        {
            string PartSpec;
            string cacheKeys = $"WO_{woCode}";
            if (!_memoryCache.TryGetValue(cacheKeys, out PartSpec))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT   B.PART_SPEC
                                        FROM      I_WO A
                                               INNER JOIN
                                                  I_MATERIAL B
                                               ON A.ITEM_CODE = B.PART_CODE
                                       WHERE   A.WO_CODE = :woCode ";
                    PartSpec = await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { woCode }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, PartSpec, TimeSpan.FromMinutes(10));
                }
            }
            return PartSpec;
        }
        /// <summary>
        /// 获取工序下道工序
        /// </summary>
        /// <param name="woCode"></param>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<Router2Op> GetNextOpCode(string woCode, string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT ROUTER_CODE, OP_SEQ, OP_CODE FROM( SELECT SYS_OP_C2C (A.ROUTER_CODE) ROUTER_CODE, SYS_OP_C2C (A.OP_CODE) OP_CODE, OP_SEQ FROM B_ROUTER2OP@QTDB A INNER JOIN I_WO B ON B.RD_ROUTER = A.ROUTER_CODE WHERE WO_CODE = SUBSTR (:WOCODE, 1, 12) AND NOT EXISTS (SELECT /*+index(D TEST_OP_INDEX) */ 1 FROM TEST_MOD_OPCODE D WHERE MODULEID = :MODULEID AND SYS_OP_C2C (A.ROUTER_CODE) = SYS_OP_C2C (D.ROUTER_CODE) AND A.OP_CODE = D.OPCODE AND D.PART_SPEC = :PARTSPEC) ORDER BY OP_SEQ) WHERE ROWNUM = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("WOCODE", woCode);
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<Router2Op>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取模组当前测试流程
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<Router2Op> GetCurrentOpCode(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT ROUTER_CODE, OP_SEQ,OPCODE AS OP_CODE FROM( SELECT /*+index(C TEST_OP_INDEX) */ ROUTER_CODE, OPCODE, OP_SEQ FROM TEST_MOD_OPCODE C WHERE MODULEID = :MODULEID AND PART_SPEC = :PARTSPEC ORDER BY TESTTIME DESC) AA WHERE ROWNUM = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<Router2Op>(executeSql, dynamicParams, commandTimeout: 2).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 获取OV测试主表ID
        /// </summary>
        /// <returns></returns>
        public static async Task<long> GetTestOVDataID()
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT TEST_MOD_DATA_ID_SEQUENCE.NEXTVAL AS SEQUENCEID FROM DUAL";
                return await conn.QueryFirstOrDefaultAsync<long>(executeSql).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 获取大工单机型
        /// </summary>
        /// <param name="Rcard"></param>
        /// <returns></returns>
        public static async Task<int> GetBigWo(string partSpec)
        {
            int count;
            string cacheKeys = $"BIGWO_{partSpec}";
            if (!_memoryCache.TryGetValue<int>(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT COUNT(1) FROM T_BIG_WO WHERE PART_SPEC=:PARTSPEC ";
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { PARTSPEC = partSpec }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            return count;
        }
        /// <summary>
        /// 获取首道流程工序 By Wo_Code
        /// </summary>
        /// <param name="RouterCode"></param>
        /// <returns></returns>
        public static async Task<Router2Op> GetRouter2OpByWo(string WoCode)
        {
            Router2Op router2Op;
            string cacheKeys = $"Get_Router2Op_i_wo_{WoCode}";
            if (!_memoryCache.TryGetValue(cacheKeys, out router2Op))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT B.ROUTER_CODE,B.OP_CODE,B.OP_SEQ FROM I_WO A INNER JOIN B_ROUTER2OP B ON A.RD_ROUTER=B.ROUTER_CODE WHERE A.WO_CODE=:WoCode ORDER BY B.OP_SEQ ASC";
                    router2Op = await conn.QueryFirstOrDefaultAsync<Router2Op>(executeSql, new { WoCode }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, router2Op, TimeSpan.FromMinutes(10));
                }
            }
            return router2Op;
        }

        /// <summary>
        /// 获取首道流程工序 By ModuleName
        /// </summary>
        /// <param name="RouterCode"></param>
        /// <returns></returns>
        public static async Task<Router2Op> GetRouter2OpByModuleName(string ModuleName)
        {
            Router2Op router2Op;
            string cacheKeys = $"Get_Router2Op_i_wo_{ModuleName}";
            if (!_memoryCache.TryGetValue(cacheKeys, out router2Op))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT B.ROUTER_CODE,B.OP_CODE,B.OP_SEQ FROM I_WO A INNER JOIN B_ROUTER2OP B ON A.RD_ROUTER=B.ROUTER_CODE WHERE A.MODULE_NAME=:ModuleName ORDER BY B.OP_SEQ ASC";
                    router2Op = await conn.QueryFirstOrDefaultAsync<Router2Op>(executeSql, new { ModuleName }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, router2Op, TimeSpan.FromMinutes(10));
                }
            }
            return router2Op;
        }

        /// <summary>
        /// 获取流程工序
        /// </summary>
        /// <param name="RouterCode"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Router2Op>> GetRouter2Op(string RouterCode)
        {
            IEnumerable<Router2Op> router2Op;
            string cacheKeys = $"Get_Router2Op_{RouterCode}";
            if (!_memoryCache.TryGetValue(cacheKeys, out router2Op))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT ROUTER_CODE,OP_CODE,OP_SEQ FROM B_ROUTER2OP WHERE ROUTER_CODE=:RouterCode ORDER BY OP_SEQ ASC ";
                    router2Op = await conn.QueryAsync<Router2Op>(executeSql, new { RouterCode }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, router2Op, TimeSpan.FromMinutes(10));
                }
            }
            return router2Op;
        }
        /// <summary>
        /// OV检查混批
        /// </summary>
        /// <param name="Rcard"></param>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetMixRcard(string Rcard, string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM TEST_MOD_DATA WHERE MODULEID = :MODULEID AND IS_VALID = 1 AND PART_SPEC = :PARTSPEC AND OPCODE NOT IN ('Resolution') AND RCARD <> :RCARD AND ROWNUM=1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                dynamicParams.Add("RCARD", Rcard);
                return await conn.QuerySingleAsync<int>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 模组绑定二维码
        /// </summary>
        /// <param name="Rcard"></param>
        /// <param name="moduleid"></param>
        /// <param name="snCode"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestMsr(string Rcard, string moduleid, string snCode, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("vrcard", Rcard);
                dynamicParams.Add("vmoduleid", moduleid);
                dynamicParams.Add("vsncode", snCode);
                dynamicParams.Add("vpartspec", partSpec);
                dynamicParams.Add("vmsg", string.Empty, DbType.String, ParameterDirection.Output, 4000);
                await conn.ExecuteAsync("SAVE_TESTMSR_NEW", dynamicParams, null, 2, CommandType.StoredProcedure).ConfigureAwait(false);
                return dynamicParams.Get<string>("vmsg");
            }
        }
        /// <summary>
        /// 非FP测试数据保存
        /// </summary>
        /// <param name="data"></param>
        /// <param name="router2Op"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestOVDataAndDetail(cameraQCVoList data, Router2Op router2Op)
        {
            string result = "OK";
            DateTime dt = DateTime.Now;
            string UpdatefpDataSql = "UPDATE /*+index(C TESTMODDATA_MODULEID) */ TEST_MOD_DATA C SET IS_VALID=0 WHERE MODULEID=:MODULEID AND OPCODE=:OPCODE AND  PART_SPEC=:PART_SPEC";
            var UpdatefpDataParams = new DynamicParameters();
            UpdatefpDataParams.Add("MODULEID", data.moduleid);
            UpdatefpDataParams.Add("OPCODE", data.opcode);
            UpdatefpDataParams.Add("PART_SPEC", data.partspec);

            string InsertfpDataSql = "INSERT INTO TEST_MOD_DATA(PART_SPEC,SUPPLIERNAME,FACTORYNAME,PARTNUMBER,BARCODE,TESTTIME,RCARD,LINE,TESTTYPE,TESTSTATION,EQUIPMENTNUMBER,MAC,IP,EMPID,WO,MODULEID,OPCODE,PROGRAMVER,RESULT,LINKMODULEID,DEVICEID,NGTYPE,IS_VALID,TRANS_STATUS,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5,ROUTER_CODE) VALUES(:PART_SPEC,:SUPPLIERNAME,:FACTORYNAME,:PARTNUMBER,:BARCODE,:TESTTIME,:RCARD,:LINE,:TESTTYPE,:TESTSTATION,:EQUIPMENTNUMBER,:MAC,:IP,:EMPID,:WO,:MODULEID,:OPCODE,:PROGRAMVER,:RESULT,:LINKMODULEID,:DEVICEID,:NGTYPE,1,0,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5,:ROUTER_CODE)";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("PART_SPEC", data.partspec);
            dynamicParams.Add("SUPPLIERNAME", data.supplierName);
            dynamicParams.Add("FACTORYNAME", data.factoryName);
            dynamicParams.Add("PARTNUMBER", data.partNumber);
            dynamicParams.Add("BARCODE", data.barcode);
            dynamicParams.Add("TESTTIME", dt);
            dynamicParams.Add("RCARD", data.lotNumber);
            dynamicParams.Add("LINE", data.line);
            dynamicParams.Add("TESTTYPE", data.testType);
            dynamicParams.Add("TESTSTATION", data.testStation);
            dynamicParams.Add("EQUIPMENTNUMBER", data.equipmentNumber);
            dynamicParams.Add("MAC", data.mac);
            dynamicParams.Add("IP", data.ip);
            dynamicParams.Add("EMPID", data.empid);
            dynamicParams.Add("WO", data.wo);
            dynamicParams.Add("MODULEID", data.moduleid);
            dynamicParams.Add("OPCODE", data.opcode);
            dynamicParams.Add("PROGRAMVER", data.programVersion);
            dynamicParams.Add("RESULT", data.result);
            dynamicParams.Add("LINKMODULEID", data.linkmoduleid);
            dynamicParams.Add("DEVICEID", data.deviceid);
            dynamicParams.Add("NGTYPE", data.ngtype);
            dynamicParams.Add("MEMO1", data.memo1);
            dynamicParams.Add("MEMO2", data.memo2);
            dynamicParams.Add("MEMO3", data.memo3);
            dynamicParams.Add("MEMO4", data.memo4);
            dynamicParams.Add("MEMO5", data.memo5);
            dynamicParams.Add("ROUTER_CODE", router2Op?.Router_Code);

            string deletefpOpCodeSql = "DELETE /*+index(C TEST_OP_INDEX) */ FROM TEST_MOD_OPCODE C WHERE MODULEID=:MODULEID and OPCODE=:OPCODE and  PART_SPEC= :PART_SPEC";
            var deletefpOpCodeParams = new DynamicParameters();
            deletefpOpCodeParams.Add("MODULEID", data.moduleid);
            deletefpOpCodeParams.Add("OPCODE", data.opcode);
            deletefpOpCodeParams.Add("PART_SPEC", data.partspec);
            //List<TestFpDataDetail> testFpDataDetails = new List<TestFpDataDetail>();
            int rowCount = 0;
            if (data.testItemList != null && data.testItemList.Count > 0)
            {
                foreach (testItemList testItemList in data.testItemList)
                {
                    if (testItemList.testSubItem != null && testItemList.testSubItem.Count > 0)
                    {
                        foreach (testSubItem testSubItem in testItemList.testSubItem)
                        {
                            rowCount++;
                        }
                    }
                }
            }
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    int row = await conn.ExecuteAsync(UpdatefpDataSql, UpdatefpDataParams, transaction, 2).ConfigureAwait(false);
                    await conn.ExecuteAsync(InsertfpDataSql, dynamicParams, transaction, 2).ConfigureAwait(false);
                    if (row > 0)
                        await conn.ExecuteAsync(deletefpOpCodeSql, deletefpOpCodeParams, transaction, 2).ConfigureAwait(false);
                    if ("OK".Equals(data.result, StringComparison.OrdinalIgnoreCase))
                    {
                        string insertfpOpCodeSql = "INSERT INTO TEST_MOD_OPCODE(MODULEID,OPCODE,ROUTER_CODE,OP_SEQ,MEMO,TESTTIME,PART_SPEC,MEMO1) VALUES(:MODULEID,:OPCODE,:ROUTER_CODE,:OP_SEQ,NULL,:TESTTIME,:PART_SPEC,:MEMO1)";
                        var insertfpOpCodeParams = new DynamicParameters();
                        insertfpOpCodeParams.Add("MODULEID", data.moduleid);
                        insertfpOpCodeParams.Add("OPCODE", data.opcode);
                        insertfpOpCodeParams.Add("ROUTER_CODE", router2Op?.Router_Code);
                        insertfpOpCodeParams.Add("OP_SEQ", router2Op?.Op_Seq);
                        insertfpOpCodeParams.Add("TESTTIME", dt);
                        insertfpOpCodeParams.Add("PART_SPEC", data.partspec);
                        insertfpOpCodeParams.Add("MEMO1", data.lotNumber);
                        await conn.ExecuteAsync(insertfpOpCodeSql, insertfpOpCodeParams, transaction, 2).ConfigureAwait(false);
                    }
                    if (rowCount > 0)
                    {
                        OracleCommand cmd = (OracleCommand)conn.CreateCommand();
                        cmd.ArrayBindCount = rowCount;
                        cmd.CommandText = " INSERT INTO TEST_MOD_DATA_DETAIL(ID,TESTITEM,TESTSUBITEM,TESTCONDITION,LOWER,UPPER,UNIT,SUBITEMTESTVALUE,SUBITEMTESTRESULT,TESTTIME,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5) VALUES (TEST_MOD_DATA_ID_SEQUENCE.CURRVAL,:TESTITEM,:TESTSUBITEM,:TESTCONDITION,:LOWER,:UPPER,:UNIT,:SUBITEMTESTVALUE,:SUBITEMTESTRESULT,:TESTTIME,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5)";
                        cmd.Connection = (OracleConnection)conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.Transaction = (OracleTransaction)transaction;
                        string[] item = new string[rowCount];
                        string[] subItem = new string[rowCount];
                        string[] testCondition = new string[rowCount];
                        string[] lower = new string[rowCount];
                        string[] upper = new string[rowCount];
                        string[] unit = new string[rowCount];
                        string[] value = new string[rowCount];
                        string[] itemResult = new string[rowCount];
                        DateTime[] testTime = new DateTime[rowCount];
                        string[] memo1 = new string[rowCount];
                        string[] memo2 = new string[rowCount];
                        string[] memo3 = new string[rowCount];
                        string[] memo4 = new string[rowCount];
                        string[] memo5 = new string[rowCount];
                        int i = 0;
                        foreach (testItemList p111 in data.testItemList)
                        {
                            foreach (testSubItem p1111 in p111.testSubItem)
                            {
                                item[i] = p111.testItemName;
                                subItem[i] = p1111.testSubItemName;
                                testCondition[i] = p1111.testCondition;
                                lower[i] = p1111.specLowerLimit;
                                upper[i] = p1111.specUpperLimit;
                                unit[i] = p1111.unit;
                                value[i] = p1111.subItemTestValue;
                                if ("OK".Equals(p1111.subItemTestResult, StringComparison.OrdinalIgnoreCase))
                                {
                                    itemResult[i] = "PASS";
                                }
                                else if ("NG".Equals(p1111.subItemTestResult, StringComparison.OrdinalIgnoreCase))
                                {
                                    itemResult[i] = "FAIL";
                                }
                                else
                                {
                                    itemResult[i] = p1111.subItemTestResult;
                                }
                                testTime[i] = dt;
                                memo1[i] = p1111.memo1;
                                memo2[i] = p1111.memo2;
                                memo3[i] = p1111.memo3;
                                memo4[i] = p1111.memo4;
                                memo5[i] = p1111.memo5;
                                i++;
                            }
                        }
                        OracleParameter para = new OracleParameter("TESTITEM", item);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTSUBITEM", subItem);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTCONDITION", testCondition);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("LOWER", lower);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("UPPER", upper);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("UNIT", unit);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("SUBITEMTESTVALUE", value);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("SUBITEMTESTRESULT", itemResult);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTTIME", testTime);
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO1", memo1);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO2", memo2);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO3", memo3);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO4", memo4);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO5", memo5);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        cmd.CommandTimeout = 2;
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
            return result;
        }
        /// <summary>
        /// AA测试数据保存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestAADataAndDetail(cameraQCVoList data)
        {
            string result = "OK";
            DateTime dt = DateTime.Now;
            string UpdatefpDataSql = "UPDATE /*+index(C TESTMODDATA_MODULEID) */ TEST_MOD_DATA C SET IS_VALID=0 WHERE MODULEID=:MODULEID AND OPCODE=:OPCODE AND  PART_SPEC=:PART_SPEC";
            var UpdatefpDataParams = new DynamicParameters();
            UpdatefpDataParams.Add("MODULEID", data.moduleid);
            UpdatefpDataParams.Add("OPCODE", data.opcode);
            UpdatefpDataParams.Add("PART_SPEC", data.partspec);

            string InsertfpDataSql = "INSERT INTO TEST_MOD_DATA(PART_SPEC,SUPPLIERNAME,FACTORYNAME,PARTNUMBER,BARCODE,TESTTIME,RCARD,LINE,TESTTYPE,TESTSTATION,EQUIPMENTNUMBER,MAC,IP,EMPID,WO,MODULEID,OPCODE,PROGRAMVER,RESULT,LINKMODULEID,DEVICEID,NGTYPE,IS_VALID,TRANS_STATUS,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5,ROUTER_CODE) VALUES(:PART_SPEC,:SUPPLIERNAME,:FACTORYNAME,:PARTNUMBER,:BARCODE,:TESTTIME,:RCARD,:LINE,:TESTTYPE,:TESTSTATION,:EQUIPMENTNUMBER,:MAC,:IP,:EMPID,:WO,:MODULEID,:OPCODE,:PROGRAMVER,:RESULT,:LINKMODULEID,:DEVICEID,:NGTYPE,1,0,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5,:ROUTER_CODE)";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("PART_SPEC", data.partspec);
            dynamicParams.Add("SUPPLIERNAME", data.supplierName);
            dynamicParams.Add("FACTORYNAME", data.factoryName);
            dynamicParams.Add("PARTNUMBER", data.partNumber);
            dynamicParams.Add("BARCODE", data.barcode);
            dynamicParams.Add("TESTTIME", dt);
            dynamicParams.Add("RCARD", data.lotNumber);
            dynamicParams.Add("LINE", data.line);
            dynamicParams.Add("TESTTYPE", data.testType);
            dynamicParams.Add("TESTSTATION", data.testStation);
            dynamicParams.Add("EQUIPMENTNUMBER", data.equipmentNumber);
            dynamicParams.Add("MAC", data.mac);
            dynamicParams.Add("IP", data.ip);
            dynamicParams.Add("EMPID", data.empid);
            dynamicParams.Add("WO", data.wo);
            dynamicParams.Add("MODULEID", data.moduleid);
            dynamicParams.Add("OPCODE", data.opcode);
            dynamicParams.Add("PROGRAMVER", data.programVersion);
            dynamicParams.Add("RESULT", data.result);
            dynamicParams.Add("LINKMODULEID", data.linkmoduleid);
            dynamicParams.Add("DEVICEID", data.deviceid);
            dynamicParams.Add("NGTYPE", data.ngtype);
            dynamicParams.Add("MEMO1", data.memo1);
            dynamicParams.Add("MEMO2", data.memo2);
            dynamicParams.Add("MEMO3", data.memo3);
            dynamicParams.Add("MEMO4", data.memo4);
            dynamicParams.Add("MEMO5", data.memo5);
            dynamicParams.Add("ROUTER_CODE", "前段AA测试");

            int rowCount = 0;
            if (data.testItemList != null && data.testItemList.Count > 0)
            {
                foreach (testItemList testItemList in data.testItemList)
                {
                    if (testItemList.testSubItem != null && testItemList.testSubItem.Count > 0)
                    {
                        foreach (testSubItem testSubItem in testItemList.testSubItem)
                        {
                            rowCount++;
                        }
                    }
                }
            }
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    int row = await conn.ExecuteAsync(UpdatefpDataSql, UpdatefpDataParams, transaction, 2).ConfigureAwait(false);
                    await conn.ExecuteAsync(InsertfpDataSql, dynamicParams, transaction, 2).ConfigureAwait(false);
                    if (rowCount > 0)
                    {
                        OracleCommand cmd = (OracleCommand)conn.CreateCommand();
                        cmd.ArrayBindCount = rowCount;
                        cmd.CommandText = " INSERT INTO TEST_MOD_DATA_DETAIL(ID,TESTITEM,TESTSUBITEM,TESTCONDITION,LOWER,UPPER,UNIT,SUBITEMTESTVALUE,SUBITEMTESTRESULT,TESTTIME,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5) VALUES (TEST_MOD_DATA_ID_SEQUENCE.CURRVAL,:TESTITEM,:TESTSUBITEM,:TESTCONDITION,:LOWER,:UPPER,:UNIT,:SUBITEMTESTVALUE,:SUBITEMTESTRESULT,:TESTTIME,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5)";
                        cmd.Connection = (OracleConnection)conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.Transaction = (OracleTransaction)transaction;
                        string[] item = new string[rowCount];
                        string[] subItem = new string[rowCount];
                        string[] testCondition = new string[rowCount];
                        string[] lower = new string[rowCount];
                        string[] upper = new string[rowCount];
                        string[] unit = new string[rowCount];
                        string[] value = new string[rowCount];
                        string[] itemResult = new string[rowCount];
                        DateTime[] testTime = new DateTime[rowCount];
                        string[] memo1 = new string[rowCount];
                        string[] memo2 = new string[rowCount];
                        string[] memo3 = new string[rowCount];
                        string[] memo4 = new string[rowCount];
                        string[] memo5 = new string[rowCount];
                        int i = 0;
                        foreach (testItemList p111 in data.testItemList)
                        {
                            foreach (testSubItem p1111 in p111.testSubItem)
                            {
                                item[i] = p111.testItemName;
                                subItem[i] = p1111.testSubItemName;
                                testCondition[i] = p1111.testCondition;
                                lower[i] = p1111.specLowerLimit;
                                upper[i] = p1111.specUpperLimit;
                                unit[i] = p1111.unit;
                                value[i] = p1111.subItemTestValue;
                                if ("OK".Equals(p1111.subItemTestResult, StringComparison.OrdinalIgnoreCase))
                                {
                                    itemResult[i] = "PASS";
                                }
                                else if ("NG".Equals(p1111.subItemTestResult, StringComparison.OrdinalIgnoreCase))
                                {
                                    itemResult[i] = "FAIL";
                                }
                                else
                                {
                                    itemResult[i] = p1111.subItemTestResult;
                                }
                                testTime[i] = dt;
                                memo1[i] = p1111.memo1;
                                memo2[i] = p1111.memo2;
                                memo3[i] = p1111.memo3;
                                memo4[i] = p1111.memo4;
                                memo5[i] = p1111.memo5;
                                i++;
                            }
                        }
                        OracleParameter para = new OracleParameter("TESTITEM", item);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTSUBITEM", subItem);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTCONDITION", testCondition);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("LOWER", lower);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("UPPER", upper);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("UNIT", unit);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("SUBITEMTESTVALUE", value);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("SUBITEMTESTRESULT", itemResult);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTTIME", testTime);
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO1", memo1);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO2", memo2);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO3", memo3);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO4", memo4);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("MEMO5", memo5);
                        para.OracleDbType = OracleDbType.NVarchar2;
                        cmd.Parameters.Add(para);
                        cmd.CommandTimeout = 2;
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
            return result;
        }
        /// <summary>
        /// 非FP点不亮测试数据保存
        /// </summary>
        /// <param name="data"></param>
        /// <param name="router2Op"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestOVDataNoLigth(cameraQCVoList data)
        {
            string result = "OK";
            DateTime dt = DateTime.Now;

            string InsertfpDataSql = "INSERT INTO TEST_MOD_DATA(ID,PART_SPEC,SUPPLIERNAME,FACTORYNAME,PARTNUMBER,BARCODE,TESTTIME,RCARD,LINE,TESTTYPE,TESTSTATION,EQUIPMENTNUMBER,MAC,IP,EMPID,WO,MODULEID,OPCODE,PROGRAMVER,RESULT,LINKMODULEID,DEVICEID,NGTYPE,IS_VALID,TRANS_STATUS,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5,ROUTER_CODE) VALUES(TEST_MOD_DATA_ID_SEQUENCE.NEXTVAL,:PART_SPEC,:SUPPLIERNAME,:FACTORYNAME,:PARTNUMBER,:BARCODE,:TESTTIME,:RCARD,:LINE,:TESTTYPE,:TESTSTATION,:EQUIPMENTNUMBER,:MAC,:IP,:EMPID,:WO,:MODULEID,:OPCODE,:PROGRAMVER,:RESULT,:LINKMODULEID,:DEVICEID,:NGTYPE,1,0,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5,:ROUTER_CODE)";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("PART_SPEC", data.partspec);
            dynamicParams.Add("SUPPLIERNAME", data.supplierName);
            dynamicParams.Add("FACTORYNAME", data.factoryName);
            dynamicParams.Add("PARTNUMBER", data.partNumber);
            dynamicParams.Add("BARCODE", data.barcode);
            dynamicParams.Add("TESTTIME", dt);
            dynamicParams.Add("RCARD", data.lotNumber);
            dynamicParams.Add("LINE", data.line);
            dynamicParams.Add("TESTTYPE", data.testType);
            dynamicParams.Add("TESTSTATION", data.testStation);
            dynamicParams.Add("EQUIPMENTNUMBER", data.equipmentNumber);
            dynamicParams.Add("MAC", data.mac);
            dynamicParams.Add("IP", data.ip);
            dynamicParams.Add("EMPID", data.empid);
            dynamicParams.Add("WO", data.wo);
            dynamicParams.Add("MODULEID", data.moduleid);
            dynamicParams.Add("OPCODE", data.opcode);
            dynamicParams.Add("PROGRAMVER", data.programVersion);
            dynamicParams.Add("RESULT", data.result);
            dynamicParams.Add("LINKMODULEID", data.linkmoduleid);
            dynamicParams.Add("DEVICEID", data.deviceid);
            dynamicParams.Add("NGTYPE", data.ngtype);
            dynamicParams.Add("MEMO1", data.memo1);
            dynamicParams.Add("MEMO2", data.memo2);
            dynamicParams.Add("MEMO3", data.memo3);
            dynamicParams.Add("MEMO4", data.memo4);
            dynamicParams.Add("MEMO5", data.memo5);
            dynamicParams.Add("ROUTER_CODE", "");
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                await conn.ExecuteAsync(InsertfpDataSql, dynamicParams, null, 2).ConfigureAwait(false);
            }
            return result;
        }
        /// <summary>
        /// OV数据保存结果
        /// </summary>
        /// <param name="Uid"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static async Task<string> SaveFpDealResult(string Uid, string result)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string InsertSql = "INSERT INTO TEST_DEAL_RESULT(UUID,RESULT) VALUES(:UUID,:RESULT)";
                var para = new DynamicParameters();
                para.Add("UUID", Uid);
                para.Add("RESULT", result);
                await conn.ExecuteAsync(InsertSql, para, commandTimeout: 2).ConfigureAwait(false);
                return "OK";
            }
        }

        /// <summary>
        /// 获取FP数据保存结果
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetFpDealResult(string Uid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT RESULT FROM TEST_DEAL_RESULT WHERE UUID=:UUID";
                return await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { UUID = Uid }, commandTimeout: 2).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 防跳站特殊拦截
        /// </summary>
        /// <param name="Rcard"></param>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static async Task<string> GetErrorCount(string Rcard, string moduleid, string partSpec, string opCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("vPARTSPEC", partSpec);
                dynamicParams.Add("vRCARD", Rcard);
                dynamicParams.Add("vMODULEID", moduleid);
                dynamicParams.Add("vSTATION", opCode);
                dynamicParams.Add("RESULT", string.Empty, DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SP_OVCHECKSTATION", dynamicParams, null, commandTimeout: 3, CommandType.StoredProcedure).ConfigureAwait(false);
                return dynamicParams.Get<string>("RESULT");
            }
        }
        /// <summary>
        /// 小米防跳站特殊拦截
        /// </summary>
        /// <param name="Rcard"></param>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static async Task<string> GetErrorCountForFC(string Rcard, string moduleid, string partSpec, string opCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("vPARTSPEC", partSpec);
                dynamicParams.Add("vRCARD", Rcard);
                dynamicParams.Add("vMODULEID", moduleid);
                dynamicParams.Add("vSTATION", opCode);
                dynamicParams.Add("RESULT", string.Empty, DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SP_OVCHECKSTATION_FC", dynamicParams, null, commandTimeout: 3, CommandType.StoredProcedure).ConfigureAwait(false);
                return dynamicParams.Get<string>("RESULT");
            }
        }
        /// <summary>
        /// 获取领料工号
        /// </summary>
        /// <param name="rcard">流程卡号</param>
        /// <returns></returns>
        public static async Task<RcardIO> GetEmpNoByRcard(string rcard)
        {
            RcardIO rcardIO;
            string cacheKeys = $"GetEmpNoByRcard_{rcard}";
            if (!_memoryCache.TryGetValue(cacheKeys, out rcardIO))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT   EMPNO || '|' || DESCRIPTION AS USERNAME, OPCODE
                                         FROM   (SELECT   A.EMPNO, B.DESCRIPTION, A.OPCODE
                                                   FROM      RCARD_IO A
                                                          INNER JOIN
                                                             WSCUSER B
                                                          ON TO_CHAR(A.EMPNO) = B.ACCOUNT
                                                  WHERE   A.RCARD = :rcard AND A.STATE = '1')
                                        WHERE   ROWNUM = 1";
                    rcardIO = await conn.QueryFirstOrDefaultAsync<RcardIO>(executeSql, new { rcard }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, rcardIO, TimeSpan.FromMinutes(10));
                }
            }
            return rcardIO;
        }

        /// <summary>
        /// 判断当前查询的模组号是否存在测试记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<int> GetTestModOpcode(CheckModuleIsExistsInput input)
        {
            int count;
            string cacheKeys = $"GetSortWO_{input.ModuleId}";
            if (!_memoryCache.TryGetValue<int>(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"
                                    SELECT   COUNT (1)
                                    FROM   Test_Mod_OPCODE
                                    WHERE   MODULEID = :MODULEID 
                                    AND PART_SPEC = :PART_SPEC 
                                    And OPCODE = :OPCONDE ";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("MODULEID", input.ModuleId);
                    dynamicParams.Add("PART_SPEC", input.ModuleName);
                    dynamicParams.Add("OPCODE", input.ReturnStation);
                    count = await conn.QuerySingleAsync<int>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            return count;

        }

        /// <summary>
        /// 捞取当前模组号的测试记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestModOpcodeDto>> GetTestModOpcode(CheckStationInput input)
        {
            IEnumerable<TestModOpcodeDto> testmodopcodedto;
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select MODULEID,PART_SPEC,OPCODE,ROUTER_CODE,OP_SEQ  from TEST_MOD_OPCODE 
                                      Where MODULEID = :MODULEID AND PART_SPEC = :PART_SPEC  ";
                testmodopcodedto = await conn.QueryAsync<TestModOpcodeDto>(executeSql, 
                                         new { MODULEID = input.ModuleId,PART_SPEC = input.PART_SPEC }, commandTimeout: 2)
                                         .ConfigureAwait(false);
            }
            return testmodopcodedto;
        }

        /// <summary>
        /// DeleteTestModOpcode
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<int> DeleteTestModOpcode(DeleteTestModOpcodeInput input)
        {
            string sql = @"Delete From TEST_MOD_OPCODE Where OP_SEQ = :OP_SEQ and MODULEID = :MODULEID AND PART_SPEC = :PART_SPEC";
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var result = conn.Execute(sql, new { OP_SEQ = input.OP_SEQ,MODULEID = input.MODE
                ,PART_SPEC = input.PART_SPEC});
                return result;
            }
        }
    }
}
