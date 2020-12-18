using HangFire.Common.Base;
using System;
using System.Threading.Tasks;

namespace HangFire.Application.Caching.FaceImageApi
{
    /// <summary>
    /// FaceImageApi Cache Interface
    /// </summary>
    public interface IFaceImageApiCacheSevice
    {
        /// <summary>
        /// Get FaceImage Token Cache
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<string> GetFaceImageTokenCacheAsync(string Tokenurl,Func<Task<string>> factory);

        /// <summary>
        /// Get New Employee Cahce
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<Domain.FaceImage.FaceImageApi>> GetNewEmployeeCacheAsync(Func<Task<ServiceResult<Domain.FaceImage.FaceImageApi>>> factory);

        /// <summary>
        /// Get Resigned Employee Cahce
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<Domain.FaceImage.FaceImageApi>> GetResignedEmployeeCacheAsync(Func<Task<ServiceResult<Domain.FaceImage.FaceImageApi>>> factory);


        /// <summary>
        /// Get Updated Employee Cahce
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<Domain.FaceImage.FaceImageApi>> GetUpdatedEmployeeCacheAsync(Func<Task<ServiceResult<Domain.FaceImage.FaceImageApi>>> factory);

    }
}
