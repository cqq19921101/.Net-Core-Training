using Meowv.Blog.Domain.AccessControl.Repositories;
using Meowv.Blog.Dto.AccessControl;
using Meowv.Blog.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Meowv.Blog.AccessControl.Impl
{
    public partial class AccessControlService
    {


        /// <summary>
        /// Get Access Control Information list of posts by paging.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("api/meowv/accesscontrol/posts/{page}/{limit}")]
        public async Task<SmartParkResponse<PagedList<GetAccessControlListDto>>> GetAccessControlListAsync([Range(1, 100)] int page = 1, [Range(10, 100)] int limit = 10)
        {
            var response = new SmartParkResponse<PagedList<GetAccessControlListDto>>();

            //var result = await _facecapturerepository.GetAccessControlHistoryListAsync(page, limit);
            var result = await _facecapturerepository.GetListAsync(page, limit);
            //var total = null;
            //var posts = ObjectMapper.Map<List<FaceCapture>, List<GetAccessControlListDto>>(result.Item2);

            //response.Result = new PagedList<GetAccessControlListDto>(total, posts);
            //return response;
            return null;
        }




    }
}
