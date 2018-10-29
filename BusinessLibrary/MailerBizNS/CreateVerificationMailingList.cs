using AliKuli.Extentions;
using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UserModels;


namespace UowLibrary.MailerNS
{
    /// <summary>
    /// This creates a mailing list for the mailer. 
    /// Mailer has choices of Postal/Courier Local/Foreign and combinations thereof.
    /// The Number of mails allowed for Postal and Courier are different.
    /// The Courier mails will be more expensive so there will be more profit per item.
    /// 
    /// </summary>
    public partial class MailerBiz
    {




        public void CreatMailingList(MailerVMForAssigningVerifList mv)
        {
            mv.IsNullThrowExceptionArgument("Model is null");
            var verificationHdr = CreateVerificationMailingList(mv);
            _addressVerifHdrBiz.CreateAndSave(verificationHdr);

        }

        private AddressVerificationHdr CreateVerificationMailingList(MailerVMForAssigningVerifList mv)
        {
            //mv.MailerId.IsNullOrWhiteSpaceThrowArgumentException("View lost the data of Mailer Id");
            //always a mailer will be logged in and the mailer id will be with the User id.
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            ApplicationUser appuser = UserBiz.Find(UserId);
            appuser.IsNullThrowException("No user found!");
            appuser.Mailer.IsNullThrowException("You are not an authorized mailer. ");

            Mailer mailer = appuser.Mailer;

            var verificationHdr = CreateVerificationMailingList(mailer, mv.MailServiceEnum, mv.MailLocalOrForiegnEnum);

            return verificationHdr;
        }

        public AddressVerificationHdr CreateVerificationMailingList(Mailer mailer, MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum)
        {
            List<AddressVerificationTrx> mailingList = createVerificationListFor(mailer, mailLocalOrForiegnEnum, mailServiceEnum);

            if (mailingList.IsNullOrEmpty())
                return null;

            AddressVerificationHdr header = AddressVerificationHdrBiz.Factory() as AddressVerificationHdr;
            string dateTickStr = DateTime.UtcNow.Ticks.ToString();

            dateTickStr = dateTickStr.Substring(dateTickStr.Length - 5); //gets last 5 digits
            
            header.Name = string.Format("{1} -{0} ", dateTickStr, UserName);
            header.AddBeginAndEndDateController(mailLocalOrForiegnEnum, mailServiceEnum, UserName);
            //header.SuccessEnum = SuccessENUM.Inproccess;

            //db stuff
            header.MailerId = mailer.Id;
            header.Mailer = mailer;
            mailer.AddressVerificationHdrs.Add(header);
            header.AddressVerificationTrxs = mailingList;

            return header;
        }


        private List<AddressVerificationTrx> createVerificationListFor(Mailer mailer, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum)
        {
            checkIfMailerIsAllowedAnyMoreMailingsIfNotThrowException_Helper(mailer);


            //Check to see if there are any mails for verification
            if (areThereAnyAddressesForVerificationFor_Helper(mailServiceEnum, mailLocalOrForiegnEnum))
            {
                //get the number allowed to the user
                int qtyAllowedToUser = getNumbrOfLettersAllowedToUserIfZeroThrowException_Helper(mailer, mailServiceEnum);

                //this gets the list
                List<AddressVerificationTrx> list = getVerificationTrxFor_SQL(mailServiceEnum, mailLocalOrForiegnEnum).ToList();
                list.IsNullOrEmptyThrowException("Sorry. There are no open verification requests for Pakistan.");


                List<AddressVerificationTrx> lstOfAddys = createListOfVerificationAddresses(qtyAllowedToUser, list);
                return lstOfAddys;

            }

            throw new Exception(string.Format("Sorry. There are no open verification requests."));

        }

        /// <summary>
        /// This also filters out the owners own verification requests
        /// </summary>
        /// <param name="qtyAllowedToUser"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<AddressVerificationTrx> createListOfVerificationAddresses(int qtyAllowedToUser, List<AddressVerificationTrx> list)
        {
            int qty = 0;
            List<AddressVerificationTrx> addyVerifLst = new List<AddressVerificationTrx>();
            foreach (AddressVerificationTrx item in list)
            {
                //dont accept the addressverification requests
                //of the mailer... someone else will do those.
                if (item.UserIdOfOwner == UserId)
                    continue;

                qty++;
                if (qty > qtyAllowedToUser)
                    break;

                item.VerificaionStatusEnum = EnumLibrary.EnumNS.VerificaionStatusENUM.SelectedForProcessing;
                addyVerifLst.Add(item);

            }
            return addyVerifLst;
        }
        //private AddressVerificationHdr createHeaderAndChildren(List<AddressVerificationTrx> mailingList, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum)
        //{
        //    //AddressVerificationHdr header = AddressVerificationHdrBiz.Factory() as AddressVerificationHdr;

