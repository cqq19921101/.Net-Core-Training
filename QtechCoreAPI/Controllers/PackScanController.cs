using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QtechCoreAPI
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PackScanController : ControllerBase
    {
        private readonly ILogger<PackScanController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public PackScanController(ILogger<PackScanController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        /// <summary>
        /// 获取扫码软件版本
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> GetScanSoftVersion()
        {
            string result;
            try
            {
                result = await SnPackBusiness.GetScanSoftVersion().ConfigureAwait(false);
                return new ResponseModel() { Code = 200, Msg = result };

            }
            catch (Exception ex)
            {
                result = $"{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result);
                return new ResponseModel() { Code = 300, Msg = ex.Message };
            }
        }
        /// <summary>
        /// 根据小箱号返回对应的数量
        /// </summary>
        /// <param name="cartonNo">小箱号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> GetCartonQty(StringParam param)
        {
            int countQty = 0;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return countQty;
                return await SnPackBusiness.GetCartonQty(param.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return countQty;
        }
        /// <summary>
        /// 获取流程卡当前在制良品数
        /// </summary>
        /// <param name="cartonNo">小箱号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> GetRcardWipQty(StringParam param)
        {
            int countQty = 0;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return countQty;
                return await SnPackBusiness.GetRcardWipQty(param.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return countQty;
        }
        /// <summary>
        /// 根据二维码返回对应的tray
        /// </summary>
        /// <param name="Message">二维码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetSnTray(StringParam param)
        {
            string trayNo = "";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return "NG";
                return await SnPackBusiness.GetSnTray(param.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return trayNo;
        }
        /// <summary>
        /// 根据Tray返回对应的二维码
        /// </summary>
        /// <param name="Message">Tray</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<string>> getTraySn(StringParam param)
        {
            List<string> ls = new List<string>();
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return ls;
                return await SnPackBusiness.GetTraySn(param.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger?.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return ls;
        }
        [HttpPost]
        public async Task<string> GetPartSpecByCartonNo(StringParam param)
        {
            string PartSpec = "";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return "NG";
                var partSpecClientModel = await SnPackBusiness.GetPartSpecByCarton(param.Message).ConfigureAwait(false);
                if (partSpecClientModel != null)
                    return partSpecClientModel.PartSpec;
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return PartSpec;
        }
        [HttpPost]
        public async Task<string> SaveTrayNoPrintLog(TraySnParam param)
        {
            string result = "";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.cartonNo) || string.IsNullOrEmpty(param.trayNo) || param.snCodes == null || param.snCodes.Count == 0)
                    return "NG";
                return await SnPackBusiness.SaveTrayNoPrintLog(param.cartonNo, param.trayNo, param.snCodes.ToArray(), param.stationId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(TraySnParam)));
            }
            return result;
        }
        /// <summary>
        /// 根据小箱号返回小箱号关联二维码数量
        /// </summary>
        /// <param name="cartonNo">小箱号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> checkCartonQty(StringParam param)
        {
            int countQty = 0;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return countQty;
                return await SnPackBusiness.CheckCartonQty(param.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(StringParam)));
            }
            return countQty;
        }
        [HttpPost]
        public async Task<string> GenerateTrayNo(GenerateTrayNoParam param)
        {
            string TrayNo = "";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.cartonNo))
                    return "NG";
                return await SnPackBusiness.SNGetString("TRAYNO生成规则", param.stationId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(GenerateTrayNoParam)));
            }
            return TrayNo;
        }
        [HttpPost]
        /// <summary>
        /// 云台+TFPC关联
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> LinkSn(LinkSnParam param)
        {
            string result = "OK";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.snCodes) || string.IsNullOrEmpty(param.LinkSnCodes))
                    return "NG";
                await SnPackBusiness.InsertSnLink(param.snCodes, param.LinkSnCodes).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(LinkSnParam)));
            }
            return result;
        }
        [HttpPost]
        /// <summary>
        /// TFPC+AF关联
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> LinkAfSn(LinkSnParam param)
        {
            string result = "OK";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.snCodes) || string.IsNullOrEmpty(param.LinkSnCodes))
                    return "NG";
                await SnPackBusiness.InsertSnLink(param.snCodes, param.LinkSnCodes).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(LinkSnParam)));
            }
            return result;
        }
        /// <summary>
        /// 产品失效接口
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> InvalidModuleTestResult(SnParam param)
        {
            string result;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.SnCode) || string.IsNullOrEmpty(param.Rcard))
                    return "NG";
                return await SnPackBusiness.InvalidModuleTestResult(param.Rcard, param.SnCode, param.StationId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnParam)));
            }
            return result;
        }
        [HttpPost]
        public async Task<string> InvalidTestResultByPartSpec(SnInvlidParam param)
        {
            string result;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.SnCode) || string.IsNullOrEmpty(param.PartSpec))
                    return "NG;参数空";
                var customer = await SnPackBusiness.GetCustomerByPartSpec(param.PartSpec).ConfigureAwait(false);
                if (string.IsNullOrEmpty(customer))
                    return "NG;机型找不到客户";
                string moduleid = await SnPackBusiness.GetModuleidBySn(param.SnCode).ConfigureAwait(false);
                if (string.IsNullOrEmpty(moduleid))
                {
                    return "NG;无关联模组";
                }
                var count = await SnPackBusiness.GetNewSoftExists(param.PartSpec).ConfigureAwait(false);
                if (customer.Equals("华为") && !param.PartSpec.Equals("FH259AE-S") && !param.PartSpec.Equals("FH259AG"))
                {
                    result = await SnPackBusiness.InvalidTestResultForFP(moduleid, param.SnCode, param.PartSpec, param.StationId, param.isInvlidSnLink).ConfigureAwait(false);
                }
                else if (count > 0)
                {
                    result = await SnPackBusiness.InvalidTestResultForMOD(moduleid, param.SnCode, param.PartSpec, param.StationId, param.isInvlidSnLink).ConfigureAwait(false);
                }
                else
                {
                    result = await SnPackBusiness.InvalidTestResultForOld(moduleid, param.SnCode, param.PartSpec, param.StationId).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnInvlidParam)));
            }
            return result;
        }
        /// <summary>
        /// tray盘解link接口
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> InvalidModuleLinkResult(SnParam param)
        {
            string result;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.SnCode) || string.IsNullOrEmpty(param.Rcard))
                    return "NG";
                return await SnPackBusiness.InvalidModuleLinkResult(param.Rcard, param.SnCode, param.StationId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnParam)));
            }
            return result;
        }
        [HttpPost]
        public async Task<int> checkRCardSnCodeQty(RcardTypeParam param)
        {
            int countQty = 0;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Rcard) || string.IsNullOrEmpty(param.CheckType))
                    return countQty;
                return await SnPackBusiness.CheckRCardSnCodeQty(param.Rcard, param.CheckType).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(RcardTypeParam)));
            }
            return countQty;
        }
        /// <summary>
        /// A,B规挑选
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SnSortCheck(SnSortParam param)
        {
            string result = "OK;";
            try
            {
                if (param == null)
                {
                    return "NG;机种/二维码空";
                }
                if (string.IsNullOrEmpty(param.SnCode))
                {
                    return "NG;二维码空";
                }
                if (string.IsNullOrEmpty(param.PartSpec))
                {
                    return "NG;机种空";
                }
                if (param.PartSpec.Equals("C9CS02", StringComparison.Ordinal))
                {
                    var value = await SnPackBusiness.GetSnHold(param.SnCode, "").ConfigureAwait(false);
                    if (value > 0)
                        return "NG;无数据";
                    else
                        return result;
                }
                string moduleid = await SnPackBusiness.GetModuleidBySn(param.SnCode).ConfigureAwait(false);
                if (string.IsNullOrEmpty(moduleid))
                {
                    return "NG;无关联模组";
                }
                int count = await SnPackBusiness.GetPartSpecExists(param.PartSpec).ConfigureAwait(false);
                if (count == 0)
                {
                    return "NG;机种错";
                }
                if (param.PartSpec.Equals("FX332AB", StringComparison.Ordinal))
                {
                    var value = await SnPackBusiness.GetFpSortFX332ABValue(moduleid, param.PartSpec).ConfigureAwait(false);
                    if (!value.Any())
                        return "NG;无数据";
                    foreach (string str in value.ToList())
                    {
                        double d;
                        _ = double.TryParse(str, out d);
                        if (d > 25 || d < -25)
                        {
                            return "NG;值小于49";
                        }
                    }
                }
                else if (param.PartSpec.Equals("C08S03", StringComparison.Ordinal))
                {
                    result = await C08S03ABSpecSort(moduleid, param.PartSpec).ConfigureAwait(false);
                }
                else if (param.PartSpec.Equals("C0QS01", StringComparison.Ordinal))
                {
                    result = await C0QS01SpecSort(moduleid, param.PartSpec).ConfigureAwait(false);
                }
                else if (param.PartSpec.Equals("C92F06-A", StringComparison.Ordinal))
                {
                    int num = await SnPackBusiness.GetC92F06Data(moduleid).ConfigureAwait(false);
                    if (num != 2)
                    {
                        return "NG;B规";
                    }
                }
                else
                {
                    //var dataDetails = await SnPackBusiness.GetFpSortValue(moduleid, param.PartSpec).ConfigureAwait(false);
                    //var value = dataDetails.Where(x => x.TestItem == "201远焦120CMSFR" && x.TestSubItem.StartsWith("0.5视场", StringComparison.Ordinal)).Min(x => x.SubItemTestValue);
                    //var Near = dataDetails.Where(x => x.TestItem == "217AFcode20CM细调" && x.TestSubItem.Equals("Near", StringComparison.Ordinal)).Min(x => x.SubItemTestValue);
                    //double d, n;
                    //_ = double.TryParse(value, out d);
                    //_ = double.TryParse(Near, out n);
                    //if (d < 49 || n >= 2800)
                    //{
                    //    return "NG;值小于49";
                    //}
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnSortParam)));
            }
            return result;
        }
        /// <summary>
        /// 测试分类
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SnSort(SnSortParam param)
        {
            string result;
            try
            {
                if (param == null)
                {
                    return "NG;[机种/二维码空]";
                }
                if (string.IsNullOrEmpty(param.SnCode))
                {
                    return "NG;[二维码空]";
                }
                if (string.IsNullOrEmpty(param.PartSpec))
                {
                    return "NG;[机种空]";
                }
                string customer = await SnPackBusiness.GetCustomerByPartSpec(param.PartSpec).ConfigureAwait(false);
                if (string.IsNullOrEmpty(customer))
                {
                    return "NG;[机种客户为空]";
                }
                string moduleid = await SnPackBusiness.GetModuleidBySn(param.SnCode).ConfigureAwait(false);
                if (string.IsNullOrEmpty(moduleid))
                {
                    return "NG;[无关联模组]";
                }
                StringBuilder stringBuilder = new StringBuilder();
                if (customer.Equals("华为"))
                {
                    var testModDatas = await SnPackBusiness.GetAllFPData21(moduleid, param.PartSpec).ConfigureAwait(false);
                    if (testModDatas.Count() == 0)
                        return "NG;[无测试数据]";
                    if (testModDatas.Count() > 1)
                    {
                        var testModData = testModDatas.OrderByDescending(a => a.TestTime).ToList()[0];
                        stringBuilder.Append($"OK;[{testModData.OpCode};");
                        if (testModData.Result.Equals("OK"))
                            stringBuilder.Append($"OK;");
                        else
                            stringBuilder.Append($"{testModData.NgType};");
                        var count = testModDatas.Where(x => x.OpCode == testModData.OpCode).ToList().Count;
                        stringBuilder.Append($"{count};");
                        var repair = testModDatas.Where(x => !string.IsNullOrEmpty(x.Rcard) && x.Rcard.StartsWith("X01") || !string.IsNullOrEmpty(x.Rcard) && x.Rcard.Contains("Q")).ToList();
                        if (repair != null && repair.Count > 0)
                            stringBuilder.Append($"修复品]");
                        else
                            stringBuilder.Append($"正常品]");
                        return stringBuilder.ToString();
                    }
                    else
                    {
                        var testModData = testModDatas.ToList()[0];
                        stringBuilder.Append($"OK;[{testModData.OpCode};");
                        if (testModData.Result.Equals("OK"))
                            stringBuilder.Append($"OK;");
                        else
                            stringBuilder.Append($"{testModData.NgType};");
                        var count = testModDatas.Where(x => x.OpCode == testModData.OpCode).ToList().Count;
                        stringBuilder.Append($"{count};");
                        var repair = testModDatas.Where(x => !string.IsNullOrEmpty(x.Rcard) && x.Rcard.StartsWith("X01") || !string.IsNullOrEmpty(x.Rcard) && x.Rcard.Contains("Q")).ToList();
                        if (repair != null && repair.Count > 0)
                            stringBuilder.Append($"修复品]");
                        else
                            stringBuilder.Append($"正常品]");
                        return stringBuilder.ToString();
                    }
                }
                else
                {
                    if (param.PartSpec.Equals("C0QS01", StringComparison.Ordinal))
                    {
                        var value = await SnPackBusiness.C0QS01GetTestModData(moduleid, param.PartSpec).ConfigureAwait(false);
                        if (value == null || string.IsNullOrEmpty(value.PartSpec))
                        {
                            return $"OK;[OK,OK,1,正常品]";
                        }
                        else
                        {
                            return $"OK;[{value.OpCode},{value.NgType},1,异常品]";
                        }
                    }
                    else
                    {
                        var testModDatas = await SnPackBusiness.GetModData57(moduleid, param.PartSpec).ConfigureAwait(false);
                        if (testModDatas.Count() == 0)
                            return "NG;[无测试数据]";
                        if (testModDatas.Count() > 1)
                        {
                            var testModData = testModDatas.OrderByDescending(a => a.TestTime).ToList()[0];
                            stringBuilder.Append($"OK;[{testModData.OpCode};");
                            if (testModData.Result.Equals("OK"))
                                stringBuilder.Append($"OK;");
                            else
                                stringBuilder.Append($"{testModData.NgType};");
                            var count = testModDatas.Where(x => x.OpCode == testModData.OpCode).ToList().Count;
                            stringBuilder.Append($"{count};");
                            var repair = testModDatas.Where(x => !string.IsNullOrEmpty(x.Rcard) && x.Rcard.StartsWith("X01") || !string.IsNullOrEmpty(x.Rcard) && x.Rcard.Contains("Q")).ToList();
                            if (repair != null && repair.Count > 0)
                                stringBuilder.Append($"修复品]");
                            else
                                stringBuilder.Append($"正常品]");

                            return stringBuilder.ToString();
                        }
                        else
                        {
                            var testModData = testModDatas.ToList()[0];
                            stringBuilder.Append($"OK;[{testModData.OpCode};");
                            if (testModData.Result.Equals("OK"))
                                stringBuilder.Append($"OK;");
                            else
                                stringBuilder.Append($"{testModData.NgType};");
                            var count = testModDatas.Where(x => x.OpCode == testModData.OpCode).ToList().Count;
                            stringBuilder.Append($"{count};");
                            var repair = testModDatas.Where(x => !string.IsNullOrEmpty(x.Rcard) && x.Rcard.StartsWith("X01") || !string.IsNullOrEmpty(x.Rcard) && x.Rcard.Contains("Q")).ToList();
                            if (repair != null && repair.Count > 0)
                                stringBuilder.Append($"修复品]");
                            else
                                stringBuilder.Append($"正常品]");

                            return stringBuilder.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = $"NG;[{ ex.Message}{ ex.Source }{ ex.StackTrace}]";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnSortParam)));
            }
            return result;
        }
        /// <summary>
        /// 包装二维码扫描
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SnPackCheck(SnPackParam param)
        {
            string result = string.Empty;

            if (param == null)
            {
                return "NG;<无关联模组>";
            }
            if (string.IsNullOrEmpty(param.SnCode) || param.SnCode.Length > 50)
            {
                return "NG;<无关联模组>";
            }
            if (string.IsNullOrEmpty(param.CatonNo))
            {
                return "NG;<箱号找不到客户和机种>";
            }
            try
            {
                result = await SnCodeCheck(param).ConfigureAwait(false);
                if (result.Equals("NG;<重码扫描>", StringComparison.Ordinal))
                {
                    await SnPackBusiness.SaveSnCheckResultNoIsValue(param.SnCode, param.CatonNo, result).ConfigureAwait(false);
                }
                else
                {
                    if (result.Equals("OK;<WB补线>", StringComparison.Ordinal) || result.Equals("OK;<首件产品>", StringComparison.Ordinal)
                        || result.Equals("OK;<新固件27304>", StringComparison.Ordinal) || result.Equals("OK;<A规>", StringComparison.Ordinal)
                        || result.StartsWith("OK", StringComparison.Ordinal))
                        await SnPackBusiness.SaveSnCheckResult(param.SnCode, param.CatonNo, "OK").ConfigureAwait(false);
                    else
                        await SnPackBusiness.SaveSnCheckResult(param.SnCode, param.CatonNo, result).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + ex.StackTrace + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnPackParam)));
            }
            return result;
        }
        public async Task<string> SnCodeCheck(SnPackParam param)
        {
            string result;
            try
            {
                PartSpecClientModel partSpec = await SnPackBusiness.GetPartSpecByCarton(param.CatonNo).ConfigureAwait(false);
                if (partSpec != null && (partSpec.Customer.Equals("QT印度厂", StringComparison.Ordinal)))
                {
                    WO wO = await SnPackBusiness.GetSpecCheck21(param.SnCode, "").ConfigureAwait(false);
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code) && "10000".Equals(wO.Aa, StringComparison.Ordinal))
                    {
                        return $"OK;<{wO.Bb}>";
                    }
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code))
                    {
                        return $"NG;<特殊拦截:{wO.Bb}>";
                    }
                    if (param.SnCode.Substring(1, 1) == "H")
                    {
                        return "NG;<H规则二维码>";
                    }
                    else
                    {
                        return "OK";
                    }
                }
                ModuleidPackResult moduleidPackResult = await SnPackBusiness.GetModuleiPackdBySn(param.SnCode).ConfigureAwait(false);
                if (moduleidPackResult == null || string.IsNullOrEmpty(moduleidPackResult.Moduleid))
                {
                    return "NG;<无关联模组>";
                }
                //int count = await SnPackBusiness.GetSnExistsPackResult(param.SnCode).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(moduleidPackResult.IS_VALID))
                {
                    return "NG;<重码扫描>";
                }
                int AACount = await SnPackBusiness.GetAAResult(moduleidPackResult.Moduleid).ConfigureAwait(false);
                if (AACount > 0)
                {
                    return "NG;<AA不良>";
                }
                // PartSpecClientModel partSpec = await SnPackBusiness.GetPartSpecByCarton(param.CatonNo).ConfigureAwait(false);
                if (partSpec == null || string.IsNullOrEmpty(partSpec.Customer) || string.IsNullOrEmpty(partSpec.PartSpec))
                {
                    return "NG;<箱号找不到客户和机种>";
                }
                if (partSpec.PartSpec.Equals("C0MA12-S-A", StringComparison.Ordinal))
                {
                    WO wO = await SnPackBusiness.GetSpecCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code))
                    {
                        return $"NG;<特殊拦截:{wO.Bb}>";
                    }
                }
                int count = await SnPackBusiness.GetNewSoftExists(partSpec.PartSpec).ConfigureAwait(false);
                if (partSpec.Customer.Equals("华为", StringComparison.Ordinal) && !partSpec.PartSpec.Equals("FH259AE-S", StringComparison.Ordinal) && !partSpec.PartSpec.Equals("FH259AG", StringComparison.Ordinal))
                {
                    result = await SnPackCheckForFP(moduleidPackResult.Moduleid, partSpec.Customer, param.SnCode, partSpec.PartSpec, param.CatonNo).ConfigureAwait(false);
                    if (result.Equals("OK", StringComparison.Ordinal) &&
                        (partSpec.PartSpec.Equals("C9EF02", StringComparison.Ordinal)
                        || partSpec.PartSpec.Equals("C0EF16", StringComparison.Ordinal)
                        || partSpec.PartSpec.Equals("C08F11", StringComparison.Ordinal)
                        || partSpec.PartSpec.Equals("C0NA05-S", StringComparison.Ordinal)
                        || partSpec.PartSpec.Equals("C0EF18", StringComparison.Ordinal)
                        || partSpec.PartSpec.Equals("C0DA56-B", StringComparison.Ordinal)))
                    {
                        result = await CheckOtpTimeAsync(param.SnCode, moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                    }
                }
                else if (count > 0)
                {
                    result = await SnPackCheckForMOD(moduleidPackResult.Moduleid, partSpec.Customer, param.SnCode, partSpec.PartSpec, param.CatonNo, partSpec.ITEM_CODE).ConfigureAwait(false);
                    if (result.Equals("OK", StringComparison.Ordinal) &&
        (partSpec.PartSpec.Equals("C08F32-A", StringComparison.Ordinal) || partSpec.PartSpec.Equals("C08F32-B", StringComparison.Ordinal)))
                    {
                        result = await CheckOtpTimeAsync(param.SnCode, moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                    }
                }
                else
                {
                    result = await SnPackCheckForOld(moduleidPackResult.Moduleid, partSpec.Customer, param.SnCode, partSpec.PartSpec, param.CatonNo).ConfigureAwait(false);
                }
                if (result.Equals("OK", StringComparison.Ordinal))
                {
                    WO wO = await SnPackBusiness.GetSpecCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code) && "10000".Equals(wO.Aa, StringComparison.Ordinal))
                    {
                        return $"OK;<{wO.Bb}>";
                    }
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code))
                    {
                        return $"NG;<特殊拦截:{wO.Bb}>";
                    }
                    if (partSpec.PartSpec.Equals("C9MA02", StringComparison.Ordinal))
                    {
                        count = await SnPackBusiness.GetSpecCheckQrGenenation21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                        if (count > 0)
                        {
                            return "NG;<特殊拦截FPC>";
                        }
                    }
                    if (partSpec.PartSpec.Equals("C9QA01", StringComparison.Ordinal))
                    {
                        TestMsr tQg = await SnPackBusiness.GetSpecCheckTestMsr21C9QA01(param.SnCode).ConfigureAwait(false);
                        if (tQg != null && !string.IsNullOrEmpty(tQg.SnCode))
                        {
                            DateTime Mdate = Convert.ToDateTime("2021-01-01");
                            if (tQg.Mdate < Mdate)
                                return $"NG;<特殊拦截陈强拦截C9QA01 21年之前全部拦截>";
                        }
                    }
                    //if (partSpec.PartSpec.Equals("C0MA12-S", StringComparison.Ordinal) || partSpec.PartSpec.Equals("C0MA12-S-A", StringComparison.Ordinal))
                    //{
                    //    count = await SnPackBusiness.GetSpecCheckQrGenenation21C0MA12(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                    //    if (count > 0)
                    //    {
                    //        return "NG;<风险FPC>";
                    //    }
                    //}
                    if (partSpec.PartSpec.Equals("C0QS01", StringComparison.Ordinal))
                    {
                        count = await SnPackBusiness.GetC0QS01Data(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                        if (count > 0)
                        {
                            return "NG;<OIS B规品>";
                        }
                    }
                    if (partSpec.PartSpec.Equals("C98A02", StringComparison.Ordinal))
                    {
                        if (param.SnCode.StartsWith("554", StringComparison.Ordinal))
                        {
                            return "NG;<板材不可互用,联系韩峰>";
                        }
                    }
                    if (partSpec.PartSpec.Equals("S0LS07", StringComparison.Ordinal))
                    {
                        var value = await SnPackBusiness.GetS0LS07Data(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false); ;
                        if (value > 0)
                        {
                            return "NG;<201不良,联系丁敏>";
                        }
                    }
                    if (partSpec.PartSpec.Equals("C0HF34", StringComparison.Ordinal))
                    {
                        var value = await SnPackBusiness.GetC0HF34Data(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false); ;
                        if (value > 0)
                        {
                            return "NG;<特殊拦截,联系邢中普>";
                        }
                    }
                    //特殊备注 2021年1/19 漏测904子项目
                    //if (partSpec.PartSpec.Equals("S0LS02", StringComparison.Ordinal)
                    //    || partSpec.PartSpec.Equals("S0LS05", StringComparison.Ordinal)
                    //   || partSpec.PartSpec.Equals("S0LS06", StringComparison.Ordinal))
                    //{
                    //    var value = await SnPackBusiness.GetMod904(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false); ;
                    //    if (value <= 0)
                    //    {
                    //        return "NG;<S0LS02漏测子项904,联系丁敏>";
                    //    }
                    //}
                    //if (partSpec.PartSpec.Equals("F02A10BT-1", StringComparison.Ordinal))
                    //{
                    //    var value = await SnPackBusiness.GetF02A10BTData(moduleidPackResult.Moduleid).ConfigureAwait(false);
                    //    double d;
                    //    _ = double.TryParse(value, out d);
                    //    if (d > 8)
                    //    {
                    //        return "NG;OB值大于8";
                    //    }                       
                    //}
                    if (partSpec.PartSpec.Equals("C0DA07", StringComparison.Ordinal))
                    {
                        var value = await SnPackBusiness.GetC0DA07Data(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                        if (value < 6)
                        {
                            return "NG;漏测试项";
                        }
                    }
                    //if (partSpec.PartSpec.Equals("F4H7YAZ", StringComparison.Ordinal))
                    //{
                    //    var value = await SnPackBusiness.GetF4H7YAZData(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                    //    if (value > 0)
                    //    {
                    //        return "NG;<特殊挑选>";
                    //    }
                    //}
                    count = await SnPackBusiness.GetFirstCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                    if (count > 0)
                    {
                        return $"{result};<首件产品>";
                    }
                    count = await SnPackBusiness.GetGoldCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                    if (count > 0)
                    {
                        return $"{result};<WB补线>";
                    }
                    if (partSpec.PartSpec.Equals("S0LS02", StringComparison.Ordinal) ||
                        partSpec.PartSpec.Equals("S0LS06", StringComparison.Ordinal))//&& !param.CatonNo.StartsWith("SW", StringComparison.Ordinal)
                    {
                        return await S0LS02SnSpec(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                    }

                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + ex.StackTrace + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnPackParam)));
            }
            return result;
        }
        /// <summary>
        /// 外观AOI前检查功能
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> BeforeAoiCheckSn(SnSortParam param)
        {
            string result = string.Empty;
            try
            {
                if (param == null)
                {
                    return "NG;无关联模组";
                }
                if (string.IsNullOrEmpty(param.SnCode))
                {
                    return "NG;无关联模组";
                }
                if (string.IsNullOrEmpty(param.PartSpec))
                {
                    return "NG;无关联模组";
                }
                string moduleid = await SnPackBusiness.GetModuleidBySn(param.SnCode).ConfigureAwait(false);
                if (string.IsNullOrEmpty(moduleid))
                {
                    return "NG;无关联模组";
                }
                var Customer = await SnPackBusiness.GetCustomerByPartSpec(param.PartSpec).ConfigureAwait(false);
                if (Customer == null || string.IsNullOrEmpty(Customer))
                {
                    return "NG;箱号找不到客户和机种";
                }
                int count = await SnPackBusiness.GetSnExistsPackResult(param.SnCode).ConfigureAwait(false);
                if (count > 0)
                {
                    return "NG;重码扫描";
                }
                int AACount = await SnPackBusiness.GetAAResult(moduleid).ConfigureAwait(false);
                if (AACount > 0)
                {
                    return "NG;AA不良";
                }
                count = await SnPackBusiness.GetNewSoftExists(param.PartSpec).ConfigureAwait(false);
                if (Customer.Equals("华为") && !param.PartSpec.Equals("FH259AE-S") && !param.PartSpec.Equals("FH259AG"))
                {
                    result = await SnPackCheckForFP(moduleid, Customer, param.SnCode, param.PartSpec, "").ConfigureAwait(false);
                }
                else if (count > 0)
                {
                    result = await SnPackCheckForMOD(moduleid, Customer, param.SnCode, param.PartSpec, "").ConfigureAwait(false);
                }
                else
                {
                    result = await SnPackCheckForOld(moduleid, Customer, param.SnCode, param.PartSpec, "").ConfigureAwait(false);
                }
                if (result.Equals("OK"))
                {
                    WO wO = await SnPackBusiness.GetSpecCheck21(param.SnCode, moduleid).ConfigureAwait(false);
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code))
                    {
                        return $"NG;<特殊拦截:{wO.Bb}>";
                    }
                    if (param.PartSpec.Equals("C9MA02"))
                    {
                        count = await SnPackBusiness.GetSpecCheckQrGenenation21(param.SnCode, "").ConfigureAwait(false);
                        if (count > 0)
                        {
                            return "NG;<特殊拦截FPC>";
                        }
                    }
                    //if (param.PartSpec.Equals("F4HAYAA"))
                    //{
                    //    count = await SnPackBusiness.GetSpecCheckQrGenenation21F4HAYAA(param.SnCode, "").ConfigureAwait(false);
                    //    if (count > 0)
                    //    {
                    //        return "NG;<特殊拦截FPC>";
                    //    }
                    //}

                    count = await SnPackBusiness.GetFirstCheck21(param.SnCode, moduleid).ConfigureAwait(false);
                    if (count > 0)
                    {
                        return $"{result};<首件产品>";
                    }
                    count = await SnPackBusiness.GetGoldCheck21(param.SnCode, moduleid).ConfigureAwait(false);
                    if (count > 0)
                    {
                        return $"{result};<WB补线>";
                    }

                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + ex.StackTrace + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnSortParam)));
            }
            return result;
        }
        /// <summary>
        /// 功测完检查产品状态
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> CheckSnByRcard(SnSortParam param)
        {
            string result = string.Empty;
            try
            {
                if (param == null)
                {
                    return "NG;无关联模组";
                }
                if (string.IsNullOrEmpty(param.SnCode))
                {
                    return "NG;无关联模组";
                }
                if (string.IsNullOrEmpty(param.PartSpec))
                {
                    return "NG;无关联模组";
                }
                string moduleid = await SnPackBusiness.GetModuleidBySn(param.SnCode).ConfigureAwait(false);
                if (string.IsNullOrEmpty(moduleid))
                {
                    return "NG;无关联模组";
                }
                string partSpec = await SnPackBusiness.GetPartSpecByRcard(param.PartSpec).ConfigureAwait(false);
                if (string.IsNullOrEmpty(partSpec))
                {
                    return "NG;无效流程卡";
                }
                param.PartSpec = partSpec;
                var Customer = await SnPackBusiness.GetCustomerByPartSpec(param.PartSpec).ConfigureAwait(false);
                if (Customer == null || string.IsNullOrEmpty(Customer))
                {
                    return "NG;箱号找不到客户和机种";
                }
                int count = await SnPackBusiness.GetSnExistsPackResult(param.SnCode).ConfigureAwait(false);
                if (count > 0)
                {
                    return "NG;重码扫描";
                }
                count = await SnPackBusiness.GetNewSoftExists(param.PartSpec).ConfigureAwait(false);
                if (Customer.Equals("华为") && !param.PartSpec.Equals("FH259AE-S") && !param.PartSpec.Equals("FH259AG"))
                {
                    result = await SnPackCheckForFP(moduleid, Customer, param.SnCode, param.PartSpec, "").ConfigureAwait(false);
                }
                else if (count > 0)
                {
                    result = await SnPackCheckForMOD(moduleid, Customer, param.SnCode, param.PartSpec, "").ConfigureAwait(false);
                }
                else
                {
                    result = await SnPackCheckForOld(moduleid, Customer, param.SnCode, param.PartSpec, "").ConfigureAwait(false);
                }
                if (result.Equals("OK"))
                {
                    WO wO = await SnPackBusiness.GetSpecCheck21(param.SnCode, moduleid).ConfigureAwait(false);
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code))
                    {
                        return $"NG;<特殊拦截:{wO.Bb}>";
                    }
                    count = await SnPackBusiness.GetFirstCheck21(param.SnCode, moduleid).ConfigureAwait(false);
                    if (count > 0)
                    {
                        return $"{result};<首件产品>";
                    }
                    count = await SnPackBusiness.GetGoldCheck21(param.SnCode, moduleid).ConfigureAwait(false);
                    if (count > 0)
                    {
                        return $"{result};<WB补线>";
                    }

                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + ex.StackTrace + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnSortParam)));
            }
            return result;
        }
        private async Task<string> SnPackCheckForMOD(string moduleid, string customer, string snCode, string partSpec, string cartonNo, string ItemCode = "")
        {
            string result = "OK";
            IEnumerable<TestModData> allTestModDatas = await SnPackBusiness.GetAllModData57(moduleid, partSpec).ConfigureAwait(false);
            if (allTestModDatas == null || !allTestModDatas.Any())
            {
                return "NG;<无测试数据混料>";
            }
            //IEnumerable<TestModData> testModDatas = await SnPackBusiness.GetModData57(moduleid, partSpec, 1).ConfigureAwait(false);
            var testModDatas = allTestModDatas.Where(x => x.IsValid == "1");
            if (testModDatas == null || !testModDatas.Any())
            {
                return "NG;<无有效测试数据>";
            }
            var count = testModDatas.Where(x => x.Rcard == "FA-000000000000000").Count();
            if (count > 0)
            {
                return "NG;<分析工单>;先重工失效";
            }
            count = testModDatas.Where(x => x.Result == "NG").Count();
            if (count > 0)
            {
                return "NG;<不良品管控>;";
            }
            if (!"小米".Equals(customer, StringComparison.Ordinal) && cartonNo.StartsWith("S0", StringComparison.Ordinal))
            {
                if (allTestModDatas.Where(x => !string.IsNullOrEmpty(x.Rcard) && x.Rcard.StartsWith("X0", StringComparison.Ordinal)).Any())
                {
                    return "NG;<正常品箱号存在修复品>";
                }
            }
            var router2Ops = await SnPackBusiness.GetTestRouterOpCode57(moduleid, partSpec).ConfigureAwait(false);
            if (router2Ops == null || !router2Ops.Any())
            {
                return "NG;<无测试流程工序>";
            }
            DateTime firstDateTime;
            var ModdateTime = testModDatas.Where(x => x.OpCode != "点污加测" && !x.OpCode.StartsWith("IPQC", StringComparison.Ordinal));
            if (ModdateTime.Any())
            {
                firstDateTime = ModdateTime.Max(x => x.TestTime);
            }
            else
            {
                return "NG;<时间管控>;<该模组无法获取最后测试时间>";
            }
            var LastdateTime = testModDatas.Where(x => x.OpCode == router2Ops.First().Op_Code).ToList();
            if (LastdateTime.Count <= 0)
            {
                return "NG;<时间管控>;<该模组无法获取最后工序测试记录>";
            }
            if (firstDateTime != LastdateTime[0].TestTime)
            {
                return "NG;<时间管控>;<最后一次测试时间的工序不是工艺流程最后一道>";
            }
            //if (!ItemCode.StartsWith("IOT", StringComparison.Ordinal))
            //{
            //    var value = await SnPackBusiness.GetMod904(LastdateTime[0].Id).ConfigureAwait(false);
            //    if (value <= 0)
            //    {
            //        return "NG;<漏904测试项>";
            //    }
            //}
            foreach (var router2Op in router2Ops)
            {
                //if (!router2Op.Router_Code.Contains(partSpec, StringComparison.Ordinal)) 
                //{
                //    return "NG;<混料>;";
                //}
                if (router2Op.Op_Code.Equals("调焦", StringComparison.Ordinal) && cartonNo.StartsWith("SW", StringComparison.Ordinal))
                {
                    continue;
                }
                count = testModDatas.Where(x => x.Result == "OK" && x.OpCode == router2Op.Op_Code).Count();
                if (partSpec == "C0MS02")
                {
                    //var Wo = testModDatas.Where(x => x.Result == "OK" && x.PartSpec == "C0MS02" && x.IsValid == "1"  || x.Rcard.Substring(0, 3)!="SR1").Count();
                    var IPQC = testModDatas.Where(x => x.Result == "OK" && x.OpCode == "IPQC").Count();
                    // if (Wo <= 0)
                    //{
                    if (IPQC > 0)
                    {
                        return $"NG;<IPQC拦截>;{"IPQC拦截"}";
                    }
                    //  }
                    else
                    {
                        if (count <= 0)
                        {
                            return $"NG;<工站漏测>;{router2Op.Op_Code}漏测";
                        }
                    }
                }
                else
                {
                    if (count <= 0)
                    {
                        return $"NG;<工站漏测>;{router2Op.Op_Code}漏测";
                    }
                }
            }
            return result;
        }
        private async Task<string> SnPackCheckForFP(string moduleid, string customer, string snCode, string partSpec, string cartonNo)
        {
            string result = "OK";
            var allTestModDatas = await SnPackBusiness.GetAllFPData21(moduleid, partSpec).ConfigureAwait(false);
            if (allTestModDatas == null || !allTestModDatas.Any())
            {
                return "NG;<无测试数据混料>";
            }
            //var testModDatas = await SnPackBusiness.GetFPData21(moduleid, partSpec).ConfigureAwait(false);
            var testModDatas = allTestModDatas.Where(x => x.IsValid == "1");
            if (testModDatas == null || !testModDatas.Any())
            {
                return "NG;<无有效测试数据>";
            }
            var count = testModDatas.Where(x => x.Rcard == "FA-000000000000000").Count();
            if (count > 0)
            {
                return "NG;<分析工单>;先重工失效";
            }
            count = testModDatas.Where(x => x.Result == "NG").Count();
            if (count > 0)
            {
                return "NG;<不良品管控>;";
            }
            if (cartonNo.StartsWith("S0", StringComparison.Ordinal))
            {
                if (allTestModDatas.Where(x => !string.IsNullOrEmpty(x.Rcard) && x.Rcard.StartsWith("X0", StringComparison.Ordinal)).Any())
                {
                    return "NG;<正常品箱号存在修复品>";
                }
            }
            var router2Ops = await SnPackBusiness.GetTestRouterOpCode21(moduleid, partSpec).ConfigureAwait(false);
            if (router2Ops == null || !router2Ops.Any())
            {
                return "NG;<无测试流程工序>";
            }
            DateTime firstDateTime;
            var ModdateTime = testModDatas.Where(x => x.OpCode != "点污加测" && !x.OpCode.StartsWith("IPQC", StringComparison.Ordinal));
            if (ModdateTime.Any())
            {
                firstDateTime = ModdateTime.Max(x => x.TestTime);
            }
            else
            {
                return "NG;<时间管控>;<该模组无法获取最后测试时间>";
            }
            var LastdateTime = testModDatas.Where(x => x.OpCode == router2Ops.OrderByDescending(a => a.Op_Seq).First().Op_Code).ToList();
            if (LastdateTime.Count <= 0)
            {
                return "NG;<时间管控>;<该模组无法获取最后一道工序测试记录>";
            }
            if (firstDateTime != LastdateTime[0].TestTime)
            {
                return "NG;<时间管控>;<最后一次测试时间的工序不是工艺流程最后一道>";
            }
            foreach (var router2Op in router2Ops)
            {
                //if (!router2Op.Router_Code.Contains(partSpec, StringComparison.Ordinal))
                //{
                //    return "NG;<混料>;";
                //}
                if (router2Op.Op_Code.Equals("调焦", StringComparison.Ordinal) && cartonNo.StartsWith("SW", StringComparison.Ordinal))
                {
                    continue;
                }
                count = testModDatas.Where(x => x.Result == "OK" && x.OpCode == router2Op.Op_Code).Count();
                if (count <= 0)
                {
                    return $"NG;<工站漏测>;{router2Op.Op_Code}漏测";
                }
            }
            return result;
        }
        private async Task<string> SnPackCheckForOld(string moduleid, string customer, string snCode, string partSpec, string cartonNo)
        {
            string result = "OK";
            var router2Ops = await SnPackBusiness.GtTestRouterOpCode21Old(moduleid).ConfigureAwait(false);
            if (router2Ops == null || !router2Ops.Any())
            {
                return "NG;<测试流程空>";
            }
            var afAwbDatas = await SnPackBusiness.GetTestDataAFAWB21(moduleid).ConfigureAwait(false);
            if (afAwbDatas == null || !afAwbDatas.Any())
            {
                return "NG;<无测试数据>";
            }
            var count = afAwbDatas.Where(x => !x.Judge.Equals("OK")).Count();
            if (count > 0)
            {
                return "NG;<不良品管控>;";
            }
            foreach (var router2Op in router2Ops)
            {
                count = afAwbDatas.Where(x => x.Judge == "OK" && x.OpCode == router2Op.Op_Code).Count();
                if (count <= 0)
                {
                    return $"NG;<工站漏测>;{router2Op.Op_Code}漏测";
                }
                if (router2Op.Op_Code.Equals("OTP烧录"))
                {
                    var cyShading = await SnPackBusiness.GetTestDataCYShading(moduleid).ConfigureAwait(false);
                    if (cyShading == null || !cyShading.Any())
                    {
                        return $"NG;<工站漏测>;YSHADING&COLORSHADING漏测";
                    }
                    count = cyShading.Where(x => x.Judge.Equals("OK") && x.OpCode.Equals("YSHADING")).Count();
                    if (count == 0)
                    {
                        return $"NG;<工站漏测>;YSHADING漏测";
                    }
                    count = cyShading.Where(x => x.Judge.Equals("OK") && x.OpCode.Equals("COLORSHADING")).Count();
                    if (count == 0)
                    {
                        return $"NG;<工站漏测>;COLORSHADING漏测";
                    }
                }
            }
            var afLines = await SnPackBusiness.GetTestDataAFLine21(moduleid).ConfigureAwait(false);
            if (afLines.Any())
            {
                count = afLines.Where(x => x.Judge.Equals("OK")).Count();
                if (count == 0)
                {
                    return $"NG;<工站漏测>;线性测试NG";
                }
            }
            var ModdateTime = afAwbDatas.Where(x => !x.OpCode.Contains("点污加测") && !x.OpCode.StartsWith("IPQC")).Max(x => x.TestTime);
            var LastdateTime = afAwbDatas.Where(x => x.OpCode == router2Ops.First().Op_Code).ToList();
            if (LastdateTime.Count <= 0)
            {
                return "NG;<时间管控>;<该模组无法获取最后一道工序测试记录>";
            }
            if (ModdateTime != LastdateTime[0].TestTime)
            {
                return "NG;<时间管控>;<最后一次测试时间的工序不是工艺流程最后一道>";
            }
            //双摄判断
            count = await SnPackBusiness.GetAaBomByCarton(cartonNo).ConfigureAwait(false);
            if (count > 0)
            {
                if (partSpec != "S0213AK-S" && partSpec != "S0512AQ-S" && partSpec != "S0212AD" && partSpec != "S0512AR-S" &&
                    partSpec != "S0512BA-S" && partSpec != "S0512BB-S" && partSpec != "S0212AB" && partSpec != "S0212AA" &&
                    partSpec != "S0212AC" && partSpec != "S0512BE-S")
                {
                    var dualRouter = await SnPackBusiness.GetDualModuleidRouter(moduleid).ConfigureAwait(false);
                    if (!dualRouter.Any())
                    {
                        return "NG;<双摄流程空>";
                    }
                    var secondModuleid = await SnPackBusiness.GetSecondDualModuleid(moduleid).ConfigureAwait(false);
                    if (secondModuleid.Count() > 1)
                    {
                        return "NG;<绑定多副摄ID>";
                    }
                    var secondMod = secondModuleid.ToList()[0];
                    var secondPartSpec = await SnPackBusiness.GetSecondDualPartSpec(secondMod).ConfigureAwait(false);
                    if (secondPartSpec.Count() > 1)
                    {
                        return "NG;<绑定多副摄机型>";
                    }
                    if (!secondPartSpec.Any())
                    {
                        var secondRouter2Ops = await SnPackBusiness.GtTestRouterOpCode21Old(secondMod).ConfigureAwait(false);
                        if (secondRouter2Ops == null || !secondRouter2Ops.Any())
                        {
                            return "NG;<副摄测试流程空>";
                        }
                        var secondafAwbDatas = await SnPackBusiness.GetTestDataAFAWB21(secondMod).ConfigureAwait(false);
                        if (secondafAwbDatas == null || !secondafAwbDatas.Any())
                        {
                            return "NG;<副摄无测试数据>";
                        }
                        count = secondafAwbDatas.Where(x => !x.Judge.Equals("OK")).Count();
                        if (count > 0)
                        {
                            return "NG;<副摄不良品管控>;";
                        }
                        foreach (var router2Op in secondRouter2Ops)
                        {
                            count = secondafAwbDatas.Where(x => x.Judge == "OK" && x.OpCode == router2Op.Op_Code).Count();
                            if (count <= 0)
                            {
                                return $"NG;<副摄工站漏测>;{router2Op.Op_Code}漏测";
                            }
                            if (router2Op.Op_Code.Equals("OTP烧录"))
                            {
                                var cyShading = await SnPackBusiness.GetTestDataCYShading(secondMod).ConfigureAwait(false);
                                if (cyShading == null || !cyShading.Any())
                                {
                                    return $"NG;<副摄工站漏测>;YSHADING&COLORSHADING漏测";
                                }
                                count = cyShading.Where(x => x.Judge.Equals("OK") && x.OpCode.Equals("YSHADING")).Count();
                                if (count == 0)
                                {
                                    return $"NG;<副摄工站漏测>;YSHADING漏测";
                                }
                                count = cyShading.Where(x => x.Judge.Equals("OK") && x.OpCode.Equals("COLORSHADING")).Count();
                                if (count == 0)
                                {
                                    return $"NG;<副摄工站漏测>;COLORSHADING漏测";
                                }
                            }
                            afLines = await SnPackBusiness.GetTestDataAFLine21(secondMod).ConfigureAwait(false);
                            if (afLines.Any())
                            {
                                count = afLines.Where(x => x.Judge.Equals("OK")).Count();
                                if (count == 0)
                                {
                                    return $"NG;<副摄工站漏测>;线性测试NG";
                                }
                            }
                        }
                    }
                    else
                    {
                        var secondResult = await SnPackCheckForMOD(secondMod, customer, snCode, secondPartSpec.ToList()[0], cartonNo).ConfigureAwait(false);
                        if (secondResult.Contains("NG"))
                        {
                            return $"NG;<副摄工站>;{secondResult}";
                        }
                    }
                    var dualData = await SnPackBusiness.GetTestDualData(moduleid).ConfigureAwait(false);
                    foreach (var dualRouter2op in dualRouter)
                    {
                        count = dualData.Where(x => x.Judge == "OK" && x.OpCode == dualRouter2op.Op_Code).Count();
                        if (count <= 0)
                        {
                            return $"NG;<工站漏测>;{dualRouter2op.Op_Code}漏测";
                        }
                    }
                }
            }
            return result;
        }
        private async Task<string> SnABSpecSort(string moduleid, string partSpec)
        {
            string result = "A;";
            #region item
            Dictionary<string, double> dir = new Dictionary<string, double>();
            dir.Add("1728_1728_H", 60);
            dir.Add("1728_1728_V", 60);
            dir.Add("2880_1728_H", 60);
            dir.Add("2880_1728_V", 60);
            dir.Add("1152_864_H", 36);
            dir.Add("1152_864_V", 36);
            dir.Add("3456_864_H", 36);
            dir.Add("3456_864_V", 36);
            dir.Add("3456_2592_H", 36);
            dir.Add("3456_2592_V", 36);
            dir.Add("1152_2592_H", 36);
            dir.Add("1152_2592_V", 36);
            dir.Add("691_518_H", 24);
            dir.Add("691_518_V", 24);
            dir.Add("3917_518_H", 24);
            dir.Add("3917_518_V", 24);
            dir.Add("3917_2938_H", 24);
            dir.Add("3917_2938_V", 24);
            dir.Add("691_2938_H", 24);
            dir.Add("691_2938_V", 24);
            dir.Add("2304_1728_H", 65);
            dir.Add("2304_1728_V", 65);
            dir.Add("0.3视场_左上X", 63);
            dir.Add("0.3视场_左上Y", 63);
            dir.Add("0.3视场_右上X", 63);
            dir.Add("0.3视场_右上Y", 63);
            dir.Add("0.3视场_左下X", 63);
            dir.Add("0.3视场_左下Y", 63);
            dir.Add("0.3视场_右下X", 63);
            dir.Add("0.3视场_右下Y", 63);
            dir.Add("0.5视场_左上X", 36);
            dir.Add("0.5视场_左上Y", 36);
            dir.Add("0.5视场_右上X", 36);
            dir.Add("0.5视场_右上Y", 36);
            dir.Add("0.5视场_左下X", 36);
            dir.Add("0.5视场_左下Y", 36);
            dir.Add("0.5视场_右下X", 36);
            dir.Add("0.5视场_右下Y", 36);
            dir.Add("0.7视场_左上X", 24);
            dir.Add("0.7视场_左上Y", 24);
            dir.Add("0.7视场_右上X", 24);
            dir.Add("0.7视场_右上Y", 24);
            dir.Add("0.7视场_左下X", 24);
            dir.Add("0.7视场_左下Y", 24);
            dir.Add("0.7视场_右下X", 24);
            dir.Add("0.7视场_右下Y", 24);
            dir.Add("中心X方向", 65);
            dir.Add("中心Y方向", 65);
            #endregion
            var testFpDataDetails = await SnPackBusiness.GetTestModDataDetail(moduleid, partSpec).ConfigureAwait(false);
            if (testFpDataDetails == null || !testFpDataDetails.Any())
            {
                return "NG;无测试项数据";
            }
            foreach (var key in dir)
            {
                var value = testFpDataDetails.Where(x => x.testSubItem == key.Key).ToList();
                if (value.Count == 0)
                {
                    return "NG;测试项不全";
                }
                if (value.Min(x => Convert.ToDouble(x.subItemTestValue)) < key.Value)
                {
                    return "OK;";
                }
            }
            return result;
        }
        /// <summary>
        /// 云烧录项目check数据回传结果
        /// </summary>
        /// <param name="sncode"></param>
        /// <param name="moduleid"></param>
        /// <param name="partSpec"></param>
        /// <returns></returns>
        public async Task<string> CheckOtpTimeAsync(string sncode, string moduleid, string partSpec)
        {
            string result = "OK";
            DataStatusParam dataStatus = new DataStatusParam();
            ModuleidData data = new ModuleidData() { ModuleId = moduleid, ProductName = partSpec };
            List<ModuleidData> datas = new List<ModuleidData>();
            long sendTime = 0;
            datas.Add(data);
            dataStatus.Data = datas;
            using (var client = _httpClientFactory.CreateClient())
            {
                using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(dataStatus), Encoding.UTF8, "application/json"))
                {
                    using (var response = await client.PostAsync(new Uri("http://dsx.qtech.com:8080/cldapi/prod/GetDataStatus"), httpContent).ConfigureAwait(false))
                    {
                        var res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        //DataStatusResponse dataStatusResponse = JsonConvert.DeserializeObject<DataStatusResponse>(result);
                        DataStatusResponse dataStatusResponse = System.Text.Json.JsonSerializer.Deserialize<DataStatusResponse>(res);
                        if (dataStatusResponse.Result == "OK")
                        {
                            if (dataStatusResponse.Detail == null || dataStatusResponse.Detail.Data == null || dataStatusResponse.Detail.Data.Count == 0 || !dataStatusResponse.Detail.Data[0].Status)
                            {
                                result = "NG;<产品烧录未上传FP>";
                            }
                            else
                            {
                                ModuleidDataStatus moduleidData = dataStatusResponse.Detail.Data[0];
                                if (moduleidData.Status)
                                {
                                    sendTime = moduleidData.SendTime;
                                }
                                else
                                {
                                    result = "NG;<产品烧录未上传FP>";
                                }
                            }
                        }
                        else
                        {
                            result = "NG;<接口异常>";
                        }
                    }
                }
            }
            await SnPackBusiness.InsertOtpLogAsync(moduleid, result, sendTime, sncode).ConfigureAwait(false);
            return result;
        }
        private async Task<string> F3L6MAZSnSpec(string moduleid, string partSpec)
        {
            string result = "OK;<A规>";
            var value = await SnPackBusiness.C0MA11GetTestModDataDetail(moduleid, partSpec).ConfigureAwait(false);
            bool isA = false;
            foreach (var v in value)
            {
                double d = 0;
                if (v.testSubItem.StartsWith("0.850", StringComparison.Ordinal))
                {
                    _ = double.TryParse(v.subItemTestValue, out d);
                    if (d < 20)
                    {
                        return "B规";
                    }
                }
            }
            foreach (var v in value)
            {
                double d = 0;
                if (v.testItem == "T05" && v.testSubItem.Equals("OIS_X", StringComparison.Ordinal))
                {
                    _ = double.TryParse(v.subItemTestValue, out d);
                    if (d < 26)
                    {
                        return "OK";
                    }
                }
                else if (v.testItem == "T06" && v.testSubItem.Equals("OIS_Y", StringComparison.Ordinal))
                {
                    _ = double.TryParse(v.subItemTestValue, out d);
                    if (d < 26)
                    {
                        return "OK";
                    }
                }
                else if (v.testSubItem.StartsWith("0.850", StringComparison.Ordinal))
                {
                    _ = double.TryParse(v.subItemTestValue, out d);
                    if (d < 40)
                    {
                        return "OK";
                    }
                }
            }
            return result;
        }
        private async Task<string> S0LS02SnSpec(string moduleid, string partSpec)
        {
            string result = "OK";
            var value = await SnPackBusiness.S0LS02GetTestModDataDetail(moduleid, partSpec).ConfigureAwait(false);
            if (value > 0)
            {
                return "OK;<新固件27304>";
            }
            else
                return result;
        }
        //private async Task<string> C0MA11SnABSpecSort(string moduleid, string partSpec)
        //{
        //    string result = "A;";
        //    var value = await SnPackBusiness.C0MA11GetTestModDataDetail(moduleid, partSpec).ConfigureAwait(false);
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        return "NG;无测试项数据";
        //    }
        //    double d;
        //    _ = double.TryParse(value, out d);
        //    if (d > 0.2 && d <= 0.3)
        //    {
        //        return result;
        //    }
        //    else if (d > 0.3 && d <= 0.34)
        //    {
        //        return "NG;B规";
        //    }
        //    else if (d > 0.34 && d <= 0.45)
        //    {
        //        return "NG;C规";
        //    }
        //    else 
        //    {
        //        return "NG;不在范围";
        //    }            
        //}
        /// <summary>
        /// 流程卡检查
        /// </summary>
        /// <param name="二维码/流程卡"></param>
        /// <returns></returns>
        public async Task<string> SnRcarCheck(SnRcardParam param)
        {
            string result;
            try
            {
                ModuleidPackResult moduleidPackResult = await SnPackBusiness.GetModuleiPackdBySn(param.SnCode).ConfigureAwait(false);
                if (moduleidPackResult == null || string.IsNullOrEmpty(moduleidPackResult.Moduleid))
                {
                    return "NG;<无关联模组>";
                }
                // int count = await SnPackBusiness.GetSnExistsPackResult(param.SnCode).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(moduleidPackResult.IS_VALID))
                {
                    return "NG;<重码扫描>";
                }
                PartSpecClientModel partSpec = await SnPackBusiness.GetRcardPartSpec(param.Rcard).ConfigureAwait(false);
                if (partSpec == null || string.IsNullOrEmpty(partSpec.Customer) || string.IsNullOrEmpty(partSpec.PartSpec))
                {
                    return "NG;<流程卡找不到客户和机种>";
                }
                int count = await SnPackBusiness.GetNewSoftExists(partSpec.PartSpec).ConfigureAwait(false);
                if (partSpec.Customer.Equals("华为") && !partSpec.PartSpec.Equals("FH259AE-S") && !partSpec.PartSpec.Equals("FH259AG"))
                {
                    result = await SnPackCheckForFP(moduleidPackResult.Moduleid, partSpec.Customer, param.SnCode, partSpec.PartSpec, param.Rcard).ConfigureAwait(false);
                    if (result.Equals("OK", StringComparison.Ordinal) &&
                        (partSpec.PartSpec.Equals("C9EF02", StringComparison.Ordinal)
                        || partSpec.PartSpec.Equals("C0EF16", StringComparison.Ordinal)))
                    {
                        result = await CheckOtpTimeAsync(param.SnCode, moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                    }
                }
                else if (count > 0)
                {
                    result = await SnPackCheckForMOD(moduleidPackResult.Moduleid, partSpec.Customer, param.SnCode, partSpec.PartSpec, param.Rcard).ConfigureAwait(false);
                }
                else
                {
                    result = await SnPackCheckForOld(moduleidPackResult.Moduleid, partSpec.Customer, param.SnCode, partSpec.PartSpec, param.Rcard).ConfigureAwait(false);
                }
                if (result.Equals("OK", StringComparison.Ordinal))
                {
                    WO wO = await SnPackBusiness.GetSpecCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                    if (wO != null && !string.IsNullOrEmpty(wO.Wo_Code))
                    {
                        return $"NG;<特殊拦截:{wO.Bb}>";
                    }
                    if (partSpec.PartSpec == "C9MA02" || partSpec.PartSpec == "F4HAYAA")
                    {
                        count = await SnPackBusiness.GetSpecCheckQrGenenation21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                        if (count > 0)
                        {
                            return "NG;<特殊拦截FPC>";
                        }
                    }
                    if (partSpec.PartSpec.Equals("C98A02"))
                    {
                        if (param.SnCode.StartsWith("554"))
                        {
                            return "NG;<板材不可互用,联系韩峰>";
                        }
                    }
                    if (partSpec.PartSpec.Equals("FGW1SAB-2", StringComparison.Ordinal))
                    {
                        result = await SnABSpecSort(moduleidPackResult.Moduleid, partSpec.PartSpec).ConfigureAwait(false);
                    }
                    else
                    {
                        count = await SnPackBusiness.GetFirstCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                        if (count > 0)
                        {
                            return $"{result};<首件产品>";
                        }
                        count = await SnPackBusiness.GetGoldCheck21(param.SnCode, moduleidPackResult.Moduleid).ConfigureAwait(false);
                        if (count > 0)
                        {
                            return $"{result};<WB补线>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError(ex.Message + ex.Source + ex.StackTrace + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnPackParam)));
            }
            return result;
        }
        /// <summary>
        /// 获取测试品最后工序
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> SnFinalOpCode(SnSortParam param)
        {
            string result;
            try
            {
                if (param == null)
                {
                    return new ResponseModel() { Code = 110, Msg = "机种/二维码空" };
                }
                if (string.IsNullOrEmpty(param.SnCode))
                {
                    return new ResponseModel() { Code = 110, Msg = "二维码空" };
                }
                if (string.IsNullOrEmpty(param.PartSpec))
                {
                    return new ResponseModel() { Code = 110, Msg = "机种空" };
                }
                string customer = await SnPackBusiness.GetCustomerByPartSpec(param.PartSpec).ConfigureAwait(false);
                if (string.IsNullOrEmpty(customer))
                {
                    return new ResponseModel() { Code = 110, Msg = "机种客户为空" };
                }
                string moduleid = await SnPackBusiness.GetModuleidBySn(param.SnCode).ConfigureAwait(false);
                if (string.IsNullOrEmpty(moduleid))
                {
                    return new ResponseModel() { Code = 110, Msg = "无关联模组" };
                }
                TestModData testModDatas;
                if (customer.Equals("华为", StringComparison.Ordinal))
                {
                    testModDatas = await SnPackBusiness.GetFPFinalData21(moduleid, param.PartSpec).ConfigureAwait(false);
                }
                else
                {
                    testModDatas = await SnPackBusiness.GetFPFinalData57(moduleid, param.PartSpec).ConfigureAwait(false);
                }
                if (testModDatas == null || string.IsNullOrEmpty(testModDatas.OpCode))
                {
                    return new ResponseModel() { Code = 110, Msg = "无测试数据" };
                }
                else
                {
                    if ("OK".Equals(testModDatas.Result, StringComparison.Ordinal))
                    {
                        return new ResponseModel() { Code = 200, Msg = testModDatas.Result };
                    }
                    else
                    {
                        return new ResponseModel() { Code = 110, Msg = testModDatas.Result };
                    }
                }
            }
            catch (Exception ex)
            {
                result = $"{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnSortParam)));
                return new ResponseModel() { Code = 110, Msg = ex.Message };
            }
        }
        private async Task<string> C0QS01SpecSort(string moduleid, string partSpec)
        {
            string result = "A;";
            var value = await SnPackBusiness.C0QS01GetTestModData(moduleid, partSpec).ConfigureAwait(false);
            if (value == null || string.IsNullOrEmpty(value.PartSpec))
            {
                return result;
            }
            else
            {
                return $"NG;{value.OpCode}-{value.NgType}";
            }
        }
        private async Task<string> C0QS03SpecSort(string moduleid, string partSpec)
        {
            string result = "OK";
            var value = await SnPackBusiness.GetC0QS03Data(moduleid).ConfigureAwait(false);
            if (value == 0)
            {
                return result;
            }
            else
            {
                return $"芯片异常";
            }
        }
        private async Task<string> C08S03ABSpecSort(string moduleid, string partSpec)
        {
            string result = "OK;";
            #region item
            Dictionary<string, double> dir = new Dictionary<string, double>();
            dir.Add("1326_694_H", 46);
            dir.Add("1326_694_V", 46);
            dir.Add("1938_694_H", 46);
            dir.Add("1938_694_V", 46);
            dir.Add("1326_1754_H", 46);
            dir.Add("1326_1754_V", 46);
            dir.Add("1938_1754_H", 46);
            dir.Add("1938_1754_V", 46);
            dir.Add("1020_1224_H", 46);
            dir.Add("1020_1224_V", 46);
            dir.Add("2244_1224_H", 46);
            dir.Add("2244_1224_V", 46);
            dir.Add("911_503_H", 44);
            dir.Add("911_503_V", 44);
            dir.Add("2353_503_H", 44);
            dir.Add("2353_503_V", 44);
            dir.Add("911_1945_H", 44);
            dir.Add("911_1945_V", 44);
            dir.Add("2353_1945_H", 44);
            dir.Add("2353_1945_V", 44);
            dir.Add("622_214_H", 40);
            dir.Add("622_214_V", 40);
            dir.Add("2642_214_H", 40);
            dir.Add("2642_214_V", 40);
            dir.Add("622_2234_H", 40);
            dir.Add("622_2234_V", 40);
            dir.Add("2642_2234_H", 40);
            dir.Add("2642_2234_V", 40);
            dir.Add("1632_1224_H", 53);
            dir.Add("1632_1224_V", 53);
            dir.Add("119_612_H", 35);
            dir.Add("119_612_V", 35);
            dir.Add("3145_612_H", 35);
            dir.Add("3145_612_V", 35);
            dir.Add("119_1836_H", 35);
            dir.Add("119_1836_V", 35);
            dir.Add("3145_1836_H", 35);
            dir.Add("3145_1836_V", 35);
            #endregion
            var testFpDataDetails = await SnPackBusiness.GetTestModDataDetail(moduleid, partSpec, "T01").ConfigureAwait(false);
            if (testFpDataDetails == null || !testFpDataDetails.Any())
            {
                return "NG;无测试项数据";
            }
            foreach (var key in dir)
            {
                var value = testFpDataDetails.Where(x => x.testSubItem == key.Key).ToList();
                if (value.Count == 0)
                {
                    return "NG;测试项不全";
                }
                if (value.Min(x => Convert.ToDouble(x.subItemTestValue)) < key.Value)
                {
                    return "NG;B规";
                }
            }
            return result;
        }
        private async Task<string> F4H7YAZSpecSort(string moduleid, string partSpec)
        {
            string result = "OK";
            var value = await SnPackBusiness.GetF4H7YAZData(moduleid, partSpec).ConfigureAwait(false);
            if (value == 0)
            {
                return result;
            }
            else
            {
                return $"特殊挑选";
            }
        }
        /// <summary>
        /// 比对修复工单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> SnLinkWo(SnSortParam param)
        {
            string result;
            try
            {
                if (param == null)
                {
                    return new ResponseModel() { Code = 110, Msg = "工单/二维码空" };
                }
                if (string.IsNullOrEmpty(param.SnCode))
                {
                    return new ResponseModel() { Code = 110, Msg = "二维码空" };
                }
                if (string.IsNullOrEmpty(param.PartSpec))
                {
                    return new ResponseModel() { Code = 110, Msg = "工单空" };
                }
                TestModData testMod;
                var customer = await RDSoftBusiness.GetCustomerBySn(param.SnCode).ConfigureAwait(false);
                if (customer == null)
                {
                    return new ResponseModel() { Code = 110, Msg = "未找到SN机型" };
                }
                if (customer == null || customer.Customer.Equals("华为", StringComparison.Ordinal))
                {
                    testMod = await RDSoftBusiness.GetFPFinalData21(customer.MaterialCode, customer.PartSpec).ConfigureAwait(false);
                }
                else
                {
                    testMod = await RDSoftBusiness.GetFPFinalData57(customer.MaterialCode, customer.PartSpec).ConfigureAwait(false);
                }
                if (testMod == null || string.IsNullOrEmpty(testMod.Rcard))
                {
                    return new ResponseModel() { Code = 110, Msg = "无关联模组" };
                }
                if (!testMod.Rcard.StartsWith(param.PartSpec, StringComparison.Ordinal))
                {
                    return new ResponseModel() { Code = 110, Msg = "工单不匹配" };
                }
                return new ResponseModel() { Code = 200, Msg = "OK" };
            }
            catch (Exception ex)
            {
                result = $"{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnSortParam)));
                return new ResponseModel() { Code = 110, Msg = ex.Message };
            }
        }
        /// <summary>
        /// 根据模组ID失效数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> InvalidTestResultByModuleid(SnInvlidParam param)
        {
            string result;
            try
            {
                if (param == null || string.IsNullOrEmpty(param.SnCode) || string.IsNullOrEmpty(param.PartSpec))
                    return "NG;参数空";
                var customer = await SnPackBusiness.GetCustomerByPartSpec(param.PartSpec).ConfigureAwait(false);
                if (string.IsNullOrEmpty(customer))
                    return "NG;机型找不到客户";
                var count = await SnPackBusiness.GetNewSoftExists(param.PartSpec).ConfigureAwait(false);
                if (customer.Equals("华为") && !param.PartSpec.Equals("FH259AE-S") && !param.PartSpec.Equals("FH259AG"))
                {
                    result = await SnPackBusiness.InvalidTestResultForFP(param.SnCode, param.SnCode, param.PartSpec, param.StationId, param.isInvlidSnLink).ConfigureAwait(false);
                }
                else if (count > 0)
                {
                    result = await SnPackBusiness.InvalidTestResultForMOD(param.SnCode, param.SnCode, param.PartSpec, param.StationId, param.isInvlidSnLink).ConfigureAwait(false);
                }
                else
                {
                    result = await SnPackBusiness.InvalidTestResultForOld(param.SnCode, param.SnCode, param.PartSpec, param.StationId).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError(result + System.Text.Json.JsonSerializer.Serialize(param, typeof(SnInvlidParam)));
            }
            return result;
        }
    }
}
