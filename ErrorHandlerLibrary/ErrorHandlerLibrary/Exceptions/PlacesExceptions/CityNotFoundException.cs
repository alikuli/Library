using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class CityNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "City Not found. ";

        public CityNotFoundException()
            : base(msg) { }
    
        public CityNotFoundException(string message)
            : base(msg + message) { }
    
        public CityNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public CityNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public CityNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected CityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


