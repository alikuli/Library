using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace InterfacesLibrary.PlacesNS
{
    public interface ICountry : ICommonWithId
    {
        string Abbreviation { get; set; }

        //int LengthOfCompleteCnicNumber { get; set; }
    }
}
