using System;
using InterfacesLibrary.SharedNS;

namespace InterfacesLibrary.PlacesNS
{
    public interface ITown : ICommonWithId
    {
        ICity City { get; set; }
        string CityID { get; set; }
        string Abbreviation { get; set; }

    }
}
