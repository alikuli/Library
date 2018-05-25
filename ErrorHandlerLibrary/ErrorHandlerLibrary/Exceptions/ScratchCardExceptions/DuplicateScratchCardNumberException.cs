using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class DuplicateScratchCardNumberException:Exception
    {
        //public NoDuplicateException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "No duplicates allowed. There is an older Scratch Card '{0}' record with the same name. ";

        public DuplicateScratchCardNumberException()
            : base(msg)
        {
        
        }

        public DuplicateScratchCardNumberException(string message)
            : base (message)
        {
             
        
        }
    
        public DuplicateScratchCardNumberException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public DuplicateScratchCardNumberException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public DuplicateScratchCardNumberException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DuplicateScratchCardNumberException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


