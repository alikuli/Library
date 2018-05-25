using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class CustomerNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Customer Not found. ";

        public CustomerNotFoundException()
            : base(msg) { }
    
        public CustomerNotFoundException(string message)
            : base(msg + message) 
        { 
                
        }
    
        public CustomerNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public CustomerNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public CustomerNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected CustomerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


