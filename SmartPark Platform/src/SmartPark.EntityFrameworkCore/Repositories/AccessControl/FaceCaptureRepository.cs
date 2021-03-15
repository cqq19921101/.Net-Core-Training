using Meowv.Blog.Domain.AccessControl.Repositories;
using Microsoft.EntityFrameworkCore;
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


        /// <summary>
        /// Get Access Control History Data By Timestamp
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public async Task<List<FaceCapture>> GetListAsync(int skipCount, int maxResultCount)
        {
            var sql = string.Empty;
            sql = @"select top 1000 * from [SP_FaceCapture]";
            return await DbContext.Set<FaceCapture>().FromSqlRaw(sql).ToListAsync();
        }

    }
}
