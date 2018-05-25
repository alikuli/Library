using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class NoDuplicateException:Exception
    {
        //public NoDuplicateException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "No duplicates allowed. There is another record with the same name. ";

        public NoDuplicateException()
            : base(msg)
        {
        
        }

        public NoDuplicateException(string message)
            : base (message)
        {  }
    
        public NoDuplicateException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public NoDuplicateException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public NoDuplicateException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        protected NoDuplicateException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


