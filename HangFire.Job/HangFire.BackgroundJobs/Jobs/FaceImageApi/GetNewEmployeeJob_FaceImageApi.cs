using HangFire.Application.Contracts.FaceImageApi;
using HangFire.Application.FaceImageApi;
using HangFire.Common.Helper;
using HangFire.Domain.Configurations;
using HangFire.Domain.FaceImage.Repositories;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;

namespace HangFire.BackgroundJobs.Jobs.FaceImageApi
{
    public class GetNewEmployeeJob_FaceImageApi : IBackgroundJob
    {
        private IFaceImageRepository _faceImageRepository;
        private IFaceImageService _faceImageService;

        public GetNewEmployeeJob_FaceImageApi(IFaceImageRepository faceImageRepository, IFaceImageService faceImageService)
        {
            _faceImageRepository = faceImageRepository;
            _faceImageService = faceImageService;
        }

        //private IFaceImageService _faceImageService;

        //public GetNewEmployeeJob_FaceImageApi(IFaceImageService faceImageService)
        //{
        //    _faceImageService = faceImageService;
        //}


        public async Task ExecuteAsync()
        {
           await  _faceImageService.ExcuteInsertNewEmpAsync();
            //List<Domain.FaceImage.FaceImageApi> lst = await _faceImageRepository.QueryNewEmployeeAsync();
            //var message = new MimeMessage
            //{
            //    Subject = "【定时任务】测试推送",
            //    Body = new BodyBuilder
            //    {
            //        HtmlBody = $"测试成功，时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            //    }.ToMessageBody()
            //};
            //await EmailHelper.SendMailAsync(message);

        }
    }
}
