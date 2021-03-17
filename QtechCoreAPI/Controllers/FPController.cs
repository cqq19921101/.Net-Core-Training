using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace QtechCoreAPI
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FPController : ControllerBase
    {
        private readonly ILogger<FPController> _logger;
        private static string[] s1 = { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static int threadcount = 0;

        public FPController(ILogger<FPController> logger)
        {
            _logger = logger;
        }
        public async Task<string> GetSysTime()
        {
            return await Task.FromResult(DateTime.Now.ToString("G")).ConfigureAwait(false);
        }
        [HttpGet]
        public async Task<string> GetWipCount()
        {
            int availableWorkerThreads;
            int availableAsyncIOThreads;
            ThreadPool.GetAvailableThreads(out availableWorkerThreads, out availableAsyncIOThreads);
            return await Task.FromResult($"{threadcount}|{availableWorkerThreads}|{availableAsyncIOThreads}").ConfigureAwait(false);
        }

        /// <summary>
        /// FP防跳站
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> FPCheckStation(CheckStationParam param)
        {
            string Result;
            Interlocked.Increment(ref threadcount);
            Result = await FPCheckReturn(param).ConfigureAwait(false);
            Interlocked.Decrement(ref threadcount);
            return Result;
        }

        /// <summary>
        /// FP测试数据保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> FPSaveData(cameraQCVoList param)
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
                    temp = await FPCheckBusiness.GetFpDealResult(param.GuId).ConfigureAwait(false);
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
                try
                {
                    return await FPCheckBusiness.SaveTestFpDataNoLigth(param).ConfigureAwait(false);
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
                    result = await FPCheckBusiness.SaveTestMsr(param.lotNumber, param.moduleid, param.barcode, param.partspec).ConfigureAwait(false);
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
            //检查机种 MD5是否匹配对应的机台 过滤正式软件 true:正式  false:测试
            //string mac = string.Empty;
            //bool exists;
            //try
            //{
            //    exists = await FPCheckBusiness.CheckSoftIsFormal(param).ConfigureAwait(false);
            //}
            //catch (Exception ex)
            //{
            //    result = "510NG:" + ex.Message;
            //    _logger?.LogError(result + ex.Source);
            //    return result;
            //}
            //if (!exists)
            //{
            //    //测试软件
            //    try
            //    {
            //        mac = await FPCheckBusiness.CheckFPMappingMAC(param).ConfigureAwait(false);
            //    }
            //    catch (Exception ex)
            //    {
            //        result = "510NG:" + ex.Message;
            //        _logger?.LogError(result + ex.StackTrace);
            //        return result;
            //    }
            //}
            //else
            //{
            //    //正式软件 无法在已经有测试软件运行的机台上运行
            //    if (!await FPCheckBusiness.CheckFormalsoft(param).ConfigureAwait(false))
            //    {
            //        return "501NG:已经运行测试软件的机台无法再运行正式软件";
            //    }
            //}
            Router2Op routerOp;
            try
            {
                routerOp = await FPCheckBusiness.GetNextOpCode(param.wo, param.moduleid, param.partspec).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = "510NG:" + ex.Message;
                _logger?.LogError(result + ex.StackTrace);
                return result;
            }
            if (routerOp == null || string.IsNullOrEmpty(routerOp.Router_Code) || string.IsNullOrEmpty(routerOp.Op_Code))
            {
                try
                {
                    routerOp = await FPCheckBusiness.GetCurrentOpCode(param.moduleid, param.partspec).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = "510NG:" + ex.Message;
                    _logger?.LogError(result + ex.StackTrace);
                    return result;
                }
                if (routerOp == null || string.IsNullOrEmpty(routerOp.Router_Code) || string.IsNullOrEmpty(routerOp.Op_Code))
                {
                    return "501NG:流程未维护";
                }
            }
            long id;
            try
            {
                id = await FPCheckBusiness.GetTestFpDataID().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = "510NG:" + ex.Message;
                _logger?.LogError(result + ex.StackTrace);
                return result;
            }
            if (id <= 0)
            {
                return "510NG:ID为空";
            }
            param.id = id;
            try
            {
                result = await FPCheckBusiness.SaveTestFpDataAndDetail(param, routerOp).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(param.GuId))
                {
                    if (result.Equals("OK", StringComparison.OrdinalIgnoreCase))
                    {
                        await FPCheckBusiness.SaveFpDealResult(param.GuId, result).ConfigureAwait(false);
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
        /// FP获取机台ID
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetFPStationIdByMac(StringParam param)
        {
            string result;
            if (param == null || string.IsNullOrEmpty(param.Message))
                return "NG";
            try
            {
                return await FPCheckBusiness.GetFPStationIdByMac(param.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return result;
        }
        /// <summary>
        /// FP ORT数据上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> FPORTData(TestFpORTData param)
        {
            string result;
            if (param == null)
                return "NG";
            try
            {
                return await FPCheckBusiness.SaveTestFpDataORT(param).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(TestFpORTData)));
            }
            return result;
        }
        /// <summary>
        /// FPCheckStation
        /// </summary>
        /// <param name="param">Input</param>
        /// <returns></returns>
        public async Task<string> FPCheckReturn(CheckStationParam param)
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
                snPackResult = await FPCheckBusiness.GetSnPackByModuleid(param.Moduleid).ConfigureAwait(false);
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

            #region 流程卡机型检查
            RcardPanelPartSpec woPanel;
            try
            {
                woPanel = await FPCheckBusiness.GetRcardPanelPartSpec(param.Rcard).ConfigureAwait(false);
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

            #region 机种工序检查
            Router2Op routerOp;
            try
            {
                routerOp = await FPCheckBusiness.GetNextOpCode(woPanel.Wo_Code, param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"510NG:{ ex.Message}";
                _logger?.LogError(ex.Message + ex.StackTrace);
                return result;
            }
            #endregion

            #region 测试流程检查
            if (routerOp == null || string.IsNullOrEmpty(routerOp.Router_Code) || string.IsNullOrEmpty(routerOp.Op_Code))
            {
                Router2Op currentRouterOp;
                try
                {
                    currentRouterOp = await FPCheckBusiness.GetCurrentOpCode(param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    result = $"510NG:{ ex.Message}";
                    _logger?.LogError(ex.Message + ex.StackTrace);
                    return result;
                }
                if (currentRouterOp == null || string.IsNullOrEmpty(currentRouterOp.Router_Code) || string.IsNullOrEmpty(currentRouterOp.Op_Code))
                {
                    return "501NG:流程未维护";
                }
                if (!currentRouterOp.Op_Code.Equals(param.Station, StringComparison.Ordinal))
                {
                    return $"502NG:已完工";
                }
                try
                {
                    count = await FPCheckBusiness.GetBigWo(woPanel.Part_Spec).ConfigureAwait(false);
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
                        count = await FPCheckBusiness.GetMixRcard(param.Rcard, param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
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
            else
            {
                if (routerOp.Op_Seq > 0)
                {
                    try
                    {
                        count = await FPCheckBusiness.GetBigWo(woPanel.Part_Spec).ConfigureAwait(false);
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
                            count = await FPCheckBusiness.GetMixRcard(param.Rcard, param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
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
                if (!routerOp.Op_Code.Equals(param.Station, StringComparison.Ordinal))
                {
                    Router2Op currentRouterOp;
                    try
                    {
                        currentRouterOp = await FPCheckBusiness.GetCurrentOpCode(param.Moduleid, woPanel.Part_Spec).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        result = $"510NG:{ ex.Message}";
                        _logger?.LogError(ex.Message + ex.StackTrace);
                        return result;
                    }
                    if (currentRouterOp == null || string.IsNullOrEmpty(currentRouterOp.Router_Code) || string.IsNullOrEmpty(currentRouterOp.Op_Code))
                    {
                        return $"502NG:{ routerOp.Op_Code}漏做";
                    }
                    else
                    {
                        if (!currentRouterOp.Op_Code.Equals(param.Station, StringComparison.Ordinal))
                        {
                            return $"502NG:{ routerOp.Op_Code}漏做";
                        }
                    }
                }
            }
            #endregion

            #region 防跳站特殊拦截
            try
            {
                return await FPCheckBusiness.GetErrorCount(param.Rcard, param.Moduleid, woPanel.Part_Spec, param.Station).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"510NG:{ ex.Message}";
                _logger?.LogError(ex.Message + ex.StackTrace);
                return result;
            }
            #endregion
        }
    }
}
