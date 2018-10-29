using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        public AddressVerificationRequest GetAddressVerificationRequestConfirmation(AddressVerificationRequest avr)
        {
            //check to see if the bugger is a mailer!

            if (!avr.IsSure1)
                throw new Exception("User not sure about making payment!");

            var avr2 = GetAddressVerificationRequest(avr.AddressId);
            avr2.MailServiceEnum = avr.MailServiceEnum;
            avr2.IsSure1 = avr.IsSure1;
            avr2.DateIsSure1 = DateTime.UtcNow;
            avr2.PaymentAmount = getVerificaionCost(avr2);
            return avr2;
        }

        double getVerificaionCost(AddressVerificationRequest avr)
        {
            string error = "";
            double verificationCost = 0;

            switch (avr.MailServiceEnum)
            {
                case MailServiceENUM.Post:
                    switch (CountryBiz.IsAddressInPakistan(avr.CountryId))
                    {
                        case true:
                            //is in Pakistan
                            verificationCost = VerificationConfig.Cost_Postal_Local;
                            break;
                        case false:
                            //is foreign
                            verificationCost = VerificationConfig.Cost_Postal_International;
                            break;
                        default:
                            error = string.Format("No such option");
                            throw new Exception(error);
                    }
                    break;
                case MailServiceENUM.Courier:
                    switch (CountryBiz.IsAddressInPakistan(avr.CountryId))
                    {
                        case true:
                            //is in Pakistan
                            verificationCost = VerificationConfig.Cost_Courier_Local;
                            break;
                        case false:
                            verificationCost = VerificationConfig.Cost_Courier_International;
                            //is foreign
                            break;
                        default:
                            error = string.Format("No such option");
                            throw new Exception(error);
                    }
                    break;
                default:
                    error = string.Format("No such option: {0}", avr.MailServiceEnum);
                    throw new Exception(error);
            }

            return verificationCost;
        }
        //public void issueAddressVerificationRequest(AddressVerificationRequest avr)
        //{
        //    avr.AddressId.IsNullOrWhiteSpaceThrowArgumentException("id");

        //    if (avr.MailServiceEnum == MailServiceENUM.Unknown)
        //        throw new Exception("Mail Service is unknown");

        //    if (avr.IsSure1 == false)
        //        throw new Exception("Not Sure 1");

        //    if (avr.IsSure2 == false)
        //        throw new Exception("Not Sure 2");

        //    if (avr.PaymentAmount == 0)
        //        throw new Exception("No payment amount.");


        //    AddressWithId address = Find(avr.AddressId);
        //    address.IsNullThrowException("Address Not found");

        //    AddressVerificationTrx addyVerfTrx = updateAddressVerificationTrxForPosting(avr, address);

        //    updateAddressForVerification(address, addyVerfTrx);
        //    UpdateAndSave(address);

        //}

        //AddressVerificationTrx updateAddressVerificationTrxForPosting(AddressVerificationRequest avr, AddressWithId address)
        //{
        //    address.Verification.UpdateDateFor(EnumLibrary.EnumNS.VerificaionStatusENUM.Requested);

        //    //create a Verification Request
        //    AddressVerificationTrx addyVerfTrx = AddressVerificationTrxBiz.Factory() as AddressVerificationTrx;
        //    addyVerfTrx.Address = address;
        //    addyVerfTrx.AddressId = avr.AddressId;

        //    addyVerfTrx.MailServiceEnum = avr.MailServiceEnum;
        //    addyVerfTrx.Name = string.Format("{0}-{1}", UserName, address.Name);

        //    return addyVerfTrx;
        //}


        //void updateAddressForVerification(AddressWithId address, AddressVerificationTrx addyVerfTrx)
        //{
        //    if (address.AddressVerificationTrxs.IsNull())
        //    {
        //        address.AddressVerificationTrxs = new List<AddressVerificationTrx>();
        //    }
        //    address.AddressVerificationTrxs.Add(addyVerfTrx);
        //    address.Verification.VerificaionStatusEnum = VerificaionStatusENUM.Requested;

        //    AddressVerificationTrxBiz.CreateEntity(addyVerfTrx);
        //}



    }
}
