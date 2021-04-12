namespace SweatSpace.Api.Business.Exceptions
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ApiException(int statuscode, string message = null, string stackTrace = null)
        {
            StatusCode = statuscode;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}