using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class RoleNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


     //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Role not found. ";
        public RoleNotFoundException()
            : base(msg) { }
    
        public RoleNotFoundException(string message)
            : base(msg + message) 
        {
            
        }
    
        public RoleNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public RoleNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public RoleNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected RoleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


