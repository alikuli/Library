using System;
using System.Runtime.Serialization;

namespace ErrorHandlerLibrary.ExceptionsNS
{

    public class AncestorExistsException : Exception
    {

        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "The ancestor already exists. This will create a circulr reference. ";
        public AncestorExistsException()
            : base(msg) { }

        public AncestorExistsException(string message)
            : base(msg + message) { }

        public AncestorExistsException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public AncestorExistsException(string message, Exception innerException)
            : base(msg + message, innerException) { }

        public AncestorExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected AncestorExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


