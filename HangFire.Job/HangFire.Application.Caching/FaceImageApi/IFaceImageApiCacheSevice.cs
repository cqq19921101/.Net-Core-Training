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
        /// Get New Employee Cahce
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetNewEmployee(Func<Task<ServiceResult<string>>> factory);

        /// <summary>
        /// Get Resigned Employee Cahce
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetResignedEmployee(Func<Task<ServiceResult<string>>> factory);


        /// <summary>
        /// Get Updated Employee Cahce
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetUpdatedEmployee(Func<Task<ServiceResult<string>>> factory);

    }
}
