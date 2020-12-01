using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Domain.FaceImage
{
    /// <summary>
    /// Data View
    /// </summary>
    public partial class v_smartpark_emp : Entity<Guid>
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string EmpNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// JDate
        /// </summary>
        public DateTime JDate { get; set; }
        /// <summary>
        /// JDate
        /// </summary>
        public DateTime? LDate { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        public byte[] FileData { get; set; }

    }
}
