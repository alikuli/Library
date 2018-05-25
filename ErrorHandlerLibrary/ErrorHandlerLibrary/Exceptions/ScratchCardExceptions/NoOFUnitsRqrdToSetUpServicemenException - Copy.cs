using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class NoOfUnitsRqrdToSetUpServicemenException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Setup problem. The number of units required to set up the customer has not been setup. Please set it up. ";
        
        public NoOfUnitsRqrdToSetUpServicemenException()
            : base(msg) { }
    
        public NoOfUnitsRqrdToSetUpServicemenException(string message)
            : base(msg + message) 
        
        {
            
        }
    
        public NoOfUnitsRqrdToSetUpServicemenException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public NoOfUnitsRqrdToSetUpServicemenException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public NoOfUnitsRqrdToSetUpServicemenException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected NoOfUnitsRqrdToSetUpServicemenException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


