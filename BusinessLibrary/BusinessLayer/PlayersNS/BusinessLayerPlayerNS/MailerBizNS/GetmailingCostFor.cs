using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.MailerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        public MailingCostsVM CreateMailingCostsVm(string addressVerificationHdrId)
        {
            addressVerificationHdrId.IsNullOrWhiteSpaceThrowArgumentException("addressVerificationHdrId");
            AddressVerificationHdr addressVerificationHdr = AddressVerificationHdrBiz.Find(addressVerificationHdrId);
            addressVerificationHdr.IsNullThrowException();

            MailingCostsVM mailingCostsVM = new MailingCostsVM();
            mailingCostsVM.AddressVerificationHdr = addressVerificationHdr;
            mailingCostsVM.TotalLettersMailed = addressVerificationHdr.AddressVerificationTrxs.Count;
            mailingCostsVM.AddressVerificationHdrId = addressVerificationHdrId;
            return mailingCostsVM;

        }

        public double getVerificaionCost(AddressVerificationRequest avr)
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
                            verificationCost = VerificationConfig.Sale_Postal_Local;
                            break;
                        case false:
                            //is foreign
                            verificationCost = VerificationConfig.Sale_Postal_International;
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
                            verificationCost = VerificationConfig.Sale_Courier_Local;
                            break;
                        case false:
                            verificationCost = VerificationConfig.Sale_Courier_International;
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
    }
}
