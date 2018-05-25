using System;
using ModelsClassLibrary.ModelsNS.PlacesNS;
namespace ModelsClassLibrary.Models.AddressNS
{
    public interface IAddressWithTownClass: IAddressComplex
    {

        Guid?  TownId { get; set; }
        Town Town { get; set; }
        void LoadFrom(IAddressWithTownClass iAddressWithTownClass);

    }
}
