using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;


using UserModels;

namespace InterfacesLibrary.AddressNS
{
    public interface IAddressMain : ICommonWithId, IAddressStringWithNames
    {
        AddressTypeComplex AddressType { get; set; }
        //ApplicationUser User { get; set; }
        //string PersonId { get; set; }
        //AddressComplex AddressComplex { get; set; }


    }
}
