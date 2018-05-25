using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ProductCategoryException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Product Category Exception. ";
        public ProductCategoryException()
            : base(msg) { }
    
        public ProductCategoryException(string message)
            : base(msg + message) 
        {
            
        }
    
        public ProductCategoryException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ProductCategoryException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ProductCategoryException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ProductCategoryException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


