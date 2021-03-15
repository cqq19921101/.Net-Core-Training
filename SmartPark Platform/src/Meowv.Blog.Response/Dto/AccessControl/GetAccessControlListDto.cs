using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meowv.Blog.Dto.AccessControl
{
    public class GetAccessControlListDto
    {
        /// <summary>
        /// Tkey
        /// </summary>
        public Guid TKEY { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string extra_id { get; set; }

        /// <summary>
        /// 相机位置
        /// </summary>
        public string camera_position { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? timestamp { get; set; }


    }
}
