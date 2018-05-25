using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ScratchCardStateException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Bad Scratchcard State. ";
        public ScratchCardStateException()
            : base(msg) { }
    
        public ScratchCardStateException(string message)
            : base(msg + message) {
                
        
        }
    
        public ScratchCardStateException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ScratchCardStateException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ScratchCardStateException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ScratchCardStateException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


