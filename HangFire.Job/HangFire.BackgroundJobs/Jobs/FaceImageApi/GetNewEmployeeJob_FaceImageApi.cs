using HangFire.Application.FaceImageApi;
using HangFire.Domain.Configuration;
using HangFire.Domain.FaceImage.Repository;
using System;
using System.Threading.Tasks;

namespace HangFire.BackgroundJobs.Jobs.FaceImageApi
{
    public class GetNewEmployeeJob_FaceImageApi : IBackgroundJobs
    {
        private IFaceImageApiRepository _faceImageApiRepository;
        private IFaceImageApiService _faceImageApiService;

        public GetNewEmployeeJob_FaceImageApi(IFaceImageApiRepository faceImageApiRepository, IFaceImageApiService faceImageApiService)
        {
            _faceImageApiRepository = faceImageApiRepository;
            _faceImageApiService = faceImageApiService;
        }

        public async Task ExecuteAsync()
        {
            //string Token = await _faceImageApiService.GetFaceImageToken(Appsettings.FaceImageInterface.TokenUrl);
            Console.WriteLine("Testtesttestetete");
        }
    }
}
