using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ScratchcardNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Scratchcard Not found. ";

        public ScratchcardNotFoundException()
            : base(msg) { }
    
        public ScratchcardNotFoundException(string message)
            : base(msg + message) {
                
        
        }
    
        public ScratchcardNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ScratchcardNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ScratchcardNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ScratchcardNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


