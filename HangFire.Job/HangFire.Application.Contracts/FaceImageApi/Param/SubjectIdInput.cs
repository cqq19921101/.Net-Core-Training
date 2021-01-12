using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.FaceImageApi.Param
{
    public partial class SubjectIdInput
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// EmpName
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// EmpNumber
        /// </summary>
        public string EmpNumber { get; set; }


        /// <summary>
        /// SubList
        /// </summary>
        public List<string> SubList { get; set; }
    }
}
