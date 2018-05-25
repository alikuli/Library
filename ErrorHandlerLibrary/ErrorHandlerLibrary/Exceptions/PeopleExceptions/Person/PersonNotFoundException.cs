using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class PersonNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Person Not found. ";
        
        public PersonNotFoundException()
            : base(msg) { }
    
        public PersonNotFoundException(string message)
            : base(msg + message) 
        {
            
        }
    
        public PersonNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public PersonNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public PersonNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PersonNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


