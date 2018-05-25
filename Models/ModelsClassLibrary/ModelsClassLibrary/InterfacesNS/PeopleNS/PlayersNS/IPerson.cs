using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Common;
namespace InterfacesLibrary.PeopleNS
{
    public interface IPerson 
        //: IEncryption
    {
        //MetaDataComplex Common { get; set; }
        string IdentificationNo { get; set; }
        string FName { get; set; }
        string LName { get; set; }
        string MName { get; set; }
        SexENUM Sex { get; set; }
        string NameOfFatherOrHusband { get; set; }

        SonOfWifeOfDotOfENUM SonOfOrWifeOf { get; set; }
        void LoadFrom(IPerson p);
        string PersonFullName();


    }
}
