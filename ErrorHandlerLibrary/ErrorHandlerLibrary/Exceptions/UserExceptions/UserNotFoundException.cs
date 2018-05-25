using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class UserNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


     //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "User not found. ";
        public UserNotFoundException()
            : base(msg) { }
    
        public UserNotFoundException(string message)
            : base(msg + message) 
        {
        }
    
        public UserNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public UserNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public UserNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


