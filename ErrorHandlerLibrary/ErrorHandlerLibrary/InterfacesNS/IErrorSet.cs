using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
namespace ErrorHandlerLibrary
{
    public interface IErrorSet
    {
        string LibraryName { get; set; }
        string ClassName { get; set; }

        void Add(string customMsg, string methodName, Exception e = null);
        ICollection<IErrorSingle> Errors { get; set; }
        string Get_DbEntityValidationException(DbEntityValidationException e);
        bool HasErrors { get; }
        ICollection<string> ToList();

        string UserName { get; set; }

        void ResetLibAndClass(string libraryname, string className);
        void ResetLibAndClass(string className);


    }
}
