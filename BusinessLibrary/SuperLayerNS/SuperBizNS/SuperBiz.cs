using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.CashNS.PenaltyNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsNS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using UowLibrary.AddressNS;
using UowLibrary.BusinessLayer.ProductNS.ShopNS;
using UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestHdrNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashEncashmentTrxNS;
using UowLibrary.CashTtxNS;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.MailerNS;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.MessageNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;
using UowLibrary.PlayersNS.SalesmanNS;
using UowLibrary.PlayersNS.ServiceRequestHdrNS;
//using UowLibrary.PlayersNS.ServiceRequestTrxNS;
using UowLibrary.PlayersNS.VehicalTypeNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;
using UserModels;

namespace UowLibrary.SuperLayerNS
{


    public partial class SuperBiz
    {

        BankBiz _bankBiz;
        BuySellDocBiz _buySellDocBiz;
        AbstractControllerParameters _abstractControllerParameters;
        PaymentMethodBiz _paymentMethodBiz;
        CashEncashmentTrxBiz _cashEncashmentTrxBiz;
        PenaltyHeaderBiz _penaltyHeaderBiz;
        IServiceRequestHdrBiz _iserviceRequestHdrBiz;
        MailerBiz _mailerBiz;
        public SuperBiz(BankBiz bankBiz, AbstractControllerParameters abstractControllerParameters, BuySellDocBiz buySellDocBiz, PaymentMethodBiz paymentMethodBiz, CashEncashmentTrxBiz cashEncashmentTrxBiz, PenaltyHeaderBiz penaltyHeaderBiz, IServiceRequestHdrBiz iserviceRequestHdrBiz, MailerBiz mailerBiz)
        {
            _bankBiz = bankBiz;
            _buySellDocBiz = buySellDocBiz;
            _abstractControllerParameters = abstractControllerParameters;
            _paymentMethodBiz = paymentMethodBiz;
            _cashEncashmentTrxBiz = cashEncashmentTrxBiz;
            _penaltyHeaderBiz = penaltyHeaderBiz;
            _iserviceRequestHdrBiz = iserviceRequestHdrBiz;
            _mailerBiz = mailerBiz;
        }

        #region Bizs

        public ShopBiz ShopBiz
        {
            get
            {
                return BuySellDocBiz.ShopBiz;
            }
        }
        public MailerBiz MailerBiz
        {
            get
            {
                _mailerBiz.UserId = UserId;
                _mailerBiz.UserName = UserName;
                return _mailerBiz;
            }
        }
        public ServiceRequestHdrBiz ServiceRequestHdrBiz
        {
            get
            {
                _iserviceRequestHdrBiz.UserId = UserId;
                _iserviceRequestHdrBiz.UserName = UserName;

                return ServiceRequestHdrBiz.Unbox(_iserviceRequestHdrBiz);
            }
        }

        //public ServiceRequestTrxBiz ServiceRequestTrxBiz
        //{
        //    get
        //    {
        //        return ServiceRequestHdrBiz.ServiceRequestTrxBiz;
        //    }
        //}
        public PenaltyHeaderBiz PenaltyHeaderBiz
        {
            get
            {
                _penaltyHeaderBiz.IsNullThrowException();
                _penaltyHeaderBiz.UserId = UserId;
                _penaltyHeaderBiz.UserName = UserName;

                return _penaltyHeaderBiz;

            }
        }
        public PenaltyTrxBiz PenaltyTrxBiz
        {
            get
            {

                return PenaltyHeaderBiz.PenaltyTrxBiz;

            }
        }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public CashEncashmentTrxBiz CashEncashmentTrxBiz
        {
            get
            {
                _cashEncashmentTrxBiz.IsNullThrowException();
                _cashEncashmentTrxBiz.UserId = UserId;
                _cashEncashmentTrxBiz.UserName = UserName;
                return _cashEncashmentTrxBiz;
            }
        }
        public PaymentMethodBiz PaymentMethodBiz
        {
            get
            {

                _paymentMethodBiz.IsNullThrowException();
                _paymentMethodBiz.UserId = UserId;
                _paymentMethodBiz.UserName = UserName;
                return _paymentMethodBiz;
            }
        }
        public bool IsAdmin
        {
            get
            {

                return UserBiz.IsAdmin(UserId);
            }
        }
        public FreightOfferTrxBiz FreightOfferTrxBiz
        {
            get
            {
                return BuySellDocBiz.FreightOfferTrxBiz;
            }
        }

        public SalesmanBiz SalesmanBiz
        {
            get
            {
                return BuySellDocBiz.SalesmanBiz;
            }
        }
        public SalesmanCategoryBiz SalesmanCategoryBiz
        {
            get
            {
                return BuySellDocBiz.SalesmanBiz.SalesmanCategoryBiz;
            }
        }
        public AbstractControllerParameters AbstractControllerParameters
        {
            get
            {
                _abstractControllerParameters.IsNullThrowException();
                _abstractControllerParameters.UserId = UserId;
                _abstractControllerParameters.UserName = UserName;
                return _abstractControllerParameters;
            }
        }
        public BankBiz BankBiz
        {
            get
            {
                _bankBiz.IsNullThrowException();
                _bankBiz.UserId = UserId;
                _bankBiz.UserName = UserName;

                return _bankBiz;
            }
        }

        public PersonBiz PersonBiz
        {
            get
            {
                return BankBiz.PersonBiz;
            }
        }

