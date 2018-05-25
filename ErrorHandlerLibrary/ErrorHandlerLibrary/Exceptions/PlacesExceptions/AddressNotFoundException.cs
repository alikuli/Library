using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class AddressNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Address Not found. ";

        public AddressNotFoundException()
            : base(msg) { }
    
        public AddressNotFoundException(string message)
            : base(msg + message) { }
    
        public AddressNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public AddressNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public AddressNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected AddressNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


