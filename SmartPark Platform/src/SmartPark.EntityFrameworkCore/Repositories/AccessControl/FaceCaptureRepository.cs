using Meowv.Blog.Domain.AccessControl.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartPark.EntityFrameworkCore.Repositories.AccessControl
{
    public partial class FaceCaptureRepository : EfCoreRepository<SmartParkDbContext, FaceCapture, Guid>, IFaceCaptureRepository
    {
        public FaceCaptureRepository(IDbContextProvider<SmartParkDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// Get Access Control History Data By Timestamp
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public async Task<Tuple<int, List<FaceCapture>>> GetAccessControlHistoryListAsync(int skipCount, int maxResultCount)
        {


            return null;
        }

    }
}