        //    //header.Name = string.Format("Mailing List For: {1} -{0} ", DateTime.UtcNow.Ticks.ToString(), UserName);
        //    //header.AddBeginDateController(mailLocalOrForiegnEnum, mailServiceEnum, UserName);

        //    //if (mailingList.IsNullOrEmpty())
        //    //    return null;

        //    //header.AddressVerificationTrx = mailingList;
        //    return header;
        //}


        /// <summary>
        /// This is the trust level of mailer
        /// </summary>
        private TrustLevelENUM getMailerTrustLevel_Helper(Mailer mailer)
        {
            mailer.IsNullThrowException(string.Format("Mailer is Null in AddressVerificationHdr. Id = {0}", mailer.Id));
            return mailer.TrustLevelEnum;
        }


        /// <summary>
        /// These are the number of pieces allowed to the mailer per mailing.
        /// </summary>
        private int numberOfMailPcsAllowedPerMailingToMailer(Mailer mailer, MailServiceENUM mailServiceEnum)
        {
            if (mailServiceEnum == MailServiceENUM.Post)
            {
                switch (getMailerTrustLevel_Helper(mailer))
                {
                    case TrustLevelENUM.Level1:
                        return MailersTrustLevelConfig.Level1_Postal;

                    case TrustLevelENUM.Level2:
                        return MailersTrustLevelConfig.Level2_Postal;

                    case TrustLevelENUM.Level3:
                        return MailersTrustLevelConfig.Level3_Postal;

                    case TrustLevelENUM.Level4:
                        return MailersTrustLevelConfig.Level4_Postal;

                    case TrustLevelENUM.Level5:
                        return MailersTrustLevelConfig.Level5_Postal;

                    case TrustLevelENUM.Unknown:
                    case TrustLevelENUM.BlackListed:
                    default:
                        return 0;
                }
            }
            if (mailServiceEnum == MailServiceENUM.Courier)
            {
                switch (getMailerTrustLevel_Helper(mailer))
                {
                    case TrustLevelENUM.Level1:
                        return MailersTrustLevelConfig.Level1_Courier;

                    case TrustLevelENUM.Level2:
                        return MailersTrustLevelConfig.Level2_Courier;

                    case TrustLevelENUM.Level3:
                        return MailersTrustLevelConfig.Level3_Courier;

                    case TrustLevelENUM.Level4:
                        return MailersTrustLevelConfig.Level4_Courier;

                    case TrustLevelENUM.Level5:
                        return MailersTrustLevelConfig.Level5_Courier;

                    case TrustLevelENUM.Unknown:
                    case TrustLevelENUM.BlackListed:
                    default:
                        return 0;
                }

            }

            throw new Exception(
                string.Format("No such mail serivice! {0}",
                    mailServiceEnum
                        .ToString()
                        .ToTitleSentance()));
        }




        /// <summary>
        /// This takes the mailers request to create a list according to what he wants.
        /// </summary>
        /// <param name="mailServiceEnum"></param>
        /// <param name="mailLocalOrForiegnEnum"></param>
        /// <returns></returns>

        private IQueryable<AddressVerificationTrx> getVerificationTrxFor_SQL(MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum)
        {

            var dataForMailServiceType = AddressVerificationTrxBiz.FindAll()
                .Where(x => x.MailServiceEnum == mailServiceEnum && x.MailLocalOrForiegnEnum == mailLocalOrForiegnEnum);

            //var test = dataForMailServiceType.ToList();
            return dataForMailServiceType;

        }




        private bool areThereAnyAddressesForVerificationFor_Helper(MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum)
        {
            bool exists = getVerificationTrxFor_SQL(mailServiceEnum, mailLocalOrForiegnEnum).Any();
            return exists;
        }


        private int getNumbrOfLettersAllowedToUserIfZeroThrowException_Helper(Mailer mailer, MailServiceENUM mailServiceEnum)
        {
            int qtyAllowedToUser = numberOfMailPcsAllowedPerMailingToMailer(mailer, mailServiceEnum);

            if (qtyAllowedToUser == 0)
                throw new Exception(string.Format("You are not allowed any mails for this Trust Level = '{0}'", getMailerTrustLevel_Helper(mailer).ToString().ToTitleSentance()));
            return qtyAllowedToUser;
        }
        private void checkIfMailerIsAllowedAnyMoreMailingsIfNotThrowException_Helper(Mailer mailer)
        {

            MailCounterModel mcm = TotalOpenMailingsForMailer(mailer);

            if (mcm.Unknown > 0)
            {
                //ToDo send a message to the administrator that there are problems

            }

            if (!IsMailerAllowedMoreMailings(mailer))
                throw new Exception(string.Format("Your Quota for Mailing is Full. You have {0} inproccess + {1} unknown Problems (Admin has been informed) = {2} Outstanding",
                    mcm.Inproccess,
                    mcm.Unknown,
                    mcm.TotalOutstanding));
        }

