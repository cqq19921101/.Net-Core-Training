using MailKit.Net.Smtp;
using HangFire.Domain.Configurations;
using MimeKit;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Collections.Generic;
using System.Net.Mime;

namespace HangFire.Common.Helper
{
    public static class EmailHelper
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

            var client = new System.Net.Mail.SmtpClient(AppSettings.Email.Host, AppSettings.Email.Port);
            client.Credentials = new System.Net.NetworkCredential(AppSettings.Email.From.Username, AppSettings.Email.From.Password);
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
                message.From.Add(new MailboxAddress(AppSettings.Email.From.Name, AppSettings.Email.From.Address));
            }
            if (!message.To.Any())
            {
                var address = AppSettings.Email.To.Select(x => new MailboxAddress(x.Key, x.Value));
                message.To.AddRange(address);
            }

            using var client = new MailKit.Net.Smtp.SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.ConnectAsync(AppSettings.Email.Host, AppSettings.Email.Port, AppSettings.Email.UseSsl);
            await client.AuthenticateAsync(AppSettings.Email.From.Username, AppSettings.Email.From.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        #endregion
    }
}