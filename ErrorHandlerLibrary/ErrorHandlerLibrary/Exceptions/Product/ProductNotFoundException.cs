using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ProductNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Product Not found. ";

        public ProductNotFoundException()
            : base(msg) { }
    
        public ProductNotFoundException(string message)
            : base(msg + message) 
        {
                
        
        }
    
        public ProductNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ProductNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ProductNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


