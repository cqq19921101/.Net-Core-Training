using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.ExceptionEntity
{
    public class ExceptionEntity
    {
        public class Data
        {
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
            /// <summary>
            /// 无权限
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int timecost { get; set; }
        }

    }
}
