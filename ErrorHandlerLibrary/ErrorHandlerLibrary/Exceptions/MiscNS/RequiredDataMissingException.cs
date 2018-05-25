using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class RequiredDataMissingException:Exception
    {

        private static string msg = " Required Data Missing. ";

        public RequiredDataMissingException()
            : base(msg) { }
    
        public RequiredDataMissingException(string message)
            : base(msg + message) { }
    
        public RequiredDataMissingException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public RequiredDataMissingException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public RequiredDataMissingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected RequiredDataMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}