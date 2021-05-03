using System;
using System.Runtime.Serialization;

namespace RoutingRecords
{
    [Serializable]
    public class InvalidMediaTypeException : Exception
    {
        public InvalidMediaTypeException()
        {
        }

        public InvalidMediaTypeException(string message) : base(message)
        {
        }

        public InvalidMediaTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidMediaTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}