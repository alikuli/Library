using System;
using System.Runtime.Serialization;

namespace ErrorHandlerLibrary.ExceptionsNS
{

    public class DoNotCatchException : Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Do Not Catch Exception. ";
        public DoNotCatchException()
            : base(msg) { }

        public DoNotCatchException(string message)
            : base(msg + message)
        {

        }

        public DoNotCatchException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DoNotCatchException(string message, Exception innerException)
            : base(msg + message, innerException) { }

        public DoNotCatchException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DoNotCatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }



    }


}


