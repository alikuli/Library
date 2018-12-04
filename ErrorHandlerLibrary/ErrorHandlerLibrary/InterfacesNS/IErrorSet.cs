using System;
using System.Collections.Generic;
using System.Reflection;
namespace ErrorHandlerLibrary
{
    public interface IErrorSet
    {
        bool DoNotClearMessages { get; set; }
        void Add(string customMsg, global::System.Reflection.MethodBase methodBase, Exception e = null);
        void Add(string customMsg, string methodName);
        void AddError_EmptyString(global::System.Reflection.MethodBase methodBase, string stringName);
        void AddError_InvalidBool(global::System.Reflection.MethodBase methodBase, string variableValue);
        void AddError_InvalidEmailFormat(global::System.Reflection.MethodBase methodBase, string variableValue);
        void AddError_InvalidFilePath(global::System.Reflection.MethodBase methodBase, string variableValue);
        void AddError_InvalidNumber(global::System.Reflection.MethodBase methodBase, string variableValue, string offenderName);
        void AddError_InvalidUrl(global::System.Reflection.MethodBase methodBase, string variableValue);
        void AddMessage(string customMsg);
        void AddMessage(string customMsg, global::System.Reflection.MethodBase methodBase, Exception e = null);
        string ClassName { get; set; }
        void ClearAllErrors();
        global::System.Collections.Generic.ICollection<global::ErrorHandlerLibrary.IErrorSingle> Errors { get; set; }
        string Get_DbEntityValidationException(global::System.Data.Entity.Validation.DbEntityValidationException e);
        bool HasErrors { get; }
        bool HasMessages { get; }
        string LibraryName { get; set; }
        //List<string> ListOfErrors { get; }
        string GetExceptionMessageString(string customMsg, MethodBase methodBase, Exception e);
        void MemoryRetrieve();
        bool MemorySave();
        global::System.Collections.Generic.ICollection<global::ErrorHandlerLibrary.IErrorSingle> Messages { get; set; }
        void SetLibAndClass(string libraryname, string className);
        global::System.Collections.Generic.List<string> ToListErrs();
        global::System.Collections.Generic.ICollection<string> ToList_Messages();
        string ToString();
        string ToString_Messages();
        string UserName { get; set; }
    }
}
