using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        public CreateMailerVM Factory_CreateMailerVm()
        {
            CreateMailerVM vm = new CreateMailerVM();
            vm.TrustLevelEnum = EnumLibrary.EnumNS.VerificationNS.TrustLevelENUM.Level1;
            return vm;
        }


        ///// <summary>
        ///// This creates the view model for getting a mailing list.
        ///// </summary>
        ///// <param name="mailer"></param>
        ///// <returns></returns>
        //MailerVMForAssigningVerifList CreateAssignMailingListModel(Mailer mailer)
        //{
        //    mailer.IsNullThrowException("Mailer");
        //    MailerVMForAssigningVerifList mvm = new MailerVMForAssigningVerifList();

        //    mvm.MailerId = mailer.Id;
        //    mvm.Total_Open_Mailings_For_Mailer = TotalOpenMailingsForMailer(mailer)
        //        .TotalOutstanding
        //        .ToString();

        //    mvm.Pakistan_Courier_Verifications_Available = Get_Total_Pakistan_Courier_Verifications_Available().ToString();
        //    mvm.Pakistan_Postal_Verifications_Available = GetTotal_Pakistan_Postal_Verifications_Available().ToString();
        //    mvm.Foreign_Courier_Verifications_Available = GetTotal_Foreign_Courier_Verifications_Available().ToString();
        //    mvm.Foreign_Postal_Verifications_Available = Get_Total_Foreign_Postal_Verifications_Available().ToString();
        //    return mvm;
        //}


        //long Get_Total_Foreign_Postal_Verifications_Available()
        //{
        //    long total = AddressVerificationTrxBiz
        //        .GetVerificationRequestTrx_Other()
        //        .Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Requested && x.MailServiceEnum == MailServiceENUM.Post)
        //        .Count();

        //    return total;
        //}

        //long GetTotal_Foreign_Courier_Verifications_Available()
        //{
        //    long total = AddressVerificationTrxBiz
        //        .GetVerificationRequestTrx_Other()
        //        .Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Requested && x.MailServiceEnum == MailServiceENUM.Courier)
        //        .Count();

        //    return total;
        //}

        //long GetTotal_Pakistan_Postal_Verifications_Available()
        //{
        //    long total = AddressVerificationTrxBiz
        //        .GetVerificationRequestTrx_Pakistan()
        //        .Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Requested && x.MailServiceEnum == MailServiceENUM.Post)
        //        .Count();

        //    return total;
        //}

        //long Get_Total_Pakistan_Courier_Verifications_Available()
        //{
        //    long total = AddressVerificationTrxBiz
        //        .GetVerificationRequestTrx_Pakistan()
        //        .Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Requested && x.MailServiceEnum == MailServiceENUM.Courier)
        //        .Count();

        //    return total;
        //}



    }
}
