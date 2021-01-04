using System;

namespace HangFire.Application.Contracts.FaceImageApi
{
    public partial class FaceImageApiDto
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