        public UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }
        }

        public CashTrxBiz CashTrxBiz
        {
            get
            {

                return BankBiz.CashTrxBiz;
            }
        }
        public BuySellDocBiz BuySellDocBiz
        {
            get
            {
                _buySellDocBiz.IsNullThrowException();
                _buySellDocBiz.UserId = UserId;
                _buySellDocBiz.UserName = UserName;

                return _buySellDocBiz;
            }
        }

        public CustomerBiz CustomerBiz
        {
            get
            {
                return BuySellDocBiz.CustomerBiz;
            }
        }

        public OwnerBiz OwnerBiz
        {
            get
            {
                return BuySellDocBiz.OwnerBiz;
            }
        }

        public DeliverymanBiz DeliverymanBiz
        {
            get
            {
                return BuySellDocBiz.DeliverymanBiz;
            }
        }

        public ProductBiz ProductBiz
        {
            get
            {
                return BuySellDocBiz.ProductBiz;
            }
        }


        public MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                return BuySellDocBiz.ProductBiz.MenuPathMainBiz;
            }
        }


        public ProductChildBiz ProductChildBiz
        {
            get
            {
                return BuySellDocBiz.ProductChildBiz;
            }
        }

        public VehicalTypeBiz VehicalTypeBiz
        {
            get
            {
                return BuySellDocBiz.VehicalTypeBiz;
            }
        }
        public AddressBiz AddressBiz
        {
            get
            {
                return CustomerBiz.AddressBiz;
            }
        }

        public ErrorSet ErrorsGlobal
        {
            get
            {
                return BuySellDocBiz.ErrorsGlobal;
            }
        }
        public PeopleMessageBiz PeopleMessageBiz
        {
            get
            {
                return BuySellDocBiz.PeopleMessageBiz;
            }
        }
        public MessageBiz MessageBiz
        {
            get
            {
                return BuySellDocBiz.MessageBiz;
            }
        }
        #endregion

        public BuySellStatementModel GetOrdersList(BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum, bool isWantAdminPrivilages)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            DateTime fromDate = DateTime.Now.AddMonths(-3);
            DateTime toDate = DateTime.Now;
            bool isAdmin = false;
            string userId = UserId;

            if (isWantAdminPrivilages)
            {
                isAdmin = UserBiz.IsAdmin(UserId);

                if (isAdmin)
                    userId = "";

            }

            BuySellStatementModel buySellStatementModel = BuySellDocBiz.GetBuySellStatementModel(userId, fromDate, toDate, isAdmin, buySellDocumentTypeEnum, buySellDocStateEnum);
            return buySellStatementModel;
        }



        public CashPaymentModel Setup_CashPaymentModel(CashPaymentModel cashPaymentModel, CashTypeENUM CashTypeEnum)
        {
            if (cashPaymentModel.IsNull())
                cashPaymentModel = new CashPaymentModel();

            if (UserBiz.IsAdmin(UserId))
            {
                cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();

            }
            else
            {
                cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
            }

            //cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
            cashPaymentModel.CashTypeEnum = CashTypeEnum;
            return cashPaymentModel;
        }




        /// <summary>
        /// Use this to add a refundable payment
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// 



        public MoneyInfo GetMoneyInfoForUser()
        {

            decimal amountRefundable = 0;
            decimal amountNonRefundable = 0;

            amountRefundable = BalanceFor_Person(CurrentUserParameter.PersonId, CashTypeENUM.Refundable);
            amountNonRefundable = BalanceFor_Person(CurrentUserParameter.PersonId, CashTypeENUM.NonRefundable);

            MoneyInfo moneyType = new MoneyInfo();

            moneyType.Refundable.MoneyAmount = amountRefundable;
            moneyType.Non_Refundable.MoneyAmount = amountNonRefundable;
            moneyType.TotalCash.MoneyAmount = amountNonRefundable + amountRefundable;

            string menuType_Refundable = ConfigurationManager.AppSettings["menu.person_CashRefundable_MenuItem"];
            string tooltip_Refundable = ConfigurationManager.AppSettings["menu.person_CashRefundable_ToolTip"];
            moneyType.Refundable.MenuName = string.Format(menuType_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());
            moneyType.Refundable.MenuToolTip = string.Format(tooltip_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());

            string menuType_NON_Refundable = ConfigurationManager.AppSettings["menu.person_CashNonRefundable_MenuItem"];
            string tooltip_NON_Refundable = ConfigurationManager.AppSettings["menu.person_CashNonRefundable_ToolTip"];
            moneyType.Non_Refundable.MenuName = string.Format(menuType_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());
            moneyType.Non_Refundable.MenuToolTip = string.Format(tooltip_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

            string menuType_AllCash = ConfigurationManager.AppSettings["menu.person_TotalCash_MenuItem"];
            string tooltip_AllCash = ConfigurationManager.AppSettings["menu.person_TotalCash_ToolTip"];
            moneyType.TotalCash.MenuName = string.Format(menuType_AllCash, moneyType.TotalCash.MoneyAmount.ToString().ToRuppeeFormat());
            moneyType.TotalCash.MenuToolTip = string.Format(
                tooltip_AllCash,
                moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat(),
                moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

            return moneyType;

        }


        public MoneyInfo GetMoneyForSystem()
        {

            if (!CurrentUserParameter.IsAdmin)
                return new MoneyInfo();

            decimal amountRefundable = 0;
            decimal amountNonRefundable = 0;

            amountRefundable = TotalCashAvailableInSystem_System(CashTypeENUM.Refundable);
            amountNonRefundable = TotalCashAvailableInSystem_System(CashTypeENUM.NonRefundable);


            MoneyInfo moneyType = new MoneyInfo();

            moneyType.Refundable.MoneyAmount = amountRefundable;
            moneyType.Non_Refundable.MoneyAmount = amountNonRefundable;
            moneyType.TotalCash.MoneyAmount = amountNonRefundable + amountRefundable;

            string menuType_Refundable = Get_menu_system_Cash_Refundable_MenuItem();
            string tooltip_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_Refundable_ToolTip"];

            string menuType_NON_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_NonRefundable_MenuItem"];
            string tooltip_NON_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_NonRefundable_ToolTip"];

            string menuType_AllCash = ConfigurationManager.AppSettings["menu.system_TotalCash_MenuItem"];
            string tooltip_AllCash = ConfigurationManager.AppSettings["menu.system_TotalCash_ToolTip"];

            moneyType.Refundable.MenuName = string.Format(menuType_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());
            moneyType.Refundable.MenuToolTip = string.Format(tooltip_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());

            moneyType.Non_Refundable.MenuName = string.Format(menuType_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());
            moneyType.Non_Refundable.MenuToolTip = string.Format(tooltip_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

            moneyType.TotalCash.MenuName = string.Format(menuType_AllCash, moneyType.TotalCash.MoneyAmount.ToString().ToRuppeeFormat());
            moneyType.TotalCash.MenuToolTip = string.Format(
                tooltip_AllCash,
                moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat(),
                moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

            return moneyType;
        }

        private static string Get_menu_system_Cash_Refundable_MenuItem()
        {
            string menuType_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_Refundable_MenuItem"];
            return menuType_Refundable;
        }


        /// <summary>
        /// You can get 4 kind of reports from this
        /// User Cash Transaction
        ///     Refundable -Available
        ///     Refundable -Allocatted
        ///     NON-Refundable -Available
        ///     NON-Refundable -Allocatted
        /// </summary>
        /// <param name="personIdToUse"></param>
        /// <param name="cashTypeEnum"></param>
        /// <param name="cashStateEnum"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        private CashTrxDbCrModel getCashTrxDbCrModel(string personIdForWhomWeAreWorking, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, DateTime fromDate, DateTime toDate, bool isShowAdminReports)
        {




            List<CashTrxVM2> totalCashTrxForPerson = allTrxFor(personIdForWhomWeAreWorking, cashTypeEnum, cashStateEnum, isShowAdminReports);
            CashTrxDbCrModel cashTrxDbCrModel = new CashTrxDbCrModel(totalCashTrxForPerson, fromDate, toDate, cashTypeEnum, cashStateEnum, CurrentUserParameter, isShowAdminReports);

            return cashTrxDbCrModel;
        }

        private UserParameter initUserParameter()
        {
            return initUserParameter(UserId);
        }




        private UserParameter initUserParameter(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException();

            Customer customer = CustomerBiz.GetPlayerFor(userId);
            string err = string.Format("UserId = '{0}, customer is null", userId);
            customer.IsNullThrowException(err);

            //get person
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("Person");
            string personId = person.Id;


            string bankId = "";
            Bank bank = BankBiz.GetPlayerFor(userId);
            if (!bank.IsNull())
            {
                bankId = bank.Id;
            }


            //get owner
            Owner owner = OwnerBiz.GetPlayerFor(userId);
            //owner.IsNullThrowException("owner");
            string ownerId = "";
            if (!owner.IsNull())
            {
                ownerId = owner.Id;
            }
            //get deliveryman
            Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(userId);
            //deliveryman.IsNullThrowException("deliveryman");
            string deliverymanId = deliveryman.IsNull() ? "" : deliveryman.Id;

            //get salesman
            Salesman salesman = SalesmanBiz.GetPlayerFor(userId);
            string salesmanId = salesman.IsNull() ? "" : salesman.Id;

            bool isSuperSalesman = false;
            if (!salesmanId.IsNullOrWhiteSpace())
            {
                isSuperSalesman = salesman.IsSuperSalesman;
            }

            bool isSuperSuperSalesman = false;
            if (isSuperSalesman)
            {
                isSuperSuperSalesman = checkSuperSuperSalesmanStatus(salesman);
            }

            //get mailer
            Mailer mailer = MailerBiz.GetPlayerFor(userId);
            string mailerId = mailer.IsNull() ? "" : mailer.Id;
            //salesman.IsNullThrowException("salesman");


            bool isAdmin = UserBiz.IsAdmin(userId);

            //get name of Admin
            string adminName = new ConfigManagerHelper().AdminName;
            Person personAcceptingPaymentForSystem = PersonBiz.FindByName(adminName);
            personAcceptingPaymentForSystem.IsNullThrowException();

            UserParameter userParameter = new UserParameter(
                userId,
                UserName,
                customer.Id,
                ownerId,
                deliverymanId,
                salesmanId,
                person.Id,
                person.FullName(),
                isAdmin,
                personAcceptingPaymentForSystem.Id,
                mailerId,
                bankId,
                isSuperSalesman,
                isSuperSuperSalesman);

            return userParameter;

        }




        public bool checkSuperSuperSalesmanStatus(Salesman sm)
        {
            List<Salesman> childrenSalesmen = SalesmanBiz.FindAll().Where(x => x.ParentSalesmanId == sm.Id).ToList();
            if (childrenSalesmen.IsNullOrEmpty())
                return false;

            foreach (Salesman childSalesman in childrenSalesmen)
            {
                bool isGrandChild = SalesmanBiz.FindAll().Any(x => x.ParentSalesmanId == childSalesman.Id);
                if (isGrandChild)
                    return true;
            }

            return false;
        }




        //fROM BANKbIZ used in YerOrNo.CashTrxController
        public bool Make_Payment_Refundable(CashPaymentModel cashPaymentModel)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("UserId");

            bool isBank = BankBiz.IsBanker_User(UserId);
            string personFromId = PersonBiz.GetPersonIdForUserId(UserId);

            return CashTrxBiz.Add_RefundablePayment(personFromId, cashPaymentModel.PersonToId, cashPaymentModel.Amount, cashPaymentModel.Comment, isBank, BalanceFor_Person(personFromId, CashTypeENUM.Refundable));
        }
        //fROM BANKbIZ
        public bool Make_Payment_NonRefundable(CashPaymentModel cashPaymentModel)
        {
            string personFromId = PersonBiz.GetPersonIdForUserId(UserId);
            MakePaymentModel makePaymentModel = new MakePaymentModel(personFromId, cashPaymentModel.PersonToId, cashPaymentModel.Amount, cashPaymentModel.Comment, CashTypeENUM.NonRefundable);

            return Make_Payment(makePaymentModel);
        }



        public bool Make_Payment(CashPaymentModel cashPaymentModel)
        {

            MakePaymentModel makePaymentModel = new MakePaymentModel(cashPaymentModel.PersonFromId, cashPaymentModel.PersonToId, cashPaymentModel.Amount, cashPaymentModel.Comment, cashPaymentModel.CashTypeEnum);

            return Make_Payment(makePaymentModel);
        }

        public bool Make_Payment(MakePaymentModel makePaymentModel)
        {
            bool isBank = BankBiz.IsBanker_Person(makePaymentModel.PersonFromId);
            decimal availableFunds = BalanceFor_Person(makePaymentModel.PersonFromId, makePaymentModel.CashTypeEnum);

            return CashTrxBiz.AddPayment(makePaymentModel.PersonFromId, makePaymentModel.PersonToId, makePaymentModel.Amount, makePaymentModel.CashTypeEnum, makePaymentModel.Comment, isBank, availableFunds);
        }


        #region HasAvailableBalancePerson
        public bool HasAvailableBalancePerson(CashPaymentModel cashPaymentModel, string paymentFromUserId, bool isBank, out decimal currBalance)
        {

            Person person = PersonBiz.GetPersonForUserId(paymentFromUserId);
            person.IsNullThrowException("Person not found!");
            return HasAvailableBalancePerson(cashPaymentModel, person, isBank, out currBalance);
        }
        public bool HasAvailableBalancePerson(CashPaymentModel cashPaymentModel, Person person, bool isBank, out decimal currBalance)
        {
            return HasAvailableBalancePerson(person.Id, cashPaymentModel.CashTypeEnum, cashPaymentModel.Amount, isBank, out currBalance);
        }

        public bool HasAvailableBalancePerson(string personId, CashTypeENUM cashTypeEnum, decimal amountRequired, bool isBank, out decimal currBalance)
        {

            currBalance = BalanceFor_Person(personId, cashTypeEnum);

            if (isBank)
                return true;

            return currBalance >= amountRequired;
        }


        public bool HasAvailableBalance_User(string userId, CashTypeENUM cashTypeEnum, decimal amountRequired, bool isBank, out decimal balanceForPerson)
        {
            userId.IsNullOrWhiteSpaceThrowException();
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown.");



            Person person = PersonBiz.GetPersonForUserId(userId);
            person.IsNullThrowException();
            string personId = person.Id;
            balanceForPerson = BalanceFor_Person(personId, cashTypeEnum);

            if (isBank)
                return true;

            return balanceForPerson >= amountRequired;
        }

        #endregion





        public CashTrxDbCrModel GetCashStatement(CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, string toDateStr, bool isShowAdminReports)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in");
            CashTrxDbCrModel cashTrxDbCrModel = new CashTrxDbCrModel();

            DateTime toDate;
            DateTime fromDate;
            fix_Dates(toDateStr, out toDate, out fromDate);

            if (isShowAdminReports)
            {
                //make sure user is Admin
                if (UserBiz.IsAdmin(UserId))
                {

                    cashTrxDbCrModel = getCashTrxDbCrModel("", cashTypeEnum, cashStateEnum, fromDate, toDate, isShowAdminReports);
                    return cashTrxDbCrModel;

                }

                throw new Exception("You are not an admin! How dare you!");


            }



            cashTrxDbCrModel = getCashTrxDbCrModel(CurrentUserParameter.PersonId, cashTypeEnum, cashStateEnum, fromDate, toDate, isShowAdminReports);
            return cashTrxDbCrModel;
        }

        private static void fix_Dates(string toDateStr, out DateTime toDate, out DateTime fromDate)
        {
            toDate = DateTime.Now;

            if (!toDateStr.IsNullOrWhiteSpace())
            {
                bool success = DateTime.TryParse(toDateStr, out toDate);
                if (!success)
                    toDate = DateTime.Now;
            }

            fromDate = toDate.AddMonths(-3).AddDays(-1);
        }

        UserParameter _userParameter;
        UserParameter CurrentUserParameter
        {
            get
            {
                if (_userParameter.IsNull())
                {
                    if (UserId.IsNullOrWhiteSpace())
                        return new UserParameter();
                    _userParameter = initUserParameter();
                }
                return _userParameter;
            }
        }

        /// <summary>
        /// this uses internal UserId, ie the logged in UserId
        /// </summary>
        /// <returns></returns>
        public GlobalObject GetGlobalObject()
        {
            if (UserId.IsNullOrWhiteSpace())
                return new GlobalObject();

            GlobalObject globalObject = Load_SO_And_PO_And_DO_Into_GlobalObject(UserId, DateTime.MinValue, DateTime.MaxValue);
            globalObject.Money_System = GetMoneyForSystem();
            globalObject.Money_User = GetMoneyInfoForUser();

            return globalObject;

        }

        /// <summary>
        /// This is the entry point.
        /// This loads up all the account for the user or admin.  
        /// if User is the Customer,then it is a purchase;
        /// if user is the Owner/Seller, then it is a sale;
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="buySellDocumentTypeEnum"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        GlobalObject Load_SO_And_PO_And_DO_Into_GlobalObject(string userId, DateTime fromDate, DateTime toDate)
        {
            bool isCustomer = CustomerBiz.GetPlayerFor(UserId) != null;
            bool isOwner = OwnerBiz.GetPlayerFor(UserId) != null;
            bool isDeliveryman = DeliverymanBiz.GetPlayerFor(UserId) != null;
            bool isSalesman = SalesmanBiz.GetPlayerFor(UserId) != null;
            bool isAdmin = UserBiz.IsAdmin(UserId);
            bool isLoggedIn = !UserId.IsNullOrWhiteSpace();
            bool isBank = CurrentUserParameter.IsBank;
            bool isSuperSalesman = CurrentUserParameter.IsSuperSalesman;

            GlobalObject mip = new GlobalObject(userId, isAdmin, isCustomer, isOwner, isDeliveryman, isSalesman, isLoggedIn, isBank, isSuperSalesman);

            //if we let this through, thr program will think we have an admin.
            //anyway, without userId we fail.
            if (userId.IsNullOrEmpty())
                return mip;

            if (isOwner)
                mip.Sale = BuySellDocBiz.GetOrderTypesFor(userId, BuySellDocumentTypeENUM.Sale, fromDate, toDate);

            if (isCustomer)
                mip.Purchase = BuySellDocBiz.GetOrderTypesFor(userId, BuySellDocumentTypeENUM.Purchase, fromDate, toDate);

            if (isDeliveryman)
                mip.Delivery = BuySellDocBiz.GetOrderTypesFor(userId, BuySellDocumentTypeENUM.Delivery, fromDate, toDate);

            if (isSalesman)
                mip.Salesman = BuySellDocBiz.GetOrderTypesFor(userId, BuySellDocumentTypeENUM.Salesman, fromDate, toDate);

            if (isAdmin)
            {
                mip.Sale_Admin = BuySellDocBiz.GetOrderTypesFor("", BuySellDocumentTypeENUM.Sale, fromDate, toDate);
                mip.Purchase_Admin = BuySellDocBiz.GetOrderTypesFor("", BuySellDocumentTypeENUM.Purchase, fromDate, toDate);
                mip.Delivery_Admin = BuySellDocBiz.GetOrderTypesFor("", BuySellDocumentTypeENUM.Delivery, fromDate, toDate);
                mip.Salesman_Admin = BuySellDocBiz.GetOrderTypesFor("", BuySellDocumentTypeENUM.Salesman, fromDate, toDate);

            }
            return mip;
        }


        public async Task<ApplicationUser> RegisterAsync(RegisterViewModel model)
        {
            ApplicationUser user = UserBiz.FindByName(model.UserName);

            if (!user.IsNull())
                UserBiz.ErrorsGlobal.Add(string.Format("You cannot register a user with this name: '{0}'. This user already exists.", model.UserName), "Register User");

            //below this, we have ascertained, there is no user.

            Customer customer = CustomerBiz.FindByName(model.UserName);
            Owner owner = OwnerBiz.FindByName(model.UserName);
            Person person = PersonBiz.FindByName(model.UserName);
            bool somethingCreated = false;
            if (person.IsNull())
            {
                person = createPerson(model);
                somethingCreated = true;
            }

            person.IsNullThrowException();

            string personId = person.Id;

            if (owner.IsNull())
            {
                owner = createOwner(model, personId);
                somethingCreated = true;
            }

            if (customer.IsNull())
            {
                customer = createCustomer(model, personId);
                somethingCreated = true;
            }

            if (somethingCreated)
                await PersonBiz.SaveChangesAsync();

            //user = await UserBiz.RegisterAsync(model, personId);
            user = UserBiz.Factory() as ApplicationUser;
            user.PersonId = personId;
            user.UserName = model.UserName;
            user.Name = model.UserName;
            if (await UserBiz.CreateAsync(user, model.Password))
            {
                await UserBiz.SignInUser(user, false);
            }


            return user;


        }
        #region BalanceForPerson
        public decimal BalanceFor(CashPaymentModel cashPaymentModel)
        {
            return BalanceFor_Person(cashPaymentModel.PersonToId, cashPaymentModel.CashTypeEnum);
        }



        public decimal BalanceFor_User(string userId, CashTypeENUM cashTypeEnum)
        {
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException();
            return BalanceFor_Person(person.Id, cashTypeEnum);
        }










        //This calculates the person's available amount
        public decimal BalanceFor_Person(string personIdForWhomWeAreWorking, CashTypeENUM cashTypeEnum)
        {
            personIdForWhomWeAreWorking.IsNullOrWhiteSpaceThrowArgumentException("personIdForWhomWeAreWorking");
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");


            List<CashTrxVM2> cashTrxPaid = allTrxFor(personIdForWhomWeAreWorking, cashTypeEnum, CashStateENUM.All, false);


            decimal availableAmount = CashTrxVM2.RunningTotal(cashTrxPaid);


            return availableAmount;

        }

        public CashBalanceVM BalanceFor_Person(string personIdForWhomWeAreWorking, CashStateENUM cashStateEnum)
        {
            personIdForWhomWeAreWorking.IsNullOrWhiteSpaceThrowArgumentException("personIdForWhomWeAreWorking");


            List<CashTrxVM2> cashTrxPaid_Refundable = allTrxFor(personIdForWhomWeAreWorking, CashTypeENUM.Refundable, cashStateEnum, false);
            List<CashTrxVM2> cashTrxPaid_NonRefundable = allTrxFor(personIdForWhomWeAreWorking, CashTypeENUM.NonRefundable, cashStateEnum, false);


            decimal availableAmount_Refundable = CashTrxVM2.RunningTotal(cashTrxPaid_Refundable);
            decimal availableAmount_NonRefundable = CashTrxVM2.RunningTotal(cashTrxPaid_NonRefundable);

            CashBalanceVM cashBalanceVm = new CashBalanceVM(availableAmount_Refundable, availableAmount_NonRefundable);

            return cashBalanceVm;

        }



        #endregion






        public bool IsCustomer(string personId, out Customer customer)
        {
            customer = CustomerBiz.GetPlayerForPersonId(personId);
            return !customer.IsNull();

        }

        public bool IsOwner(string personId, out Owner owner)
        {
            owner = OwnerBiz.GetPlayerForPersonId(personId);
            return !owner.IsNull();

        }



        public bool IsSalesman(string personId, out Salesman salesman)
        {
            salesman = SalesmanBiz.GetPlayerForPersonId(personId);
            return !salesman.IsNull();

        }


        public bool IsDeliveryman(string personId, out Deliveryman deliveryman)
        {
            deliveryman = DeliverymanBiz.GetPlayerForPersonId(personId);
            return !deliveryman.IsNull();

        }





        /// <summary>
        /// This is injected into getCashFor
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        private IQueryable<CashTrx> cashTrx_SQL_For(string personIdForWhomWeAreWorking)
        {
            personIdForWhomWeAreWorking.IsNullOrWhiteSpaceThrowException();
            IQueryable<CashTrx> cashTrxPaid =
                CashTrxBiz.FindAll()
                .Where(x =>
                    x.PersonFromId == personIdForWhomWeAreWorking ||
                    x.PersonToId == personIdForWhomWeAreWorking);
            return cashTrxPaid;
        }






        public void CancelRejectOrder(RejectCancelDeleteInbetweenClass rcdbc)
        {

            rcdbc.BuySellDocId.IsNullOrWhiteSpaceThrowArgumentException();

            BuySellDoc buySellDoc = BuySellDocBiz.Find(rcdbc.BuySellDocId);
            buySellDoc.BuySellDocStateModifierEnum = rcdbc.BuySellDocStateModifierEnum;
            buySellDoc.BuySellDocumentTypeEnum = rcdbc.BuySellDocumentTypeEnum;


            //create subject
            rcdbc.Subject = rcdbc.ToString();

            if (rcdbc.Comment.IsNullOrWhiteSpace())
            { }
            else
            {
                //add the message
                //current user is sending the message. 
                //if the document type is purchase, then it is the customer
                //if document type is sale, then it is the owner sending the message.
                //we need to find the persons for each.
                createMessageFor(rcdbc, buySellDoc);
            }
            Person systemPerson = PersonBiz.Find(CurrentUserParameter.SystemPersonId);
            createPenaltyTrx(buySellDoc, systemPerson, rcdbc.Comment);

            ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
            parm.Entity = buySellDoc as ICommonWithId;
            parm.GlobalObject = rcdbc.GlobalObject;

            BuySellDocBiz.UpdateAndSave(parm);

        }


        //this is used in the top menu
        //Cash that is created has no PersonFromId
        private decimal TotalCashAvailableInSystem_System(CashTypeENUM cashTypeEnum)
        {

            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");



            List<CashTrxVM2> cashTrxPaid = allTrxFor("", cashTypeEnum, CashStateENUM.All, CurrentUserParameter.IsAdmin);


            return CashTrxVM2.RunningTotal(cashTrxPaid);
        }





        public void BuyAjax_Code(string productChildId, out string message, out bool success)
        {
            message = "Failure!";
            success = false;
            //save the item , 
            string poNumber = "";
            DateTime poDate = DateTime.MinValue;
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            productChildId.IsNullOrWhiteSpaceThrowArgumentException("Product not recieved.");

            message = AddToSale(UserId, productChildId, poNumber, poDate);
            success = true;
        }


        private Owner createOwner(RegisterViewModel model, string personId)
        {
            Owner owner = OwnerBiz.Factory() as Owner;
            owner = OwnerBiz.Factory() as Owner;

            owner.PersonId = personId;
            owner.Name = model.UserName;

            OwnerBiz.Create(owner);

            return owner;
        }

        private Person createPerson(RegisterViewModel model)
        {
            Person person = PersonBiz.Factory() as Person;
            person = PersonBiz.Factory() as Person;
            person.Name = model.UserName;
            PersonBiz.Create(person);
            return person;

        }

        private Customer createCustomer(RegisterViewModel model, string personId)
        {
            Customer customer = CustomerBiz.Factory() as Customer;
            customer = CustomerBiz.Factory() as Customer;

            customer.PersonId = personId;
            customer.Name = model.UserName;

            CustomerBiz.Create(customer);

            return customer;
        }



        //----------- Delivery and Pickup related
        public void Deliveryman_Accepts_To_Deliver(string frtOfferId, string buySellId, GlobalObject globalObject)
        {
            UserId.IsNullOrWhiteSpaceThrowException();
            buySellId.IsNullOrWhiteSpaceThrowArgumentException();
            frtOfferId.IsNullOrWhiteSpaceThrowArgumentException();

            //locate the freight trx and mark it true.
            FreightOfferTrx fot = FreightOfferTrxBiz.Find(frtOfferId);
            fot.IsNullThrowException();

            BuySellDoc bsd = BuySellDocBiz.Find(buySellId);
            bsd.IsNullThrowException();

            throwErrorIfDeliverymanDoesNotHaveEnoughMoneyToContinue(fot, bsd);
            //fot.IsOfferAccepted = true;
            //fot.InsuranceAmount = buySellDoc.InsuranceRequired;

            //setup
            bsd.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Accept;

            GetAllDeliverySalesmen(bsd);
            //bsd.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Delivery;

            //this should all happen when courier accepts.
            bsd.FreightOfferTrxAccepted = fot;
            bsd.FreightOfferTrxAcceptedId = fot.Id;

            ControllerCreateEditParameter paramFot = new ControllerCreateEditParameter();
            paramFot.Entity = fot as ICommonWithId;
            paramFot.GlobalObject = globalObject;

            FreightOfferTrxBiz.UpdateAndSave(paramFot);

            ControllerCreateEditParameter paramBsd = new ControllerCreateEditParameter();
            paramBsd.Entity = fot as ICommonWithId;
            paramBsd.GlobalObject = globalObject;

            BuySellDocBiz.UpdateAndSave(paramBsd);
        }

        /// <summary>
        /// This loads the deliveryman salesmen
        /// </summary>
        /// <param name="bsd"></param>
        public void GetAllDeliverySalesmen(BuySellDoc bsd)
        {
            Salesman deliverymanSalesman = GetSalesmanForDeliveryman(bsd.DeliverymanId);
            if (deliverymanSalesman.IsNull())
            {
                //there is no deliverysalesman
            }
            else
            {
                bsd.DeliverymanSalesmanId = deliverymanSalesman.Id;
                if (deliverymanSalesman.ParentSalesmanId.IsNullOrWhiteSpace())
                {
                    //there is no super salesman

                }
                else
                {
                    //a super deliveryman salesman exists... now get him.
                    Salesman deliverymanSuperSalesman = null;
                    if (deliverymanSalesman.ParentSalesman.IsNull())
                    {
                        deliverymanSuperSalesman = SalesmanBiz.Find(deliverymanSalesman.ParentSalesmanId);
                    }
                    else
                    {
                        deliverymanSuperSalesman = deliverymanSalesman.ParentSalesman;
                    }

                    deliverymanSuperSalesman.IsNullThrowException();

                    //load the SuperDeliverymanSalesman
                    bsd.SuperDeliverymanSalesman = deliverymanSuperSalesman;
                    bsd.SuperDeliverymanSalesmanId = deliverymanSuperSalesman.Id;

                    //Now see if there is a super super Deliveryman Salesman
                    if (bsd.SuperDeliverymanSalesman.ParentSalesmanId.IsNullOrWhiteSpace())
                    {
                        //there is no super super deliverymanSalesman
                    }
                    else
                    {
                        //there is a deliveryman salesman
                        Salesman superSuperDeliverymanSalesman = null;
                        if (bsd.SuperDeliverymanSalesman.ParentSalesmen.IsNull())
                        {
                            superSuperDeliverymanSalesman = SalesmanBiz.Find(bsd.SuperDeliverymanSalesman.ParentSalesmanId);

                        }
                        else
                        {
                            superSuperDeliverymanSalesman = bsd.SuperDeliverymanSalesman.ParentSalesman;
                        }
                        superSuperDeliverymanSalesman.IsNullThrowException();

                        bsd.SuperSuperCustomerSalesmanId = superSuperDeliverymanSalesman.Id;
                        bsd.SuperSuperCustomerSalesman = superSuperDeliverymanSalesman;

                    }

                }
            }
        }


        void getAllCustomerSalesmen(BuySellDoc bsd)
        {
            Salesman customerSalesman = getSalesmanForCustomer(bsd.CustomerId);
            if (customerSalesman.IsNull())
            {
                //there is no deliverysalesman
            }
            else
            {
                bsd.CustomerSalesmanId = customerSalesman.Id;

                if (customerSalesman.CustomerSalesmenBuySellDocs.IsNull())
                    customerSalesman.CustomerSalesmenBuySellDocs = new List<BuySellDoc>();

                customerSalesman.CustomerSalesmenBuySellDocs.Add(bsd);

                if (customerSalesman.ParentSalesmanId.IsNullOrWhiteSpace())
                {
                    //there is no super salesman

                }
                else
                {
                    //a super customer salesman exists... now get him.
                    Salesman customerSuperSalesman = null;
                    if (customerSalesman.ParentSalesman.IsNull())
                    {
                        customerSuperSalesman = SalesmanBiz.Find(customerSalesman.ParentSalesmanId);
                    }
                    else
                    {
                        customerSuperSalesman = customerSalesman.ParentSalesman;
                    }

                    customerSuperSalesman.IsNullThrowException();

                    //load the SuperCustomerSalesman
                    bsd.SuperCustomerSalesman = customerSuperSalesman;
                    if (customerSuperSalesman.SuperCustomerSalesmanBuySellDocs.IsNull())
                        customerSuperSalesman.SuperCustomerSalesmanBuySellDocs = new List<BuySellDoc>();


                    bsd.SuperCustomerSalesmanId = customerSuperSalesman.Id;
                    customerSuperSalesman.SuperCustomerSalesmanBuySellDocs.Add(bsd);

                    //Now see if there is a super super Customer Salesman
                    if (bsd.SuperCustomerSalesman.ParentSalesmanId.IsNullOrWhiteSpace())
                    {
                        //there is no super super customerSalesman
                    }
                    else
                    {
                        //there is a customer salesman
                        Salesman superSuperCustomerSalesman = null;
                        if (bsd.SuperCustomerSalesman.ParentSalesmen.IsNull())
                        {
                            superSuperCustomerSalesman = SalesmanBiz.Find(bsd.SuperCustomerSalesman.ParentSalesmanId);

                        }
                        else
                        {
                            superSuperCustomerSalesman = bsd.SuperCustomerSalesman.ParentSalesman;
                        }
                        superSuperCustomerSalesman.IsNullThrowException();

                        bsd.SuperSuperCustomerSalesmanId = superSuperCustomerSalesman.Id;
                        bsd.SuperSuperCustomerSalesman = superSuperCustomerSalesman;

                        if (superSuperCustomerSalesman.SuperSuperCustomerSalesmanBuySellDocs.IsNull())
                            superSuperCustomerSalesman.SuperSuperCustomerSalesmanBuySellDocs = new List<BuySellDoc>();

                        superSuperCustomerSalesman.SuperSuperCustomerSalesmanBuySellDocs.Add(bsd);


                    }

                }

            }
        }











        void getAllOwnerSalesmen(BuySellDoc bsd)
        {
            Salesman ownerSalesman = getSalesmanForOwner(bsd.OwnerId);
            if (ownerSalesman.IsNull())
            {
                //there is no deliverysalesman
            }
            else
            {
                bsd.OwnerSalesmanId = ownerSalesman.Id;
                if (ownerSalesman.ParentSalesmanId.IsNullOrWhiteSpace())
                {
                    //there is no super salesman

                }
                else
                {
                    //a super owner salesman exists... now get him.
                    Salesman ownerSuperSalesman = null;
                    if (ownerSalesman.ParentSalesman.IsNull())
                    {
                        ownerSuperSalesman = SalesmanBiz.Find(ownerSalesman.ParentSalesmanId);
                    }
                    else
                    {
                        ownerSuperSalesman = ownerSalesman.ParentSalesman;
                    }

                    ownerSuperSalesman.IsNullThrowException();

                    //load the SuperOwnerSalesman
                    bsd.SuperOwnerSalesman = ownerSuperSalesman;
                    bsd.SuperOwnerSalesmanId = ownerSuperSalesman.Id;

                    //Now see if there is a super super Owner Salesman
                    if (bsd.SuperOwnerSalesman.ParentSalesmanId.IsNullOrWhiteSpace())
                    {
                        //there is no super super ownerSalesman
                    }
                    else
                    {
                        //there is a owner salesman
                        Salesman superSuperOwnerSalesman = null;
                        if (bsd.SuperOwnerSalesman.ParentSalesmen.IsNull())
                        {
                            superSuperOwnerSalesman = SalesmanBiz.Find(bsd.SuperOwnerSalesman.ParentSalesmanId);

                        }
                        else
                        {
                            superSuperOwnerSalesman = bsd.SuperOwnerSalesman.ParentSalesman;
                        }
                        superSuperOwnerSalesman.IsNullThrowException();

                        bsd.SuperSuperOwnerSalesmanId = superSuperOwnerSalesman.Id;
                        bsd.SuperSuperOwnerSalesman = superSuperOwnerSalesman;

                    }

                }
            }
        }



















        private void throwErrorIfDeliverymanDoesNotHaveEnoughMoneyToContinue(FreightOfferTrx fot, BuySellDoc buySellDoc)
        {
            decimal currRefundabelBalance = 0;
            decimal amountRequired = 0;
            decimal frtPlusInsurance = buySellDoc.InsuranceRequired + fot.OfferAmount;
            decimal balanceRequired = amountRequired - frtPlusInsurance;
            if (!HasAvailableBalance_User(UserId, CashTypeENUM.Refundable, amountRequired, false, out currRefundabelBalance))
            {
                string err = string.Format("You do not have sufficent balance. Your balance is {0} and the required amount to service this order is INSURANCE {1} + FREIGHT GUARANTEE {2} = TOTAL {3}. You need to purchase Rs.{4} to continue,",
                    currRefundabelBalance,
                    buySellDoc.InsuranceRequired,
                    fot.OfferAmount,
                    frtPlusInsurance,
                    balanceRequired);
                throw new Exception(err);
            }
        }


        private void codeForEdit(BuySellDoc buySellDoc)
        {

            UserId.IsNullOrWhiteSpaceThrowException();


            //===================================================================================
            switch (buySellDoc.BuySellDocumentTypeEnum)
            {

                //=============================================================================
                case BuySellDocumentTypeENUM.Delivery:                          //====DELIVERY
                    //=============================================================================
                    BuySellDocBiz.LoadPickupDates(buySellDoc);
                    BuySellDocBiz.FixFreightOfferAndCustomerBudeget(buySellDoc);
                    BuySellDocBiz.Load_Current_User_As_Delyman_Id_Into_BuySellDoc_And_UpdateVarsWithEarlierOffer(buySellDoc);
                    BuySellDocBiz.Update_Delyman_Defaults_With_FreightOffer_From_CustomersRequestOffer(buySellDoc);
                    BuySellDocBiz.Update_VehicalType_Offered(buySellDoc);


                    switch (buySellDoc.BuySellDocStateEnum)
                    {

                        case BuySellDocStateENUM.ReadyForPickup:
                            //check to see if the insurance is required,and the deliveryman has the funds to cover it.
                            //funds will not be locked until courier is accepted.
                            Is_Delymans_Available_Balance_Enough_To_Continue(buySellDoc);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        default:
                            break;
                    }
                    break;





                //=============================================================================
                case BuySellDocumentTypeENUM.Sale:                          //SALE
                    //=============================================================================

                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        default:
                            break;
                    }
                    break;




                //=============================================================================
                case BuySellDocumentTypeENUM.Purchase:          //PURCHASE
                    //=============================================================================
                    //here the UserId is the customer

                    BuySellDocBiz.AddPickupAddress(buySellDoc);

                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            BuySellDocBiz.AddDefaultVehicalType(buySellDoc);
                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }


        }

        public void Is_Delymans_Available_Balance_Enough_To_Continue(BuySellDoc buySellDoc)
        {
            decimal balance = 0;
            bool hasAvailBal = HasAvailableBalance_User(UserId, CashTypeENUM.Refundable, buySellDoc.InsuranceRequired, false, out balance);
            decimal insuranceAmount = buySellDoc.InsuranceRequired;
            decimal freightRefund = buySellDoc.FreightCustomerBudget;
            if (hasAvailBal)
            {

            }
            else
            {
                string err = string.Format("Your refundable balance is Rs.{0:N0}. You require Rs.{1:N0} for insurance + guaranteed freight refund Rs.{2:N0}. The total amount required to continue is {3:N0}. You do not have the available balance to continue",
                    balance,
                    insuranceAmount,
                    freightRefund,
                    balance + insuranceAmount + freightRefund);

                throw new Exception(err);
            }
        }

        public void Event_Edit_ViewAndSetupSelectList_Get_Code(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

            BuySellDoc buySellDoc = BuySellDocBiz.UnboxBuySell(parm);

            BuySellDocBiz.Load_DocumentType_Into_BuySell(parm);
            BuySellDocBiz.Load_DocStateAndType_Into_Items(buySellDoc);
            //get_AcceptValue(parm, buySellDoc);
            BuySellDocBiz.Make_Heading_For_Create_Form(parm);
            BuySellDocBiz.LoadSelectListsFor_GET(buySellDoc);
            BuySellDocBiz.FillAddressShipFromComplex(buySellDoc);
            BuySellDocBiz.FillAddressShipToComplex(buySellDoc);
            BuySellDocBiz.AddDefaultValuesToPleasePickupDate(buySellDoc);
            BuySellDocBiz.AddDefaultValuesToDelivrymanOfferVariables(buySellDoc);

            buySellDoc.UserParameter = initUserParameter(UserId);


            codeForEdit(buySellDoc);
        }

        //private void getDeliverymanSalesman(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Delivery)
        //    {
        //        //this is the current state BuySellDocStateENUM.RequestConfirmed moving on to BuySellDocStateENUM.CourierComingToPickUp
        //        if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller)
        //        {
        //            if (buySellDoc.DeliverymanSalesmanId.IsNullOrWhiteSpace())
        //                buySellDoc.DeliverymanSalesmanId = GetSalesmanForDeliveryman(buySellDoc.DeliverymanId);

        //        }
        //    }
        //}

        /// <summary>
        /// this will get the salesman for the current user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Salesman getSalesmanForCustomer(string playerId)
        {
            //the customer salesman will be selected
            //depending on who has sold him the most money
            //in 3 months. If there is a tie then, who sold
            //the money first in 3 months.

            //get customers cashTrx for 3 months
            //group them by personId and get sum of their amounts
            //sort decending by Amount and ascending date
            //get first entry

            playerId.IsNullOrWhiteSpaceThrowException();

            Person person = CustomerBiz.GetPersonForPlayer(playerId);
            return getSalesmanIdForCommission_Algorithem(person);

        }

        Salesman getSalesmanForOwner(string playerId)
        {
            playerId.IsNullOrWhiteSpaceThrowException();

            Person person = OwnerBiz.GetPersonForPlayer(playerId);
            return getSalesmanIdForCommission_Algorithem(person);

        }

        public Salesman GetSalesmanForDeliveryman(string playerId)
        {
            //playerId.IsNullOrWhiteSpaceThrowException();
            if (playerId.IsNullOrWhiteSpace())
                return null;

            Person person = DeliverymanBiz.GetPersonForPlayer(playerId);
            return getSalesmanIdForCommission_Algorithem(person);

        }

        /// <summary>
        /// This only retuns a salesman Id if the amount of cash sold to the person is greater than a certain amount.
        /// </summary>
        /// <param name="noOfDays"></param>
        /// <param name="person"></param>
        /// <returns></returns>

        private Salesman getSalesmanIdForCommission_Algorithem(Person person)
        {
            int noOfDays = GetNoOfDaysSalesmanKeepsPerson();


            string personWhoSoldCashId = null;
            if (person.IsNull())
                return null;



            DateTime dayCommissionBegins = DateTime.Now.AddDays(noOfDays * (-1));

            List<CashTrx> lstOfCashTrx = CashTrxBiz
                .FindAll()
                .Where(x =>
                    x.PersonToId == person.Id &&
                    x.MetaData.Created.Date >= dayCommissionBegins)
                    .OrderBy(x => x.MetaData.Created.Date)
                    .ToList();


            //if (lstOfTrx.IsNullOrEmpty())
            //    return null;

            //now group with personFrom and get sum of cash

            //var query = (from t in Transactions
            //             group t by new { t.MaterialID, t.ProductID }
            //                 into grp
            //                 select new
            //                 {
            //                     grp.Key.MaterialID,
            //                     grp.Key.ProductID,
            //                     Quantity = grp.Sum(t => t.Quantity)
            //                 }).ToList();
            //https://stackoverflow.com/questions/847066/group-by-multiple-columns

            List<CashGroupForFindingSalesman> groupedList = (from t in lstOfCashTrx
                                                             group t by new { t.PersonFromId, t.CashTypeEnum, t.CashStateEnum }
                                                                 into grp
                                                                 select new CashGroupForFindingSalesman
                                                                 {
                                                                     PersonFromId = grp.Key.PersonFromId,
                                                                     CashTypeEnum = grp.Key.CashTypeEnum,
                                                                     CashStateEnum = grp.Key.CashStateEnum,
                                                                     TotalCash = grp.Sum(x => x.Amount)
                                                                 })
                                   .OrderBy(x => x.TotalCash)
                                   .ToList();

            if (groupedList.IsNullOrEmpty())
                return null;

            List<string> notSalesmenList = new List<string>();
            foreach (CashGroupForFindingSalesman item in groupedList)
            {
                Salesman salesman = SalesmanBiz.GetPlayerForPersonId(item.PersonFromId);
                if (salesman.IsNull())
                {
                    notSalesmenList.Add(item.PersonFromId);
                }

            }

            //if any non-salesmen found, remove them
            //they are just noise.

            if (notSalesmenList.IsNull())
            {
                //do nothing
            }
            else
            {
                //remove these from the orignal list
                foreach (var nonSalesmanPersonId in notSalesmenList)
                {
                    CashGroupForFindingSalesman cashGroupForFindingSalesman = groupedList.Find(x => x.PersonFromId == nonSalesmanPersonId);
                    if (!cashGroupForFindingSalesman.IsNull())
                        groupedList.Remove(cashGroupForFindingSalesman);

                }
            }

            if (groupedList.IsNullOrEmpty())
                return null;

            //make sure that the totalAmount of the topmost salesman is above the minimum amount.
            //if not, then the game is already over.
            string minimumAmountAllowedstr = ConfigurationManager.AppSettings["salesman.minimum.currency.sale"];
            decimal minAmount = minimumAmountAllowedstr.ToDecimal();

            if (groupedList[0].TotalCash < minAmount)
                return null;

            if (groupedList.Count == 1)
            {
                personWhoSoldCashId = groupedList[0].PersonFromId;
            }
            else
            {
                //sort by amount one more time... just in case
                groupedList = groupedList
                    .OrderByDescending(x => x.TotalCash)
                    .ToList();

                if (groupedList[0].TotalCash != groupedList[1].TotalCash)
                {
                    personWhoSoldCashId = groupedList[0].PersonFromId;
                }
                else
                {
                    //we have a tie. Now we need to find who was first.
                    //the list is in order of date. See which person is first
                    string personFound_0 = groupedList[0].PersonFromId;
                    string personFound_1 = groupedList[1].PersonFromId;

                    for (int i = 0; i < lstOfCashTrx.Count; i++)
                    {
                        if (lstOfCashTrx[i].PersonFromId == personFound_0 || lstOfCashTrx[i].PersonFromId == personFound_1)
                        {
                            personWhoSoldCashId = lstOfCashTrx[i].PersonFromId;
                            break;
                        }
                    }
                }

            }


            //get salesman for this person
            Salesman salesmanForCommission = SalesmanBiz.GetPlayerForPersonId(personWhoSoldCashId);
            salesmanForCommission.IsNullThrowException();

            return salesmanForCommission;

        }

        public int GetNoOfDaysSalesmanKeepsPerson()
        {
            string noOfDaysString = ConfigurationManager.AppSettings["Salesman.KeepsPersonForDays"];
            noOfDaysString.IsNullOrWhiteSpaceThrowException();
            int noOfDays = noOfDaysString.ToInt();

            if (noOfDays == 0)
                throw new Exception(string.Format("No of days for keeping person for salesman is 0"));
            return noOfDays;
        }


        public int GetMinimumRuppeeAmountRequiredToKeepPerson()
        {
            string noOfDaysString = ConfigurationManager.AppSettings["salesman.minimum.currency.sale"];
            noOfDaysString.IsNullOrWhiteSpaceThrowException();
            int noOfDays = noOfDaysString.ToInt();

            if (noOfDays == 0)
                throw new Exception(string.Format("Minimum sale amount by salesman is 0"));
            return noOfDays;
        }


        public int GetNoOfDaysCashBackGuarantee()
        {
            return PenaltyClassAbstract.GetNoOfDaysCashBackGuarantee();

        }

        public decimal GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly()
        {
            return PenaltyClassAbstract.GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly();
        }

        public decimal GetPenaltyPercentageForDeliverymanQuitting()
        {
            return PenaltyClassAbstract.GetPenaltyPercentageForDeliverymanQuitting();
        }
        public static decimal GetPenaltyPercentageForPurchaserQuitting()
        {
            return PenaltyClassAbstract.GetPenaltyPercentageForPurchaserQuitting();
        }
        public static decimal GetCostToBecomeASalesman()
        {

            return Salesman.GetCostToBecomeASalesman();

        }
        public static int GetNumberOfBsdToBecomeSuperSalesman()
        {

            return Salesman.GetNumberOfBsdToBecomeSuperSalesman();

        }


        ///// <summary>
        ///// This checks the buySellDoc state to see if guarantee is applicable. If it is applicable, the guartee amount becomes
        ///// the freight amount. If it is with the courier, this amount is returned. If it has been delivered then the algorithem
        ///// checks to make sure that it was 
        /////     Delivered on time
        /////     Delivered by this courier. It can be he never delivered, and someone else delivered in his place.
        ///// if delivered on time and the deliveryman is the deliveryman on the buyselldoc
        /////     the amount becomes zero.
        /////  otheriwse
        /////     if it was delivered late or early, by the deliveryman on the buyselldoc, then a penelty of half freight or wais charged.
        ///// </summary>
        ///// <param name="fot"></param>
        ///// <param name="bsd"></param>
        ///// <param name="guaranteeAmount"></param>
        ///// <returns></returns>
        //private decimal calculateDeliverymanGuaranteeAmount(FreightOfferTrx fot, BuySellDoc bsd, decimal guaranteeAmount)
        //{
        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup)
        //        return 0;

        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
        //        return 0;

        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
        //        return 0;

        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
        //        return 0;

        //    guaranteeAmount = bsd.Freight_Accepted;

        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
        //    {
        //        //if courier is on time do not make any entry, otherwise make entry
        //        DateParameter dp = new DateParameter();
        //        dp.BeginDate = bsd.AgreedPickupDateByDeliveryman;
        //        dp.EndDate = bsd.OrderDelivered.Date_NotNull_Min;
        //        bool isCourierOnTime = dp.BeginDateAndEndDateAreEqual;
        //        bool isThisTheCourierWhoDelivered = bsd.DeliverymanId == fot.DeliverymanId;

        //        if (isCourierOnTime && isThisTheCourierWhoDelivered)
        //        {
        //            guaranteeAmount = 0;
        //        }
        //        else
        //        {
        //            //courier is not on time. Either he is early, or he is late.
        //            //regardless... cut half. He must do as promised.
        //            if (isThisTheCourierWhoDelivered)
        //            {
        //                decimal percentPenalty = GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly();

        //                if (percentPenalty == 0)
        //                    return 0;

        //                //cut half. He was not on time
        //                guaranteeAmount = guaranteeAmount * percentPenalty / 100;
        //            }
        //            else
        //            {
        //                //someone else deliverd.
        //                //cut all
        //                decimal percentPenalty = GetPenaltyPercentageForDeliverymanQuitting();

        //                if (percentPenalty == 0)
        //                    return 0;

        //                //cut half. He was not on time
        //                guaranteeAmount = guaranteeAmount * percentPenalty / 100;
        //            }

        //        }
        //    }
        //    return guaranteeAmount;
        //}

    }
}
