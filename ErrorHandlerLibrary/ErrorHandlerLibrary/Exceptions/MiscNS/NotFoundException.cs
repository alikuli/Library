using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class NotFoundException:Exception
    {

        private static string msg = "Entity not found. ";

        public NotFoundException()
            : base(msg) { }
    
        public NotFoundException(string message)
            : base(msg + message) { }
    
        public NotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public NotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public NotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}