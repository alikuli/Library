using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;


namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {


        public void UpdateAndSaveDefaultAddress(string userId, string addressId, GlobalObject globalObject)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("userId");
            addressId.IsNullOrWhiteSpaceThrowArgumentException("AddressId is null");

            AddressMain address = Find(addressId);
            address.IsNullThrowException("Address");

            //the default address must always be all three
            if (!address.AddressType.IsBillAddress || !address.AddressType.IsShipAddress || !address.AddressType.IsInformAddress)
            {
                if (!address.AddressType.IsBillAddress)
                    ErrorsGlobal.AddMessage("Updated to Billing address.");

                if (!address.AddressType.IsShipAddress)
                    ErrorsGlobal.AddMessage("Updated to Shipping address.");

                if (!address.AddressType.IsInformAddress)
                    ErrorsGlobal.AddMessage("Updated to Inform To address.");

                address.AddressType.IsBillAddress = true;
                address.AddressType.IsShipAddress = true;
                address.AddressType.IsInformAddress = true;
                Update(address);
            }

            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("Person not found");

            person.DefaultBillAddressId = addressId;

            ControllerCreateEditParameter param = new ControllerCreateEditParameter();
            param.Entity = person as ICommonWithId;
            param.GlobalObject = globalObject;

            PersonBiz.UpdateAndSave(param);

            //PersonBiz.UpdateAndSave(person);

        }


    }
}
