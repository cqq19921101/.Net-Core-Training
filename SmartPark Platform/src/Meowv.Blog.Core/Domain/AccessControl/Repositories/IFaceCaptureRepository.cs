using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.Domain.AccessControl.Repositories
{
    public interface IFaceCaptureRepository : IRepository<FaceCapture, Guid>
    {
        Task<Tuple<int, List<FaceCapture>>> GetAccessControlHistoryListAsync(int skipCount, int maxResultCount);



        Task<List<FaceCapture>> GetListAsync(int skipCount, int maxResultCount);

    }
}
