using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static HangFire.Domain.Shared.HangFireConsts;

namespace HangFire.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
    public class Test2Controller : AbpController
    {
        /// <summary>
        /// GetTest2
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetTest2(string code)
        {
            return  "This is Test2";
        }

    }
}