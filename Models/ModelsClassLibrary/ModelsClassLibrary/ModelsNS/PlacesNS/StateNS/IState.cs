using System;
//using InterfacesLibrary.DiscountNS;
namespace InterfacesLibrary.Interfaces.PlacesNS
{
    interface IState
    {
        //ICollection<ICity> Cities { get; set; }
        ICountry Country { get; set; }
        Guid CountryId { get; set; }
        //ICollection<IDiscount> Discounts { get; set; }
        string FullName();
        void SelfErrorCheck();
        string StateAbbreviation { get; set; }
        string ToString();
    }
}
