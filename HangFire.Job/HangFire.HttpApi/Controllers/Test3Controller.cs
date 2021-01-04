using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static HangFire.Domain.Shared.HangFireConsts;

namespace HangFire.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v3)]
    public class Test3Controller : AbpController
    {
        /// <summary>
        /// GetTest3
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetTest3(string code)
        {
            return  "This is Test3";
        }

    }
}