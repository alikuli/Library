using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class RoleExistsException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


     //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Role Exists. ";
        public RoleExistsException()
            : base(msg) { }
    
        public RoleExistsException(string message)
            : base(msg + message) 
        {
            
        }
    
        public RoleExistsException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public RoleExistsException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public RoleExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected RoleExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


