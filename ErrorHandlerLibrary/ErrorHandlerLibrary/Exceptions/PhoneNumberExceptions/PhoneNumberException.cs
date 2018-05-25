using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class PhoneNumberException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Bad ID card number, or missing even. ";

        public PhoneNumberException()
            : base(msg) { }
    
        public PhoneNumberException(string message)
            : base(msg + message) { }
    
        public PhoneNumberException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public PhoneNumberException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public PhoneNumberException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PhoneNumberException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


