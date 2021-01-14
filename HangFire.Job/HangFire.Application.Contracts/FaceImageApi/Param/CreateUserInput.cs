using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.FaceImageApi.Param
{
    public partial class CreateUserInput 
    {
        /// <summary>
        /// EmpName
        /// </summary>
        public string EmpName { get; set; }


        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Parameter Key/Value
        /// </summary>
        public Dictionary<string, object> ParameterDictory { get; set; }

        /// <summary>
        /// TimeOut
        /// </summary>
        public int timeout { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Domain.FaceImage.FaceImageApi> NewEmplst { get; set; }

        public Domain.FaceImage.FaceImageApi NewEmp { get; set; }

    }
}
