using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;


namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {


        public void UpdateAndSaveDefaultAddress(string userId, string addressId)
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
            PersonBiz.UpdateAndSave(person);

        }


    }
}
