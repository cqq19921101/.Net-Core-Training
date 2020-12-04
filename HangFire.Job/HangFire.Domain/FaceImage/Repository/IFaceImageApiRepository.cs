using System;
using Volo.Abp.Domain.Repositories;

namespace HangFire.Domain.FaceImage.Repository
{
    /// <summary>
    /// IFaceImageApiRepository
    /// </summary>
    public interface IFaceImageApiRepository : IRepository<FaceImageApi, Guid>
    {

    }
}
