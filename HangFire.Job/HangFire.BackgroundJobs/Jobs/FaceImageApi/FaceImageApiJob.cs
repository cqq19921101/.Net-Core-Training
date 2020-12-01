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
        
        //private readonly IWallpaperRepository _wallpaperRepository;

        //public FaceImageApiJob(IWallpaperRepository wallpaperRepository)
        //{
        //    _wallpaperRepository = wallpaperRepository;
        //}


        /// <summary>
        /// Excute Task 
        /// </summary>
        /// <returns></returns>
        public async Task ExcuteAsync()
        {
            Console.WriteLine("TestTestTestTestTestTestTestTestTest");
        }
    }
}
