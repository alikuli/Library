using AliKuli.Extentions;
using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {


        /// <summary>
        /// This
        /// </summary>
        /// <param name="avr"></param>
        /// <returns></returns>
        public AddressVerificationRequest GetAddressVerificationRequestConfirmationHttp(string addressId, MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnENUM)
        {


            var avr2 = GetAddressVerificationRequest(addressId);
            avr2.MailServiceEnum = mailServiceEnum;
            avr2.IsSure1 = true;
            avr2.DateIsSure1 = DateTime.UtcNow;
            
            string btnCaption;
            avr2.PaymentAmount = getVerificaionCharges(avr2, out btnCaption);
            
            return avr2;
        }

        private double getVerificaionCharges(AddressVerificationRequest avr, out string btnCaption)
        {
            double charges = getVerificaionCharges(avr.MailServiceEnum, avr.MailLocalOrForeignEnum, out btnCaption);
            return charges;
        }



        private double getVerificaionCharges(MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, out string btnCaption)
        {
            string error = "";
            double verificationCost = 0;
            btnCaption = "Error";
            switch (mailServiceEnum)
            {
                case MailServiceENUM.Post:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            //is in Pakistan
                            verificationCost = VerificationConfig.Sale_Postal_Local;
                            btnCaption = string.Format("POST [pkrs {0}]", verificationCost);
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            //is foreign
                            verificationCost = VerificationConfig.Sale_Postal_International;
                            btnCaption = string.Format("POST [pkrs {0}]", verificationCost);
                            break;
                        case MailLocalOrForiegnENUM.Unknown:
                        default:
                            error = string.Format("No such option");
                            throw new Exception(error);
                    }
                    break;
                case MailServiceENUM.Courier:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            //is in Pakistan
                            verificationCost = VerificationConfig.Sale_Courier_Local;
                            btnCaption = string.Format("COURIER [pkrs {0}]", verificationCost);
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            verificationCost = VerificationConfig.Sale_Courier_International;
                            btnCaption = string.Format("COURIER [pkrs {0}]", verificationCost);
                            //is foreign
                            break;
                        case MailLocalOrForiegnENUM.Unknown:
                        default:
                            error = string.Format("No such option");
                            throw new Exception(error);
                    }
                    break;
                default:
                    error = string.Format("No such option: {0}", mailServiceEnum.ToString().ToTitleSentance());
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
