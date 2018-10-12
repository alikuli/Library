using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using System.Collections.Generic;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    public class ErrorSingle : IErrorSingle
    {
        public ErrorSingle(string libName, string className, string customErrorMessage)
        {
            Date = DateTime.UtcNow;
            CustomErrorMessage = customErrorMessage;
            LibraryName = libName;
            ClassName = className;

        }
        public ErrorSingle(string libName, string className, string customErrorMessage, string methodName)
            : this(libName, className, customErrorMessage)
        {
            MethodName = methodName;
            Date = DateTime.UtcNow;

        }

        public ErrorSingle(string libName, string className, string customErrorMessage, MethodBase methodBase, Exception exception) :
            this(libName, className, customErrorMessage, methodBase.Name)
        {
            MethodBase = methodBase;
            ExceptionIn = exception;
        }


        #region IErrorSingle Members



        public DateTime Date { get; set; }

        public string LibraryName { get; set; }


        public string ClassName { get; set; }

        public MethodBase MethodBase { get; set; }
        public string MethodName { get; set; }

        public string CustomErrorMessage { get; set; }

        public Exception ExceptionIn { get; set; }

        public string InnerErrorMessage
        {
            get
            {
                string errMsg = "";
                if (!ExceptionIn.IsNull())
                {
                    if (ExceptionIn is DbEntityValidationException)
                        errMsg = ErrorHelpers.Get_DbEntityValidationException((DbEntityValidationException)ExceptionIn);
                    else
                        errMsg = ErrorHelpers.GetInnerException(ExceptionIn);

                }
                return errMsg;
            }

        }

        private string GetCallForExceptionThisMethod()
        {
            if (ExceptionIn.IsNull())
                return null;

            StackTrace trace = new StackTrace(ExceptionIn);
            StackFrame previousFrame = null;

            foreach (StackFrame frame in trace.GetFrames())
            {
                if (frame.GetMethod() == MethodBase)
                {
                    break;
                }

                previousFrame = frame;
            }

            return previousFrame != null ? previousFrame.GetMethod().Name : null;
        }


        private string MakeHeading()
        {
            StringBuilder sb = new StringBuilder();

            if (!LibraryName.IsNullOrEmpty())
                sb.Append(string.Format("{0}.", LibraryName));

            if (!ClassName.IsNullOrEmpty())
                sb.Append(string.Format("{0}.", ClassName));

            return sb.ToString();
        }



        public override string ToString()
        {
            ConfigManagerHelper configHelper = new ConfigManagerHelper();
            Date = DateTime.UtcNow;

            StringBuilder sb = new StringBuilder();

            if (configHelper.IsVerbose)
            {
                string heading = MakeHeading();

                if (!heading.IsNullOrWhiteSpace())
                {
                    sb.Append(heading + " ");
                }
            }

            sb.Append(string.Format("{0} ", CustomErrorMessage));

            if (configHelper.IsVerbose)
            {
                if (!ExceptionIn.IsNull())
                    sb.Append(string.Format("*** {0} ***", InnerErrorMessage));

                string realMethodWithException = GetCallForExceptionThisMethod();
                if (!realMethodWithException.IsNullOrWhiteSpace())
                    sb.Append(string.Format("### Actual Method where error happened '{0}' ###", realMethodWithException));

                if (!ExceptionIn.IsNull())
                    sb.Append(string.Format("$$$ {0} $$$", ExceptionIn.ToString()));
            }

            if (sb.ToString().IsNullOrWhiteSpace())
                return "";

            string dateString = (configHelper.IsVerbose ? string.Format("{0:yyyy-MM-dd H:mm:ss:ffff} ", Date) : "");

            return string.Format(dateString + sb.ToString());
        }


        //public string[] ErrorMessagesSplit()
        //{
        //    if(!ToString().IsNullOrEmpty())
        //    {
        //        string [] strToSplitOn = {" at "};
        //        var msgs = ToString().Split(
        //            strToSplitOn, 
        //            StringSplitOptions.RemoveEmptyEntries);
        //        return msgs;
        //    }
        //    return null;
        //}
        #endregion

    }
        
}
