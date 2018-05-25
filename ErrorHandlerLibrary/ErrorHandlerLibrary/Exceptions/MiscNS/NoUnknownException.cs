using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class NoUnkownException:Exception
    {


        private static string msg = "No UNKNWON allowed. ";

        public NoUnkownException()
            : base(msg) { }
    
        public NoUnkownException(string message)
            : base(msg + message) { }
    
        public NoUnkownException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public NoUnkownException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public NoUnkownException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected NoUnkownException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}