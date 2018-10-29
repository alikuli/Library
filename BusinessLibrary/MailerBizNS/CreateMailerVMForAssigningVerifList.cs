using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {
        /// <summary>
        /// This is loaded during CreateMailerVMForAssigningVerifList
        /// </summary>
        ICollection<AddressVerificationHdr> Inprocess_AddressVerificationHdrsList (string mailerId)
        {
            //all the ones in proccess can be printed.
            return GetAllHeaders_InProccess(mailerId);
        }

        /// <summary>
        /// This is used as the first screen to assign mailings. It provides all the info to the
        /// mailer about his current status.
        /// </summary>
        /// <returns></returns>
        public MailerVMForAssigningVerifList CreateMailerVMForAssigningVerifList()
        {

            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            string mailerId = getMailerIdFor(UserId);
            mailerId.IsNullOrWhiteSpaceThrowException("You are not authorized to be a mailer.");

            MailerVMForAssigningVerifList mv = factory_MailerVMForAssigningVerifList();

            mv.Pakistan_Postal_Verifications_Available = pakistan_Postal_Verifications_Available().ToString();
            mv.Foreign_Courier_Verifications_Available = total_Foreign_Courier_Available().ToString();
            mv.Foreign_Postal_Verifications_Available = foreign_Postal_Verifications_Available().ToString();
            mv.Pakistan_Courier_Verifications_Available = pakistan_Courier_Verifications_Available().ToString();

            mv.MailerId = mailerId;

            //initialize Enums
            mv.MailLocalOrForiegnEnum = MailLocalOrForiegnENUM.Unknown;
            mv.MailServiceEnum = MailServiceENUM.Unknown;

            int openMailingsForMailer =total_Open_Mailings_For_Mailer(mv.MailerId);
            mv.Total_Open_Mailings_For_Mailer = openMailingsForMailer.ToString();

            if (openMailingsForMailer > 0)
            {
                mv.AddressVerificationHdrList = inProcess_VerifHdrs_ReadyForPrinting(mv.MailerId);
            }

            return mv;
        }

        private ICollection<AddressVerificationHdr> inProcess_VerifHdrs_ReadyForPrinting(string mailerId)
        {
            List<AddressVerificationHdr> Inprocess_AddressVerificationHdrsList = GetAllHeaders_InProccess(mailerId);

            if (Inprocess_AddressVerificationHdrsList.IsNullOrEmpty())
                return null;


            //Now if even one trx in these is ready for printing... accept the header
            List<AddressVerificationHdr> selectedList = new List<AddressVerificationHdr>();
            foreach (AddressVerificationHdr hdr in Inprocess_AddressVerificationHdrsList)
            {
                if (hdr.AddressVerificationTrxs.IsNullOrEmpty())
                    continue;

                bool selected = hdr.AddressVerificationTrxs.Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.SelectedForProcessing).Any();

                if(selected)
                    selectedList.Add(hdr);
            }
            return selectedList;
        }


        MailerVMForAssigningVerifList factory_MailerVMForAssigningVerifList()
        {
            MailerVMForAssigningVerifList mvm = new MailerVMForAssigningVerifList();
            return mvm;

        }

        List<AddressVerificationHdr> findAllVerifHeadersForMailer(string mailerId)
        {
            Mailer mailer = Find(mailerId);


            if (mailer.IsNull())
                return null;

            if (mailer.AddressVerificationHdrs.IsNullOrEmpty())
                return null;

            return mailer.AddressVerificationHdrs.ToList();
        }


        string getMailerIdFor(string userId)
        {
            if (userId.IsNullOrWhiteSpace())
                return "";

            Mailer mailer = FindAll().FirstOrDefault(x => x.UserId == userId);
            if (mailer.IsNull())
                return "";
            return mailer.Id;
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

        ///// <summary>
        ///// This creates and saves a mailer. It makes sure that there is only one created.
        ///// Also, it ensures that deposit/account are taken care of
        ///// </summary>
        ///// <param name="vm"></param>
        //public void CreateAndSaveMailer(CreateMailerVM vm)
        //{
        //    //ControllerCreateEditParameter para
        //    vm.UserId.IsNullOrWhiteSpaceThrowArgumentException("User is required!");

        //    //first check if a mailer exists for the mailer
        //    bool mailerExists = FindAll().Any(x => x.UserId == vm.UserId);

        //    if (mailerExists)
        //    {
        //        ErrorsGlobal.AddMessage("Mailer already exists for user!");
        //        string error = ErrorsGlobal.ToString();
        //        throw new Exception(error);
        //    }

        //    ApplicationUser user = UserBiz.Find(vm.UserId);
        //    user.IsNullThrowException("No such user.");

        //    //Make sure user has enough deposit etc to become a mailer
        //    //todo 
        //    ErrorsGlobal.AddMessage("Financial checks need to be done!");

        //    Mailer mailer = new Mailer();

        //    mailer.Name = user.Name;
        //    mailer.UserId = user.Id;
        //    mailer.TrustLevelEnum = vm.TrustLevelEnum;

        //    user.Mailer = mailer;
        //    user.MailerId = mailer.Id;

        //    CreateAndSave(mailer);
        //    ErrorsGlobal.AddMessage(string.Format("Mailer created for {0}", user.UserName));

        //}



        long total_Foreign_Courier_Available()
        {

            //get all the Request Trx
            return getVerificationListCountFor(MailServiceENUM.Courier, MailLocalOrForiegnENUM.OutOfPakistan);
        }


        long foreign_Postal_Verifications_Available()
        {
            return getVerificationListCountFor(MailServiceENUM.Post, MailLocalOrForiegnENUM.OutOfPakistan);
        }

        long pakistan_Courier_Verifications_Available()
        {
            return getVerificationListCountFor(MailServiceENUM.Courier, MailLocalOrForiegnENUM.InPakistan);
        }

        long pakistan_Postal_Verifications_Available()
        {
            return getVerificationListCountFor(MailServiceENUM.Post, MailLocalOrForiegnENUM.InPakistan);

        }



        int total_Open_Mailings_For_Mailer(string mailerId)
        {
            //List<AddressVerificationHdr> allVerfHdrs = findAllVerifHeadersForMailer(mailerId);
            List<AddressVerificationHdr> allVerfHdrs = GetAllHeaders_InProccess(mailerId);

            if (allVerfHdrs.IsNullOrEmpty())
                return 0;

            int numberInProccess = allVerfHdrs.Count();
            return numberInProccess;
        }

        
        List<AddressVerificationHdr> GetAllHeaders_InProccess(string mailerId)
        {
            List<AddressVerificationHdr> _allVerfHdrs = findAllVerifHeadersForMailer(mailerId);

            if (_allVerfHdrs.IsNullOrEmpty())
                return null;

            _allVerfHdrs = _allVerfHdrs
                .Where(x => x.SuccessEnum == SuccessENUM.InProccess)
                .ToList();

            return _allVerfHdrs;
        }


        long getVerificationListCountFor(MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum)
        {


            var lst = verfTrxRequestListExceptSelfUser.Where(
                x => x.MailServiceEnum == mailServiceEnum &&
                    x.MailLocalOrForiegnEnum == mailLocalOrForiegnEnum).ToList();

            return lst.Count();
        }

        List<AddressVerificationTrx> _verfTrxRequestList = null;
        
        List<AddressVerificationTrx> verfTrxRequestListExceptSelfUser
        {
            get
            {
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in!");

                //first initialize the userid
                _verfTrxRequestList = AddressVerificationTrxBiz
                    .FindAll()
                    .Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Requested)
                    .ToList();


                //Todo This would work better with func
                if (!_verfTrxRequestList.IsNullOrEmpty())
                {
                    //update the UserId so we can filter out the current user
                    foreach (var item in _verfTrxRequestList)
                    {
                        item.UserIdOfOwner = item.Address.UserId;   
                    }

                    //now we can use it in linq
                    _verfTrxRequestList =
                        _verfTrxRequestList
                    .Where(x => x.UserIdOfOwner != UserId)
                    .ToList();
                }

                return _verfTrxRequestList;
            }
        }
    }
}
