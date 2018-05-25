using System;
using System.Runtime.Serialization;
//
namespace ErrorHandlerLibrary.ExceptionsNS
{

    public class InsufficentUnitsException : Exception
    {
        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Insufficent units for transaction. Please purchase more units to continue. ";

        public InsufficentUnitsException()
            : base(msg)
        {

        }

        public InsufficentUnitsException(string message)
            : base(message)
        { }

        public InsufficentUnitsException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InsufficentUnitsException(string message, Exception innerException)
            : base(msg + message, innerException) { }

        public InsufficentUnitsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected InsufficentUnitsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}