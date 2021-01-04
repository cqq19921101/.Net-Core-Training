using HangFire.Application.Contracts.FaceImageApi;
using HangFire.Application.FaceImageApi;
using HangFire.Domain.Configurations;
using HangFire.Domain.FaceImage.Repositories;
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
            List<Domain.FaceImage.FaceImageApi> lst = await _faceImageRepository.QueryNewEmployeeAsync();
            Console.WriteLine("Testtesttestetete");
        }
    }
}
