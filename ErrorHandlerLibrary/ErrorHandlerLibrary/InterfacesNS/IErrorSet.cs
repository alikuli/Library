using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
namespace ErrorHandlerLibrary
{
    public interface IErrorSet
    {
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
        global::ErrorHandlerLibrary.ExceptionsNS.ErrorSet MemoryRetrieve();
        bool MemorySave();
        global::System.Collections.Generic.ICollection<global::ErrorHandlerLibrary.IErrorSingle> Messages { get; set; }
        void SetLibAndClass(string libraryname, string className);
        global::System.Collections.Generic.List<string> ToList();
        global::System.Collections.Generic.ICollection<string> ToList_Messages();
        string ToString();
        string ToString_Messages();
        string UserName { get; set; }
    }
}
