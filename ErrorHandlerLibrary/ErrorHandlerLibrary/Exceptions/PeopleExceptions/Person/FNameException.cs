using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class FNameException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Problem with First Name. ";

        public FNameException()
            : base(msg) { }
    
        public FNameException(string message)
            : base(msg + message) 
        {
            
        }
    
        public FNameException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public FNameException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public FNameException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected FNameException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


