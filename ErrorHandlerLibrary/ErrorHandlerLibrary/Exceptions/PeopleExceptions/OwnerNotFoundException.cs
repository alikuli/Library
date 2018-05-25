using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class OwnerNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Owner Not found. ";

        public OwnerNotFoundException()
            : base(msg) { }
    
        public OwnerNotFoundException(string message)
            : base(msg + message)  
        { 
                
        }
    
        public OwnerNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public OwnerNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public OwnerNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected OwnerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


