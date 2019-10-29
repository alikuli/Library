using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.MailerNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        public void UpdateAndSaveMailingCost(MailingCostsVM mailingCostsVM, GlobalObject globalObject)
        {
            mailingCostsVM.AddressVerificationHdrId.IsNullOrWhiteSpaceThrowArgumentException("View has lost the AddressVerificationHdrId");

            AddressVerificationHdr addressVerificationHdr = AddressVerificationHdrBiz.Find(mailingCostsVM.AddressVerificationHdrId);
            addressVerificationHdr.IsNullThrowException("AddressVerificationHdr not found.");

            if (mailingCostsVM.Cost == 0)
                throw new Exception("Mailing cost cannot be 0. Please try again.");

            if (mailingCostsVM.TotalLettersMailed == 0)
                throw new Exception("Total letters mailed cannot be 0. Try again.");

            if (mailingCostsVM.TotalLettersMailed < addressVerificationHdr.AddressVerificationTrxs.Count)
                ErrorsGlobal.AddMessage("You have left over letters. Please go to Letter Cancelation screen");

            if (mailingCostsVM.TotalLettersMailed > addressVerificationHdr.AddressVerificationTrxs.Count)
                ErrorsGlobal.AddMessage("You cannot mail more letters than you have! Please try again");


            //update fields
            addressVerificationHdr.TotalQtyLettersMailed = mailingCostsVM.TotalLettersMailed;
            addressVerificationHdr.BudgetedCost =
                mailingCostsVM.TotalLettersMailed *
                MailerBiz.GetBugetedCost(addressVerificationHdr.MailLocalOrForiegnEnum, addressVerificationHdr.MailServiceEnum);

            addressVerificationHdr.Verification.VerificaionStatusEnum = VerificaionStatusENUM.Mailed;
            addressVerificationHdr.Verification.MailedDate.SetToTodaysDate(UserName, UserId);

            //update all the children to mailed.
            addressVerificationHdr.AddressVerificationTrxs.IsNullOrEmptyThrowException("There are no letters");
            foreach (AddressVerificationTrx letter in addressVerificationHdr.AddressVerificationTrxs)
            {
                letter.Verification.VerificaionStatusEnum = VerificaionStatusENUM.Mailed;
                letter.Verification.MailedDate.SetToTodaysDate(UserName, UserId);
                AddressVerificationTrxBiz.Update(letter);
            }

            ControllerCreateEditParameter param = new ControllerCreateEditParameter();
            param.Entity = addressVerificationHdr as ICommonWithId;
            param.GlobalObject = globalObject;

            AddressVerificationHdrBiz.UpdateAndSave(param);





        }



    }
}
