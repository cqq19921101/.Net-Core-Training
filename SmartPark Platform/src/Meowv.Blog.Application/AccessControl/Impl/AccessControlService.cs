using AutoMapper;
using Meowv.Blog.Domain.AccessControl.Repositories;
using Meowv.Blog.Dto.AccessControl;
using Meowv.Blog.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meowv.Blog.AccessControl.Impl
{
    public partial class AccessControlService  : ServiceBase , IAccessControlService
    {
        private readonly IFaceCaptureRepository _facecapturerepository;
        //private readonly ICategoryRepository _categories;
        //private readonly ITagRepository _tags;
        //private readonly IFriendLinkRepository _friendLinks;
        //private readonly IBlogCacheService _cache;

        public AccessControlService(IFaceCaptureRepository facecapturerepository)
        {
            _facecapturerepository = facecapturerepository;
        }




    }
}
