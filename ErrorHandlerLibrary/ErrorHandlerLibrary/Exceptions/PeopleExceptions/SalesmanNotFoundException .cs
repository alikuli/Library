using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class SalesmanNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Salesman Not found. ";

        public SalesmanNotFoundException()
            : base(msg) { }
    
        public SalesmanNotFoundException(string message)
            : base(msg + message)  
        { 
                
        }
    
        public SalesmanNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public SalesmanNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public SalesmanNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected SalesmanNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


