namespace SweatSpace.Api.Exceptions
{
    public class ApiException
    {
        public int StatusCode { get; }
        public string Message { get; }
        public string StackTrace { get; }

        public ApiException(int statuscode, string message = null, string stackTrace = null)
        {
            StatusCode = statuscode;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}