using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    
    public class PersonCommentNotFoundException:Exception
    {
        //public ZeroValueException():base("Bad Web Request. I received a zero index. Try again. If problem presists, contact your systems Administrator")
        //{

        //}


        //http://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/


        private static string msg = "Comment does not exist for this person. ";


        public PersonCommentNotFoundException()
            : base(msg) { }
    
        public PersonCommentNotFoundException(string message)
            : base(msg + message) 
        
        {

            
        }
    
        public PersonCommentNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public PersonCommentNotFoundException(string message, Exception innerException)
            : base(msg + message, innerException) { }
    
        public PersonCommentNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PersonCommentNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        }
}


