using HangFire.Application.Caching.FaceImageApi;
using HangFire.Application.Contracts.ExceptionEntity;
using HangFire.Application.Contracts.FaceImageApi.Param;
using HangFire.Common.Base;
using HangFire.Common.Helper;
using HangFire.Domain.Configurations;
using HangFire.Domain.FaceImage.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static HangFire.Domain.Shared.HangFireConsts;

namespace HangFire.Application.FaceImageApi.Impl
{
    /// <summary>
    /// Implement FaceImage Service Interface
    /// </summary>
    public class FaceImageService : HangFireApplicationServiceBase, IFaceImageService
    {
        private readonly IFaceImageRepository _faceimageRepository;
        private readonly IFaceImageCacheService _faceimageCacheService;
        private readonly IHttpClientFactory _httpclientfactory;

        public FaceImageService(IFaceImageRepository faceimageRepository,
                                IFaceImageCacheService faceimageCacheService,
                                IHttpClientFactory httpclientfactory)
        {
            _faceimageRepository = faceimageRepository;
            _faceimageCacheService = faceimageCacheService;
            _httpclientfactory = httpclientfactory;
        }

        #region Serveice
        /// <summary>
        /// Get FaceImage Api Token
        /// </summary>
        /// <param name="TokenUrl"></param>
        /// <returns></returns>
        public async Task<string> GetFaceImageToken_Test(string TokenUrl)
        {
            return await _faceimageCacheService.GetFaceImageTokenCacheAsync(TokenUrl, async () =>
            {
                string result = string.Empty;
                var content = new StringContent(signatureUrl.Parameter);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                using var client = _httpclientfactory.CreateClient();
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Koala Admin");
                client.DefaultRequestHeaders.Add("Method", "POST");

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(TokenUrl));
                //request.Timeout = 30 * 1000;//设置30s的超时
                //request.ContentType = "application/json";
                //request.UserAgent = "Koala Admin";
                //request.Method = "POST";

                var temp = new
                {
                    username = AppSettings.FaceImageInterface.LoginId,
                    password = AppSettings.FaceImageInterface.LoginPsd,
                    auth_token = true
                };

                var postData = JsonConvert.SerializeObject(temp);
                byte[] data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = data.Length;
                Stream postStream = await request.GetRequestStreamAsync();
                postStream.Write(data, 0, data.Length);
                postStream.Close();
                var res = await request.GetResponseAsync() as HttpWebResponse;
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    result = await reader.ReadToEndAsync();
                    reader.Close();
                }
                request.Abort();

                JsonEntity.Root da = JsonConvert.DeserializeObject<JsonEntity.Root>(result);
                return da.data.auth_token;
            });
        }

        /// <summary>
        /// Excute Insert NewEmpAsync
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<string>> ExcuteInsertNewEmpAsync()
        {
            var result = new ServiceResult<string>();

            string Token = await GetFaceImageToken(AppSettings.FaceImageInterface.TokenUrl);

            var NewEmpLst = await _faceimageRepository.QueryNewEmployeeAsync();
            if (NewEmpLst.Any())
            {
                var input = new CreateUserInput
                {
                    Token = Token,
                    NewEmplst = NewEmpLst,
                };
                result = await CreateUploadUser(input);
            }

            return result;
        }

        /// <summary>
        /// Excute Delete Resigned EmpAsync
        /// </summary>
        /// <returns></returns>
        public async Task ExcuteDeleteResignedEmpAsync()
        {
            string result;
            var sublist = new List<string>();
            string Token = await GetFaceImageToken(AppSettings.FaceImageInterface.TokenUrl);
            var ResignEmplst = await _faceimageRepository.QueryResignedEmployeeAsync();
            if (ResignEmplst.Any())
            {
                foreach (var item in ResignEmplst)
                {
                    var input = new SubjectIdInput
                    {
                        Token = Token,
                        EmpNumber = item.EmpNumber,
                    };
                    string subjectid = await GetSubjectIdByEmpNumber(input);
                    if (subjectid != null && subjectid.Length > 0)
                    {
                        sublist.Add(subjectid);
                    }
                }

                if (sublist.Count > 0)
                {
                    foreach (string subjectid in sublist)
                    {
                        var deleteinput = new DeleteUserInput
                        {
                            SubjectId = subjectid,
                            Token = Token,
                        };
                        result = await PostDeleteResignedUser(deleteinput);
                    }
                }
            }
        }

        /// <summary>
        /// Excute Updated Emp
        /// </summary>
        /// <returns></returns>
        public async Task ExcuteUpdateEmpAsync()
        {
            string Token = await GetFaceImageToken(AppSettings.FaceImageInterface.TokenUrl);
            var UpdatedEmp = await _faceimageRepository.QueryUpdatedEmployeeAsync();
            if (UpdatedEmp.Any())
            {

            }
        }

        /// <summary>
        /// Excute Insert All Emp
        /// </summary>
        /// <returns></returns>
        public async Task ExcuteInsertAllEmp()
        {
            string Token = await GetFaceImageToken(AppSettings.FaceImageInterface.TokenUrl);
            var AllEmp = await _faceimageRepository.QueryAllEmployeeAsync();
            if (AllEmp != null)
            {

            }
        }

        #endregion

        #region Private Method
        /// <summary>
        /// Get FaceImage Api Token
        /// </summary>
        /// <param name="TokenUrl"></param>
        /// <returns></returns>
        private async Task<string> GetFaceImageToken(string TokenUrl)
        {
            return await _faceimageCacheService.GetFaceImageTokenCacheAsync(TokenUrl, async () =>
            {
                string result = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(TokenUrl));
                request.Timeout = 30 * 1000;//设置30s的超时
                request.ContentType = "application/json";
                request.UserAgent = "Koala Admin";
                request.Method = "POST";

                var temp = new
                {
                    username = AppSettings.FaceImageInterface.LoginId,
                    password = AppSettings.FaceImageInterface.LoginPsd,
                    auth_token = true
                };

                var postData = JsonConvert.SerializeObject(temp);
                byte[] data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = data.Length;
                Stream postStream = await request.GetRequestStreamAsync();
                postStream.Write(data, 0, data.Length);
                postStream.Close();
                var res = await request.GetResponseAsync() as HttpWebResponse;
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    result = await reader.ReadToEndAsync();
                    reader.Close();
                }
                request.Abort();

                JsonEntity.Root da = JsonConvert.DeserializeObject<JsonEntity.Root>(result);
                return da.data.auth_token;
            });
        }

        /// <summary>
        /// 根据当天更新过资料的工号获取对应的subjectid集合和员工实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<string>> GetUpdatedSubjectid(UpdatedUserInput input)
        {
            string subjectid;
            var result = new ServiceResult<string>();
            var sublist = new List<string>();
            foreach (Domain.FaceImage.FaceImageApi EmpItem in input.UpdatedEmp)
            {
                var subinput = new SubjectIdInput
                {
                    Token = input.Token,
                    EmpNumber = EmpItem.EmpNumber,
                };
                subjectid = await GetSubjectIdByEmpNumber(subinput);
                if (subjectid != null && subjectid.Length > 0)
                {
                    sublist.Add(subjectid);
                }
                else if (EmpItem.Ldate == null)
                {
                    var newinput = new CreateUserInput
                    {
                        Token = input.Token,
                        NewEmp = EmpItem
                    };
                    result = await CreateUploadUserSingle(newinput);
                }
            }
            return null;
        }

        /// <summary>
        /// 接口创建方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<string> PostCreateUpLoadUser(CreateUserInput input)
        {
            string ResponseResult;
            //var  result = new ServiceResult<string>();
            MemoryStream ms = new MemoryStream();
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

            //开始标识
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            //结束标识
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AppSettings.FaceImageInterface.CreateUserUrl);
            var Headers = request.Headers;
            Headers["Authorization"] = input.Token;//Token认证
            request.Method = "POST";
            request.Timeout = input.timeout;
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            // 写入图片
            var fileStream = new FileStream(input.FilePath, FileMode.Open, FileAccess.Read);
            const string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: application/octet-stream\r\n\r\n";
            var header = string.Format(filePartHeader, input.FileName, input.FilePath);
            var headerbytes = Encoding.UTF8.GetBytes(header);

            ms.Write(beginBoundary, 0, beginBoundary.Length);
            ms.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[1024];
            int bytesRead; // =0
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                ms.Write(buffer, 0, bytesRead);
            }
            var str = ms.ToArray();
            string strstr = Encoding.UTF8.GetString(str);

            // 字符串拼接
            var stringKeyHeader = "\r\n--" + boundary +
                                   "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                   "\r\n\r\n{1}\r\n";

            foreach (byte[] formitembytes in from string key in input.ParameterDictory.Keys
                                             select string.Format(stringKeyHeader, key, input.ParameterDictory[key])
                                                 into formitem
                                             select Encoding.UTF8.GetBytes(formitem))
            {
                ms.Write(formitembytes, 0, formitembytes.Length);
            }

            // 结束
            ms.Write(endBoundary, 0, endBoundary.Length);
            request.ContentLength = ms.Length;
            var requestStream = await request.GetRequestStreamAsync();
            ms.Position = 0;
            var tempBuffer = new byte[ms.Length];
            ms.Read(tempBuffer, 0, tempBuffer.Length);
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);

            var httpWebResponse = await request.GetResponseAsync() as HttpWebResponse;
            using (StreamReader StreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                            Encoding.UTF8))
            {
                ResponseResult = await StreamReader.ReadToEndAsync();
            }
            requestStream.Close();
            ms.Close();
            fileStream.Close();
            httpWebResponse.Close();
            request.Abort();

            return ResponseResult;
        }

        /// <summary>
        /// 创建用户并上传图片底库 集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult<string>> CreateUploadUser(CreateUserInput input)
        {
            string ResponseResult;
            var result = new ServiceResult<string>();
            foreach (Domain.FaceImage.FaceImageApi item in input.NewEmplst)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("subject_type", "0");
                dic.Add("group_ids", "0");
                dic.Add("extra_id", item.EmpNumber);
                dic.Add("name", item.EmpName);

                Stream stream = new MemoryStream(item.FileData);
                Bitmap img = new Bitmap(stream);
                string filepath = AppDomain.CurrentDomain.BaseDirectory + $@"\Photo\{item.EmpName}.jpg";
                img.Save(filepath);

                input = new CreateUserInput
                {
                    Token = input.Token,
                    timeout = 30000,
                    FileName = "photo",
                    FilePath = filepath,
                    ParameterDictory = dic
                };
                ResponseResult = await PostCreateUpLoadUser(input);

                ExceptionEntity.Root da = JsonConvert.DeserializeObject<ExceptionEntity.Root>(ResponseResult);
                if (da.desc != null && da.desc.Length > 0)
                {
                    if (da.desc != "唯一标识重复")
                    {
                        string ErrprPhoto = AppDomain.CurrentDomain.BaseDirectory + $@"\ErrorPhoto\{item.EmpName}.jpg";
                        if (!IOHelper.FileExists(ErrprPhoto))
                        {
                            IOHelper.CreateIfNotExists(ErrprPhoto);
                        }
                        img.Save(ErrprPhoto);
                        //Write Exception log
                        LoggerHelper.WriteErrorLog($"工号 : {item.EmpNumber} 姓名 ：{item.EmpName} 异常信息 ： {da.desc}");
                    }
                }
            }
            result.IsSuccess(ResponseText.RESPONSE_RESULT);
            return result;
        }

        /// <summary>
        /// 创建用户并上传图片底库 Single
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult<string>> CreateUploadUserSingle(CreateUserInput input)
        {
            string ResponseResult;
            var result = new ServiceResult<string>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("subject_type", "0");
            dic.Add("group_ids", "0");
            dic.Add("extra_id", input.NewEmp.EmpNumber);
            dic.Add("name", input.NewEmp.EmpName);

            Stream stream = new MemoryStream(input.NewEmp.FileData);
            Bitmap img = new Bitmap(stream);
            string filepath = AppDomain.CurrentDomain.BaseDirectory + $@"\Photo\{input.NewEmp.EmpName}.jpg";
            img.Save(filepath);

            input = new CreateUserInput
            {
                Token = input.Token,
                timeout = 30000,
                FileName = "photo",
                FilePath = filepath,
                ParameterDictory = dic
            };
            ResponseResult = await PostCreateUpLoadUser(input);

            ExceptionEntity.Root da = JsonConvert.DeserializeObject<ExceptionEntity.Root>(ResponseResult);
            if (da.desc != null && da.desc.Length > 0)
            {
                if (da.desc != "唯一标识重复")
                {
                    string ErrorPhoto = AppDomain.CurrentDomain.BaseDirectory + $@"\ErrorPhoto\{input.NewEmp.EmpName}.jpg";
                    if (!IOHelper.FileExists(ErrorPhoto))
                    {
                        IOHelper.CreateIfNotExists(ErrorPhoto);
                    }
                    img.Save(ErrorPhoto);
                    //Write Exception log
                    LoggerHelper.WriteErrorLog($"工号 : {input.NewEmp.EmpNumber} 姓名 ：{input.NewEmp.EmpName} 异常信息 ： {da.desc}");
                }
            }
            result.IsSuccess(ResponseText.RESPONSE_RESULT);
            return result;
        }

        /// <summary>
        /// Get SubjectId By EmpNumber
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<string> GetSubjectIdByEmpNumber(SubjectIdInput input)
        {
            string result;
            string url = AppSettings.FaceImageInterface.GetSubjectIDUrl;
            url = AppSettings.FaceImageInterface.GetSubjectIDUrl + $"category=employee&name=&department=&interviewee=&start_time=&end_time=&filterstr=&remark=&extra_id={input.EmpNumber}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 30 * 1000;//设置30s的超时
            request.ContentType = "application/json";
            var Headers = request.Headers;
            Headers["Authorization"] = input.Token;//Token认证
            request.Method = "GET";

            var httpWebResponse = await request.GetResponseAsync() as HttpWebResponse;
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            result = await streamReader.ReadToEndAsync();
            httpWebResponse.Close();
            streamReader.Close();

            Root da = JsonConvert.DeserializeObject<Root>(result);
            if (da.page.count > 0)
            {
                PhotosItem photoitem = da.data.FirstOrDefault().photos.FirstOrDefault();
                return photoitem.subject_id.ToString();
            }
            return null;
        }

        /// <summary>
        /// 接口删除方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<string> PostDeleteResignedUser(DeleteUserInput input)
        {
            string result;
            string url = AppSettings.FaceImageInterface.DelLeaveEmpUrl + int.Parse(input.SubjectId);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 20 * 1000;//设置30s的超时
            request.ContentType = "application/x-www-form-urlencoded";
            var Headers = request.Headers;
            Headers["Authorization"] = input.Token;//Token认证
            request.Method = "DELETE";

            HttpWebResponse httpWebResponse = await request.GetResponseAsync() as HttpWebResponse;
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            result = await streamReader.ReadToEndAsync();
            httpWebResponse.Close();
            streamReader.Close();
            return result;
        }

        #endregion
    }
}
