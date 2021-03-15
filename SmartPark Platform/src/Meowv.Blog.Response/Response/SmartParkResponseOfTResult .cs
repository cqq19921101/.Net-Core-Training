namespace Meowv.Blog.Response
{
    public class SmartParkResponse<TResult> : SmartParkResponse where TResult : class
    {
        public TResult Result { get; set; }

        public void IsSuccess(TResult result = null, string message = "")
        {
            Code = SmartParkResponseCode.Succeed;
            Message = message;
            Result = result;
        }
    }
}