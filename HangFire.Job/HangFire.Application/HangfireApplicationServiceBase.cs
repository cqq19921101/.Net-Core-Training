using HangFire.Application.Contracts.ExceptionEntity;
using HangFire.Application.Contracts.FaceImageApi.Param;
using HangFire.Domain.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HangFire.Application
{
    public class HangfireApplicationServiceBase : ApplicationService
    {
        #region EmailHelper
        /// <summary>
        /// 基础邮件发送方法
        /// </summary>
        /// <param name="sender">发件人Name</param>
        /// <param name="from">发件人Mail Address</param>
        /// <param name="alTo">收件人Mail Address</param>
        /// <param name="alCc">抄送人Mail Address</param>
        /// <param name="ssub">邮件主旨</param>
        /// <param name="strBody">正文</param>
        /// <returns>发送结果</returns>
        private static async Task SendMailByLocalhost(string sender, string from, string ssub, string strBody)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, sender, System.Text.Encoding.UTF8);

            mail.To.Add("qianqian.chen@qtechglobal.com");
            mail.Subject = ssub;

            List<string> lsBody = new List<string>(strBody.Split('\n'));
            string htmlBody = "<html><body>";
            for (int i = 0; i < lsBody.Count; ++i)
            {
                if (i == 0)
                {
                    htmlBody = "<h style=\"font-family:新细明体; font-size:18px;\">" + lsBody[i] + "</h>";
                }
                else
                {
                    htmlBody += "<p style=\"font-family:新细明体; font-size:18px;\">" + lsBody[i] + "</p>";
                }
            }

            htmlBody += "</body></html>";

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                  (htmlBody, null, MediaTypeNames.Text.Html);
            mail.AlternateViews.Add(avHtml);

            var client = new System.Net.Mail.SmtpClient(Appsettings.Email.Host, Appsettings.Email.Port);
            client.Credentials = new System.Net.NetworkCredential(Appsettings.Email.From.Username, Appsettings.Email.From.Password);
            try
            {
                await client.SendMailAsync(mail);
                //client.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {

            }
            finally
            {
                mail.Dispose();
            }
        }

        /// <summary>
        /// 基础邮件发送方法
        /// </summary>
        /// <param name="alTo"></param>
        /// <param name="alCc"></param>
        /// <param name="ssub"></param>
        /// <param name="strBody"></param>
        /// <param name="strImgPath"></param>
        /// <param name="AttachmentFiles"></param>
        /// <returns></returns>
        public static async Task SendMailAsync(MimeMessage message)
        {
            string sender = "Mes Administrator";
            string from = "alert.it@qtechglobal.com";
            await SendMailByLocalhost(sender, from, message.Subject, message.HtmlBody);
        }


        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task SendAsync(MimeMessage message)
        {
            if (!message.From.Any())
            {
                message.From.Add(new MailboxAddress(Appsettings.Email.From.Name, Appsettings.Email.From.Address));
            }
            if (!message.To.Any())
            {
                var address = Appsettings.Email.To.Select(x => new MailboxAddress(x.Key, x.Value));
                message.To.AddRange(address);
            }

            using var client = new MailKit.Net.Smtp.SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.ConnectAsync(Appsettings.Email.Host, Appsettings.Email.Port, Appsettings.Email.UseSsl);
            await client.AuthenticateAsync(Appsettings.Email.From.Username, Appsettings.Email.From.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        #endregion

        #region FaceImageApi

        /// <summary>
        /// 根据登录账号密码获取Token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
    //    public async Task<string> GetFaceImageToken()
    //    {
    //        return await _blogCacheService.GetCategoryAsync(name, async () =>
    //        {
    //            var result = new ServiceResult<string>();

    //            var category = await _categoryRepository.FindAsync(x => x.DisplayName.Equals(name));
    //            if (null == category)
    //            {
    //                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("分类", name));
    //                return result;
    //            }

    //            result.IsSuccess(category.CategoryName);
    //            return result;
    //        });
    //    }

    //    string result = string.Empty;
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Appsettings.FaceImageInterface.TokenUrl));
    //    request.Timeout = 30 * 1000;//设置30s的超时
    //        request.ContentType = "application/json";
    //        request.UserAgent = "Koala Admin";
    //        request.Method = "POST";

    //        var temp = new
    //        {
    //            username = Appsettings.FaceImageInterface.LoginId,
    //            password = Appsettings.FaceImageInterface.LoginPsd,
    //            auth_token = true
    //        };

    //    var postData = JsonConvert.SerializeObject(temp);
    //    byte[] data = Encoding.UTF8.GetBytes(postData);
    //    request.ContentLength = data.Length;
    //        Stream postStream = await request.GetRequestStreamAsync();
    //    postStream.Write(data, 0, data.Length);
    //        postStream.Close();
    //        var res = await request.GetResponseAsync() as HttpWebResponse;
    //        if (res.StatusCode == HttpStatusCode.OK)
    //        {
    //            StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
    //    result = await reader.ReadToEndAsync();
    //    reader.Close();
    //        }
    //request.Abort();

    //        JsonEntity.Root da = JsonConvert.DeserializeObject<JsonEntity.Root>(result);
    //        return da.data.auth_token;
    //    }

        #endregion

        #region ExportTask

        #endregion
    }
}
