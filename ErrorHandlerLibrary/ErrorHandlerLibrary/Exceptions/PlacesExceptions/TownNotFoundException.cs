using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class TownNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Town not found. ";

        public TownNotFoundException()
            : base(msg) { }
    
        public TownNotFoundException(string message)
            : base(msg + message) { }
    
        public TownNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public TownNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public TownNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected TownNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


