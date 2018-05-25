using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class IdCardNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Id Card Not found. ";
        
        public IdCardNotFoundException()
            : base(msg) 
        {
        }
    
        public IdCardNotFoundException(string message)
            : base(msg + message) {

                
        
        }
    
        public IdCardNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public IdCardNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public IdCardNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected IdCardNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


