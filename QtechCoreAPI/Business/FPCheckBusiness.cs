using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using QtechCoreAPI.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace QtechCoreAPI
{
    public static class FPCheckBusiness
    {
        private static IMemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
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
                return await conn.QueryFirstOrDefaultAsync<SnPackResult>(executeSql, new { MODULEID = moduleid },commandTimeout:3).ConfigureAwait(false);
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
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { SNCODE = snCode },commandTimeout:3).ConfigureAwait(false);
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
                    string executeSql = @"SELECT A.RCARD,B.WO_CODE,C.PART_SPEC FROM B_WO_PANEL A LEFT JOIN T_WIP B ON A.RCARD=B.RCARD LEFT JOIN I_MATERIAL C ON B.ITEM_CODE=C.PART_CODE WHERE A.RCARD = :RCARD";
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
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { Rcard },commandTimeout:2).ConfigureAwait(false);
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
                    string executeSql = @" SELECT   b.part_spec
                                        FROM      I_WO a
                                               INNER JOIN
                                                  i_material b
                                               ON a.item_code = b.part_code
                                       WHERE   a.WO_CODE = :woCode ";
                    PartSpec = await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { woCode },commandTimeout:2).ConfigureAwait(false);
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
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT ROUTER_CODE, OP_SEQ, OP_CODE FROM( SELECT SYS_OP_C2C (A.ROUTER_CODE) ROUTER_CODE, SYS_OP_C2C (A.OP_CODE) OP_CODE, OP_SEQ FROM B_ROUTER2OP A INNER JOIN I_WO B ON B.RD_ROUTER = A.ROUTER_CODE WHERE WO_CODE = SUBSTR (:WOCODE, 1, 12) AND NOT EXISTS (SELECT 1 FROM TEST_OPCODE_FP D WHERE MODULEID = :MODULEID AND SYS_OP_C2C (A.ROUTER_CODE) = SYS_OP_C2C (D.ROUTER_CODE) AND A.OP_CODE = D.OPCODE AND D.PART_SPEC = :PARTSPEC) ORDER BY OP_SEQ) WHERE ROWNUM = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("WOCODE", woCode);
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<Router2Op>(executeSql, dynamicParams,commandTimeout:3).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取模组测试流程
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<Router2Op> GetCurrentOpCode(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT ROUTER_CODE, OP_SEQ,OPCODE AS OP_CODE FROM( SELECT ROUTER_CODE, OPCODE, OP_SEQ FROM TEST_OPCODE_FP WHERE MODULEID = :MODULEID AND PART_SPEC = :PARTSPEC ORDER BY TESTTIME DESC) AA WHERE ROWNUM = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<Router2Op>(executeSql, dynamicParams, commandTimeout: 2).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rcard"></param>
        /// <returns></returns>
        public static async Task<int> GetBigWo(string partSpec)
        {
            int count;
            string cacheKeys = $"BIGWO_{partSpec}";
            if (!_memoryCache.TryGetValue<int>(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT COUNT(1) FROM T_BIG_WO WHERE PART_SPEC=:PARTSPEC ";
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { PARTSPEC = partSpec }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            return count;
        }
        /// <summary>
        /// FP检查混批
        /// </summary>
        /// <param name="Rcard"></param>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetMixRcard(string Rcard, string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND IS_VALID = 1 AND PART_SPEC = :PARTSPEC AND OPCODE NOT IN ('Resolution') AND RCARD <> :RCARD AND ROWNUM=1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                dynamicParams.Add("RCARD", Rcard);
                return await conn.QuerySingleAsync<int>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestModData>> GetFPNgType(string moduleid, string partSpec, string rcard, string opCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,NGTYPE FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND RCARD=:RCARD AND OPCODE=:OPCODE AND PART_SPEC=:PARTSPEC ";//AND NGTYPE IN('101', '102', '103', '104', '105', '106') AND ROWNUM=1
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("RCARD", rcard);
                dynamicParams.Add("OPCODE", opCode);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams, commandTimeout: 2).ConfigureAwait(false);
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
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("vPARTSPEC", partSpec);
                dynamicParams.Add("vRCARD", Rcard);
                dynamicParams.Add("vMODULEID", moduleid);
                dynamicParams.Add("vSTATION", opCode);
                dynamicParams.Add("RESULT", string.Empty, DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SP_FPCHECKSTATION", dynamicParams, null, commandTimeout: 3, CommandType.StoredProcedure).ConfigureAwait(false);
                return dynamicParams.Get<string>("RESULT");
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
        /// 获取FP测试主表ID
        /// </summary>
        /// <returns></returns>
        public static async Task<long> GetTestFpDataID()
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT TEST_FP_DATA_ID_SEQUENCE.NEXTVAL AS SEQUENCEID FROM DUAL";
                return await conn.QueryFirstOrDefaultAsync<long>(executeSql, commandTimeout: 2).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// FP测试数据保存
        /// </summary>
        /// <param name="data"></param>
        /// <param name="router2Op"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestFpDataAndDetail(cameraQCVoList data, Router2Op router2Op)
        {
            string result = "OK";
            DateTime dt = DateTime.Now;
            string UpdatefpDataSql = "UPDATE TEST_FP_DATA SET IS_VALID=0 WHERE MODULEID=:MODULEID AND OPCODE=:OPCODE AND  PART_SPEC=:PART_SPEC";
            var UpdatefpDataParams = new DynamicParameters();
            UpdatefpDataParams.Add("MODULEID", data.moduleid);
            UpdatefpDataParams.Add("OPCODE", data.opcode);
            UpdatefpDataParams.Add("PART_SPEC", data.partspec);

            string InsertfpDataSql = "INSERT INTO TEST_FP_DATA(ID,PART_SPEC,SUPPLIERNAME,FACTORYNAME,PARTNUMBER,BARCODE,TESTTIME,RCARD,LINE,TESTTYPE,TESTSTATION,EQUIPMENTNUMBER,MAC,IP,EMPID,WO,MODULEID,OPCODE,PROGRAMVER,RESULT,LINKMODULEID,DEVICEID,NGTYPE,IS_VALID,TRANS_STATUS,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5,ROUTER_CODE) VALUES(:ID,:PART_SPEC,:SUPPLIERNAME,:FACTORYNAME,:PARTNUMBER,:BARCODE,:TESTTIME,:RCARD,:LINE,:TESTTYPE,:TESTSTATION,:EQUIPMENTNUMBER,:MAC,:IP,:EMPID,:WO,:MODULEID,:OPCODE,:PROGRAMVER,:RESULT,:LINKMODULEID,:DEVICEID,:NGTYPE,1,0,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5,:ROUTER_CODE)";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("ID", data.id);
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

            string deletefpOpCodeSql = "DELETE FROM TEST_OPCODE_FP WHERE MODULEID=:MODULEID and OPCODE=:OPCODE and  PART_SPEC= :PART_SPEC";
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
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
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
                        string insertfpOpCodeSql = "INSERT INTO TEST_OPCODE_FP(MODULEID,OPCODE,ROUTER_CODE,OP_SEQ,MEMO,TESTTIME,PART_SPEC,MEMO1) VALUES(:MODULEID,:OPCODE,:ROUTER_CODE,:OP_SEQ,NULL,:TESTTIME,:PART_SPEC,:MEMO1)";
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
                        cmd.CommandText = " INSERT INTO TEST_FP_DATA_DETAIL(ID,TESTITEM,TESTSUBITEM,TESTCONDITION,LOWER,UPPER,UNIT,SUBITEMTESTVALUE,SUBITEMTESTRESULT,TESTTIME,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5) VALUES (:ID,:TESTITEM,:TESTSUBITEM,:TESTCONDITION,:LOWER,:UPPER,:UNIT,:SUBITEMTESTVALUE,:SUBITEMTESTRESULT,:TESTTIME,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5)";
                        cmd.Connection = (OracleConnection)conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.Transaction = (OracleTransaction)transaction;
                        double[] id = new double[rowCount];
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
                                id[i] = data.id;
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
                        OracleParameter para = new OracleParameter("ID", id);
                        para.OracleDbType = OracleDbType.Double;
                        cmd.Parameters.Add(para);
                        para = new OracleParameter("TESTITEM", item);
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
        /// FP点不亮测试数据保存
        /// </summary>
        /// <param name="data"></param>
        /// <param name="router2Op"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestFpDataNoLigth(cameraQCVoList data)
        {
            string result = "OK";
            DateTime dt = DateTime.Now;

            string InsertfpDataSql = "INSERT INTO TEST_FP_DATA(ID,PART_SPEC,SUPPLIERNAME,FACTORYNAME,PARTNUMBER,BARCODE,TESTTIME,RCARD,LINE,TESTTYPE,TESTSTATION,EQUIPMENTNUMBER,MAC,IP,EMPID,WO,MODULEID,OPCODE,PROGRAMVER,RESULT,LINKMODULEID,DEVICEID,NGTYPE,IS_VALID,TRANS_STATUS,MEMO1,MEMO2,MEMO3,MEMO4,MEMO5,ROUTER_CODE) VALUES(TEST_FP_DATA_ID_SEQUENCE.NEXTVAL,:PART_SPEC,:SUPPLIERNAME,:FACTORYNAME,:PARTNUMBER,:BARCODE,:TESTTIME,:RCARD,:LINE,:TESTTYPE,:TESTSTATION,:EQUIPMENTNUMBER,:MAC,:IP,:EMPID,:WO,:MODULEID,:OPCODE,:PROGRAMVER,:RESULT,:LINKMODULEID,:DEVICEID,:NGTYPE,1,0,:MEMO1,:MEMO2,:MEMO3,:MEMO4,:MEMO5,:ROUTER_CODE)";
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
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                await conn.ExecuteAsync(InsertfpDataSql, dynamicParams, null, 2).ConfigureAwait(false);
            }
            return result;
        }
        /// <summary>
        /// FP获取MAC对应机台ID
        /// </summary>
        /// <param name="Mac"></param>
        /// <returns></returns>
        public static async Task<string> GetFPStationIdByMac(string Mac)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var para = new DynamicParameters();
                para.Add("vMac", Mac);
                para.Add("vStationId", "", DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SP_GETFPSTATIONIDBYMAC", para, null, 2, CommandType.StoredProcedure).ConfigureAwait(false);
                return para.Get<string>("vStationId");
            }
        }
        /// <summary>
        /// FP数据保存结果
        /// </summary>
        /// <param name="Uid"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static async Task<string> SaveFpDealResult(string Uid, string result)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
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
                return await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { UUID = Uid },commandTimeout:2).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// FP ORT数据上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> SaveTestFpDataORT(TestFpORTData param)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var para = new DynamicParameters();
                para.Add("VID", param.Id);
                para.Add("VMAC", param.mac);
                para.Add("VIP", param.ip);
                para.Add("VEMPID", param.empid);
                para.Add("VWO", param.wo);
                para.Add("VPART_SPEC", param.partspec);
                para.Add("VTESTTIME", param.testTime);
                para.Add("VSUPPLIERNAME", param.supplierName);
                para.Add("VFACTORYNAME", param.factoryName);
                para.Add("VPARTNUMBER", param.partNumber);
                para.Add("VMODULEID", param.moduleid);
                para.Add("VBARCODE", param.barcode);
                para.Add("VRCARD", param.lotNumber);
                para.Add("VLINE", param.line);
                para.Add("VOPCODE", param.opcode);
                para.Add("VTESTTYPE", param.testType);
                para.Add("VTESTSTATION", param.testStation);
                para.Add("VEQUIPMENTNUMBER", param.equipmentNumber);
                para.Add("VPROGRAMVER", param.programVersion);
                para.Add("VRESULT", param.result);
                para.Add("VLINKMODULEID", param.linkmoduleid);
                para.Add("VROUTER_CODE", param.RouterCode);
                para.Add("VIS_VALID", param.Isvalid);
                para.Add("V_TESTSTAGE", param.memo1);
                para.Add("VMEMO3", param.memo3);
                para.Add("v_msg", "", DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SAVE_TEST_FP_DATA_ORT_LC", para, null, 5, CommandType.StoredProcedure).ConfigureAwait(false);
                return para.Get<string>("v_msg");
            }
        }

        /// <summary>
        /// 校验测试软件关联MAC地址
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> CheckFPMappingMAC(cameraQCVoList param)
        {
            string MacValue = string.Empty;
            string MD5 = param.programVersion.Substring(param.programVersion.Length - 6, 6);
            string cacheKeys = $"MD5_{MD5}";
            //var Parammeter = RedisHelperNetCore.Default.StringGet(MD5);
            if (!_memoryCache.TryGetValue(cacheKeys, out MacValue))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"
                                    Select MAC from  T_MAC_SOFT_CLASSIFY
                                    WHERE CHECK_NUMBER IN (
                                    select CHECK_NUMBER from T_SOFT_CLASSIFY 
                                    WHERE SWSTATUS = '1' 
                                    AND MD5 = :MD5 and PART_SPEC = :PART_SPEC 
                                    ) and MAC = :MAC and  ROWNUM = 1";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("MD5", MD5);
                    dynamicParams.Add("PART_SPEC", param.partspec);
                    dynamicParams.Add("MAC", param.mac);
                    MacValue = await conn.QueryFirstOrDefaultAsync<string>(executeSql, dynamicParams).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, MacValue, TimeSpan.FromMinutes(10));
                    //RedisHelperNetCore.Default.StringSet(MD5, param.mac, TimeSpan.FromMinutes(10));
                }
            }
            return MacValue;
        }
        /// <summary>
        /// 校验测试软件关联MAC地址
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<bool> CheckFPMappingMAC(string mac, string MD5)
        {
            IEnumerable<SoftClassifyView> MacValue;
            bool result = false;
            string cacheKeys = $"CheckFPMappingMAC";
            if (!_memoryCache.TryGetValue(cacheKeys, out MacValue))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT A.MD5,A.PART_SPEC,B.MAC FROM T_SOFT_CLASSIFY A LEFT JOIN T_MAC_SOFT_CLASSIFY B ON A.CHECK_NUMBER=B.CHECK_NUMBER WHERE A.SWSTATUS = '1'";
                    MacValue = await conn.QueryAsync<SoftClassifyView>(executeSql, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, MacValue, TimeSpan.FromMinutes(10));
                }
            }
            if (MacValue != null && MacValue.Any())
            {
                var existsMac = MacValue.Where(x => !string.IsNullOrEmpty(x.MAC) && x.MAC.Equals(mac, StringComparison.Ordinal));
                if (existsMac != null && existsMac.Any())
                {
                    if (!existsMac.Where(x => MD5.Equals(x.MD5, StringComparison.Ordinal)).Any())
                    {
                        result = true;
                    }
                }
                else
                {
                    existsMac = MacValue.Where(x => MD5.Equals(x.MD5, StringComparison.Ordinal));
                    if (existsMac != null && existsMac.Any())
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 校验传入的MD5是否是属于正式软件
        /// </summary>
        /// <param name="MD5"></param>
        /// <returns>true : 属于正式软件  false:属于测试软件</returns>
        public static async Task<bool> CheckSoftIsFormal(cameraQCVoList param)
        {
            List<SOFTCLASSIFY> SCLst;
            if (param.programVersion.Length - 6 > 6)
            {
                string MD5 = param.programVersion.Substring(param.programVersion.Length - 6, 6);
                string cacheKeys = "SoftClassify";
                if (!_memoryCache.TryGetValue(cacheKeys, out SCLst))
                {
                    using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                    {
                        string executeSql = @"select MD5 from T_SOFT_CLASSIFY WHERE SWSTATUS = '1' ";
                        var dynamicParams = new DynamicParameters();
                        SCLst = conn.QueryMultiple(executeSql, dynamicParams, commandTimeout: 2).Read<SOFTCLASSIFY>().AsList();
                        _memoryCache.Set(cacheKeys, SCLst, TimeSpan.FromMinutes(10));
                    }
                }
                if (!SCLst.Where(x => x.MD5 == MD5).Any())
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 测试软件已经运行的机台 正式软件不能运行
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<bool> CheckFormalsoft(cameraQCVoList param)
        {
            int count;
            string cacheKeys = "CheckFormalsoft";
            if (!_memoryCache.TryGetValue(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT COUNT(1) FROM  T_MAC_SOFT_CLASSIFY where MAC = :MAC  AND ROWNUM=1";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("MAC", param.mac);
                    count = await conn.QuerySingleAsync<int>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(10));
                }
            }
            if (count > 0)
            {
                //此机台已运行测试软件
                return false;
            }
            return true;

        }
    }
}
