using System;
using System.Threading.Tasks;
using HangFire.Application.Caching.Module;
using HangFire.Common;
using HangFire.Common.Base;
using static HangFire.Domain.Shared.HangFireConsts;

namespace HangFire.Application.Caching.FaceImageApi.Impl
{
    /// <summary>
    /// FaceImageApi Implement Interface
    /// </summary>
    public class FaceImageApiCachingService : CachingServiceBase, IFaceImageApiCacheSevice
    {
        private const string KEY_GetToken = "FaceImageApi:GetToken";
        private const string KEY_QueryNewEmployee = "FaceImageApi:QueryNewEmployee";
        private const string KEY_QueryResignedEmployee = "FaceImageApi:QueryResignedEmployee";
        private const string KEY_QueryUpdatedEmployee = "FaceImageApi:QueryUpdatedEmployee";

        /// <summary>
        /// Get FaceImage Token Cache
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<string> GetFaceImageTokenCacheAsync(string TokenUrl,Func<Task<string>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetToken, factory, CacheStrategy.ONE_HOURS);
        }

        /// <summary>
        /// Get New Employee Cache
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<Domain.FaceImage.FaceImageApi>> GetNewEmployeeCacheAsync(Func<Task<ServiceResult<Domain.FaceImage.FaceImageApi>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryNewEmployee,factory,CacheStrategy.ONE_HOURS);
        }

        /// <summary>
        /// Get Resigned Employee Cache
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<Domain.FaceImage.FaceImageApi>> GetResignedEmployeeCacheAsync(Func<Task<ServiceResult<Domain.FaceImage.FaceImageApi>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryResignedEmployee, factory, CacheStrategy.ONE_HOURS);
        }

        /// <summary>
        /// Get Updated Employee Cache
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<Domain.FaceImage.FaceImageApi>> GetUpdatedEmployeeCacheAsync(Func<Task<ServiceResult<Domain.FaceImage.FaceImageApi>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryUpdatedEmployee, factory, CacheStrategy.ONE_HOURS);
        }
    }
}
