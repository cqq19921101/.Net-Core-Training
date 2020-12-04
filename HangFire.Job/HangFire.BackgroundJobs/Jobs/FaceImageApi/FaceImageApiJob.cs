using HangFire.Domain.FaceImage.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HangFire.BackgroundJobs.Jobs.FaceImageApi
{
    /// <summary>
    /// Excute FaceImageApi Job
    /// </summary>
    public class FaceImageApiJob : IBackgroundJobs
    {

        private readonly IFaceImageApiRepository _FaceImageApiRepository;

        public FaceImageApiJob(IFaceImageApiRepository FaceImageApiRepository)
        {
            _FaceImageApiRepository = FaceImageApiRepository;
        }


        /// <summary>
        /// Excute Task 
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            Console.WriteLine("TestTestTestTestTestTestTestTestTest");
        }
    }
}
