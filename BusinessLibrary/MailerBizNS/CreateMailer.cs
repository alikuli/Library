using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using System;
using System.Linq;
using System.Reflection;
using UserModels;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        /// <summary>
        /// This creates and saves a mailer. It makes sure that there is only one created.
        /// Also, it ensures that deposit/account are taken care of
        /// </summary>
        /// <param name="vm"></param>
        public void CreateAndSaveMailer(CreateMailerVM vm)
        {
            //ControllerCreateEditParameter para
            vm.UserId.IsNullOrWhiteSpaceThrowArgumentException("User is required!");

            //first check if a mailer exists for the mailer
            bool mailerExists = FindAll().Any(x => x.UserId == vm.UserId);

            if (mailerExists)
            {
                ErrorsGlobal.Add("Mailer already exists for user!",MethodBase.GetCurrentMethod(),null);
                string error = ErrorsGlobal.ToString();
                throw new Exception(error);
            }

            ApplicationUser user = UserBiz.Find(vm.UserId);
            user.IsNullThrowException("No such user.");

            //Make sure user has enough deposit etc to become a mailer
            //todo 
            ErrorsGlobal.AddMessage("Financial checks need to be done!");

            Mailer mailer = new Mailer();

            mailer.Name = user.UserName;
            mailer.UserId = user.Id;
            mailer.TrustLevelEnum = vm.TrustLevelEnum;

            user.Mailer = mailer;
            user.MailerId = mailer.Id;

            CreateAndSave(mailer);
            ErrorsGlobal.AddMessage(string.Format("Mailer created for {0}", user.UserName));

        }




        //List<AddressVerificationTrx> _verfTrxRequestList;
        //List<AddressVerificationTrx> VerfTrxRequestList
        //{
        //    get
        //    {
        //        return _verfTrxRequestList ?? (_verfTrxRequestList = AddressVerificationTrxBiz.FindAll().Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Requested).ToList());
        //    }
        //}

        //private long Total_Foreign_Courier_Available()
        //{

        //    //get all the Request Trx
        //    return getVerificationListCountFor(MailServiceENUM.Courier, MailLocalOrForiegnENUM.OutOfPakistan);
        //}

        //private long getVerificationListCountFor(MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum)
        //{
        //    if (VerfTrxRequestList.IsNullOrEmpty())
        //        return 0;

        //    long lst = VerfTrxRequestList.Where(
        //        x => x.MailServiceEnum == mailServiceEnum &&
        //            x.MailLocalOrForiegnEnum == mailLocalOrForiegnEnum)
        //            .Count();

        //    return lst;
        //}

        //private long Foreign_Postal_Verifications_Available()
        //{
        //    return getVerificationListCountFor(MailServiceENUM.Post, MailLocalOrForiegnENUM.OutOfPakistan);
        //}

        //private long Pakistan_Courier_Verifications_Available()
        //{
        //    return getVerificationListCountFor(MailServiceENUM.Courier, MailLocalOrForiegnENUM.InPakistan);
        //}

        //private long Pakistan_Postal_Verifications_Available()
        //{
        //    return getVerificationListCountFor(MailServiceENUM.Post, MailLocalOrForiegnENUM.InPakistan);

        //}


        //List<AddressVerificationHdr> FindAllVerifHeadersForMailer(string mailerId)
        //{
        //    Mailer mailer = Find(mailerId);


        //    if(mailer.IsNull())
        //        return null;

        //    if(mailer.AddressVerificationHdrs.IsNullOrEmpty())
        //        return null;

        //    return mailer.AddressVerificationHdrs.ToList();
        //}


        //private string GetMailerIdFor(string userId)
        //{
        //    if (userId.IsNullOrWhiteSpace())
        //        return "";

        //    Mailer mailer = FindAll().FirstOrDefault(x => x.UserId == userId);
        //    if (mailer.IsNull())
        //        return "";
        //    return mailer.Id;
        //}

        //private int Total_Open_Mailings_For_Mailer(string mailerId)
        //{
        //    List<AddressVerificationHdr> allVerfHdrs = FindAllVerifHeadersForMailer(mailerId);

        //    if (allVerfHdrs.IsNullOrEmpty())
        //        return 0;

        //    int numberInProccess= allVerfHdrs.Where(x => x.SuccessEnum == SuccessENUM.Inproccess).Count();
        //    return numberInProccess;
        //}

        //public MailerVMForAssigningVerifList CreateMailerVMForAssigningVerifList()
        //{
        //    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");

        //    MailerVMForAssigningVerifList mv = CreateAssignMailingListModel(UserId);

        //    mv.Foreign_Courier_Verifications_Available = Total_Foreign_Courier_Available().ToString();
        //    mv.Foreign_Postal_Verifications_Available = Foreign_Postal_Verifications_Available().ToString();
        //    mv.Pakistan_Courier_Verifications_Available = Pakistan_Courier_Verifications_Available().ToString();
        //    mv.Pakistan_Postal_Verifications_Available = Pakistan_Postal_Verifications_Available().ToString();

        //    mv.MailerId = GetMailerIdFor(UserId);
        //    mv.MailLocalOrForiegnEnum = MailLocalOrForiegnENUM.Unknown;
        //    mv.MailServiceEnum = MailServiceENUM.Unknown;
        //    mv.Total_Open_Mailings_For_Mailer = Total_Open_Mailings_For_Mailer(mv.MailerId).ToString();
        //    return mv;
        //}

    }
}
