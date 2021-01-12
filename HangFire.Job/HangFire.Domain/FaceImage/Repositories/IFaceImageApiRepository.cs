using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HangFire.Domain.FaceImage.Repositories
{
    /// <summary>
    /// IFaceImageApiRepository
    /// </summary>
    public interface IFaceImageRepository : IRepository<FaceImageApi, long>
    {
        /// <summary>
        /// Get All Employee List
        /// </summary>
        /// <returns></returns>
        Task<List<Domain.FaceImage.FaceImageApi>> QueryAllEmployeeAsync();


        /// <summary>
        /// Get New Employee List
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Domain.FaceImage.FaceImageApi>> QueryNewEmployeeAsync();

        /// <summary>
        /// Get Resigned Employee List
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Domain.FaceImage.FaceImageApi>> QueryResignedEmployeeAsync();

        /// <summary>
        /// Get Updated Employee List
        /// </summary>
        /// <returns></returns>
        Task<List<Domain.FaceImage.FaceImageApi>> QueryUpdatedEmployeeAsync();

    }
}
