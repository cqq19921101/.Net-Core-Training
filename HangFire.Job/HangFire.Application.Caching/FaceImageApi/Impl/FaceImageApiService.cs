using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HangFire.Common;
using HangFire.Common.Base;

namespace HangFire.Application.Caching.FaceImageApi.Impl
{
    /// <summary>
    /// FaceImageApi Implement Interface
    /// </summary>
    public class FaceImageApiService : IFaceImageApiCacheSevice
    {
        /// <summary>
        /// Get New Employee
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public Task<ServiceResult<string>> GetNewEmployee(Func<Task<ServiceResult<string>>> factory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Resigned Employee
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public Task<ServiceResult<string>> GetResignedEmployee(Func<Task<ServiceResult<string>>> factory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Updated Employee
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public Task<ServiceResult<string>> GetUpdatedEmployee(Func<Task<ServiceResult<string>>> factory)
        {
            throw new NotImplementedException();
        }
    }
}
