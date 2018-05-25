using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class CountryNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Country Not found. ";

        public CountryNotFoundException()
            : base(msg) { }
    
        public CountryNotFoundException(string message)
            : base(msg + message) { }
    
        public CountryNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public CountryNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public CountryNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected CountryNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


