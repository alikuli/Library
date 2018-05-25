using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class NoDataException:Exception
    {

        private static string msg = "No Data found. ";

        public NoDataException()
            : base(msg) { }
    
        public NoDataException(string message)
            : base(msg + message) { }
    
        public NoDataException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public NoDataException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public NoDataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        protected NoDataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}