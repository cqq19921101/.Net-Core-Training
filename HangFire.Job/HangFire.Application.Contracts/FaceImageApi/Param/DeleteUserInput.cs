using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.FaceImageApi.Param
{
    public partial class DeleteUserInput 
    {
        /// <summary>
        /// SubjectId
        /// </summary>
        public string SubjectId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}
