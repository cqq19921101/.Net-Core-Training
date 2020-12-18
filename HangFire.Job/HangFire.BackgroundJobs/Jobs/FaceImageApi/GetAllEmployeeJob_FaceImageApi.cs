using HangFire.Application.FaceImageApi;
using HangFire.Domain.Configuration;
using HangFire.Domain.FaceImage.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HangFire.BackgroundJobs.Jobs.FaceImageApi
{
    public class GetAllEmployeeJob_FaceImageApi : IBackgroundJobs
    {

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Testtesttestetete");
        }
    }
}
