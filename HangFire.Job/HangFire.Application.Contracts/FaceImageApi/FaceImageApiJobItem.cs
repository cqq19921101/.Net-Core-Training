using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.FaceImageApi
{
    public class FaceImageApiJobItem<T>
    {
        /// <summary>
        /// <see cref="Result"/>
        /// </summary>
        public T Result { get; set; }

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public FaceImageEnum Type { get; set; }
    }
}