        //private IQueryable<AddressVerificationTrx> GetRequestVerificationList_Pakistan_Post
        //{
        //    get
        //    {

        //        IQueryable<AddressVerificationTrx> iq = AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Pakistan()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Post);
        //        return iq;
        //    }
        //}

        //private IQueryable<AddressVerificationTrx> GetRequestVerificationList_Pakistan_Courier
        //{
        //    get
        //    {

        //        IQueryable<AddressVerificationTrx> iq = AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Pakistan()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Courier);
        //        return iq;
        //    }
        //}


        //private IQueryable<AddressVerificationTrx> GetRequestVerificationList_Foreign_Post
        //{
        //    get
        //    {

        //        IQueryable<AddressVerificationTrx> iq = AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Pakistan()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Post);
        //        return iq;
        //    }
        //}


        //private IQueryable<AddressVerificationTrx> GetRequestVerificationList_Foreign_Courier
        //{
        //    get
        //    {
        //        IQueryable<AddressVerificationTrx> iq = AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Pakistan()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Courier);
        //        return iq;

        //    }
        //}
        //public bool AreThereAnyAddressesForVerificationFor_Pakistan_Post
        //{
        //    get
        //    {
        //        bool exits =
        //             AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Pakistan()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Post)
        //            .Any();

        //        return exits;
        //    }
        //}

        //public bool AreThereAnyAddressesForVerificationFor_Pakistan_Courier
        //{
        //    get
        //    {
        //        bool exits =
        //             AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Pakistan()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Courier)
        //            .Any();

        //        return exits;
        //    }
        //}
        //public bool AreThereAnyAddressesForVerificationFor_Other_Post
        //{
        //    get
        //    {
        //        bool exits =
        //             AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Other()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Post)
        //            .Any();

        //        return exits;
        //    }
        //}

        //public bool AreThereAnyAddressesForVerificationFor_Foreign_Courier
        //{
        //    get
        //    {
        //        bool exits =
        //             AddressVerificationHdrBiz
        //            .AddressVerificationTrxBiz
        //            .GetVerificationRequestTrx_Other()
        //            .Where(x => x.MailTypeEnum == MailServiceENUM.Courier)
        //            .Any();

        //        return exits;
        //    }
        //}
        //private AddressVerificationHdr CreateMailingList_Other_Courier_For(Mailer mailer)
        //{
        //    throw new NotImplementedException();
        //}

        //private AddressVerificationHdr CreateMailingList_Other_Post_For(Mailer mailer)
        //{
        //    throw new NotImplementedException();
        //}

        //private AddressVerificationHdr CreateMailingList_Pakistan_Courier_For(Mailer mailer)
        //{
        //    throw new NotImplementedException();
        //}


        //private List<AddressVerificationTrx> getListForVerification_Pakistan_Post(Mailer mailer)
        //{
        //    checkIfMailerIsAllowedAnyMoreMailingsIfNotThrowException(mailer);


        //    //Check to see if there are any mails for verification
        //    if (AreThereAnyAddressesForVerificationFor_Pakistan_Post)
        //    {
        //        //get the number allowed to the user
        //        int qtyAllowedToUser = getNumbrOfLettersAllowedToUserIfZeroThrowException(mailer);


        //        var list = GetRequestVerificationList_Pakistan_Post;
        //        list.IsNullOrEmptyThrowException("Sorry. There are no open verification requests for Pakistan. (programming error)");

        //        int qty = 0;
        //        List<AddressVerificationTrx> lstForUser = new List<AddressVerificationTrx>();
        //        foreach (var item in list)
        //        {
        //            qty++;
        //            if (qty > qtyAllowedToUser)
        //                break;

        //            item.VerificaionStatusEnum = EnumLibrary.EnumNS.VerificaionStatusENUM.SelectedForProcessing;
        //            lstForUser.Add(item);

        //        }
        //        return lstForUser;

        //    }
        //    throw new Exception(string.Format("Sorry. There are no open verification requests."));

        //}
        //public AddressVerificationHdr CreateMailingList_Pakistan_Post_For(Mailer mailer)
        //{
        //    List<AddressVerificationTrx> mailingList = getListForVerification_Pakistan_Post(mailer);
        //    mailingList.IsNullOrEmptyThrowException(string.Format("Sorry. There are no open verification requests. (Programming error 2)"));

        //    return createMailingList(mailingList);
        //}


        //public AddressVerificationHdr CreateMailingListFor(Mailer mailer, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum)
        //{
        //    List<AddressVerificationTrx> mailingList = getVerificationListFor(mailer,mailLocalOrForiegnEnum,mailServiceEnum);
        //    mailingList.IsNullOrEmptyThrowException(string.Format("Sorry. There are no open verification requests. (Programming error 2)"));

        //    return createHeaderAndChildren(mailingList);
        //}
    }
}
