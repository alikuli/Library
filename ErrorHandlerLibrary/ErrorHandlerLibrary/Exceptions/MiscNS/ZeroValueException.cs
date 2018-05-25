using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ZeroValueException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Bad Web Request. I received a zero index. ";

        public ZeroValueException()
            : base(msg) { }
    
        public ZeroValueException(string message)
            : base(msg + message) { }
    
        public ZeroValueException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ZeroValueException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ZeroValueException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        protected ZeroValueException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


