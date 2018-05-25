using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class PersonExistsException : Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/

        private static string msg = "Error. This person already exists. ";

        public PersonExistsException()
            : base(msg) { }

        public PersonExistsException(string message)
            : base(string.Format("{1}. This record is being used by: '{0}'", message, msg)) { }

        public PersonExistsException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public PersonExistsException(string message, Exception innerException)
            : base(msg + message, innerException) { }

        public PersonExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PersonExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}


