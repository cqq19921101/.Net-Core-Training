using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QtechCoreAPI
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AsnOtpController : ControllerBase
    {
        private const string appID = "com.huawei.ipd.tplm.thec.supplier.z00q8f";
        private const string appToken = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCELDhDfBQbVY0m1nnTFAGdye6KPXhthltDM8KyvirRelcZMEa0lb0lKSTh9nQbWEtFGroOx7zVJ4mF93nmxLpDuXIx9Gl7rnw50zgSVdVQTQa+WNrRw0AlD+ai7PtYJNGA6oBcyCrf4TWjP9v9h/3RdXV5LDSoFd80b3kchfSUzPt5uWhEw08dnW61kst/bJivxcRTjkJa8kHXYkLwMr8wRayC5yWU4Uk4l05hQNh2CB9fmdPkAMhyE5QhkGD9bMLkiSW8SY9N9DMXCTtidSOHZPM8L2wqk/SJ30qxyDrRvs4XFy5CWx3CeZ3M0BUwdl8Mrz9Emgxi8cjJcciqALM5AgMBAAECggEAW52XJlf0S/oiDCCwcvo0IJ5fBV2Ugz6VNeDD/UfJwEfv5ZrL99KvN/nsAeDmHCo4il77RtF2d72fGsgurCIDBnydxjingT6yBxb8j+EFKlMp8HkAvFN9u/LoqFhh5Hgo7BYgAbAR3/toRY83Ouavrief1O0qVrRKcjsulPHZVeY9CAPihRlvTfFs0y2meAxrkwN8fOAkl9Xa/72P8Jr3eqSF6VUp1GoRFQiYdEfa9mtK0HvBFWKORRt5yAWRIWi0HZJ4reVOupwTYx0clDz73PwULWEbBxqojxKCZGMKR9RpifwdRcs5zfRuaC6arAr3hFsPXE4C50hd3bXAp1+HAQKBgQC55jm9tLcD1l3xDF7HDL6EOhXMWR+xjz0G5rEOfg00uQJXK0O39drsePT8tlFpzBpBdO5J2R7GEe455ESX3p/5IPw7F1+7QskSQu8nFCf66Apb57jDifwWbg+LVvd12WWxYuPuPE8kf2YN4w/UU0obwV/grC73rAelA1XX2XMC4QKBgQC2A4DHcErvX7OcIkWS3LvGrtMTEM57vfAmQT9PLenfZM6ZJ3MUwMmGczg6xY+VcfZhctL6chpnonWSwRQImnhWp0BxTOTX01ElD0Va+7eD6pe2QSetEo3q2KAulN5+V5mAE5qlsVI1yn59+76rr2vGxyI+IiJsHj56gKHbz44TWQKBgQCGiyHvMwPiOxvygcl2trqMaJ2uMDLIB6tbe0tZ+dEE/4G6JQ317nJsA38MB8gUkivOhBqSSBoFsKSCMbuTTwTpAf6LpfcbSNLN7LO5zNTCu34D1kna+9r/QNjvOBoRqS34nGHq4qtE9w+drWCfg0h3nhUOPpAfoNpHi4ptlmuvAQKBgQCGaJfUr2vm0PlFt1lBVpej5L4iOisA3gZVeaFSy0kFmt+l4afuD1/CVZuUawfZCEs1MyHxqAkvtpy0PriYkdmDxAampG3pozcs8tX4liGy3K4j/IFcIXODlWzUeULEcdP87H0rFoLrDhnS1vC6v9QK+kqjL3nT9ZGVzR+5vs/gIQKBgFLy926Zvks7Vx1ja9nBnUC/5BuY3qYquk9kdD8GTySzKzl1NqTuPfqUyAAh6x2p2VvDymNWEvL45RahYXkTvhlJKa70df4pEOFpympAdeQzphF6E1xKaO3QHpJ/gmPFsM8+Amxjmt/oTw3SDJAV+3HJ17KmlYmDZ+B6JTMedt2M";
        //测试Token
        //private readonly static string appToken = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCWTsnFXjFpdtmqB7rA+tDPjOdc7lm3mQrFi/Hv5xUlFtaJS7G9Q5tTlFCOvmAyNfVmpAnKrOTjRw9V054Ss7WANKZUYSM87iY/iZLEcBarDKwoE0GPIktJn/cJlNaZAnZW3pGk+bMkMcsihT0YasmeoCH/C8AesgLzwY3hGA5itNohC7LX3tYJhypIQILDgsP38eaj1em9itdTObVWZnnckQG/lZV4ar2M00DjBsVoDFNO1W+aZOin3LumZtVsM2IB5tlvXtZMGreI2ca5+eZbqb79z1Fcldb0ZKCjyZlFKQ/KMCYTe0PzzEcenjJ62IJFWIBVrt/Nt23Eztg5qlVbAgMBAAECggEAS/B8gooHXuhcKkqDcTKHQwVGcy49B8R7q7j7wCA5D6cNNwqWPzAkDsMVPDk08slONdBU3iAh+C4TsmIbMAm5fo+bJXB6VDNiAaHpE/Qpj7v0Ur7/1WgXL5hxQFcBYQbccSE75mT9vCCoJ8W3S1nQiaMP6gKuyxNW6H52xy3XPsinBqnlsrd0P9Q/R2nKImzUa9Yjo3CzM0jeTJXHTSRmQk5OEOPASvFeFzll0m4uQDzD2KF7/yjeQmukdA845dC0zL7leG1NWE8dNsPIpsFuA526XrYlnfh5g8oF1fkD4P7ZXZ+dH1Q2e1GFcDTjJfTdmq10FluNaG9AxMyyGG/RaQKBgQDceZfTGdGlXY+d/6LdvT6pgAc0GpPSOBKbDJ+ssNmHsXO4SDJj/LujUD2QizXah2PJV8oUD32Z5V/Jr9hNllUxylFM/0AxKpGbviyxvLsgWm0j050N24cxue3rQ1VDdT8MYL35HnzM111pMGXslqeuwH8+DGJFGyAx7BH2f+gpzwKBgQCuhttfrAM5ceQt+Ad0ZCiSjM2biboogAC/TJfSzAJ8ZUw+41hEZZzs4PJGJrh5BO1RfZYqU4AKpHGme2smugOerqsMZJf8bmBIJg433LfGOJDbbR3nZ56gbslsedCsqyUWY6XBP7uE4nY0wB0rzmcuvVHRsWRjbrXPgPUtFY5atQKBgGcsq8UJRxd40jWhJMlZv1I47KYQbe6peOOAI9T/qbybaUjq2FH8Op7xdO4Ih3otc6AMai+7FA8JzSqoFLQyTsoPIL7EkHixsBRPCcFfTb6VMo/afpu2iOzXUqSPDP6Vhpy3RQ+omcSOYFNSZ9AwrGXWBXW2++HVENrr23gcYTv3AoGASyXgMcrEPKjwomaI9zHlUmz0X0Ond6beK4DUYrzbWSrY8L89k33ZWxnk2BX2Go3njc4wqZr4qBx0n0XHNo6j3mDBX/20f8obBRX1y1Hrg5t5rDHIzYoNgHETo/SzyJWMy54ukg5aNPa6BS6voNwoC7UTFOJ4ImRaPrX9W+NLP30CgYEAuYjO8TlMAUBfDPaWXovzeRl2FWkTwiR33YtlONsejA2qTmUvz8g1yO44V+O6Pf4tT6uMpjPKW7OBD6OwV85UKZjGEByTFBDr9LlTP/3YNddy3UpsfXlVDQzTDM860KHlC4qcSquiyCor2Q7YJKg7cH7egoRByI88Tii6U7yRQ4Q=";
        private const string apiURL = "https://apigw.huawei.com/api/thecCenter/services/thec/common/pushData";
        private const string service = "/thecCenter/services/thec/common/pushData";
        private const string apiURL09 = "https://apigw.huawei.com/api/thecCenter/services/thec/cameraotp09/getSensorIdList";
        private const string service09 = "/thecCenter/services/thec/cameraotp09/getSensorIdList";

        private readonly ILogger<AsnOtpController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public AsnOtpController(ILogger<AsnOtpController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost]
        public async Task<string> TranASN(StringParam param)
        {
            string result = "OK";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return "NG;09码为空";
                int jsonCount = 1;
                DateTime dt = DateTime.Now;
                TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                List<string> pallet = new List<string>();
                using (DataTable dataTable = await AsnOtpBusiness.Get09SnPallet(param.Message).ConfigureAwait(false))
                {
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        return $"NG;{param.Message}无数据";
                    }
                    var exists = await AsnOtpBusiness.GetPalletStatus(dataTable.Rows[0][0].ToString()).ConfigureAwait(false);
                    if (exists > 0)
                    {
                        return $"NG;{param.Message}已回传";
                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        pallet.Add(dataTable.Rows[i][0].ToString());
                        DataTable dtModuleid = await AsnOtpBusiness.GetCartonModuleid(dataTable.Rows[i][0].ToString()).ConfigureAwait(false);
                        using (dtModuleid)
                        {
                            if (dtModuleid == null || dtModuleid.Rows.Count == 0)
                            {
                                return $"NG;{dataTable.Rows[i][0].ToString() }无数据";
                            }
                            else
                            {
                                if (dataTable.Rows[i][3].ToString() != dtModuleid.Rows.Count.ToString())
                                {
                                    return $"NG;{dataTable.Rows[i][0].ToString()}少数据";
                                }
                            }
                            ToJsonFP p1 = new ToJsonFP();
                            p1.dataType = "cameraOTP09VoList";
                            p1.appKey = "fe903ca34c4f6ac12bdfd3bb4000cdde";

                            List<cameraOTP09VoList> ListcameraQCVoList = new List<cameraOTP09VoList>();
                            cameraOTP09VoList p11 = new cameraOTP09VoList();
                            p11.supplierName = "丘钛微";
                            p11.factoryName = "昆山工厂";
                            p11.barCode = dataTable.Rows[i][1].ToString();
                            p11.deliveryNo = dataTable.Rows[i][2].ToString();
                            p11.deliverySendTime = long.Parse(Math.Round(ts.TotalMilliseconds).ToString());
                            p11.remark1 = p11.remark2 = "";
                            ListcameraQCVoList.Add(p11);
                            List<Moduleid> idList = new List<Moduleid>();
                            for (int j = 0; j < dtModuleid.Rows.Count; j++)
                            {
                                Moduleid moduleid = new Moduleid();
                                moduleid.sensorID = dtModuleid.Rows[j][0].ToString().PadRight(60, '0');
                                //moduleid.sendTime = long.Parse(dtModuleid.Rows[j][1].ToString());
                                idList.Add(moduleid);
                            }
                            p11.idList = idList;
                            p11.jsonCount = jsonCount.ToString();
                            p11.count = idList.Count.ToString();
                            p11.jsonTotal = dataTable.Rows.Count.ToString();
                            p1.cameraOTP09VoList = ListcameraQCVoList;
                            //string json = System.Text.Json.JsonSerializer.Serialize(p1,p1.GetType());
                            string json = JsonConvert.SerializeObject(p1);
                            var res = await sendPushData(apiURL, service, json).ConfigureAwait(false);
                            _logger?.LogError($"{param.Message}-{p11.jsonTotal}-{ p11.jsonCount}-{p11.count}-{res}");
                            AsnUploadResponse response = System.Text.Json.JsonSerializer.Deserialize<AsnUploadResponse>(res);
                            if (response.successMsg != "success")
                            {
                                return $"NG;[{dataTable.Rows[i][0].ToString()}] {response.errorMsg}";
                            }                            
                            jsonCount++;
                        }
                    }
                   await AsnOtpBusiness.UpdatePalletStatus(pallet).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}";
                _logger?.LogError($"{ ex.Message}{ ex.Source }{ ex.StackTrace}--{param.Message}");
            }
            return result;
        }
        public async Task<string> GetAsnStatus(StringParam param)
        {
            string result = "";
            try
            {
                if (param == null || string.IsNullOrEmpty(param.Message))
                    return "NG";
                string json = System.Text.Json.JsonSerializer.Serialize(new Asn() { deliveryNo = param.Message });
                result = await sendPushData(apiURL09, service09, json).ConfigureAwait(false);
                FpResponse response = System.Text.Json.JsonSerializer.Deserialize<FpResponse>(result);
                if (response.status)
                {
                    return "OK";
                }
                else 
                {
                    return $"NG;{response.errorMsg}";
                }
            }
            catch (Exception ex)
            {
                result = $"NG;{ ex.Message}{ ex.Source }{ ex.StackTrace}";
                _logger.LogError($"{result}--{param.Message}");
            }
            return result;
        }
        private async Task<string> sendPushData(string url, string serviceRouter, string postData)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                string date = DateTime.Now.ToString("yyyyMMddTHHmmssZ");
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8, "application/json"))
                {
                    httpContent.Headers.Add("X-HW-ID", appID);
                    httpContent.Headers.Add("X-HW-DATE", date);
                    httpContent.Headers.Add("X-HW-SIGN", Sha256($"{serviceRouter}|POST|{date}|{appID}|{appToken}"));
                    using (var response = await client.PostAsync(new Uri(url), httpContent).ConfigureAwait(false))
                    {
                        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally 
            {
                if (client != null)
                    client.Dispose();
            }           
        }
        private static string Sha256(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            using (SHA256 hA256 = SHA256.Create())
            {
                byte[] hash = hA256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
