using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace QtechCoreAPI
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RDSoftController : ControllerBase
    {
        private readonly ILogger<FPController> _logger;
        public RDSoftController(ILogger<FPController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 测试软件类型数据接收
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> SaveSoftClassify(SoftClassify param)
        {
            if (param == null || string.IsNullOrEmpty(param.CheckNumber))
                return new ResponseModel() { Code = 101, Msg = "CheckNumber参数为空" };
            if (string.IsNullOrEmpty(param.MD5))
                return new ResponseModel() { Code = 101, Msg = "MD5参数为空" };
            if (string.IsNullOrEmpty(param.ProductName))
                return new ResponseModel() { Code = 101, Msg = "ProductName参数为空" };
            if (string.IsNullOrEmpty(param.Station))
                return new ResponseModel() { Code = 101, Msg = "Station参数为空" };
            if (string.IsNullOrEmpty(param.SwStatus))
                return new ResponseModel() { Code = 101, Msg = "SwStatus参数为空" };
            int count = await RDSoftBusiness.GetPartSpec(param.ProductName).ConfigureAwait(false);
            if (count == 0)
            {
                return new ResponseModel() { Code = 101, Msg = $"{param.ProductName}不存在" };
            }
            _logger?.LogError(System.Text.Json.JsonSerializer.Serialize(param, typeof(SoftClassify)));
            SoftClassify softClassify;
            try
            {
                softClassify = await RDSoftBusiness.GetSoftClassify(param.CheckNumber).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(SoftClassify)));
                return new ResponseModel() { Code = 500, Msg = ex.Message };
            }
            if (softClassify == null)
            {
                try
                {
                    await RDSoftBusiness.SaveSoftClassify(param).ConfigureAwait(false);
                    return new ResponseModel() { Code = 200, Msg = "OK" };
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(SoftClassify)));
                    return new ResponseModel() { Code = 500, Msg = ex.Message };
                }
            }
            else
            {
                try
                {
                    await RDSoftBusiness.UpdateSoftClassify(param).ConfigureAwait(false);
                    return new ResponseModel() { Code = 200, Msg = "OK" };
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex.Message + ex.Source + System.Text.Json.JsonSerializer.Serialize(param, typeof(SoftClassify)));
                    return new ResponseModel() { Code = 500, Msg = ex.Message };
                }
            }
        }
        /// <summary>
        /// 获取流程卡OK模组
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> GetTestModuleid(string Message)
        {
            if (string.IsNullOrEmpty(Message))
                return new ResponseModel() { Code = 101, Msg = "流程卡参数为空" };
            var customer = await RDSoftBusiness.GetCustomerByRcard(Message).ConfigureAwait(false);
            if (customer == null || customer.Customer.Equals("华为", StringComparison.Ordinal))
            {
                var data = await RDSoftBusiness.GetFPModuleidByRcard(Message).ConfigureAwait(false);
                return new ResponseModel() { Code = 200, Msg = "OK", data = data };
            }
            else
            {
                var data = await RDSoftBusiness.GetModuleidByRcard(Message).ConfigureAwait(false);
                return new ResponseModel() { Code = 200, Msg = "OK", data = data };
            }
        }
        /// <summary>
        /// 获取流程卡测试项数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> GetTestDataDetail(string Message, string SnCode)
        {
            if (string.IsNullOrEmpty(Message) && string.IsNullOrEmpty(SnCode))
                return new ResponseModel() { Code = 101, Msg = "流程卡参数为空" };
            var rcard = Message.Split(',');
            if (!string.IsNullOrEmpty(SnCode))
            {
                var customer = await RDSoftBusiness.GetCustomerBySn(rcard[0]).ConfigureAwait(false);
                if (customer == null || customer.Customer.Equals("华为", StringComparison.Ordinal))
                {
                    var data = await RDSoftBusiness.GetFPDataDateilByModuleud(customer.MaterialCode).ConfigureAwait(false);
                    return new ResponseModel() { Code = 200, Msg = "OK", data = data };
                }
                else
                {
                    var data = await RDSoftBusiness.GetDataDateilByModuleud(customer.MaterialCode).ConfigureAwait(false);
                    return new ResponseModel() { Code = 200, Msg = "OK", data = data };
                }
            }
            else
            {
                var customer = await RDSoftBusiness.GetCustomerByRcard(rcard[0]).ConfigureAwait(false);
                if (customer == null || customer.Customer.Equals("华为", StringComparison.Ordinal))
                {
                    var data = await RDSoftBusiness.GetFPDataDateilByRcard(rcard.ToList()).ConfigureAwait(false);
                    return new ResponseModel() { Code = 200, Msg = "OK", data = data };
                }
                else
                {
                    var data = await RDSoftBusiness.GetDataDateilByRcard(rcard.ToList()).ConfigureAwait(false);
                    return new ResponseModel() { Code = 200, Msg = "OK", data = data };
                }
            }
        }
        /// <summary>
        /// 获取SN模组数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetSnData(CheckStationParam param)
        {
            if (param == null || string.IsNullOrEmpty(param.Rcard) || string.IsNullOrEmpty(param.Station) || string.IsNullOrEmpty(param.Moduleid))
                return "505NG;参数为空";
            var data = await RDSoftBusiness.GetFPFinalData57(param.Moduleid, param.Rcard, param.Station).ConfigureAwait(false);
            if (data != null && data.Result == "OK")
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }
        /// <summary>
        /// 获取机型Bom
        /// </summary>
        /// <param name="PartSpec">机型</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> GetBom(string PartSpec)
        {
            if (string.IsNullOrEmpty(PartSpec))
                return new ResponseModel() { Code = 101, Msg = "机型参数为空" };
            var data = await RDSoftBusiness.GetBom21(PartSpec).ConfigureAwait(false);
            string JSONresult = JsonConvert.SerializeObject(data, Formatting.Indented);
            return new ResponseModel() { Code = 200, Msg = "OK", data = JSONresult };
        }
    }
}