using System;
using System.Runtime.Serialization;

namespace SweatSpace.Api.Business.Exceptions
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}