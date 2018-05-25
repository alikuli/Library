using System;
using System.Runtime.Serialization;

namespace ErrorHandlerLibrary.ExceptionsNS
{

    public class ChildDataExistsException : Exception
    {

        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Error. This item has child data. It cannot be deleted. ";
        public ChildDataExistsException()
            : base(msg) { }

        public ChildDataExistsException(string message)
            : base(msg + message) { }

        public ChildDataExistsException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ChildDataExistsException(string message, Exception innerException)
            : base(msg + message, innerException) { }

        public ChildDataExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ChildDataExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


