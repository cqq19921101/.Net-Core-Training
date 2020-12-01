using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangFire.Job.Filter
{
    /// <summary>
    /// Filters
    /// </summary>
    public class HangFireExceptionFilter : IExceptionFilter
    {
        private readonly ILog _log;

        public HangFireExceptionFilter()
        {
            _log = LogManager.GetLogger(typeof(HangFireExceptionFilter));
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void OnException(ExceptionContext context)
        {
            // 错误日志记录
            _log.Error($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
        }

    }
}
