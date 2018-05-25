using System;

namespace ErrorHandlerLibrary
{
    public interface IErrorSingle
    {
        DateTime Date { get; set; }
        string LibraryName { get; set; }
        string ClassName { get; set; }
        string MethodName { get; set; }
        string CustomErrorMessage { get; set; }
        string InnerErrorMessage { get;  }
        Exception ExceptionIn { get; set; }
    }
}
