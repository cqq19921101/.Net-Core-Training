using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QtechCoreAPI.Business;

namespace QtechCoreAPI
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OVController : ControllerBase
    {
        private readonly ILogger<OVController> _logger;
        private static string[] s1 = { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static int threadcount = 0;

        public OVController(ILogger<OVController> logger)
        {
            _logger = logger;
        }
        public async Task<string> GetSysTime()
        {
            return await OVCheckBussiness.GetSysDate().ConfigureAwait(false);
        }
        [HttpGet]
        public int GetOnLineCount()
        {
            return threadcount;
        }
        /// <summary>
        /// OV防跳站
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> OVCheckStation(CheckStationParam param)
        {
            string Result;
            Interlocked.Increment(ref threadcount);
            Result = await OVCheckReturn(param).ConfigureAwait(false);
            Interlocked.Decrement(ref threadcount);
            return Result;
        }

        /// <summary>
        /// 测试数据保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> OVSaveData(cameraQCVoList param)
        {
            string result;
            if (param == null || string.IsNullOrEmpty(param.lotNumber) || string.IsNullOrEmpty(param.moduleid) || string.IsNullOrEmpty(param.opcode))
            {
                return "505NG:流程卡/模组号/工序空";
            }
            if (param.ReyCount > 0 && !string.IsNullOrEmpty(param.GuId))
            {
                string temp;
                try
                {
                    temp = await OVCheckBussiness.GetFpDealResult(param.GuId).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.Source);
                    return result;
                }
                if (!string.IsNullOrEmpty(temp))
                {
                    return temp;
                }
            }
            //检查机种 MD5是否匹配对应的机台 过滤正式软件
            //true:正式  false:测试
            //if ("C08F17".Equals(param.partspec, StringComparison.Ordinal))
            //{
            //    if (!string.IsNullOrEmpty(param.mac) && !string.IsNullOrEmpty(param.programVersion) && param.programVersion.Length - 6 > 6)
            //    {
            //        bool softCheck;
            //        try
            //        {
            //            softCheck = await FPCheckBusiness.CheckFPMappingMAC(param.mac, param.programVersion.Substring(param.programVersion.Length - 6, 6)).ConfigureAwait(false);
            //        }
            //        catch (Exception ex)
            //        {
            //            result = "510NG:" + ex.Message;
            //            _logger?.LogError(result + ex.StackTrace);
            //            return result;
            //        }
            //        if (softCheck)
            //        {
            //            return "501NG:验证软件不能使用";
            //        }
            //    }
            //}
            if (param.moduleid.Equals("0000", StringComparison.Ordinal))
            {
                param.moduleid = $"0000{DateTime.Now.ToString("HHmmssfffff") + s1[(new Random()).Next(0, s1.Length)]}";
                try
                {
                    return await OVCheckBussiness.SaveTestOVDataNoLigth(param).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.StackTrace);
                    return result;
                }
            }
            if ("C9DA07-I".Equals(param.partspec, StringComparison.Ordinal))
            {
                param.partspec = "C9DA07";
            }
            if ("C9DA07".Equals(param.partspec, StringComparison.Ordinal) && "T03".Equals(param.opcode, StringComparison.Ordinal) && "OK".Equals(param.result, StringComparison.Ordinal))
            {
                try
                {
                    if (param.testItemList == null || param.testItemList.Count==0)
                        return "512NG:漏测试项";                   
                    string[] item = new string[] { "221FarCodeCheck", "101脏点测试", "227NearCodeCheck", "904DataCheck", "904远近焦差异管控", "904QCPDAFDataCheck", "444二维码检测" };
                    foreach (var str in item)
                    {
                        var items = param.testItemList.Where(x => str.Equals(x.testItemName, StringComparison.Ordinal));
                        if (items == null || !items.Any())
                        {
                            return $"512NG:漏{str.Substring(0, 3)}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.StackTrace);
                    return result;
                }
            }
            if (!string.IsNullOrEmpty(param.barcode))
            {
                try
                {
                    result = await OVCheckBussiness.SaveTestMsr(param.lotNumber, param.moduleid, param.barcode, param.partspec).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.Source);
                    return result;
                }
                if (!result.Equals("OK", StringComparison.Ordinal))
                    return result;
            }
            if (!string.IsNullOrEmpty(param.wo) && !param.wo.StartsWith("FA-", StringComparison.Ordinal))
            {
                try
                {
                    string partSpec = await OVCheckBussiness.GetPartSpec(param.wo).ConfigureAwait(false);
                    if (string.IsNullOrEmpty(partSpec))
                    {
                        return "505NG:工单机型空";
                    }
                    if (!partSpec.Equals(param.partspec, StringComparison.Ordinal))
                    {
                        return "505NG:混料";
                    }
                }
                catch (Exception ex)
                {
                    result = $"510NG:{ ex.Message}";
                    _logger?.LogError(ex.Message + ex.StackTrace);
                    return result;
                }
            }
            Router2Op routerOp;
            try
            {
                routerOp = await OVCheckBussiness.GetCurrentOpCode(param.moduleid, param.partspec).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"510NG:{ ex.Message}";
                _logger?.LogError(ex.Message + ex.StackTrace);
                return result;
            }
            if (routerOp == null || string.IsNullOrEmpty(routerOp.Router_Code) || string.IsNullOrEmpty(routerOp.Op_Code))
            {
                try
                {
                    routerOp = await OVCheckBussiness.GetRouter2OpByWo(param.wo).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = $"510NG:{ ex.Message}";
                    _logger?.LogError(ex.Message + ex.StackTrace);
                    return result;
                }
                if (routerOp == null || string.IsNullOrEmpty(routerOp.Router_Code) || string.IsNullOrEmpty(routerOp.Op_Code))
                {
                    return "501NG:流程未维护";
                }
            }
            else
            {
                if (!routerOp.Op_Code.Equals(param.opcode, StringComparison.Ordinal))
                {
                    routerOp.Op_Seq = routerOp.Op_Seq + 1;
                    routerOp.Op_Code = param.opcode;
                }
            }
            try
            {
                result = await OVCheckBussiness.SaveTestOVDataAndDetail(param, routerOp).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(param.GuId))
                {
                    if (result.Equals("OK", StringComparison.OrdinalIgnoreCase))
                    {
                        //await OVCheckBussiness.SaveFpDealResult(param.GuId, result).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                result = "510NG:" + ex.Message;
                _logger?.LogError(result + ex.StackTrace);
                return result;
            }
            return result;
        }
        /// <summary>
        /// AA测试数据保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> AASaveData(cameraQCVoList param)
        {
            string result;
            if (param == null|| string.IsNullOrEmpty(param.moduleid))
            {
                return "505NG:模组号空";
            }
            if (param.ReyCount > 0 && !string.IsNullOrEmpty(param.GuId))
            {
                string temp;
                try
                {
                    temp = await OVCheckBussiness.GetFpDealResult(param.GuId).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.Source);
                    return result;
                }
                if (!string.IsNullOrEmpty(temp))
                {
                    return temp;
                }
            }           
            if (param.moduleid.Equals("0000", StringComparison.Ordinal))
            {
                param.moduleid = $"0000{DateTime.Now.ToString("HHmmssfffff") + s1[(new Random()).Next(0, s1.Length)]}";
            }
            if (!string.IsNullOrEmpty(param.barcode))
            {
                try
                {
                    result = await OVCheckBussiness.SaveTestMsr(param.lotNumber, param.moduleid, param.barcode, param.partspec).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.Source);
                    return result;
                }
                if (!result.Equals("OK", StringComparison.Ordinal))
                    return result;
            }          
            try
            {
                result = await OVCheckBussiness.SaveTestAADataAndDetail(param).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(param.GuId))
                {
                    if (result.Equals("OK", StringComparison.OrdinalIgnoreCase))
                    {
                        //await OVCheckBussiness.SaveFpDealResult(param.GuId, result).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                result = "510NG:" + ex.Message;
                _logger?.LogError(result + ex.StackTrace);
                return result;
            }
            return result;
        }

        /// <summary>
        /// OVCheckStation
        /// </summary>
        /// <param name="param">Input</param>
        /// <returns></returns>
        public async Task<string> OVCheckReturn(CheckStationParam param)
        {
            string result;
            int count;
            #region 特殊条件 --- 流程卡空&&IPQC 直接return
            if (param == null || string.IsNullOrEmpty(param.Rcard) || string.IsNullOrEmpty(param.Moduleid) || string.IsNullOrEmpty(param.Station))
            {
                return "505NG:流程卡空";
            }
            if (param.Rcard.Equals("FA-000000000000000", StringComparison.Ordinal)
                || param.Station.StartsWith("IPQC", StringComparison.Ordinal)
                || param.Moduleid.Equals("0000", StringComparison.Ordinal))
            {
                return "OK";
            }
            #endregion         
            #region 产品包装检查
            SnPackResult snPackResult;
            try
            {
                snPackResult = await OVCheckBussiness.GetSnPackByModuleid(param.Moduleid).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + ex.StackTrace);
                result = $"510NG:{ ex.Message}";
                return result;
            }
            if (snPackResult != null && !string.IsNullOrEmpty(snPackResult.IS_VALID))
            {
                return "504NG:产品已包装扫描";
            }
            #endregion
            #region 特殊工单直接放行
            if (param.Rcard.Length > 11)
            {
                int SortWOCount = 0;
                try
                {
                    SortWOCount = await OVCheckBussiness.GetSortWO(param.Rcard.Substring(0, 12)).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = $"510NG:{ ex.Message}";
                    _logger?.LogError(ex.Message + ex.StackTrace);
                    return result;
                }
                if (SortWOCount > 0)
                {
                    return "OK";
                }
            }
            #endregion
            #region 流程卡机型检查
            RcardPanelPartSpec woPanel;
            try
            {
                woPanel = await OVCheckBussiness.GetRcardPanelPartSpec(param.Rcard).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"510NG:{ ex.Message}";
                _logger?.LogError(ex.Message + ex.StackTrace);
                return result;
            }
            if (woPanel == null || string.IsNullOrEmpty(woPanel.Rcard))
            {
                return "505NG:流程卡错";
            }
            if (string.IsNullOrEmpty(woPanel.Wo_Code))
            {
                return "508NG:漏制程";
            }
            if (string.IsNullOrEmpty(woPanel.Part_Spec))
            {
                return "505NG:机种错";
            }
            #endregion
            #region 测试流程检查
            Router2Op currentRouterOp;
            try
            {
                currentRouterOp = await OVCheckBussiness.GetCurrentOpCode(param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"510NG:{ ex.Message}";
                _logger?.LogError(ex.Message + ex.StackTrace);
                return result;
            }
            Router2Op routerOp;
            if (currentRouterOp == null || string.IsNullOrEmpty(currentRouterOp.Router_Code) || string.IsNullOrEmpty(currentRouterOp.Op_Code))
            {
                try
                {
                    routerOp = await OVCheckBussiness.GetRouter2OpByWo(woPanel.Wo_Code).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = $"510NG:{ ex.Message}";
                    _logger?.LogError(ex.Message + ex.StackTrace);
                    return result;
                }
                if (routerOp == null || string.IsNullOrEmpty(routerOp.Router_Code) || string.IsNullOrEmpty(routerOp.Op_Code))
                {
                    return "501NG:流程未维护";
                }
                if (!routerOp.Op_Code.Equals(param.Station, StringComparison.Ordinal))
                {
                    return $"502NG:{ routerOp.Op_Code}漏做";
                }
            }
            else
            {
                if (!currentRouterOp.Op_Code.Equals(param.Station, StringComparison.Ordinal))
                {
                    IEnumerable<Router2Op> router2Ops;
                    try
                    {
                        router2Ops = await OVCheckBussiness.GetRouter2Op(currentRouterOp.Router_Code).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        result = $"510NG:{ ex.Message}";
                        _logger?.LogError(ex.Message + ex.StackTrace);
                        return result;
                    }
                    if (router2Ops == null || !router2Ops.Any())
                    {
                        return "501NG:流程未维护";
                    }
                    var its = router2Ops.GetEnumerator();
                    bool exists = false;
                    string NextOp = string.Empty;
                    while (its.MoveNext())
                    {
                        if (!exists)
                        {
                            if (its.Current.Op_Code.Equals(currentRouterOp.Op_Code, StringComparison.Ordinal))
                            {
                                exists = true;
                            }
                        }
                        else
                        {
                            NextOp = its.Current.Op_Code;
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(NextOp))
                    {
                        return "502NG:已完工";
                    }
                    if (!param.Station.Equals(NextOp, StringComparison.Ordinal))
                    {
                        return $"502NG:{ NextOp}漏做";
                    }
                }

                try
                {
                    count = await OVCheckBussiness.GetBigWo(woPanel.Part_Spec).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = $"510NG:{ ex.Message}";
                    _logger?.LogError(ex.Message + ex.StackTrace);
                    return result;
                }
                if (count == 0)
                {
                    try
                    {
                        count = await OVCheckBussiness.GetMixRcard(param.Rcard, param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        result = $"510NG:{ ex.Message}";
                        _logger?.LogError(ex.Message + ex.StackTrace);
                        return result;
                    }
                    if (count > 0)
                    {
                        return "506NG:混批";
                    }
                }
            }
            #endregion
            #region 防跳站特殊拦截
            try
            {
                if ("小米".Equals(woPanel.Customer, StringComparison.Ordinal))
                    return await OVCheckBussiness.GetErrorCountForFC(param.Rcard, param.Moduleid, woPanel.Part_Spec, param.Station).ConfigureAwait(false);
                else
                    return await OVCheckBussiness.GetErrorCount(param.Rcard, param.Moduleid, woPanel.Part_Spec, param.Station).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"510NG:{ ex.Message}";
                _logger?.LogError(ex.Message + ex.StackTrace);
                return result;
            }
            #endregion
        }

        /// <summary>
        /// OVCheckStation
        /// </summary>
        /// <param name="param">Input</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RcardIO> GetPartSpecAndEmpNo(RcardIOParam param)
        {
            string result = string.Empty;
            if (param == null || string.IsNullOrEmpty(param.Rcard) || string.IsNullOrEmpty(param.OpCode))
            {
                return new RcardIO() { UserName = "NG:流程卡未领料", OpCode = "NG:流程卡/工序空", PartSpec = "NG:获取机种失败" };
            }
            try
            {
                RcardIO rcardIO = await OVCheckBussiness.GetEmpNoByRcard(param.Rcard).ConfigureAwait(false);
                if (rcardIO == null || string.IsNullOrEmpty(rcardIO.UserName) || string.IsNullOrEmpty(rcardIO.OpCode))
                {
                    if (rcardIO == null)
                        rcardIO = new RcardIO { UserName = "NG:流程卡未领料", OpCode = "NG:无记录" };
                    if (param.Rcard.Length > 12)
                    {
                        string partSpec = await OVCheckBussiness.GetPartSpec(param.Rcard.Substring(0, 12)).ConfigureAwait(false);
                        if (string.IsNullOrEmpty(partSpec))
                        {
                            rcardIO.PartSpec = "NG:获取机种为空";
                        }
                        else
                        {
                            rcardIO.PartSpec = partSpec;
                        }
                    }
                    else
                    {
                        rcardIO.PartSpec = "NG:流程卡错";
                    }
                    return rcardIO;
                }
                if (rcardIO.OpCode.Contains("四合一", StringComparison.Ordinal))
                {
                    rcardIO.OpCode = "功能测试";
                }
                else if (rcardIO.OpCode.Contains("PDAF烧录", StringComparison.Ordinal)
                     || rcardIO.OpCode.Contains("PDAF高通烧录", StringComparison.Ordinal)
                     || rcardIO.OpCode.Contains("PDAF MTK烧录", StringComparison.Ordinal))
                {
                    rcardIO.OpCode = "PDAF烧录";
                }
                if (param.Rcard.Length > 12)
                {
                    string partSpec = await OVCheckBussiness.GetPartSpec(param.Rcard.Substring(0, 12)).ConfigureAwait(false);
                    if (string.IsNullOrEmpty(partSpec))
                    {
                        rcardIO.PartSpec = "NG:获取机种为空";
                    }
                    else
                    {
                        rcardIO.PartSpec = partSpec;
                    }
                }
                else
                {
                    rcardIO.PartSpec = "NG:流程卡错";
                }
                if (!rcardIO.OpCode.Equals(param.OpCode, StringComparison.Ordinal))
                {
                    return new RcardIO() { UserName = "NG:流程卡未领料", OpCode = "NG:工序不匹配", PartSpec = "NG:获取机种失败" };
                }
                else
                {
                    rcardIO.OpCode = "OK";
                }
                return rcardIO;
            }
            catch (Exception ex)
            {
                _logger?.LogError(result + ex.StackTrace);
                return new RcardIO() { UserName = "NG:获取工号发生异常", OpCode = $"NG:{ex.Message}", PartSpec = "NG:获取机种发生异常" };
            }
        }

        public string Test(cameraQCVoList param)
        {
            string result = "";
            if ("C9DA07".Equals(param.partspec, StringComparison.Ordinal) && "T03".Equals(param.opcode, StringComparison.Ordinal))
            {
                try
                {
                    if (param.testItemList == null || param.testItemList.Count == 0)
                        return "512NG:漏测试项";
                    var item = param.TestList.Where(x => "221FarCodeCheck".Equals(x.testItemName, StringComparison.Ordinal));
                    if (item == null || !item.Any())
                    {
                        return "512NG:漏221";
                    }
                    item = param.testItemList.Where(x => "101脏点测试".Equals(x.testItemName, StringComparison.Ordinal));
                    if (item == null || !item.Any())
                    {
                        return "512NG:漏101";
                    }
                    item = param.testItemList.Where(x => "227NearCodeCheck".Equals(x.testItemName, StringComparison.Ordinal));
                    if (item == null || !item.Any())
                    {
                        return "512NG:漏227";
                    }
                    //item = param.testItemList.Where(x => "904DataCheck".Equals(x.testItemName, StringComparison.Ordinal));
                    //if (item == null || !item.Any())
                    //{
                    //    return "512NG:漏904";
                    //}
                    //item = param.testItemList.Where(x => "904远近焦差异管控".Equals(x.testItemName, StringComparison.Ordinal));
                    //if (item == null || !item.Any())
                    //{
                    //    return "512NG:漏904";
                    //}
                    //item = param.testItemList.Where(x => "904QCPDAFDataCheck".Equals(x.testItemName, StringComparison.Ordinal));
                    //if (item == null || !item.Any())
                    //{
                    //    return "512NG:漏904";
                    //}
                    //item = param.testItemList.Where(x => "444二维码检测".Equals(x.testItemName, StringComparison.Ordinal));
                    //if (item == null || !item.Any())
                    //{
                    //    return "512NG:漏444";
                    //}         
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.StackTrace);
                    return result;
                }
            }

            return result;

        }
    }
}
