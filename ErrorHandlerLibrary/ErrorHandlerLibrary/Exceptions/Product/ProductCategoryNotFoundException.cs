using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class ProductCategoryNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Product Category Not found. ";
        public ProductCategoryNotFoundException()
            : base(msg) { }
    
        public ProductCategoryNotFoundException(string message)
            : base(msg + message) 
        {
            
        }
    
        public ProductCategoryNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public ProductCategoryNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public ProductCategoryNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ProductCategoryNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


