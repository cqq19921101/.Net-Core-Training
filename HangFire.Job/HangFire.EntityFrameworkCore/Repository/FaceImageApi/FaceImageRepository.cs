using HangFire.EntityFrameworkCore.Module;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HangFire.Domain.FaceImage.Repository;

namespace HangFire.EntityFrameworkCore.Repository.FaceImageApi
{
    public class FaceImageRepository : EfCoreRepository<HangFireDBContext, Domain.FaceImage.FaceImageApi, Guid>, IFaceImageApiRepository
    {
        public FaceImageRepository(IDbContextProvider<HangFireDBContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
