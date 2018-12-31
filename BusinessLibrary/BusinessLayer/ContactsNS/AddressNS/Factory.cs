

using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        public override ICommonWithId Factory()
        {
            ICommonWithId icommon = base.Factory();

            AddressMain address = icommon as AddressMain;

            address.AddressType.IsBillAddress = true;
            address.AddressType.IsInformAddress = true;
            address.AddressType.IsShipAddress = true;

            return icommon;
        }



    }
}
