using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class RoleException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Role Exception. "; 
        public RoleException()
            : base(msg) { }
    
        public RoleException(string message)
            : base(msg + message) 
        {
            
        }
    
        public RoleException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public RoleException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public RoleException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected RoleException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


