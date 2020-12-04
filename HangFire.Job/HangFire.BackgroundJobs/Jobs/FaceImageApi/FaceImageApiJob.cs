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


        //private readonly IFaceImageApiRepository _faceimageapirepository;

        ///// <summary>
        ///// Construct 
        ///// </summary>
        ///// <param name="FaceImageApiRepository"></param>
        //public FaceImageApiJob(IFaceImageApiRepository faceimageapirepository)
        //{
        //    _faceimageapirepository = faceimageapirepository;
        //}


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
