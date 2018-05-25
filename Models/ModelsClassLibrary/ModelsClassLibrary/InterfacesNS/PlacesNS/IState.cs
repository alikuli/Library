using System;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace InterfacesLibrary.PlacesNS
{
    public interface IState : ICommonWithId
    {
        string CountryId { get; set; }
        Country Country { get; set; }
        string Abbreviation { get; set; }

    }
}
