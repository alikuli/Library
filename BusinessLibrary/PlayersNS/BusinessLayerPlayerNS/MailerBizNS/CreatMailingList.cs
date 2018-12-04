using AliKuli.Extentions;
using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;


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




        public void CreatMailingList(MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum)
        {
            if (mailLocalOrForiegnEnum == MailLocalOrForiegnENUM.Unknown)
                throw new Exception("Mail Local Or Foriegn is UNKNOWN.");

            if (mailServiceEnum == MailServiceENUM.Unknown)
                throw new Exception("Mail Service is UNKNOWN.");

            var verificationHdr = CreateVerificationMailingList_Helper(mailLocalOrForiegnEnum, mailServiceEnum);
            AddressVerificationHdrBiz.CreateAndSave(verificationHdr);

        }

        private AddressVerificationHdr CreateVerificationMailingList_Helper(MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum)
        {
            //mv.MailerId.IsNullOrWhiteSpaceThrowArgumentException("View lost the data of Mailer Id");
            //always a mailer will be logged in and the mailer id will be with the User id.
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("Person not found for user");

            person.Mailers.IsNullOrEmptyThrowException("You are not an authorized mailer");

            Mailer mailer = person.Mailers.FirstOrDefault(x => x.MetaData.IsDeleted == false);
            mailer.IsNullThrowException("You are not an authourized mailer");



            var verificationHdr = CreateVerificationMailingListFor(mailer, mailServiceEnum, mailLocalOrForiegnEnum, VerificaionStatusENUM.Requested);

            return verificationHdr;
        }

        private AddressVerificationHdr CreateVerificationMailingListFor(Mailer mailer, MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, VerificaionStatusENUM VerificaionStatusEnum)
        {
            List<AddressVerificationTrx> mailingList = createVerificationListFor(mailer, mailLocalOrForiegnEnum, mailServiceEnum, VerificaionStatusEnum);

            if (mailingList.IsNullOrEmpty())
                return null;

            updateTrxs(mailingList);

            AddressVerificationHdr header = AddressVerificationHdrBiz.Factory() as AddressVerificationHdr;
            string dateTickStr = DateTime.UtcNow.Ticks.ToString();

            dateTickStr = dateTickStr.Substring(dateTickStr.Length - 5); //gets last 5 digits

            header.Name = string.Format("{1} -{0} ", dateTickStr, UserName);
            header.AddBeginAndEndDateController(mailLocalOrForiegnEnum, mailServiceEnum, UserName);

            header.MailLocalOrForiegnEnum = mailLocalOrForiegnEnum;
            header.MailServiceEnum = mailServiceEnum;
            header.BatchNo = getNextBatchNo();
            header.BeginDate.SetToTodaysDate(UserId);

            header.EndDate.SetToTodaysDate(UserId);
            int timeAllowed = NoOfDaysAllowed(mailServiceEnum, mailLocalOrForiegnEnum);
            header.EndDate.Date = header.EndDate.Date.Value.AddDays(timeAllowed);
            header.Verification.SetTo(VerificaionStatusENUM.SelectedForProcessing);
            //header.SuccessEnum = SuccessENUM.Inproccess;

            //db stuff
            header.MailerId = mailer.Id;
            header.Mailer = mailer;
            mailer.AddressVerificationHdrs.Add(header);
            header.AddressVerificationTrxs = mailingList;

            return header;
        }

        //this numbers the letter 1 to ?
        private void updateTrxs(List<AddressVerificationTrx> mailingList)
        {
            int letterNo = 0;
            foreach (AddressVerificationTrx letter in mailingList)
            {
                letterNo++;
                letter.LetterNo = letterNo;
                letter.Address.Verification.SetTo(VerificaionStatusENUM.SelectedForProcessing);
                letter.Verification.SetTo(VerificaionStatusENUM.SelectedForProcessing);
            }
        }

        private long getNextBatchNo()
        {
            //long max = AddressVerificationHdrBiz.FindAll().Max(x => x.BatchNo) ?? AddressVerificationHdrBiz.FindAll().Max(x => x.BatchNo);
            //long nextNo = max + 1;
            //return nextNo;

            var lst = AddressVerificationHdrBiz.FindAll().Select(x => x.BatchNo).ToList();

            if (lst.IsNullOrEmpty())
                return 1;

            long max = lst.Max();

            return max;
        }

        private int NoOfDaysAllowed(MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnEnum)
        {
            string err = "";
            switch (mailLocalOrForiegnEnum)
            {
                case MailLocalOrForiegnENUM.InPakistan:
                    switch (mailServiceEnum)
                    {

                        case MailServiceENUM.Post:
                            return VerificationConfig.Number_Of_Days_Allowed_For_Local_Post;

                        case MailServiceENUM.Courier:
                            return VerificationConfig.Number_Of_Days_Allowed_For_Local_Courier;

                        case MailServiceENUM.Unknown:
                        default:
                            err = string.Format("No such choice for No. of days allowed: Service: {0}, Local/Foreign: {1}",
                                mailServiceEnum.ToString().ToTitleSentance(),
                                mailLocalOrForiegnEnum.ToString().ToTitleSentance());
                            throw new Exception(err);

                    };


                case MailLocalOrForiegnENUM.OutOfPakistan:
                    switch (mailServiceEnum)
                    {
                        case MailServiceENUM.Post:
                            return VerificationConfig.Number_Of_Days_Allowed_For_Foreign_Post;

                        case MailServiceENUM.Courier:
                            return VerificationConfig.Number_Of_Days_Allowed_For_Foreign_Courier;

                        case MailServiceENUM.Unknown:
                        default:
                            err = string.Format("No such choice for No. of days allowed: Service: {0}, Local/Foreign: {1}",
                                mailServiceEnum.ToString().ToTitleSentance(),
                                mailLocalOrForiegnEnum.ToString().ToTitleSentance());
                            throw new Exception(err);
                    };


                case MailLocalOrForiegnENUM.Unknown:
                default:
                    err = string.Format("No such choice for No. of days allowed: Service: {0}, Local/Foreign: {1}",
                        mailServiceEnum.ToString().ToTitleSentance(),
                        mailLocalOrForiegnEnum.ToString().ToTitleSentance());
                    throw new Exception(err);
            }
        }


        private List<AddressVerificationTrx> createVerificationListFor(Mailer mailer, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum, VerificaionStatusENUM VerificaionStatusEnum)
        {
            checkIfMailerIsAllowedAnyMoreMailingsIfNotThrowException_Helper(mailer);


            //Check to see if there are any mails for verification
            if (areThereAnyAddressesForVerificationFor_Helper(mailServiceEnum, mailLocalOrForiegnEnum))
            {
                //get the number allowed to the user
                int qtyAllowedToUser = getNumbrOfLettersAllowedToUserIfZeroThrowException_Helper(mailer, mailServiceEnum);

                //this gets the list
                List<AddressVerificationTrx> list = getVerificationTrxFor_SQL(mailServiceEnum, mailLocalOrForiegnEnum)
                    .Where(x => x.Verification.VerificaionStatusEnum == VerificaionStatusEnum)
                    .ToList();
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
            //get the current user's Id.
            string personId  = GetPersonIdForCurrentUser();

            int qty = 0;
            List<AddressVerificationTrx> addyVerifLst = new List<AddressVerificationTrx>();
            foreach (AddressVerificationTrx item in list)
            {
                //dont accept the addressverification requests
                //of the mailer... someone else will do those.
                if (item.Address.People.Any(x => x.Id == personId))
                    continue;

                qty++;
                if (qty > qtyAllowedToUser)
                    break;

                item.Verification.SetTo(VerificaionStatusENUM.SelectedForProcessing);
                addyVerifLst.Add(item);

            }
            return addyVerifLst;
        }

        //private string getPersonIdForCurrentUser()
        //{
        //    UserId.IsNullOrWhiteSpaceThrowException("User not logged in.");
        //    Person person = UserBiz.GetPersonFor(UserId);
        //    person.IsNullThrowException("No person attached to this user");
        //    string personId = person.Id;
        //    return personId;
        //}
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
