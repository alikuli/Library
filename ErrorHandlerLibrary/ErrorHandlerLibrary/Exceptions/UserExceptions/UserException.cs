using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class UserException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "User Exception. ";
        public UserException()
            : base(msg) { }
    
        public UserException(string message)
            : base(msg + message) 
        {
            
        }

        public UserException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public UserException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public UserException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected UserException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }


        
    }

    
}


