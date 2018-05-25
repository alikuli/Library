using System;
namespace InterfacesLibrary.Interfaces.PlacesNS
{
    public interface ICountry
    {
        string Abbreviation { get; set; }
        string FullName();
        int LengthOfCompleteCnicNumber { get; set; }
        void SelfErrorCheck();
        string ToString();
    }
}
