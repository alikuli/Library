using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class WarehouseNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Warehouse Not found. ";

        public WarehouseNotFoundException()
            : base(msg) { }
    
        public WarehouseNotFoundException(string message)
            : base(msg + message) { }
    
        public WarehouseNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public WarehouseNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public WarehouseNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected WarehouseNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


