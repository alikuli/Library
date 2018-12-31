using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        public AddressVerificationRequest GetAddressVerificationRequest(string addressId)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("Please log in");
            addressId.IsNullOrWhiteSpaceThrowArgumentException("No Address Id");


            AddressMain address = Find(addressId);
            address.IsNullThrowException("Address Not found");


            return load_AddressVerificationRequestModel(addressId, address);
        }

        private AddressVerificationRequest load_AddressVerificationRequestModel(string addressId, AddressMain address)
        {
            addressId.IsNullOrWhiteSpaceThrowArgumentException("addressId");
            AddressVerificationRequest avr = new AddressVerificationRequest();
            avr.AddressMailForm = address.ToPostalHTML();

            avr.AddressId = addressId;
            avr.CountryId = address.CountryId;
            //avr.CourierInternationalPayment = VerificationConfig.Sale_Courier_International.ToString();
            //avr.CourierLocalPayment = VerificationConfig.Sale_Courier_Local.ToString();
            //avr.PostalInternationalPayment = VerificationConfig.Sale_Postal_International.ToString();
            //avr.PostalLocalPayment = VerificationConfig.Sale_Postal_Local.ToString();

            avr.CourierBtnCaption = "";
            MailLocalOrForiegnENUM mailLocalOrForiegnEnum = IsAddressInPakistan(addressId) ? MailLocalOrForiegnENUM.InPakistan : MailLocalOrForiegnENUM.OutOfPakistan;

            string postBtnCaption;
            getVerificaionCharges(MailServiceENUM.Post, mailLocalOrForiegnEnum, out postBtnCaption);
            avr.PostalBtnCaption = postBtnCaption;

            string courierBtnCaption;
            getVerificaionCharges(MailServiceENUM.Courier, mailLocalOrForiegnEnum, out courierBtnCaption);
            avr.CourierBtnCaption = courierBtnCaption;



            avr.MailLocalOrForeignEnum = IsAddressInPakistan(addressId) ?
                MailLocalOrForiegnENUM.InPakistan :
                MailLocalOrForiegnENUM.OutOfPakistan;

            return avr;
        }



    }
}
