﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HangFire.Domain.FaceImage.Repositories
{
    /// <summary>
    /// IFaceImageApiRepository
    /// </summary>
    public interface IFaceImageRepository : IRepository<FaceImageApi, Guid>
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
        Task<List<Domain.FaceImage.FaceImageApi>> QueryNewEmployeeAsync();

        /// <summary>
        /// Get Resigned Employee List
        /// </summary>
        /// <returns></returns>
        Task<List<Domain.FaceImage.FaceImageApi>> QueryResignedEmployeeAsync();

        /// <summary>
        /// Get Updated Employee List
        /// </summary>
        /// <returns></returns>
        Task<List<Domain.FaceImage.FaceImageApi>> QueryUpdatedEmployeeAsync();

    }
}