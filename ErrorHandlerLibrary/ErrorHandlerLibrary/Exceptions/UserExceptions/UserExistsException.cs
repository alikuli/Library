using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class UserExistsException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


     //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "User Exists. ";
        public UserExistsException()
            : base(msg) { }
    
        public UserExistsException(string message)
            : base(msg + message) 
        {
        }
    
        public UserExistsException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public UserExistsException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public UserExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected UserExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


