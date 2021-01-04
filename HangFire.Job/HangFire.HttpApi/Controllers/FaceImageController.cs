using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static HangFire.Domain.Shared.HangFireConsts;

namespace HangFire.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v1)]
    public class FaceImageController : AbpController
    {
        /// <summary>
        /// 旷世回调测试
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Callbackjson")]
        public async Task<string> Callbackjson()
        {
            
            return "This is Test1";
        }
    }
}