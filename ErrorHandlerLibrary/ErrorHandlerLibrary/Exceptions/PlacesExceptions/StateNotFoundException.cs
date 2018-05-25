using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class StateNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "State Not found. ";

        public StateNotFoundException()
            : base(msg) { }
    
        public StateNotFoundException(string message)
            : base(msg + message) { }
    
        public StateNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public StateNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public StateNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected StateNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


