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
using System.Text;
using System.Threading.Tasks;

namespace HangFire.Application.FaceImageApi.Impl
{
    /// <summary>
    /// Implement FaceImage Service Interface
    /// </summary>
    public class FaceImageService : HangFireApplicationServiceBase, IFaceImageService
    {
        private readonly IFaceImageRepository _faceimageRepository;
        private readonly IFaceImageCacheService _faceimageCacheService;

        public FaceImageService(IFaceImageRepository faceimageRepository,
                                    IFaceImageCacheService faceimageCacheService)
        {
            _faceimageRepository = faceimageRepository;
            _faceimageCacheService = faceimageCacheService;
        }

        /// <summary>
        /// Get FaceImage Api Token
        /// </summary>
        /// <param name="TokenUrl"></param>
        /// <returns></returns>
        public async Task<string> GetFaceImageToken(string TokenUrl)
        {
            return await _faceimageCacheService.GetFaceImageTokenCacheAsync(TokenUrl , async() =>
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
        /// Get SubjectId By EmpNumber
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> GetSubjectIdByEmpNumber(SubjectIdInput input)
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
        /// 创建用户并上传图片底库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CreateUploadUser(CreateUserInput input)
        {
            throw new NotImplementedException();

            //string ResponseResult;
            //foreach (v_smartpark_emp item in EmpList)
            //{
            //    Dictionary<string, object> dic = new Dictionary<string, object>();
            //    dic.Add("subject_type", "0");
            //    dic.Add("group_ids", "0");
            //    dic.Add("extra_id", item.EmpNumber);
            //    dic.Add("name", item.EmpName);

            //    Stream stream = new MemoryStream(item.FileData);
            //    Bitmap img = new Bitmap(stream);
            //    string filepath = AppDomain.CurrentDomain.BaseDirectory + $@"\Photo\{input.EmpName}.jpg";
            //    if (!IOHelper.FileExists(filepath))
            //    {
            //        IOHelper.CreateIfNotExists(filepath);
            //    }
            //    img.Save(filepath);

            //    input = new CreateUserInput
            //    {
            //        Token = null,
            //        timeout = 30000
            //    };
            //    ResponseResult = await PostCreateUpLoadUser(AppSettings.FaceImageInterface.CreateUserUrl, input.Token, 30000, "photo", filepath, dic);

            //    ExceptionEntity.Root da = JsonConvert.DeserializeObject<ExceptionEntity.Root>(ResponseResult);
            //    if (da.desc != null && da.desc.Length > 0)
            //    {
            //        if (da.desc != "唯一标识重复")
            //        {
            //            string ErrprPhoto = AppDomain.CurrentDomain.BaseDirectory + $@"\ErrorPhoto\{item.EmpName}.jpg";
            //            if (!IOHelper.FileExists(ErrprPhoto))
            //            {
            //                IOHelper.CreateIfNotExists(ErrprPhoto);
            //            }
            //            img.Save(ErrprPhoto);
            //            //Write Exception log
            //            LoggerHelper.WriteErrorLog($"工号 : {item.EmpNumber} 姓名 ：{item.EmpName} 异常信息 ： {da.desc}");
            //        }
            //    }
            //}
            //return null;
        }

        /// <summary>
        /// 根据离职工号获取对应的subjectid集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ArrayList> GetSubListByLeavingEmpNo(SubjectIdInput input)
        {
            throw new NotImplementedException();

            //string subjectid;
            //Usublist = new ArrayList();
            //UEmplist = _faceimageapiRepository.QueryUpdatedEmployeeAsync();
            //if (UEmplist != null && UEmplist.Count > 0)
            //{
            //    foreach (v_smartpark_emp item in UEmplist)
            //    {
            //        subjectid = _StaffManagementRepository.GetSubjectID(url, Token, item.EmpNumber);
            //        if (subjectid != null && subjectid.Length > 0)
            //        {
            //            Usublist.Add(subjectid);
            //        }
            //        else if (item.LDate == null)
            //        {
            //          await  CreateUploadUser(AppSettings.FaceImageInterface.CreateUserUrl, item, Token);
            //        }
            //    }
            //}
            //else
            //{

            //}
        }

        /// <summary>
        /// 根据当天更新过资料的工号获取对应的subjectid集合和员工实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task GetUpdatedSubjectid(UpdatedUserInput input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 接口创建方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> PostCreateUpLoadUser(CreateUserInput input)
        {
            string ResponseResult;
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
        /// 接口删除方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> PostDeleteResignedUser(DeleteUserInput input)
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
    }
}
