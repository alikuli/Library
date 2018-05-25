using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Models.Shared;
using System;
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.Models.People
{
    public interface IPerson 
    {
        bool IsEncrypted { get; set; }
        string IdentificationNo { get; set; }
        string FName { get; set; }

        string LName { get; set; }
        string MName { get; set; }
        SexENUM Sex { get; set; }
        string NameOfFatherOrHusband { get; set; }

        SonOfWifeOfDotOfENUM SonOfOrWifeOf { get; set; }
        void LoadFrom(IPerson p);
        string Helper_CreateFullName();

        string FNameEncryptDecrypt { get; set; }

        string LNameEncryptDecrypt { get; set; }

        string MNameEncryptDecrypt { get; set; }

        string NameOfFatherOrHusbandEncryptDecrypt { get; set; }

    }
}
