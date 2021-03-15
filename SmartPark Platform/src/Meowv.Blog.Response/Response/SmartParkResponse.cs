using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meowv.Blog.Response
{
    public class SmartParkResponse
    {
        public SmartParkResponseCode Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool Success => Code == SmartParkResponseCode.Succeed;

        public void IsSuccess(string message = "")
        {
            Code = SmartParkResponseCode.Succeed;
            Message = message;
        }

        public void IsFailed(string message = "")
        {
            Code = SmartParkResponseCode.Failed;
            Message = message;
        }

        public void IsFailed(Exception exception)
        {
            Code = SmartParkResponseCode.Failed;
            Message = exception.InnerException?.StackTrace;
        }
    }
}
