using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class DuplicatePhoneNumberException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Duplicate phone number. This phone number already exists. ";

        public DuplicatePhoneNumberException()
            : base(msg) { }
    
        public DuplicatePhoneNumberException(string message)
            : base(msg + message) { }
    
        public DuplicatePhoneNumberException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public DuplicatePhoneNumberException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public DuplicatePhoneNumberException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DuplicatePhoneNumberException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


