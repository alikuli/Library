using System;
namespace ModelsClassLibrary.Models.Shared
{
    public interface ICommonNoIdBasics
    {

        /// <summary>
        /// If this is true, then overall encryption for the record is on.
        /// </summary>
        bool IsEncrypted { get; set; }

        string GetSelfClassName();

        string GetSelfMethodName();
        void SelfErrorCheck();
        void Initialize();
        
        

    }
}
