using AliKuli.Extentions;
using ConfigManagerLibrary;
using ModelsClassLibrary.ModelsNS.AddressNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        public AddressVerificationRequest GetAddressVerificationRequest(string addressId)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("Please log in");
            addressId.IsNullOrWhiteSpaceThrowArgumentException("No Address Id");


            AddressWithId address = Find(addressId);
            address.IsNullThrowException("Address Not found");


            return load_AddressVerificationRequestModel(addressId, address);
        }

        private static AddressVerificationRequest load_AddressVerificationRequestModel(string addressId, AddressWithId address)
        {
            AddressVerificationRequest avr = new AddressVerificationRequest();
            avr.AddressMailForm = address.ToPostalHTML();

            avr.AddressId = addressId;
            avr.CountryId = address.CountryId;
            avr.CourierInternationalPayment = VerificationConfig.Cost_Courier_International.ToString();
            avr.CourierLocalPayment = VerificationConfig.Cost_Courier_Local.ToString();
            avr.PostalInternationalPayment = VerificationConfig.Cost_Postal_International.ToString();
            avr.PostalLocalPayment = VerificationConfig.Cost_Postal_Local.ToString();


            return avr;
        }



    }
}
