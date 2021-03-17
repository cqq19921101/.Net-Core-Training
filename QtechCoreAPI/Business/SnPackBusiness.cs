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
    public static class SnPackBusiness
    {
        private static IMemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        /// <summary>
        /// 获取扫码软件版本
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetScanSoftVersion()
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT VR_CLASS FROM B_PARAMETER_INI WHERE PRG_NAME='SCANSOFTVERSION'";
                return await conn.QueryFirstOrDefaultAsync<string>(executeSql).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 根据返回对应的数量
        /// </summary>
        /// <param name="cartonNo">小箱号</param>
        /// <returns></returns>
        public static async Task<int> GetCartonQty(string cartonNo)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT SNQTY FROM T_CARTON WHERE CARTON_NO=:CARTONNO AND IS_VALID='1'";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { CARTONNO = cartonNo }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 根据二维码返回对应的tray
        /// </summary>
        /// <param name="snCode">二维码</param>
        /// <returns></returns>
        public static async Task<string> GetSnTray(string snCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT TRAY_NO FROM TEST_SNCODE_TRAY2SN  WHERE SNCODE =:SNCODE AND IS_VALID = 1";
                return await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { SNCODE = snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 根据Tray返回对应的二维码
        /// </summary>
        /// <param name="trayNo">trayNo</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetTraySn(string trayNo)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT SNCODE FROM TEST_SNCODE_TRAY2SN  WHERE TRAY_NO =:TRAYNO AND IS_VALID = 1";
                return await conn.QueryAsync<string>(executeSql, new { TRAYNO = trayNo }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 根据小箱号返回小箱号关联二维码数量
        /// </summary>
        /// <param name="cartonNo">小箱号</param>
        /// <returns></returns>
        public static async Task<int> CheckCartonQty(string cartonNo)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM TEST_SNCODE_CHECKRESULT WHERE CARTON_NO=:CARTONNO AND TESTRESULT='OK' AND IS_VALID=1";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { CARTONNO = cartonNo }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 条码规则生成
        /// </summary>
        /// <param name="ruleCode"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public static async Task<string> SNGetString(string ruleCode, string paramters)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var para = new DynamicParameters();
                para.Add("vName", ruleCode);
                para.Add("vParamters", paramters);
                para.Add("ovSN", "", DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SNBuilding.SNGetString", para, null, null, CommandType.StoredProcedure).ConfigureAwait(false);
                return para.Get<string>("ovSN");
            }
        }
        /// <summary>
        /// 保存TrayNo打印记录
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <param name="trayNo"></param>
        /// <param name="snCodes"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public static async Task<string> SaveTrayNoPrintLog(string cartonNo, string trayNo, string[] snCodes, string stationId)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    int intCount = snCodes.Length;
                    cmd.Connection = (OracleConnection)conn;
                    cmd.ArrayBindCount = intCount;
                    cmd.CommandText = "INSERT INTO TEST_SNCODE_TRAY2SN (CARTON_NO,TRAY_NO,SNCODE,MDATE,IS_VALID,STATION_ID) VALUES (:carton_no,:tray_no,:sncode,sysdate,1,:station_id)";
                    string[] cartonNos = new string[intCount];
                    string[] trayNos = new string[intCount];
                    string[] stationIds = new string[intCount];
                    OracleParameter cartonNoParam = new OracleParameter("carton_no", OracleDbType.NVarchar2);
                    cartonNoParam.Direction = ParameterDirection.Input;
                    cartonNoParam.Value = cartonNos;
                    cmd.Parameters.Add(cartonNoParam);
                    OracleParameter trayNoParam = new OracleParameter("tray_no", OracleDbType.NVarchar2);
                    trayNoParam.Direction = ParameterDirection.Input;
                    trayNoParam.Value = trayNos;
                    cmd.Parameters.Add(trayNoParam);
                    OracleParameter snCodeParam = new OracleParameter("sncode", OracleDbType.NVarchar2);
                    snCodeParam.Direction = ParameterDirection.Input;
                    snCodeParam.Value = snCodes;
                    cmd.Parameters.Add(snCodeParam);
                    OracleParameter stationIdParam = new OracleParameter("station_id", OracleDbType.NVarchar2);
                    stationIdParam.Direction = ParameterDirection.Input;
                    stationIdParam.Value = stationIds;
                    cmd.Parameters.Add(stationIdParam);
                    for (int i = 0; i < intCount; i++)
                    {
                        cartonNos[i] = cartonNo;
                        trayNos[i] = trayNo;
                        stationIds[i] = stationId;
                    }
                    return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false) > 0 ? "OK" : "NG";
                }
            }
        }
        /// <summary>
        /// 获取流程卡当前在制良品数
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public static async Task<int> GetRcardWipQty(string rcard)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT OK_QTY FROM T_WIP WHERE RCARD=:RCARD";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { RCARD = rcard }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 根据流程卡及检验类型,返回流程卡关联二维码数量
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public static async Task<int> CheckRCardSnCodeQty(string rcard, string checkType)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM TEST_SNCODE_CHECK WHERE RCARD=:RCARD AND CHECKTYPE=:CHECKTYPE AND IS_VALID=1";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { RCARD = rcard, CHECKTYPE = checkType }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 失效模组测试状态
        /// </summary>
        /// <param name="rcard">流程卡</param>
        /// <param name="snCode">二维码</param>
        /// <param name="stationId">机台号</param>
        /// <returns></returns>
        public static async Task<string> InvalidModuleTestResult(string rcard, string snCode, string stationId)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var para = new DynamicParameters();
                para.Add("vRCard", rcard);
                para.Add("vSnCode", snCode);
                para.Add("vStationId", stationId);
                para.Add("vMsg", "", DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SP_InvalidModuleTestResult", para, null, null, CommandType.StoredProcedure).ConfigureAwait(false);
                return para.Get<string>("vMsg");
            }
        }
        /// <summary>
        /// 流程卡返回机型
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <returns></returns>
        public static async Task<string> GetPartSpecByRcard(string rcard)
        {
            string partSpec;
            string cacheKeys = $"getPartSpecByRcard_{rcard}";
            if (!_memoryCache.TryGetValue(cacheKeys, out partSpec))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT S.PART_SPEC FROM T_WIP R INNER JOIN I_MATERIAL S ON R.ITEM_CODE=S.PART_CODE WHERE R.RCARD=:RCARD";
                    partSpec = await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { RCARD = rcard }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, partSpec, TimeSpan.FromMinutes(2));
                }
            }
            return partSpec;
        }
        /// <summary>
        /// 解除模组和小箱号及TrayNo绑定关系
        /// </summary>
        /// <param name="cartonOrTrayNo">小箱号/Tray盘号</param>
        /// <param name="snCode">二维码</param>
        /// <param name="stationId">机台号</param>
        /// <returns></returns>
        public static async Task<string> InvalidModuleLinkResult(string cartonOrTrayNo, string snCode, string stationId)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                var para = new DynamicParameters();
                para.Add("vCartonOrTrayNo", cartonOrTrayNo);
                para.Add("vSnCode", snCode);
                para.Add("vStationId", stationId);
                para.Add("vMsg", "", DbType.String, ParameterDirection.Output, 400);
                await conn.ExecuteAsync("SP_InvalidModuleLinkResult", para, null, null, CommandType.StoredProcedure).ConfigureAwait(false);
                return para.Get<string>("vMsg");
            }
        }
        /// <summary>
        /// 获取二维码绑定模组是否绑定箱号
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public static async Task<ModuleidPackResult> GetModuleiPackdBySn(string sn)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT a.MODULEID,b.IS_VALID FROM (SELECT MODULEID,SNCODE FROM TEST_MSR WHERE SNCODE = :SN UNION SELECT MODULEID,SNCODE FROM TEST_MSR_2019@TO_REPORT WHERE SNCODE = :SN)A LEFT JOIN TEST_SNCODE_TRAY2SN B ON A.SNCODE=B.SNCODE AND B.IS_VALID=1";
                return await conn.QueryFirstOrDefaultAsync<ModuleidPackResult>(executeSql, new { SN = sn }, commandTimeout: 2).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取二维码绑定模组
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public static async Task<string> GetModuleidBySn(string sn)
        {
            string moduleid;
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT MODULEID FROM TEST_MSR WHERE SNCODE = :SN UNION SELECT MODULEID FROM TEST_MSR_2019@TO_REPORT WHERE SNCODE = :SN";
                moduleid = await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { SN = sn }).ConfigureAwait(false);
            }
            return moduleid;
        }
        /// <summary>
        /// 获取FP sorting项
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestModDataDetail>> GetFpSortValue(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"   select b.TestItem,b.TestSubItem,b.SubItemTestValue,b.SubItemTestResult from test_fp_data a inner join test_fp_data_detail b on A.id=b.id
                                                where A.moduleid=:moduleid
                                                and a.part_spec=:partSpec
                                                and a.opcode in('T01','T02')
                                                and a.is_valid=1
                                                and B.TESTITEM in ('201远焦120CMSFR','217AFcode20CM细调')
                                                and (b.TESTSUBITEM like '0.5视场%' or b.TESTSUBITEM ='Near')";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryAsync<TestModDataDetail>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取FP FX332AB sorting项
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetFpSortFX332ABValue(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"   select b.SUBITEMTESTVALUE from test_fp_data a inner join test_fp_data_detail b on A.id=b.id
                                                where A.moduleid=:moduleid
                                                and a.part_spec=:partSpec
                                                and a.opcode='T01'
                                                and a.is_valid=1
                                                and B.TESTITEM='407OC测试'
                                                and b.TESTSUBITEM like 'Offset%'";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryAsync<string>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 检查机种是否存在
        /// </summary>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetPartSpecExists(string partSpec)
        {
            int count;
            string cacheKeys = $"MATERIAL_{partSpec}";
            if (!_memoryCache.TryGetValue(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT COUNT(*) FROM I_MATERIAL WHERE PART_SPEC = :PARTSPEC ";
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { PARTSPEC = partSpec }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(5));
                }
            }
            return count;
        }
        /// <summary>
        /// 箱号获取机种和客户
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <returns></returns>
        public static async Task<PartSpecClientModel> GetPartSpecByCarton(string CartonNo)
        {
            PartSpecClientModel partSpec;
            string cacheKeys = $"T_CARTON{CartonNo}";
            if (!_memoryCache.TryGetValue(cacheKeys, out partSpec))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT B.CUSTOMER, A.WO_CODE AS PARTSPEC,B.MATERIAL_CODE AS MATERIALCODE,TO_CHAR(A.MDATE,'yyMMdd') AS PACKDATE,B.ITEM_CODE FROM T_CARTON A INNER JOIN I_CLIENT_MODEL B ON A.ITEM_CODE = B.ITEM_CODE WHERE A.CARTON_NO = :CARTONNO AND ROWNUM = 1";
                    partSpec = await conn.QueryFirstOrDefaultAsync<PartSpecClientModel>(executeSql, new { CARTONNO = CartonNo }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, partSpec, TimeSpan.FromMinutes(3));
                }
            }
            return partSpec;
        }
        /// <summary>
        /// 获取机种对应客户
        /// </summary>
        /// <param name="PartSpec"></param>
        /// <returns></returns>
        public static async Task<string> GetCustomerByPartSpec(string PartSpec)
        {
            string Customer;
            string cacheKeys = $"I_CLIENT_MODEL{PartSpec}";
            if (!_memoryCache.TryGetValue(cacheKeys, out Customer))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT B.CUSTOMER FROM I_CLIENT_MODEL B WHERE PART_SPEC =:PARTSPEC";
                    Customer = await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { PARTSPEC = PartSpec }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, Customer, TimeSpan.FromMinutes(2));
                }
            }
            return Customer;
        }
        /// <summary>
        /// 机种是否导入新库
        /// </summary>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetNewSoftExists(string partSpec)
        {
            int count;
            string cacheKeys = $"T_NEWSOFTSPEC_{partSpec}";
            if (!_memoryCache.TryGetValue(cacheKeys, out count))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT COUNT(*) FROM T_NEWSOFTSPEC WHERE PART_SPEC = :PARTSPEC ";
                    count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { PARTSPEC = partSpec }, commandTimeout: 2).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, count, TimeSpan.FromMinutes(5));
                }
            }
            return count;
        }
        /// <summary>
        /// 产品是否已包tray
        /// </summary>
        /// <param name="sncode"></param>
        /// <returns></returns>
        public static async Task<int> GetSnExistsPackResult(string sncode)
        {
            int count;
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT COUNT(1) FROM TEST_SNCODE_TRAY2SN WHERE SNCODE = :SNCODE AND IS_VALID = 1 AND ROWNUM=1 ";
                count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { SNCODE = sncode }).ConfigureAwait(false);
            }
            return count;
        }
        /// <summary>
        /// 新库测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestModData>> GetModData57(string moduleid, string partSpec, int isValid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME FROM TEST_MOD_DATA WHERE MODULEID = :MODULEID AND IS_VALID=:ISVALID AND PART_SPEC=:PARTSPEC AND OPCODE NOT IN ('点污加测','十级AA','Resolution') ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("ISVALID", isValid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 所有新库测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestModData>> GetAllModData57(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT /*+index(A TESTMODDATA_MODULEID) */ ID,RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME FROM TEST_MOD_DATA A WHERE MODULEID = :MODULEID AND PART_SPEC=:PARTSPEC AND OPCODE NOT IN ('点污加测','十级AA','Resolution') ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestModData>> GetModData57(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_MOD_DATA WHERE MODULEID = :MODULEID AND PART_SPEC=:PARTSPEC AND OPCODE NOT IN ('点污加测','十级AA','调焦','Resolution') ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 新库测试流程
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Router2Op>> GetTestRouterOpCode57(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT B.ROUTER_CODE,B.OP_SEQ, B.OP_CODE FROM(SELECT /*+index(C TEST_OP_INDEX) */ ROUTER_CODE FROM TEST_MOD_OPCODE@TO_QTSOFT C WHERE MODULEID = :MODULEID AND PART_SPEC = :PARTSPEC AND ROWNUM = 1) A INNER JOIN B_ROUTER2OP B ON A.ROUTER_CODE = SYS_OP_C2C (B.ROUTER_CODE) ORDER BY OP_SEQ DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<Router2Op>(executeSql, dynamicParams, commandTimeout: 2).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// FP测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestModData>> GetFPData21(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND IS_VALID=1 AND PART_SPEC=:PARTSPEC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        public static async Task<IEnumerable<TestModData>> GetAllFPData21(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND PART_SPEC=:PARTSPEC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取FP测试不良数据
        /// </summary>
        /// <param name="moduleid"></param>        /// 
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestModData>> GetFPData21(string rcard, string moduleid, string partSpec, string opCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT RCARD,OPCODE,RESULT,NGTYPE FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND RCARD = :RCARD AND OPCODE=:OPCODE AND NGTYPE IN('101','102','103','104','105','106','241','711','750','4') AND PART_SPEC=:PARTSPEC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("RCARD", rcard);
                dynamicParams.Add("OPCODE", opCode);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<TestModData>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// FP测试流程
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Router2Op>> GetTestRouterOpCode21(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT /*+ NO_USE_HASH(@SEL$1 B) */ B.ROUTER_CODE,B.OP_SEQ, B.OP_CODE FROM(SELECT ROUTER_CODE FROM TEST_OPCODE_FP WHERE MODULEID = :MODULEID AND PART_SPEC = :PARTSPEC AND OPCODE NOT LIKE 'IPQC%' AND ROWNUM = 1) A INNER JOIN B_ROUTER2OP B ON A.ROUTER_CODE = B.ROUTER_CODE";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryAsync<Router2Op>(executeSql, dynamicParams, commandTimeout: 3).ConfigureAwait(false);
            }
        }
        //public async Task<(int, int, int)> getSpecCheck21(string snCode)
        //{
        //    int count = 0;
        //    int count1 = 0;
        //    int count2 = 0;
        //    using (IDbConnection conn = DapperFactory.Crate21OracleConnection())
        //    {
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            string executeSql = @"BEGIN OPEN :1 FOR SELECT COUNT(1) FROM B_FIRST2WB WHERE qrcode=:snCode;
        //                                    OPEN :2 FOR SELECT COUNT(1) FROM WO WHERE wo_code=:snCode;
        //                                   OPEN :3 FOR  SELECT COUNT(1) FROM T_QRCODESCANNING WHERE QRCODE_NAME=:snCode;END;";
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = executeSql;
        //            OracleParameter p1 = new OracleParameter("refcursor1", OracleDbType.RefCursor);
        //            p1.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(p1);
        //            OracleParameter p2 = new OracleParameter("refcursor2", OracleDbType.RefCursor);
        //            p2.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(p2);
        //            OracleParameter p3 = new OracleParameter("refcursor3", OracleDbType.RefCursor);
        //            p3.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(p3);
        //            OracleParameter p4 = new OracleParameter("snCode", OracleDbType.Varchar2);
        //            cmd.Parameters.Add(p4);
        //            cmd.ExecuteNonQuery();
        //            OracleDataReader dr1 = ((OracleRefCursor)p1.Value).GetDataReader();
        //            OracleDataReader dr2 = ((OracleRefCursor)p2.Value).GetDataReader();
        //            OracleDataReader dr3 = ((OracleRefCursor)p3.Value).GetDataReader();
        //            while (dr1.Read() && dr2.Read() && dr3.Read())
        //            {
        //                count = dr1.GetInt32(0);
        //                count1 = dr2.GetInt32(0);
        //                count2 = dr3.GetInt32(0);
        //                break;
        //            }
        //            await dr1.CloseAsync();
        //            await dr2.CloseAsync();
        //            await dr3.CloseAsync();
        //        }
        //    }
        //    return (count, count1, count2);
        //}
        /// <summary>
        /// 特殊管控产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<WO> GetSpecCheck21(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT WO_CODE,AA,BB FROM WO WHERE (WO_CODE=:SNCODE OR WO_CODE=:MODULEID) AND ROWNUM=1";
                return await conn.QueryFirstOrDefaultAsync<WO>(executeSql, new { SNCODE= snCode, MODULEID= moduleid }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 特殊管控产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetSpecCheckQrGenenation21(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM T_QRCODE_GENERATION WHERE  TYPE='C9MA02'  AND  QR_CODE=:snCode AND  SUBSTR(FPC,0,1)='U'";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { snCode }).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// C0MA12-S,C0MA12-S-A 周期拦截
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetSpecCheckQrGenenation21C0MA12(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM T_QRCODE_GENERATION WHERE  TYPE in ('C0MA12-S','C0MA12-S-A')  AND  QR_CODE=:snCode AND  SUBSTR(FPC,0,1)='H'";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { snCode }).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// F13855DJ特殊管控产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetSpecCheckQrGenenation21F13855DJ(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM T_QRCODE_GENERATION WHERE TYPE='F13855DJ' AND  QR_CODE=:snCode AND FPC IN ('F02404','F02405')";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// C9QA01特殊管控产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<TestMsr> GetSpecCheckTestMsr21C9QA01(string snCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT SNCODE,MDATE FROM TEST_MSR WHERE snCode=:snCode AND ROWNUM=1";
                return await conn.QueryFirstOrDefaultAsync<TestMsr>(executeSql, new { snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// C0MS02特殊管控产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetSpecCheckYT(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT B.SUBITEMTESTVALUE FROM TEST_MOD_DATA A LEFT JOIN TEST_MOD_DATA_DETAIL B ON A.ID=B.ID AND B.TESTSUBITEM IN ('PitchMinAngle','PitchMaxAngle','YawMaxAngle','YawMinAngle')
                                        WHERE MODULEID=:moduleid
                                        AND IS_VALID=1
                                        AND PART_SPEC='C0MS02'
                                        AND OPCODE='T01'";
                return await conn.QueryAsync<string>(executeSql, new { moduleid }).ConfigureAwait(false);
            }
        }
        public static async Task<int> GetMod904( long Id)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT COUNT(1) FROM  TEST_MOD_DATA_DETAIL  WHERE ID=:Id  AND UPPER(TESTITEM) LIKE '904%'";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { Id }, commandTimeout: 3).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// COB首件产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetFirstCheck21(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM B_FIRST2WB WHERE QRCODE=:SNCODE AND ROWNUM=1";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { SNCODE= snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// WB补线
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetGoldCheck21(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM T_QRCODESCANNING WHERE QRCODE_NAME=:SNCODE AND ROWNUM=1";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { SNCODE= snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 金线AOI疑虑品拦截
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetAoiCheck21(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select  COUNT (1)  from  ( select  max(mdate),SNCODE from  T_SNERRORCODE_AUTO  WHERE SNCODE=:SNCODE  and EC_CODE=' 疑似品' group by SNCODE)";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { SNCODE = snCode }).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// 新库测试明细
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestFpDataDetail>> GetTestModDataDetail(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select b.TESTITEM,b.TESTSUBITEM,b.SUBITEMTESTVALUE from test_mod_data a inner join   test_mod_data_detail b on a.id=b.id
                                            where a.moduleid=:moduleid
                                            and a.is_valid=1
                                            and a.opcode in ('T01','T05')
                                            and a.result='OK'
                                            and a.part_spec=:partSpec and b.TESTITEM in ('201远焦0.125SFR1','201远焦0.125SFR2')";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryAsync<TestFpDataDetail>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 新库测试明细
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestFpDataDetail>> C0MA11GetTestModDataDetail(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select a.opcode as TESTITEM,b.TESTSUBITEM,b.SUBITEMTESTVALUE from test_mod_data a inner join   test_mod_data_detail b on a.id=b.id
where a.part_spec=:partSpec
and a.opcode in ('T08','T05','T06')
and moduleid=:moduleid
and is_valid=1
and b.TESTSUBITEM in ( 'OIS_X',
'OIS_Y',
'0.850视场_左上H',
'0.850视场_左上V',
'0.850视场_右上H',
'0.850视场_右上V',
'0.850视场_左下H',
'0.850视场_左下V',
'0.850视场_右下H',
'0.850视场_右下V')
";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryAsync<TestFpDataDetail>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 新库测试明细
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> S0LS02GetTestModDataDetail(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select count(1) from test_mod_data a inner join test_mod_data_detail b on a.id=b.id
where a.moduleid=:moduleid
and part_spec=:partSpec
and opcode='T04'
and b.TESTITEM='342AutoOIS27304'";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取老库测试流程
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Router2Op>> GtTestRouterOpCode21Old(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT B.ROUTER_CODE, B.OP_CODE FROM T_TEST_OPCODE A INNER JOIN V_ROUTER2OP B ON A.ROUTER_CODE = B.ROUTER_CODE WHERE A.MODULEID = :MODULEID ORDER BY OP_SEQ DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<Router2Op>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取老库AF&AWB测试数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestAF>> GetTestDataAFAWB21(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD, TO_CHAR(OPCODE) AS OPCODE, TO_CHAR (JUDGE) AS JUDGE, TO_CHAR (RESULT) AS RESULT, IS_VALID AS ISVALID, TESTTIME FROM TEST_AF WHERE MODULEID = :MODULEID AND IS_VALID = 1 UNION  SELECT RCARD, TO_CHAR (OPCODE), TO_CHAR (JUDGE), TO_CHAR (RESULT), IS_VALID, TESTTIME FROM TEST_AWB WHERE MODULEID = :MODULEID AND IS_VALID = 1 UNION  SELECT RCARD, PDTYPE AS OPCODE, TOTAL_RESULT AS JUDGE, '' AS RESULT, IS_VALID, TESTTIME FROM TEST_PDAF WHERE MODULEID = :MODULEID AND IS_VALID = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<TestAF>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取Y/CSHADING数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestAF>> GetTestDataCYShading(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD, 'YSHADING' AS OPCODE, RESULT AS JUDGE, IS_VALID, TESTTIME FROM TEST_YSHADING WHERE MODULEID = :MODULEID AND IS_VALID = 1 UNION  SELECT RCARD, 'COLORSHADING', RESULT AS JUDGE, IS_VALID, TESTTIME FROM TEST_COLORSHADING WHERE MODULEID = :MODULEID AND IS_VALID = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<TestAF>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取AFLine数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestAF>> GetTestDataAFLine21(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT RCARD, OPCODE, JUDGE, IS_VALID, TESTTIME FROM TEST_AFLINEAR WHERE MODULEID = :MODULEID AND IS_VALID = 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<TestAF>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 包装扫描判断机种单双摄
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <returns></returns>
        public static async Task<int> GetAaBomByCarton(string CartonNo)
        {
            int exists;
            string cacheKeys = $"T_CARTON{CartonNo}";
            if (!_memoryCache.TryGetValue(cacheKeys, out exists))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @" SELECT COUNT(1) FROM ( SELECT ID, COUNT (1) FROM AA_BOM A INNER JOIN T_CARTON B ON A.ID = B.ITEM_CODE WHERE A.IS_DO = 1 AND B.CARTON_NO = :CARTONNO AND UPN LIKE 'ECM%' GROUP BY ID HAVING COUNT (1) >= 3) AA";
                    exists = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { CARTONNO=CartonNo }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, exists, TimeSpan.FromMinutes(2));
                }
            }
            return exists;
        }
        /// <summary>
        /// 获取分基板副摄
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetSecondDualModuleid(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT DISTINCT SECONDMODULEID FROM TEST_DUALMODULE WHERE PRIMARYMODULEID = :MODULEID AND IS_VALID = 1 AND SECONDMODULEID IS NOT NULL ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<string>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 查找分基板副摄机种
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetSecondDualPartSpec(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT DISTINCT PART_SPEC FROM TEST_MOD_DATA WHERE MODULEID = :MODULEID AND IS_VALID = 1 ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<string>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取分基板流程
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Router2Op>> GetDualModuleidRouter(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT B.ROUTER_CODE,B.OP_CODE FROM T_TEST_DUAL_OPCODE A INNER JOIN V_ROUTER2OP B ON A.ROUTER_CODE=B.ROUTER_CODE WHERE A.MODULEID=:MODULEID ORDER BY OP_SEQ DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<Router2Op>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取分基板双摄数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestAF>> GetTestDualData(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"  SELECT RCARD, OPCODE, JUDGE, RESULT, IS_VALID AS ISVALID, TESTTIME FROM TEST_AF WHERE MODULEID = :MODULEID AND IS_VALID = 1 UNION  SELECT RCARD, OPCODE, JUDGE, RESULT, IS_VALID, TESTTIME FROM TEST_AWB WHERE MODULEID = :MODULEID AND IS_VALID = 1 UNION  SELECT '' AS RCARD, PDTYPE AS OPCODE, TOTAL_RESULT AS JUDGE, '' AS RESULT, IS_VALID, TESTTIME FROM TEST_PDAF WHERE MODULEID = :MODULEID AND IS_VALID = 1 UNION  SELECT RCARD, OPCODE, TESTRESULT AS JUDGE, '' AS RESULT, IS_VALID, TESTTIME FROM TEST_DUALMODULE WHERE PRIMARYMODULEID = :MODULEID AND IS_VALID = 1 ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryAsync<TestAF>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        public static async Task<string> SaveSnCheckResult(string moduleid, string CartonNo, string msg)
        {
            string result = "OK";
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string tray2snSql = @"UPDATE TEST_SNCODE_CHECKRESULT SET IS_VALID=0 WHERE SNCODE=:VMODULEID AND IS_VALID=1";
                var tray2snSqlParams = new DynamicParameters();
                tray2snSqlParams.Add("VMODULEID", moduleid);

                string checkResultSql = @"INSERT INTO TEST_SNCODE_CHECKRESULT(ID,SNCODE,STAGE,OPCODE,TESTTIME,TESTRESULT,CARTON_NO,IS_VALID) VALUES (SNCODE_CHECKRESULT_ID_SEQUENCE.NEXTVAL,:VMODULEID,'包装','二维码扫描',SYSDATE,:VMSG,:VCARTONNO,1)";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("VMODULEID", moduleid);
                checkResultParams.Add("VMSG", msg);
                checkResultParams.Add("VCARTONNO", CartonNo);
                try
                {
                    await conn.ExecuteAsync(tray2snSql, tray2snSqlParams, commandTimeout: 2).ConfigureAwait(false);
                    await conn.ExecuteAsync(checkResultSql, checkResultParams, commandTimeout: 2).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        public static async Task<string> SaveSnCheckResultNoIsValue(string moduleid, string CartonNo, string msg)
        {
            string result = "OK";
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string checkResultSql = @"INSERT INTO TEST_SNCODE_CHECKRESULT(ID,SNCODE,STAGE,OPCODE,TESTTIME,TESTRESULT,CARTON_NO) VALUES (SNCODE_CHECKRESULT_ID_SEQUENCE.NEXTVAL,:VMODULEID,'包装','二维码扫描',SYSDATE,:VMSG,:VCARTONNO)";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("VMODULEID", moduleid);
                checkResultParams.Add("VMSG", msg);
                checkResultParams.Add("VCARTONNO", CartonNo);
                await conn.ExecuteAsync(checkResultSql, checkResultParams, commandTimeout: 2).ConfigureAwait(false);
            }
            return result;
        }
        /// <summary>
        /// 新库数据失效
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="snCode"></param>
        /// <param name="partSpec"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public static async Task<string> InvalidTestResultForMOD(string moduleid, string snCode, string partSpec, string stationId, string isInvlidSnLink)
        {
            string result = "OK";
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string tray2snSql = @"UPDATE TEST_SNCODE_TRAY2SN SET IS_VALID = -1 WHERE SNCODE = :snCode AND IS_VALID = 1";
                var tray2snSqlParams = new DynamicParameters();
                tray2snSqlParams.Add("snCode", snCode);

                string checkResultSql = @"UPDATE TEST_SNCODE_CHECKRESULT SET IS_VALID = -1 WHERE SNCODE = :snCode AND IS_VALID = 1";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("snCode", snCode);

                string modDataSql = @"UPDATE TEST_MOD_DATA@TO_QTSOFT SET IS_VALID = -1 WHERE MODULEID = :moduleid AND PART_SPEC = :partSpec AND IS_VALID = 1";
                var modDataParams = new DynamicParameters();
                modDataParams.Add("moduleid", moduleid);
                modDataParams.Add("partSpec", partSpec);

                string modDataNgSql = @"UPDATE TEST_MOD_DATA@TO_QTSOFT SET IS_VALID = -2 WHERE MODULEID = :moduleid AND PART_SPEC = :partSpec AND IS_VALID = 0";
                var modDataNgParams = new DynamicParameters();
                modDataNgParams.Add("moduleid", moduleid);
                modDataNgParams.Add("partSpec", partSpec);

                string delMsrSql = @"DELETE TEST_MSR WHERE MODULEID = :moduleid ";
                var delMsrParams = new DynamicParameters();
                delMsrParams.Add("moduleid", moduleid);

                string modOpCodeSql = @"DELETE TEST_MOD_OPCODE@TO_QTSOFT WHERE MODULEID = :moduleid AND PART_SPEC = :partSpec";
                var modOpCodeParams = new DynamicParameters();
                modOpCodeParams.Add("moduleid", moduleid);
                modOpCodeParams.Add("partSpec", partSpec);

                string logSql = @"insert into TEST_SNCODE_InvalidResult(ID, SNCODE, MODULEID, INVALID_RESULT, TESTTIME,STATION_ID) values(SEQ_INVALIDRESULT_ID.NEXTVAL,:snCode,:moduleid,'OK',sysdate,:stationId)";
                var logParams = new DynamicParameters();
                logParams.Add("snCode", snCode);
                logParams.Add("moduleid", moduleid);
                logParams.Add("stationId", stationId);
                var transaction = conn.BeginTransaction();
                try
                {
                    if ("1".Equals(isInvlidSnLink, StringComparison.OrdinalIgnoreCase))
                        await conn.ExecuteAsync(delMsrSql, delMsrParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(tray2snSql, tray2snSqlParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(checkResultSql, checkResultParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modDataSql, modDataParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modDataNgSql, modDataNgParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modOpCodeSql, modOpCodeParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(logSql, logParams, transaction).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// FP数据失效
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="snCode"></param>
        /// <param name="partSpec"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public static async Task<string> InvalidTestResultForFP(string moduleid, string snCode, string partSpec, string stationId,string isInvlidSnLink)
        {
            string result = "OK";
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string tray2snSql = @"UPDATE TEST_SNCODE_TRAY2SN SET IS_VALID = -1 WHERE SNCODE = :snCode AND IS_VALID = 1";
                var tray2snSqlParams = new DynamicParameters();
                tray2snSqlParams.Add("snCode", snCode);

                string checkResultSql = @"UPDATE TEST_SNCODE_CHECKRESULT SET IS_VALID = -1 WHERE SNCODE = :snCode AND IS_VALID = 1";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("snCode", snCode);

                string modDataSql = @"UPDATE TEST_FP_DATA SET IS_VALID = -1 WHERE MODULEID = :moduleid AND PART_SPEC = :partSpec AND IS_VALID = 1";
                var modDataParams = new DynamicParameters();
                modDataParams.Add("moduleid", moduleid);
                modDataParams.Add("partSpec", partSpec);

                string modDataNgSql = @"UPDATE TEST_FP_DATA SET IS_VALID = -2 WHERE MODULEID = :moduleid AND PART_SPEC = :partSpec AND IS_VALID = 0";
                var modDataNgParams = new DynamicParameters();
                modDataNgParams.Add("moduleid", moduleid);
                modDataNgParams.Add("partSpec", partSpec);

                string modOpCodeSql = @"DELETE TEST_OPCODE_FP WHERE MODULEID = :moduleid AND PART_SPEC = :partSpec";
                var modOpCodeParams = new DynamicParameters();
                modOpCodeParams.Add("moduleid", moduleid);
                modOpCodeParams.Add("partSpec", partSpec);

                string delMsrSql = @"DELETE TEST_MSR WHERE MODULEID = :moduleid ";
                var delMsrParams = new DynamicParameters();
                delMsrParams.Add("moduleid", moduleid);

                string logSql = @"insert into TEST_SNCODE_InvalidResult(ID, SNCODE, MODULEID, INVALID_RESULT, TESTTIME,STATION_ID) values(SEQ_INVALIDRESULT_ID.NEXTVAL,:snCode,:moduleid,'OK',sysdate,:stationId)";
                var logParams = new DynamicParameters();
                logParams.Add("snCode", snCode);
                logParams.Add("moduleid", moduleid);
                logParams.Add("stationId", stationId);
                var transaction = conn.BeginTransaction();
                try
                {
                    if ("1".Equals(isInvlidSnLink, StringComparison.OrdinalIgnoreCase))
                        await conn.ExecuteAsync(delMsrSql, delMsrParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(tray2snSql, tray2snSqlParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(checkResultSql, checkResultParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modDataSql, modDataParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modDataNgSql, modDataNgParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modOpCodeSql, modOpCodeParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(logSql, logParams, transaction).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 老库数据失效
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="snCode"></param>
        /// <param name="partSpec"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public static async Task<string> InvalidTestResultForOld(string moduleid, string snCode, string partSpec, string stationId)
        {
            string result = "OK";
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string tray2snSql = @"UPDATE TEST_SNCODE_TRAY2SN SET IS_VALID = -1 WHERE SNCODE = :snCode AND IS_VALID = 1";
                var tray2snSqlParams = new DynamicParameters();
                tray2snSqlParams.Add("snCode", snCode);

                string checkResultSql = @"UPDATE TEST_SNCODE_CHECKRESULT SET IS_VALID = -1 WHERE SNCODE = :snCode AND IS_VALID = 1";
                var checkResultParams = new DynamicParameters();
                checkResultParams.Add("snCode", snCode);

                string afDataSql = @"UPDATE TEST_AF SET IS_VALID = -1 WHERE MODULEID = :moduleid AND IS_VALID = 1";
                var afDataParams = new DynamicParameters();
                afDataParams.Add("moduleid", moduleid);

                string awbDataSql = @"UPDATE TEST_AWB SET IS_VALID = -1 WHERE MODULEID = :moduleid AND IS_VALID = 1";
                var awbDataParams = new DynamicParameters();
                awbDataParams.Add("moduleid", moduleid);

                string pdafDataSql = @"UPDATE TEST_PDAF SET IS_VALID = -1 WHERE MODULEID = :moduleid AND IS_VALID = 1";
                var pdafDataParams = new DynamicParameters();
                pdafDataParams.Add("moduleid", moduleid);

                string modOpCodeSql = @"DELETE T_TEST_OPCODE WHERE MODULEID = :moduleid ";
                var modOpCodeParams = new DynamicParameters();
                modOpCodeParams.Add("moduleid", moduleid);

                string logSql = @"insert into TEST_SNCODE_InvalidResult(ID, SNCODE, MODULEID, INVALID_RESULT, TESTTIME,STATION_ID) values(SEQ_INVALIDRESULT_ID.NEXTVAL,:snCode,:moduleid,'OK',sysdate,:stationId)";
                var logParams = new DynamicParameters();
                logParams.Add("snCode", snCode);
                logParams.Add("moduleid", moduleid);
                logParams.Add("stationId", stationId);
                var transaction = conn.BeginTransaction();
                try
                {
                    await conn.ExecuteAsync(tray2snSql, tray2snSqlParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(checkResultSql, checkResultParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(afDataSql, afDataParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(awbDataSql, awbDataParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(pdafDataSql, pdafDataParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(modOpCodeSql, modOpCodeParams, transaction).ConfigureAwait(false);
                    await conn.ExecuteAsync(logSql, logParams, transaction).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// MES收集日志
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="MAC"></param>
        /// <param name="HostName"></param>
        /// <param name="Rcard"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static async Task<int> WriteMesLog(string IP, string MAC, string HostName, string Rcard, string Message)
        {
            using (OracleConnection con = await DapperFactory.CrateOracle32Connection().ConfigureAwait(false))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = @"INSERT INTO LOG( MDATE, THREAD, USERNAME, HOSTNAME, SQLLEVEL, MESSAGE) VALUES (sysdate, :IP,:MAC, :HostName, :Rcard, :Message)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.CommandTimeout = 2;
                    OracleParameter param1 = new OracleParameter(":IP", IP.Trim());
                    cmd.Parameters.Add(param1);
                    OracleParameter param2 = new OracleParameter(":MAC", MAC.Trim());
                    cmd.Parameters.Add(param2);
                    OracleParameter param3 = new OracleParameter(":HostName", HostName.Trim());
                    cmd.Parameters.Add(param3);
                    OracleParameter param4 = new OracleParameter(":Rcard", Rcard.Trim());
                    cmd.Parameters.Add(param4);
                    OracleParameter param5 = new OracleParameter(":Message", Message.Trim());
                    cmd.Parameters.Add(param5);
                    int rows = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows;
                }
            }
        }
        public static async Task<int> InsertOtpLogAsync(string moduleid, string message, long sendtime, string sncode)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string sql = "UPDATE R_OTPCHECK_LOG SET IS_VALID=0 WHERE MODULEID=:moduleid AND IS_VALID=1";
                var sqlParams = new DynamicParameters();
                sqlParams.Add("moduleid", moduleid);
                await conn.ExecuteAsync(sql, sqlParams).ConfigureAwait(false);
                string sqlInsert = "INSERT INTO R_OTPCHECK_LOG(MODULEID, MDATE, MESSAGE, IS_VALID, OTP_TIME, SNCODE) VALUES(:moduleid, SYSDATE, :message, '1',:sendtime,:sncode)";
                var sqlInsertParams = new DynamicParameters();
                sqlInsertParams.Add("moduleid", moduleid);
                sqlInsertParams.Add("message", message);
                sqlInsertParams.Add("sendtime", sendtime);
                sqlInsertParams.Add("sncode", sncode);
                return await conn.ExecuteAsync(sqlInsert, sqlInsertParams).ConfigureAwait(false);
            }
        }
        public static async Task<int> InsertSnLink(string snCode,string linkSnCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string sqlInsert = "insert into  test_msr_link(SNCODE, SNCODELINK, MDATE) values(:snCode,:linkSnCode, sysdate)";
                var sqlInsertParams = new DynamicParameters();
                sqlInsertParams.Add("snCode", snCode);
                sqlInsertParams.Add("linkSnCode", linkSnCode);
                return await conn.ExecuteAsync(sqlInsert, sqlInsertParams).ConfigureAwait(false);
            }
        }
       /// <summary>
       /// 流程卡找机型，客户
       /// </summary>
       /// <param name="流程卡"></param>
       /// <returns></returns>
        public static async Task<PartSpecClientModel> GetRcardPartSpec(string Rcard)
        {
            PartSpecClientModel partSpec;
            string cacheKeys = $"GetPartSpecByRcard{Rcard}";
            if (!_memoryCache.TryGetValue(cacheKeys, out partSpec))
            {
                using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
                {
                    string executeSql = @"SELECT B.CUSTOMER, B.PART_SPEC AS PARTSPEC,B.MATERIAL_CODE AS MATERIALCODE,TO_CHAR(A.MDATE,'yyMMdd') AS PACKDATE 
                                         FROM T_WIP   A
                                        INNER JOIN I_CLIENT_MODEL B ON A.ITEM_CODE = B.ITEM_CODE 
                                        WHERE A.RCARD =:Rcard AND ROWNUM = 1";
                    partSpec = await conn.QueryFirstOrDefaultAsync<PartSpecClientModel>(executeSql, new { RCARD = Rcard }).ConfigureAwait(false);
                    _memoryCache.Set(cacheKeys, partSpec, TimeSpan.FromMinutes(3));
                }
            }
            return partSpec;
        }
        /// <summary>
        /// 获取分基板副摄
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<double> GetF3L6MCDData(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT  nvl(MIN(SUBITEMTESTVALUE),0)  FROM TEST_MOD_DATA  A INNER JOIN TEST_MOD_DATA_DETAIL B ON A.ID=B.ID
                                        WHERE PART_SPEC='F3L6MCD-A'
                                        AND MODULEID=:MODULEID
                                        AND IS_VALID=1
                                        AND OPCODE='T01'
                                        AND B.TESTITEM = '201远焦SFR'
                                        AND B.TESTSUBITEM LIKE 'CAM 0.7F%' ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryFirstOrDefaultAsync<double>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// F02A10BT-1 OB挑选
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<string> GetF02A10BTData(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT  SUBITEMTESTVALUE  FROM TEST_MOD_DATA  A INNER JOIN TEST_MOD_DATA_DETAIL B ON A.ID=B.ID
                                        WHERE PART_SPEC='F02A10BT-1'
                                        AND MODULEID=:MODULEID
                                        AND IS_VALID=1
                                        AND OPCODE='T01'
                                        AND B.TESTITEM = '421OB'
                                        AND B.UPPER ='12' ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryFirstOrDefaultAsync<string>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// FP最后工序测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<TestModData> GetFPFinalData21(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE FROM TEST_FP_DATA WHERE MODULEID = :MODULEID AND IS_VALID=1 AND PART_SPEC=:PARTSPEC ORDER BY TESTTIME DESC";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<TestModData>(executeSql, dynamicParams,commandTimeout:3).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 非FP最后工序测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
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
        /// C92F06 A,B挑选
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetC92F06Data(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   nvl(sum(CASE
                                    WHEN ( (b.TESTSUBITEM = 'GrGb差异值'
                                            AND TO_NUMBER (b.SUBITEMTESTVALUE) < 16)
                                          OR (b.TESTSUBITEM = 'G通道检查'
                                              AND TO_NUMBER (b.SUBITEMTESTVALUE) < 20))
                                    THEN
                                       1
                                    ELSE
                                       0
                                 END),0) as num
                          FROM      TEST_MOD_DATA A
                                 INNER JOIN
                                    TEST_MOD_DATA_DETAIL B
                                 ON A.ID = B.ID
                         WHERE       PART_SPEC = 'C92F06-A'
                                 AND MODULEID = :MODULEID
                                 AND IS_VALID = 1
                                 AND OPCODE = 'FOCUS'
                                 AND B.TESTSUBITEM IN ('GrGb差异值', 'G通道检查') ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// C0QS01
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<TestModData> C0QS01GetTestModData(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select RCARD,PART_SPEC AS PARTSPEC,OPCODE,RESULT,IS_VALID AS ISVALID,TESTTIME,NGTYPE from test_mod_data  
                                        where part_spec=:partSpec
                                        and moduleid=:moduleid
                                        and result='NG'
                                        order by TESTTIME desc";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryFirstOrDefaultAsync<TestModData>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// S0LS02 A,B挑选
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetC0QS03Data(string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   COUNT (1)
           FROM   TEST_HOLD A
          WHERE   SN = SUBSTR (:MODULEID, 13, 4)";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 备份库测试主数据
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetModData40(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.CrateOracle40Connection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT   NVL (SUM (num), 0)
                                      FROM   (SELECT   COUNT (1) AS num
                                                FROM   TEST_MOD_DATA A
                                               WHERE       MODULEID = :MODULEID
                                                       AND PART_SPEC = :PARTSPEC
                                                       AND OPCODE = '修复点污测试1'
                                              UNION ALL
                                              SELECT   COUNT (1)
                                                FROM   TEST_MOD_DATA_2019 A
                                               WHERE       MODULEID = :MODULEID
                                                       AND PART_SPEC = :PARTSPEC
                                                       AND OPCODE = '修复点污测试1') ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams, commandTimeout: 30).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// C0DA07漏测试项检测
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetC0DA07Data(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   NVL (COUNT (1), 0)
                                        FROM   TEST_MOD_DATA_DETAIL B
                                        WHERE   TESTITEM IN ('221FarCodeCheck', '227NearCodeCheck')
                                                AND ID IN
                                                        (SELECT   ID
                                                            FROM   TEST_MOD_DATA A
                                                        WHERE       PART_SPEC = :PARTSPEC
                                                                AND OPCODE = 'T03'
                                                                AND IS_VALID = 1
                                                                AND MODULEID = :MODULEID) ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams, commandTimeout: 30).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 新库测试明细
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TestFpDataDetail>> GetTestModDataDetail(string moduleid, string partSpec,string opCode)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"select b.TESTITEM,b.TESTSUBITEM,b.SUBITEMTESTVALUE from test_mod_data a inner join   test_mod_data_detail b on a.id=b.id
                                            where a.moduleid=:moduleid
                                            and a.is_valid=1
                                            and a.opcode =:opcode
                                            and a.result='OK'
                                            and a.part_spec=:partSpec and b.TESTITEM in ('201远焦SFR1','201远焦SFR2')";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("moduleid", moduleid);
                dynamicParams.Add("opcode", opCode);
                dynamicParams.Add("partSpec", partSpec);
                return await conn.QueryAsync<TestFpDataDetail>(executeSql, dynamicParams).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// F4H7YAZ挑选风险品
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetF4H7YAZData(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT  count(1)  FROM TEST_MOD_DATA  A INNER JOIN TEST_MOD_DATA_DETAIL B ON A.ID=B.ID
                                        WHERE PART_SPEC=:PARTSPEC
                                        AND MODULEID=:MODULEID
                                        AND IS_VALID=1
                                        AND OPCODE='T01'
                                        AND B.TESTITEM = '201远焦MTF'
                                        AND B.TESTSUBITEM in ('A0_x','A0_y','A1_x','A1_y','A2_x','A2_y','A3_x','A3_y')
                                        and b.SUBITEMTESTVALUE<30 ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams, commandTimeout: 30).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 获取二维码绑定流程卡
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public static async Task<string> GetRcardBySn(string sn)
        {
            string moduleid;
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @" SELECT RCARD FROM TEST_MSR WHERE SNCODE = :SN ";
                moduleid = await conn.QueryFirstOrDefaultAsync<string>(executeSql, new { SN = sn }).ConfigureAwait(false);
            }
            return moduleid;
        }
        /// <summary>
        /// C0QS01挑选B规
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetC0QS01Data(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT  count(1)  FROM TEST_MOD_DATA  A INNER JOIN TEST_MOD_DATA_DETAIL B ON A.ID=B.ID
                                        WHERE PART_SPEC=:PARTSPEC
                                        AND MODULEID=:MODULEID
                                        AND IS_VALID=1
                                        AND OPCODE in ('T05','T04')
                                        AND B.TESTSUBITEM in ('DB_X','DB_Y')
                                        and b.SUBITEMTESTVALUE<20 ";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams, commandTimeout: 30).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// S0LS07
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetS0LS07Data(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT  count(1)  FROM TEST_MOD_DATA 
                                        WHERE PART_SPEC=:PARTSPEC
                                        AND MODULEID=:MODULEID
                                        AND OPCODE ='T08' AND NGTYPE='201'";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams, commandTimeout: 30).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// C0HF34
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public static async Task<int> GetC0HF34Data(string moduleid, string partSpec)
        {
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT   COUNT (1)
                                      FROM   TEST_MOD_DATA
                                     WHERE       PART_SPEC = :PARTSPEC
                                             AND MODULEID = :MODULEID
                                             AND OPCODE = 'T02'
                                             AND TESTTIME >= TO_DATE ('20210316080000', 'yyyyMMddhh24miss')";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("MODULEID", moduleid);
                dynamicParams.Add("PARTSPEC", partSpec);
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, dynamicParams, commandTimeout: 30).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 测试特殊管控产品
        /// </summary>
        /// <param name="snCode"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetSnHold(string snCode, string moduleid)
        {
            using (IDbConnection conn = await DapperFactory.Crate21OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM TEST_HOLD WHERE  SN=:snCode";
                return await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { snCode }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// AA不良拦截
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public static async Task<int> GetAAResult(string moduleid)
        {
            int count;
            using (IDbConnection conn = await DapperFactory.Crate57OracleConnection().ConfigureAwait(false))
            {
                string executeSql = @"SELECT COUNT(1) FROM T_AA_RESULT WHERE  MODULEID=:moduleid";
                count = await conn.QueryFirstOrDefaultAsync<int>(executeSql, new { moduleid }).ConfigureAwait(false);
            }
            return count;          
        }

    }
}