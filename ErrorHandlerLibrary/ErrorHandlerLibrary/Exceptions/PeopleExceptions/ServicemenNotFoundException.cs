using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ServicemanNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Serviceman Not found. ";

        public ServicemanNotFoundException()
            : base(msg) { }
    
        public ServicemanNotFoundException(string message)
            : base(msg + message)
        {
            
        }
    
    
        public ServicemanNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ServicemanNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ServicemanNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ServicemanNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


