using HangFire.Common.Helper;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace HangFire.BackgroundJobs.Jobs.FaceImageApi
{
    /// <summary>
    /// Excute FaceImageApi Job
    /// </summary>
    public class TestSendMail : IBackgroundJobs
    {
        //private readonly IFaceImageApiRepository _faceimageapirepository;

        ///// <summary>
        /////  
        ///// </summary>
        ///// <param name="FaceImageApiRepository"></param>
        //public TestJob(IFaceImageApiRepository faceimageapirepository)
        //{
        //    _faceimageapirepository = faceimageapirepository;
        //}


        /// <summary>
        /// Excute Task 
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            //Console.WriteLine("Begin -------------");

            //// 发送Email
            //var message = new MimeMessage
            //{
            //    Subject = "【定时任务】测试推送",
            //    Body = new BodyBuilder
            //    {
            //        HtmlBody = $"本次抓取到10000000条数据，时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            //    }.ToMessageBody()
            //};
            //await EmailHelper.SendMailAsync(message);

            //Console.WriteLine("End -------------");

        }
    }
}
