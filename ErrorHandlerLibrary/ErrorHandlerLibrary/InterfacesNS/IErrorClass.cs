using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Reflection;


namespace ErrorHandlerLibrary.ExceptionsNS
{
    public interface IErrorSet
    {
        void Add(string customMsg, MethodBase methodBase, Exception e = null);
        string Get_DbEntityValidationException(DbEntityValidationException e);
        bool HasErrors { get; }
        //bool LoadErrorMessages(ICollection<string> listIn);
        //string MethodName { get; set; }
        List<string> ToList();
        ICollection<IErrorSingle> Errors { get; set; }
        string UserName { get; set; }
        string ClassName { get; set; }
        string LibraryName { get; set; }
        void SetLibAndClass(string libraryname, string className);
        bool MemorySave();
        ErrorSet MemoryRetrieve();

        void AddError_InvalidUrl(MethodBase methodBase, string variableValue);
        void AddError_InvalidNumber(MethodBase methodBase, string variableValue, string offendingName);
        void AddError_InvalidBool(MethodBase methodBase, string variableName);
        void AddError_EmptyString(MethodBase methodBase, string stringName);


        //Messages/Warnings
        void AddMessage(string customMsg, MethodBase methodBase, Exception exception = null);
        ICollection<IErrorSingle> Messages { get; set; }
        ICollection<string> ToList_Messages();
        bool HasMessages { get; }

    }
}
