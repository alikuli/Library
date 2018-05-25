using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;


using UserModels;

namespace InterfacesLibrary.AddressNS
{
    public interface IAddressWithId : ICommonWithId
    {
        AddressType AddressType { get; set; }
        ApplicationUser User { get; set; }
        string UserId { get; set; }
        AddressComplex AddressComplex { get; set; }


    }
}
