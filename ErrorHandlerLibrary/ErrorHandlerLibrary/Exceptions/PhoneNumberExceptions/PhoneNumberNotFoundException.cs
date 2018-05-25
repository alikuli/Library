using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class PhoneNumberNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Phone number not found. ";

        public PhoneNumberNotFoundException()
            : base(msg) { }
    
        public PhoneNumberNotFoundException(string message)
            : base(msg + message) { }
    
        public PhoneNumberNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public PhoneNumberNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public PhoneNumberNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PhoneNumberNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


