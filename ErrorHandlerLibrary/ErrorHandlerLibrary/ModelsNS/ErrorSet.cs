using AliKuli.Extentions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using WebLibrary.Programs;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    /// <summary>
    /// Use this class. This has lots of features. You can load a list of error messages and receive it back as a ToString or a ToList
    /// </summary>
    public class ErrorSet : IErrorSet
    {

        IMemoryMain _memory;
        public ErrorSet()
        {
            Errors = new List<IErrorSingle>();
            Messages = new List<IErrorSingle>();
        }

        [Inject]
        public ErrorSet(IMemoryMain memoryMain)
            : this()
        {
            _memory = memoryMain;
            if (_memory.IsNull())
                throw new Exception("Memory is null. ErrorSet");
            //UserName = userName;
        }
        List<string> _listOfErrors = new List<string>();
        //public List<string> ListOfErrors
        //{
        //    get
        //    {
        //        _listOfErrors.Clear();

        //        if(!_dbEntityValidationErrors.IsNullOrEmpty())
        //        {
        //            foreach (var err in _dbEntityValidationErrors)
        //            {
        //                _listOfErrors.Add(err);
        //            }
        //        }

        //        if(!Errors.IsNullOrEmpty())
        //        {
        //            foreach (var err in Errors)
        //            {
        //                _listOfErrors.Add(err.ToString());
        //            }

        //        }
        //        return _listOfErrors;
        //    }
        //}

        //List<string> _dbEntityValidationErrors = new List<string>();

        public string Get_DbEntityValidationException(DbEntityValidationException e)
        {
            //List<String> lstErrors = new List<string>();
            StringBuilder sb = new StringBuilder();
            int count = 0;
            if (e.EntityValidationErrors != null && e.EntityValidationErrors.Count() > 0)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    count++;
                    string msg = string.Format("{2}. Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State,
                        count);

                    sb.Append(msg);
                    //_dbEntityValidationErrors.Add(msg);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        sb.Append(msg);
                        //_dbEntityValidationErrors.Add(msg);

                    }
                }
            }

            string innerException = ErrorHelpers.GetInnerException(e);

            //Now add any inner Exception errors
            if (!innerException.IsNullOrWhiteSpace())
            {
                string msg = "Inner Exception is: '" + ErrorHelpers.GetInnerException(e) + "'";
                sb.Append(msg);
                //_dbEntityValidationErrors.Add(msg);

            }
            return sb.ToString();
        }

        public string UserName { get; set; }
        public string LibraryName { get; set; }

        public string ClassName { get; set; }

        #region Add


        public string GetExceptionMessageString(string customMsg, MethodBase methodBase, Exception e = null)
        {
            var errSingle = setupExceptionMessage(customMsg, methodBase, e);
            return errSingle.ToString();
        }

        private ErrorSingle setupExceptionMessage(string customMsg, MethodBase methodBase, Exception e = null)
        {
            if (e.IsNull())
                if (customMsg.IsNullOrWhiteSpace())
                    return null;

            ErrorSingle errSingle = new ErrorSingle(LibraryName, ClassName, customMsg, methodBase, e);
            return errSingle;
        }

        public void Add(string customMsg, MethodBase methodBase, Exception e = null)
        {
            var errSingle = setupExceptionMessage(customMsg, methodBase, e);
            Errors.Add(errSingle);
        }

        public void Add(string customMsg, string methodName)
        {
            if (customMsg.IsNullOrWhiteSpace())
                return;

            ErrorSingle errSingle = new ErrorSingle(LibraryName, ClassName, customMsg, methodName);
            Errors.Add(errSingle);
        }


        public void AddError_InvalidUrl(MethodBase methodBase, string variableValue)
        {

            string errMsg = string.Format("Invalid URL value.  It's current value is: '{0}'.", variableValue);
            Add(errMsg, methodBase);

        }

        public void AddError_InvalidNumber(MethodBase methodBase, string variableValue, string offenderName)
        {

            string errMsg = string.Format("'{0}' has an invalid number value.  This can only be numeric. It's current value is: '{1}'.", offenderName, variableValue);
            Add(errMsg, methodBase);

        }

        public void AddError_InvalidBool(MethodBase methodBase, string variableValue)
        {

            string errMsg = string.Format("Invalid True-False. It can only have a value of true or false. It's current value is: '{0}'.", variableValue);
            Add(errMsg, methodBase);

        }

        public void AddError_InvalidFilePath(MethodBase methodBase, string variableValue)
        {

            string errMsg = string.Format("Invalid Filepath. It's current value is: '{0}'.", variableValue);
            Add(errMsg, methodBase);

        }

        public void AddError_InvalidEmailFormat(MethodBase methodBase, string variableValue)
        {

            string errMsg = string.Format("Invalid Email Format. It's current value is: '{0}'.", variableValue);
            Add(errMsg, methodBase);

        }


        public void AddError_EmptyString(MethodBase methodBase, string stringName)
        {

            string errMsg = string.Format("{0} is empty.", stringName);
            Add(errMsg, methodBase);

        }

        #endregion

        public ICollection<IErrorSingle> Errors { get; set; }

        public bool HasErrors
        {
            get
            {
                return !Errors.IsNullOrEmpty();
            }
        }

        public List<string> ToListErrs()
        {
            List<string> strList = new List<string>();

            if (HasErrors)
                foreach (var error in Errors)
                {
                    strList.Add(error.ToString());
                }

            return strList;

        }

        public void SetLibAndClass(string libraryname, string className)
        {
            LibraryName = libraryname;
            ClassName = className;
        }


        public void ClearAllErrors()
        {
            if (HasErrors)
            {
                Errors.Clear();
            }
        }


        #region Memory

        private const string LOCATION = "ErrorsLocation";

        /// <summary>
        /// Saves as string. If success, it returns true.
        /// </summary>
        /// <param name="httpCtx"></param>
        /// <returns></returns>
        /// 

        public bool MemorySave()
        {

            _memory.SessionMemory.ClearFor(LOCATION);
            _memory.SessionMemory.SaveTo(LOCATION, this);
            return true;

        }

        /// <summary>
        /// The errors are saved as a string and added to the errors list. If it is successful to retrieve, it returns true, o/w false.
        /// </summary>
        /// <param name="httpCtx"></param>
        /// <returns></returns>
        public ErrorSet MemoryRetrieve()
        {

            object retrievedObject = _memory.SessionMemory.GetFrom(LOCATION) as object;

            if (!retrievedObject.IsNull())
            {
                ErrorSet errorSetFromMemory = retrievedObject as ErrorSet;

                if (errorSetFromMemory.IsNull())
                    return null;

                //Get back to state.
                this.Errors = errorSetFromMemory.Errors;
                this.Messages = errorSetFromMemory.Messages;
                this.ClassName = errorSetFromMemory.ClassName;
                this.LibraryName = errorSetFromMemory.LibraryName;
                this.UserName = errorSetFromMemory.UserName;

                return errorSetFromMemory;
            }
            return null;


        }

        #endregion

        public override string ToString()
        {
            string errorString = "";
            if (!Errors.IsNullOrEmpty())
            {
                foreach (var errorSingle in Errors)
                    errorString += string.Format("{0}", errorSingle.ToString());
            }
            return errorString;
        }



        #region Messages
        public ICollection<IErrorSingle> Messages { get; set; }

        public bool HasMessages
        {
            get
            {
                return !Messages.IsNullOrEmpty();
            }
        }

        public void AddMessage(string customMsg)
        {
            if (customMsg.IsNullOrWhiteSpace())
                return;

            ErrorSingle errSingle = new ErrorSingle(LibraryName, ClassName, customMsg);
            Messages.Add(errSingle);

        }
        public void AddMessage(string customMsg, MethodBase methodBase, Exception e = null)
        {
            if (customMsg.IsNullOrWhiteSpace())
                return;

            ErrorSingle errSingle = new ErrorSingle(LibraryName, ClassName, customMsg, methodBase, e);
            Messages.Add(errSingle);
        }
        public string ToString_Messages()
        {
            string errorString = "";
            if (HasMessages)
            {
                foreach (var errorSingle in Messages)
                    errorString += string.Format("{0}", errorSingle.ToString());

            }
            return errorString;
        }

        public ICollection<string> ToList_Messages()
        {
            List<string> strList = new List<string>();

            if (HasMessages)
                foreach (var error in Messages)
                {
                    strList.Add(error.ToString());
                }

            return strList;

        }

        #endregion
    }
}
