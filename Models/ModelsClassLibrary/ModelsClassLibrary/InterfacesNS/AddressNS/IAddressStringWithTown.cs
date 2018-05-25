using System;
using InterfacesLibrary.AddressNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

namespace ModelsClassLibrary.InterfacesNS.AddressNS
{
    public interface IAddressStringWithTown : IAddressString
    {
        Town Town { get; set; }
        Guid? TownId { get; set; }
    }
}
