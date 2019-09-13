using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.CashNS.PenaltyNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsNS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashEncashmentTrxNS;
using UowLibrary.CashTtxNS;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.MessageNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.SalesmanNS;
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


        public SuperBiz(BankBiz bankBiz, AbstractControllerParameters abstractControllerParameters, BuySellDocBiz buySellDocBiz, PaymentMethodBiz paymentMethodBiz, CashEncashmentTrxBiz cashEncashmentTrxBiz, PenaltyHeaderBiz penaltyHeaderBiz)
        {
            _bankBiz = bankBiz;
            _buySellDocBiz = buySellDocBiz;
            _abstractControllerParameters = abstractControllerParameters;
            _paymentMethodBiz = paymentMethodBiz;
            _cashEncashmentTrxBiz = cashEncashmentTrxBiz;
            _penaltyHeaderBiz = penaltyHeaderBiz;
        }

        #region Bizs

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

        //#region HasAvailableBalancePerson
        //public bool HasAvailableBalancePerson(CashPaymentModel cashPaymentModel, string paymentFromUserId)
        //{

        //    Person person = PersonBiz.GetPersonForUserId(paymentFromUserId);
        //    person.IsNullThrowException("Person not found!");
        //    return HasAvailableBalancePerson(cashPaymentModel, person);
        //}
        //public bool HasAvailableBalancePerson(CashPaymentModel cashPaymentModel, Person person)
        //{
        //    return HasAvailableBalancePerson(person.Id, cashPaymentModel.CashTypeEnum, cashPaymentModel.Amount);
        //}

        //public bool HasAvailableBalancePerson(string personId, CashTypeENUM cashTypeEnum, decimal amountRequired)
        //{
        //    bool isBanker = BankBiz.IsBanker_Person(personId);

        //    if (isBanker)
        //        return true;

        //    return BalanceForPerson(personId, cashTypeEnum) >= amountRequired;
        //}

        //#endregion


        //#region BalanceForPerson
        //public decimal BalanceForPerson(CashPaymentModel cashPaymentModel)
        //{
        //    return BalanceForPerson(cashPaymentModel.PersonToId, cashPaymentModel.CashTypeEnum);
        //}
        ////This calculates the person's available amount
        //public decimal BalanceForPerson(string personId, CashTypeENUM cashTypeEnum)
        //{
        //    personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //        throw new Exception("Cash Type is unknown");


        //    //get all the person's cash trx
        //    List<CashTrx> cashTrxPaid = trxPaidFor(personId, cashTypeEnum, CashStateENUM.All);
        //    List<CashTrx> cashTrxReceived = trxReceivedFor(personId, cashTypeEnum, CashStateENUM.Available).ToList();

        //    decimal totalPaid = 0;
        //    decimal totalReceived = 0;

        //    if (!cashTrxPaid.IsNullOrEmpty())
        //        totalPaid = cashTrxPaid.Sum(x => x.Amount);

        //    if (!cashTrxReceived.IsNullOrEmpty())
        //        totalReceived = cashTrxReceived.Sum(x => x.Amount);

        //    decimal availableAmount = totalReceived - totalPaid;

        //    return availableAmount;

        //}


        //#endregion

        //public bool HasAvailableBalance_User(string userId, CashTypeENUM cashTypeEnum, decimal amountRequired)
        //{
        //    userId.IsNullOrWhiteSpaceThrowException();
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //        throw new Exception("Cash Type is unknown.");

        //    bool isBank = BankBiz.IsBanker_User(userId);

        //    if (isBank)
        //        return true;

        //    Person person = PersonBiz.GetPersonForUserId(userId);
        //    person.IsNullThrowException();
        //    string personId = person.Id;

        //    return BalanceForPerson(personId, cashTypeEnum) >= amountRequired;
        //}



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
            customer.IsNullThrowException("customer");

            //get owner
            Owner owner = OwnerBiz.GetPlayerFor(userId);
            owner.IsNullThrowException("owner");

            //get person
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("Person");

            //get deliveryman
            Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(userId);
            //deliveryman.IsNullThrowException("deliveryman");
            string deliverymanId = deliveryman.IsNull() ? "" : deliveryman.Id;

            //get salesman
            Salesman salesman = SalesmanBiz.GetPlayerFor(userId);
            string salesmanId = salesman.IsNull() ? "" : salesman.Id;

            //salesman.IsNullThrowException("salesman");


            bool isAdmin = UserBiz.IsAdmin(userId);

            //get name of Admin
            string adminName = new ConfigManagerHelper().AdminName;
            Person personAcceptingPaymentForSystem = PersonBiz.FindByName(adminName);
            personAcceptingPaymentForSystem.IsNullThrowException();

            UserParameter userParameter = new UserParameter(userId, UserName, customer.Id, owner.Id, deliverymanId, salesmanId, person.Id, person.FullName(), isAdmin, personAcceptingPaymentForSystem.Id);
            return userParameter;
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

            //return CashTrxBiz.AddPayment(makePaymentModel.PersonFromId, makePaymentModel.PersonToId, makePaymentModel.Amount, makePaymentModel.CashTypeEnum, makePaymentModel.Comment, isBank, availableFunds);
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

            GlobalObject globalObject = BuySellDocBiz.Load_SO_And_PO_And_DO_Into_GlobalObject(UserId, DateTime.MinValue, DateTime.MaxValue);
            globalObject.Money_System = GetMoneyForSystem();
            globalObject.Money_User = GetMoneyInfoForUser();

            return globalObject;

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


            //get user for person
            //ApplicationUser user = PersonBiz.GetUserFor(personIdForWhomWeAreWorking);
            //UserParameter userParameter = initUserParameter(user.Id);

            //get all the person's cash trx
            //List<CashTrx> cashTrxPaid = trxPaidFor(personId, cashTypeEnum, CashStateENUM.All);
            //List<CashTrx> cashTrxReceived = trxReceivedFor(personId, cashTypeEnum, CashStateENUM.Available, "", "");
            //string personIdForWhomWeAreWorking = CurrentUserParameter.PersonId;

            List<CashTrxVM2> cashTrxPaid = allTrxFor(personIdForWhomWeAreWorking, cashTypeEnum, CashStateENUM.All, false);


            decimal availableAmount = CashTrxVM2.RunningTotal(cashTrxPaid);


            return availableAmount;

        }


        #endregion



        //private List<CashTrxVM2> trxReceivedFor(string personIdForWhomWeAreWorking, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, bool isShowAdminReports)
        //{
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //        throw new Exception("CashTypeENUM.Unknown");


        //    if (isShowAdminReports)
        //    {

        //        //this is admin.
        //        //he only wants to see the created and destroyed transactions.
        //        List<CashTrx> cashTrxDestroyed = get_Systems_Destroyed_Cash_Trx();

        //        if (cashTrxDestroyed.IsNullOrEmpty())
        //            return null;
        //        List<CashTrxVM2> cashTrxVm2List = new List<CashTrxVM2>();
        //        foreach (CashTrx cashTrx in cashTrxDestroyed)
        //        {
        //            CashTrxVM2 cashTrxVM2 = cashTrx.ConvertToCashTrxVM2("(DESTROYED CASH)", personIdForWhomWeAreWorking);
        //        }
        //        return cashTrxVm2List;

        //    }
        //    else
        //    {

        //        List<BuySellDoc> lstBuySellDocs_Allocated = new List<BuySellDoc>();
        //        List<BuySellDoc> lstBuySellDocs_Available = new List<BuySellDoc>();
        //        //Not required... these take money out of the system
        //        //List<CashEncashmentTrx> lst_Of_Encashments_Allocated = new List<CashEncashmentTrx>(); ;
        //        //List<CashEncashmentTrx> lst_Of_Encashments_Available = new List<CashEncashmentTrx>();

        //        switch (cashStateEnum)
        //        {
        //            case CashStateENUM.Available:

        //                lstBuySellDocs_Available = get_BuySellDocs_Allocated_Receipt();
        //                break;
        //            case CashStateENUM.Allocated:

        //                lstBuySellDocs_Allocated = get_BuySellDocs_Allocated_Receipt();

        //                break;
        //            case CashStateENUM.All:

        //                lstBuySellDocs_Allocated = get_BuySellDocs_Allocated_Receipt();
        //                lstBuySellDocs_Available = get_BuySellDocs_Allocated_Receipt();
        //                break;
        //            default:
        //                break;
        //        }

        //        List<CashTrx> lst_CashTrx = cashTrx_SQL_For(cashTrxReceivedSql_For_Admin_Or_Normal(personIdForWhomWeAreWorking, isShowAdminReports), cashTypeEnum).ToList();
        //        List<CashTrxVM2> lst_CashTrxVM2 = CashTrxVM2.ConvertCashTrxListToCashVM2List(lst_CashTrx, personIdForWhomWeAreWorking);

        //        List<CashTrxVM2> lst_BuySellDoc_CashTrx_Allocated = convert_BuySellDoc_To_CashTrx_Receipts(personIdForWhomWeAreWorking, lstBuySellDocs_Allocated, cashStateEnum, cashTypeEnum, isShowAdminReports);
        //        List<CashTrxVM2> lst_BuySellDoc_CashTrx_Available = convert_BuySellDoc_To_CashTrx_Receipts(personIdForWhomWeAreWorking, lstBuySellDocs_Available, cashStateEnum, cashTypeEnum, isShowAdminReports);
        //        //

        //        List<CashTrxVM2> complete_CashTrx_List = new List<CashTrxVM2>();

        //        if (!lst_CashTrxVM2.IsNullOrEmpty())
        //            complete_CashTrx_List = complete_CashTrx_List.Concat(lst_CashTrxVM2).ToList();

        //        if (!lst_BuySellDoc_CashTrx_Allocated.IsNullOrEmpty())
        //            complete_CashTrx_List = complete_CashTrx_List.Concat(lst_BuySellDoc_CashTrx_Allocated).ToList();

        //        if (!lst_BuySellDoc_CashTrx_Available.IsNullOrEmpty())
        //            complete_CashTrx_List = complete_CashTrx_List.Concat(lst_BuySellDoc_CashTrx_Available).ToList();

        //        //complete_CashTrx_List.Concat(lst_Of_CashTrxs_Payments_Available);
        //        //complete_CashTrx_List.Concat(lst_From_BuySellDocs_Payments_Completed);
        //        //complete_CashTrx_List.Concat(lst_From_BuySellDocs_Payments_Allocated);
        //        //complete_CashTrx_List.Concat(lst_From_Encashments_Payments_ALLOCATED);
        //        //complete_CashTrx_List.Concat(lst_From_Encashments_Payments_AVAILABLE);



        //        return complete_CashTrx_List;

        //    }
        //}





        /// <summary>
        /// If personId is null or empty, you will get the total cash of the system.
        /// The cash allocated for payment is different from cash recieved. In Payment,
        /// cash will get allocated at RequestConfirmed. Whereas in Receiving, the cash
        /// will get allocated at SellerConfirms
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cashTypeEnum"></param>
        /// <param name="cashStateEnum"></param>
        /// <returns></returns>
        private List<CashTrxVM2> allTrxFor(string personIdForWhomWeAreWorking, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, bool isShowAdminReports)
        {
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("CashTypeENUM.Unknown");
            if (personIdForWhomWeAreWorking.IsNullOrWhiteSpace())
                return null;
            Person personAcceptingMoneyForSystem = PersonBiz.Find(CurrentUserParameter.PersonAcceptingPaymentForSystemId);
            personAcceptingMoneyForSystem.IsNullThrowException();

            Person personForWhomWeAreWorking = PersonBiz.Find(personIdForWhomWeAreWorking);
            personForWhomWeAreWorking.IsNullThrowException();

            //when I am a seller I want a list of all freight trx where insurance is due or payment is due to me. Sometimes, these will not have completed
            //the transaction.

            List<BuySellDoc> lst_BuySellDocsForPerson = get_lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking(personIdForWhomWeAreWorking);
            List<CashEncashmentTrx> listOfCashEncashmentsForPerson = getListOfCashEncashmentsFor(personIdForWhomWeAreWorking);
            List<CashTrx> lst_Of_CashTrxs_Payments = cashTrx_SQL_For(personIdForWhomWeAreWorking).ToList();
            //List<FreightOfferTrx> lstOfFreightOfferTrx = getFreightOfferTrxListFromBuySellDocs(lst_BuySellDocsForPerson, personIdForWhomWeAreWorking);
            List<PenaltyFlatFile> penaltyTrxFlatFile = convert_PenaltyHeader_And_Trx_To_PenaltyFlatFile(personForWhomWeAreWorking, personAcceptingMoneyForSystem);

            List<CashTrxVM2> cashTrxFromBuySellDoc = convertTo_CashTrx2For_BuySellDocs(
                lst_BuySellDocsForPerson,
                personForWhomWeAreWorking,
                personAcceptingMoneyForSystem);

            List<CashTrxVM2> cashTrxFromEncashment = convertToCashTrxFor(
                listOfCashEncashmentsForPerson,
                personForWhomWeAreWorking,
                personAcceptingMoneyForSystem);


            List<CashTrxVM2> lst_CashTrxVM2 = CashTrxVM2.ConvertCashTrxListToCashVM2List(
                lst_Of_CashTrxs_Payments,
                personIdForWhomWeAreWorking);



            List<CashTrxVM2> lst_CashTrx2FromPenaltyTrxs = convert_ToCashTrx_For_PenaltyFlatTrx(penaltyTrxFlatFile);



            //List<CashTrxVM2> lstFromFreightOfferTrx = convertToCashTrxlistFromFreightOfferTrxList(
            //    lstOfFreightOfferTrx,
            //    personForWhomWeAreWorking,
            //    personAcceptingMoneyForSystem);

            //joining lists
            List<CashTrxVM2> lstCompletePayment = new List<CashTrxVM2>();

            if (!lst_CashTrx2FromPenaltyTrxs.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(lst_CashTrx2FromPenaltyTrxs).ToList();

            if (!lst_CashTrxVM2.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(lst_CashTrxVM2).ToList();

            if (!cashTrxFromBuySellDoc.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(cashTrxFromBuySellDoc).ToList();

            if (!cashTrxFromEncashment.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(cashTrxFromEncashment).ToList();

            //if (!lstFromFreightOfferTrx.IsNullOrEmpty())
            //    lstCompletePayment = lstCompletePayment.Concat(lstFromFreightOfferTrx).ToList();

            //Now filter the cash for cash Type
            List<CashTrxVM2> lstCompletePayment_Filtered_For_CashType = filterForCashType(cashTypeEnum, lstCompletePayment);

            return lstCompletePayment_Filtered_For_CashType;
        }

        private static List<CashTrxVM2> filterForCashType(CashTypeENUM cashTypeEnum, List<CashTrxVM2> lstCompletePayment)
        {
            List<CashTrxVM2> lstCompletePayment_Filtered_For_CashType = new List<CashTrxVM2>();
            switch (cashTypeEnum)
            {
                case CashTypeENUM.Refundable:
                    lstCompletePayment_Filtered_For_CashType = lstCompletePayment.Where(x => x.CashTypeEnum == CashTypeENUM.Refundable).ToList();
                    break;
                case CashTypeENUM.NonRefundable:
                    lstCompletePayment_Filtered_For_CashType = lstCompletePayment.Where(x => x.CashTypeEnum == CashTypeENUM.NonRefundable).ToList();
                    break;
                case CashTypeENUM.Total:
                    lstCompletePayment_Filtered_For_CashType = lstCompletePayment;
                    break;
                case CashTypeENUM.Unknown:
                default:
                    throw new Exception("Cash type Unknown! Tell your administrator.");
            }
            return lstCompletePayment_Filtered_For_CashType;
        }

        private List<CashTrxVM2> convert_ToCashTrx_For_PenaltyFlatTrx(List<PenaltyFlatFile> penaltyTrxFlatFile)
        {
            List<CashTrxVM2> cashTrxList = new List<CashTrxVM2>();

            if (penaltyTrxFlatFile.IsNullOrEmpty())
                return null;

            foreach (PenaltyFlatFile pf in penaltyTrxFlatFile)
            {
                CashTrxVM2 csh = pf.ConvertTo_CashTrxVM2();
                cashTrxList.Add(csh);
            }
            return cashTrxList;
        }


        private List<PenaltyFlatFile> convert_PenaltyHeader_And_Trx_To_PenaltyFlatFile(Person personForWhomWeAreWorking, Person systemPerson)
        {
            List<PenaltyHeader> lstOfPenaltyHeaders = getPenaltyHeadersFor(personForWhomWeAreWorking);
            List<PenaltyTrx> lstOfPenaltyTrxs = getPenaltyTransactionsFor(personForWhomWeAreWorking);

            List<PenaltyFlatFile> fromHdr = convert_PenaltyHeader_To_PenaltyFlatFile(lstOfPenaltyHeaders, systemPerson);
            List<PenaltyFlatFile> fromTrx = convert_PenaltyTrx_To_PenaltyFlatFile(lstOfPenaltyTrxs);

            if (fromHdr.IsNullOrEmpty())
            {
                if (fromTrx.IsNullOrEmpty())
                    return null;
                else
                    return fromTrx;
            }
            else
            {
                if (fromTrx.IsNullOrEmpty())
                {
                    return fromHdr;
                }
                else
                {
                    List<PenaltyFlatFile> concatedList = fromHdr.Concat(fromTrx).ToList();
                    return concatedList;
                }
            }
        }
        private List<PenaltyFlatFile> convert_PenaltyHeader_To_PenaltyFlatFile(List<PenaltyHeader> lstOfPenaltyHeaders, Person toSystemPerson)
        {
            if (lstOfPenaltyHeaders.IsNullOrEmpty())
                return null;
            List<PenaltyFlatFile> listPenaltyFlatFile = new List<PenaltyFlatFile>();
            foreach (PenaltyHeader ph in lstOfPenaltyHeaders)
            {
                if (ph.PenaltyTrxs_Fixed.IsNullOrEmpty())
                    continue;

                PenaltyFlatFile pff = new PenaltyFlatFile(
                    ph.Id,
                    ph.MetaData.Created.Date_NotNull_Min,
                    ph.Amount,
                    0,
                    ph.Comment,
                    "",
                    ph.FromPerson,
                    toSystemPerson,
                    ph.Name,
                    ph.BuySellDocStateEnum,
                    ph.BuySellDocumentTypeEnum,
                    ph.BuySellDocStateModifierEnum);

                listPenaltyFlatFile.Add(pff);
            }
            return listPenaltyFlatFile;
        }


        private List<PenaltyFlatFile> convert_PenaltyTrx_To_PenaltyFlatFile(List<PenaltyTrx> lstOfPenaltyTrxs)
        {
            if (lstOfPenaltyTrxs.IsNullOrEmpty())
                return null;
            List<PenaltyFlatFile> listPenaltyFlatFile = new List<PenaltyFlatFile>();
            foreach (PenaltyTrx pt in lstOfPenaltyTrxs)
            {
                string name = pt.PenaltyHeader.IsNull() ? "Error" : pt.PenaltyHeader.Name;
                PenaltyFlatFile pff = new PenaltyFlatFile(
                    pt.Id,
                    pt.MetaData.Created.Date_NotNull_Min,
                    0,
                    pt.Amount,
                    pt.PenaltyHeader.Comment,
                    pt.Comment,
                    pt.PenaltyHeader.FromPerson,
                    pt.Person,
                    name,
                    pt.PenaltyHeader.BuySellDocStateEnum,
                    pt.PenaltyHeader.BuySellDocumentTypeEnum,
                    pt.PenaltyHeader.BuySellDocStateModifierEnum);
                listPenaltyFlatFile.Add(pff);
            }
            return listPenaltyFlatFile;
        }


        /// <summary>
        /// These are all the payment tranactions for the penalties
        /// </summary>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <returns></returns>
        private List<PenaltyHeader> getPenaltyHeadersFor(Person personForWhomWeAreWorking)
        {
            List<PenaltyHeader> penaltyHeaders = PenaltyHeaderBiz.FindAll().Where(x => x.FromPersonId == personForWhomWeAreWorking.Id).ToList();
            return penaltyHeaders;
        }

        /// <summary>
        /// These are all the received payments
        /// </summary>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <returns></returns>
        private List<PenaltyTrx> getPenaltyTransactionsFor(Person personForWhomWeAreWorking)
        {
            List<PenaltyTrx> penaltyTrxs = PenaltyTrxBiz.FindAll().Where(x => x.PersonId == personForWhomWeAreWorking.Id).ToList();
            return penaltyTrxs;
        }

        /// <summary>
        /// Convert the FreightOfferTrxs to cash Entries.
        /// Keep provision for 
        ///     Insurance
        /// Guarantee
        /// 
        /// </summary>
        /// <param name="lstOfFreightOfferTrx"></param>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <param name="personForSystem"></param>
        /// <returns></returns>
        private List<CashTrxVM2> convertToCashTrxlistFromFreightOfferTrxList(
            List<FreightOfferTrx> lstOfFreightOfferTrx,
            Person personForWhomWeAreWorking,
            Person personForSystem)
        {


            if (lstOfFreightOfferTrx.IsNullOrEmpty())
                return null;

            List<CashTrxVM2> allCashTrx = new List<CashTrxVM2>();
            foreach (FreightOfferTrx fot in lstOfFreightOfferTrx)
            {
                //get seller/owner trx
                List<CashTrxVM2> cashTrx = get_Owners_Insurance_Guarantee_TrxFromFreightOfferTrx(fot, personForWhomWeAreWorking, personForSystem);
                if (!cashTrx.IsNullOrEmpty())
                {
                    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
                }


                //get customerSalesManCommission
                cashTrx = get_CustomerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personForWhomWeAreWorking, personForSystem);

                if (!cashTrx.IsNullOrEmpty())
                {
                    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
                }


                ////get customerSalesManCommission
                //cashTrx = get_CustomerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personIdForWhomWeAreWorking);
                //if (!cashTrx.IsNullOrEmpty())
                //{
                //    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
                //}

                ////get ownerSalesManCommission
                //cashTrx = get_OwnerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personIdForWhomWeAreWorking);
                //if (!cashTrx.IsNullOrEmpty())
                //{
                //    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
                //}

                ////get the deliverySalesmanCommission
                //cashTrx = get_DeliverymanSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personIdForWhomWeAreWorking);
                //if (!cashTrx.IsNullOrEmpty())
                //{
                //    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
                //}



            }

            return allCashTrx;
        }

        private List<CashTrxVM2> get_DeliverymanSalesMan_Insurance_Guarantee_FromFreightOfferTrx(List<FreightOfferTrx> lstOfFreightOfferTrx, string personIdForWhomWeAreWorking)
        {
            throw new NotImplementedException();
        }

        private List<CashTrxVM2> get_OwnerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(FreightOfferTrx fot, Person personIdForWhomWeAreWorking, Person personForSystem)
        {
            fot.BuySellDoc.IsNullThrowException();

            if (fot.IsNull())
                return null;

            List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

            Customer customer;
            if (IsCustomer(personIdForWhomWeAreWorking.Id, out customer))
            {
                if (fot.BuySellDoc.CustomerId == customer.Id)
                {
                    BuySellDoc bsd = fot.BuySellDoc;

                    //the person is an customer in this buySellDoc -for insurance
                    decimal insuranaceReceived = 0;

                    //insurance payable by deliveryman
                    Person deliverymanPerson = DeliverymanBiz.GetPersonForPlayer(fot.DeliverymanId);
                    deliverymanPerson.IsNullThrowException();

                    if (fot.IsPayInsurance)
                    {
                        insuranaceReceived = fot.InsuranceAmount;
                    }

                    if (insuranaceReceived != 0)
                    {

                        if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
                        {
                            decimal commissionAmount =
                                insuranaceReceived * bsd.CustomerSalesmanCommission.Percent;

                            string commentFixed = string.Format("Commission on Insurance. {0} {1} {2}",
                                bsd.CustomerSalesmanCommission.Percent,
                                bsd.FullName(),
                                bsd.Comment);

                            CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                    bsd,
                                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                                    0,
                                    commissionAmount,
                                    commentFixed,
                                    personIdForWhomWeAreWorking,
                                    personForSystem,
                                    CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance,
                                    bsd.IsCashAvailableTo_Customer());

                            lstCashTrx.Add(cashTrx);
                        }
                    }
                }
            }
            return lstCashTrx;
        }

        private List<CashTrxVM2> get_CustomerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(FreightOfferTrx fot, Person personIdForWhomWeAreWorking, Person personForSystem)
        {
            fot.BuySellDoc.IsNullThrowException();


            List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

            Salesman salesman;
            if (IsSalesman(personIdForWhomWeAreWorking.Id, out salesman))
            {
                BuySellDoc bsd = fot.BuySellDoc;

                if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
                {
                    if (fot.BuySellDoc.CustomerSalesmanId == salesman.Id)
                    {

                        //the person is a customersalesman in this buySellDoc -for insurance
                        decimal insuranaceReceived = 0;
                        insuranaceReceived = fot.InsuranceAmount;

                        //if product has been delivered and Pay insurance is false...
                        if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                        {
                            if (!fot.IsPayInsurance)
                            {
                                insuranaceReceived = 0;
                            }
                        }

                        if (insuranaceReceived != 0)
                        {
                            decimal commissionAmount =
                                insuranaceReceived * bsd.CustomerSalesmanCommission.Percent;

                            string commentFixed =
                                string.Format("Commission on Insurance. {0} {1} {2}",
                                bsd.CustomerSalesmanCommission.Percent,
                                bsd.FullName(),
                                bsd.Comment);

                            CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                    bsd,
                                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                                    0,
                                    commissionAmount,
                                    commentFixed,
                                    personIdForWhomWeAreWorking,
                                    personForSystem,
                                    CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance, 
                                    bsd.IsCashAvailableTo_Owner());

                            lstCashTrx.Add(cashTrx);
                        }
                    }
                }
            }
            return lstCashTrx;
        }

        private List<CashTrxVM2> get_Owners_Insurance_Guarantee_TrxFromFreightOfferTrx(
            FreightOfferTrx fot,
            Person personForWhomWeAreWorking,
            Person personForSystem)
        {
            fot.BuySellDoc.IsNullThrowException();

            if (fot.IsNull())
                return null;

            List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

            Owner owner;
            if (IsOwner(personForWhomWeAreWorking.Id, out owner))
            {
                if (fot.BuySellDoc.OwnerId == owner.Id)
                {
                    BuySellDoc bsd = fot.BuySellDoc;

                    //the person is an owner in this buySellDoc -for insurance
                    //insurance payable by deliveryman

                    Person deliverymanPerson = DeliverymanBiz.GetPersonForPlayer(fot.DeliverymanId);
                    deliverymanPerson.IsNullThrowException();

                    create_Guarantee_Entry_From_FreightTrx_For_Owner(fot, personForWhomWeAreWorking, personForSystem, lstCashTrx, bsd, deliverymanPerson);
                    create_Insurance_Entry_From_FreightTrx_For_Owner(fot, personForWhomWeAreWorking, personForSystem, lstCashTrx, bsd, deliverymanPerson);
                }
            }
            return lstCashTrx;
        }

        /// <summary>
        /// The deliveryman belongs to the FreightTrx. The owner is paid the full amount if the deliveryman cancels after
        /// agreeing pickup
        /// This guarantee entry is not required. This will be taken care of through cancelations
        /// </summary>
        /// <param name="fot"></param>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <param name="personForSystem"></param>
        /// <param name="lstCashTrx"></param>
        /// <param name="bsd"></param>
        /// <param name="deliverymanPerson"></param>
        private void create_Guarantee_Entry_From_FreightTrx_For_Owner(FreightOfferTrx fot, Person personForWhomWeAreWorking, Person personForSystem, List<CashTrxVM2> lstCashTrx, BuySellDoc bsd, Person deliverymanPerson)
        {

            decimal guaranteeAmount = 0;
            string amountPaidComment = string.Format("payable only if Deliveryman does not deliver (Penalty {0}%), or delivers late/early (Penalty {1}%)",
                GetPenaltyPercentageForDeliverymanQuitting(),
                GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly());


            guaranteeAmount = calculateDeliverymanGuarantee(fot, bsd, guaranteeAmount);

            if (guaranteeAmount == 0)
                return;

            //guarantee is always payable

            string commentFixed = string.Format("{0} {1} {2}",
                amountPaidComment,
                bsd.FullName(),
                bsd.Comment);

            CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                    bsd,
                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                    0,
                    guaranteeAmount,
                    commentFixed,
                    deliverymanPerson,
                    personForWhomWeAreWorking,
                    CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,
                    bsd.IsCashAvailableTo_Owner());

            lstCashTrx.Add(cashTrx);

            //we have to put this otherwise, the amount is deducted from the owner
            //and there might be complaints later.... ie they have not received the 
            //amount and this is already deducted.
            if (bsd.BuySellDocStateEnum != BuySellDocStateENUM.Delivered)
                return;

            //The above total amount does not belong to the owner completely. Some is Commission and system charges
            //those are taken once bsd is delivered.

            decimal maxCommissionForInsurance =
                guaranteeAmount * bsd.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent();

            cashTrx = create_CashTrxVM2_From_BuySellDoc(
                    bsd,
                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                    maxCommissionForInsurance,
                    0,
                    "System charges on Insurance due. " + bsd.FullName() + bsd.Comment,
                    personForWhomWeAreWorking,
                    personForSystem,
                    CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,
                    bsd.IsCashAvailableTo_Owner());

            lstCashTrx.Add(cashTrx);

        }


        /// <summary>
        /// In case the deliveryman damages or loses the cargo.
        /// </summary>
        /// <param name="fot"></param>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <param name="personForSystem"></param>
        /// <param name="lstCashTrx"></param>
        /// <param name="bsd"></param>
        /// <param name="deliverymanPerson"></param>
        private void create_Insurance_Entry_From_FreightTrx_For_Owner(FreightOfferTrx fot, Person personForWhomWeAreWorking, Person personForSystem, List<CashTrxVM2> lstCashTrx, BuySellDoc bsd, Person deliverymanPerson)
        {
            //this assumes that the personForWhomWeAreWorking is the owner in the bsd
            decimal insuranceAmount = 0;
            string amountPaidComment = "Only payable by Deliveryman if goods damaged/lost during delivery.";


            insuranceAmount = fot.InsuranceAmount;

            if (insuranceAmount == 0)
                return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                if (!fot.IsPayInsurance)
                    return;

            string commentFixed = string.Format("{0} {1} {2}",
                amountPaidComment,
                bsd.FullName(),
                bsd.Comment);

            CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                    bsd,
                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                    0,
                    insuranceAmount,
                    commentFixed,
                    deliverymanPerson,
                    personForWhomWeAreWorking,
                    CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance, bsd.IsCashAvailableTo_Owner());

            lstCashTrx.Add(cashTrx);

            //we have to put this otherwise, the amount is deducted from the owner
            //and there might be complaints later.... ie they have not received the 
            //amount and this is already deducted.
            if (bsd.BuySellDocStateEnum != BuySellDocStateENUM.Delivered)
                return;

            //The above total amount does not belong to the owner completely. Some is Commission and system charges
            //those are taken once bsd is delivered.

            decimal maxCommissionForInsurance =
                insuranceAmount * bsd.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent();

            cashTrx = create_CashTrxVM2_From_BuySellDoc(
                    bsd,
                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                    maxCommissionForInsurance,
                    0,
                    "System charges on Insurance due. " + bsd.FullName() + bsd.Comment,
                    personForWhomWeAreWorking,
                    personForSystem,
                    CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,bsd.IsCashAvailableTo_Owner());

            lstCashTrx.Add(cashTrx);
        }



        /// <summary>
        /// This checks the buySellDoc state to see if guarantee is applicable. If it is applicable, the guartee amount becomes
        /// the freight amount. If it is with the courier, this amount is returned. If it has been delivered then the algorithem
        /// checks to make sure that it was 
        ///     Delivered on time
        ///     Delivered by this courier. It can be he never delivered, and someone else delivered in his place.
        /// if delivered on time and the deliveryman is the deliveryman on the buyselldoc
        ///     the amount becomes zero.
        ///  otheriwse
        ///     if it was delivered late or early, by the deliveryman on the buyselldoc, then a penelty of half freight or wais charged.
        /// </summary>
        /// <param name="fot"></param>
        /// <param name="bsd"></param>
        /// <param name="guaranteeAmount"></param>
        /// <returns></returns>
        private decimal calculateDeliverymanGuarantee(FreightOfferTrx fot, BuySellDoc bsd, decimal guaranteeAmount)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup)
                return 0;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return 0;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
                return 0;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return 0;

            guaranteeAmount = bsd.Freight_Accepted;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
            {
                //if courier is on time do not make any entry, otherwise make entry
                DateParameter dp = new DateParameter();
                dp.BeginDate = bsd.AgreedPickupDateByDeliveryman;
                dp.EndDate = bsd.OrderDelivered.Date_NotNull_Min;
                bool isCourierOnTime = dp.BeginDateAndEndDateAreEqual;
                bool isThisTheCourierWhoDelivered = bsd.DeliverymanId == fot.DeliverymanId;

                if (isCourierOnTime && isThisTheCourierWhoDelivered)
                {
                    guaranteeAmount = 0;
                }
                else
                {
                    //courier is not on time. Either he is early, or he is late.
                    //regardless... cut half. He must do as promised.
                    if (isThisTheCourierWhoDelivered)
                    {
                        decimal percentPenalty = GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly();

                        if (percentPenalty == 0)
                            return 0;

                        //cut half. He was not on time
                        guaranteeAmount = guaranteeAmount * percentPenalty / 100;
                    }
                    else
                    {
                        //someone else deliverd.
                        //cut all
                        decimal percentPenalty = GetPenaltyPercentageForDeliverymanQuitting();

                        if (percentPenalty == 0)
                            return 0;

                        //cut half. He was not on time
                        guaranteeAmount = guaranteeAmount * percentPenalty / 100;
                    }

                }
            }
            return guaranteeAmount;
        }



        //private decimal create_Insusrance_Entry_From_FreightTrx(FreightOfferTrx fot, Person personForWhomWeAreWorking, Person personForSystem, List<CashTrxVM2> lstCashTrx, BuySellDoc bsd, Person deliverymanPerson)
        //{
        //    decimal insuranaceReceived = 0;

        //    if (fot.IsPayInsurance)
        //    {
        //        insuranaceReceived = fot.InsuranceAmount;
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            0,
        //            insuranaceReceived,
        //            "Insurance due. " + bsd.FullName() + bsd.Comment,
        //            deliverymanPerson,
        //            personForWhomWeAreWorking,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance);

        //    lstCashTrx.Add(cashTrx);

        //    decimal maxCommissionForInsurance =
        //        insuranaceReceived * bsd.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent();

        //    cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            0,
        //            maxCommissionForInsurance,
        //            "System charges on Insurance due. " + bsd.FullName() + bsd.Comment,
        //            personForWhomWeAreWorking,
        //            personForSystem,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance);

        //    lstCashTrx.Add(cashTrx);

        //    return insuranaceReceived;
        //}



        //this returns all the freight trxs for.
        private List<FreightOfferTrx> getFreightOfferTrxListFromBuySellDocs(List<BuySellDoc> lstOfBuySellDocsFor, string personIdForWhomWeAreWorking)
        {
            if (lstOfBuySellDocsFor.IsNullOrEmpty())
                return null;

            List<FreightOfferTrx> allFrtTrx = new List<FreightOfferTrx>();

            //now get all frtTrx
            foreach (BuySellDoc bds in lstOfBuySellDocsFor)
            {
                if (bds.FreightOfferTrxs.IsNull())
                    continue;

                List<FreightOfferTrx> bdsFrtTrx = bds.FreightOfferTrxs.ToList();
                if (bdsFrtTrx.IsNullOrEmpty())
                    continue;

                allFrtTrx = allFrtTrx.Concat(bdsFrtTrx)
                    .ToList();

            }
            if (allFrtTrx.IsNull())
                return null;

            List<FreightOfferTrx> allFrtTrxDistinct = new HashSet<FreightOfferTrx>(allFrtTrx).ToList();
            return allFrtTrxDistinct;
        }


        private List<CashTrxVM2> convertTo_CashTrx2For_BuySellDocs(
            List<BuySellDoc> lst_BuySellDocsForPerson,
            Person personWeAreWorkingFor,
            Person personForSystem)
        {
            List<CashTrxVM2> lstOfCashTrxVm2 = new List<CashTrxVM2>();

            if (lst_BuySellDocsForPerson.IsNullOrEmpty())
            {

            }
            else
            {
                foreach (BuySellDoc bsd in lst_BuySellDocsForPerson)
                {
                    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                        continue;

                    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                        continue;

                    Person bsdCustomerPerson = CustomerBiz.GetPersonForPlayer(bsd.CustomerId);
                    Person bsdOwnerPerson = OwnerBiz.GetPersonForPlayer(bsd.OwnerId);
                    Person bsdDeliverymanPerson = get_Deliveryman_Person(bsd);
                    Person bsdCustomerSalesmanPerson = get_CustomerSalesman_Person(bsd);
                    Person bsdDeliverymanSalesmanPerson = get_DeliverymanSalesman_Person(bsd);
                    Person bsdOwnerSalesmanPerson = get_OwnerSalesman_Person(bsd);

                    //this one is allowed so that the money of the customer becomes allocated.
                    get_Customer_CashTrxVM2(
                        personWeAreWorkingFor,
                        lstOfCashTrxVm2,
                        bsd,
                        bsdCustomerPerson,
                        bsdOwnerPerson);

                    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
                        continue;


                    get_Owner_CashTrxVM2(
                        personWeAreWorkingFor,
                        lstOfCashTrxVm2,
                        bsd,
                        bsdCustomerPerson,
                        bsdOwnerPerson,
                        bsdDeliverymanPerson);

                    //I am planning to get this from all the freightTrx
                    get_Deliveryman_CashTrxVM2(
                        personWeAreWorkingFor,
                        personForSystem,
                        lstOfCashTrxVm2,
                        bsd,
                        bsdDeliverymanPerson,
                        bsdOwnerPerson);

                    get_Salesmen_CashTrxVM2(
                        personWeAreWorkingFor,
                        personForSystem,
                        lstOfCashTrxVm2,
                        bsd,
                        bsdCustomerSalesmanPerson,
                        bsdDeliverymanSalesmanPerson,
                        bsdOwnerSalesmanPerson);

                    get_System_CashTrxVM2(
                        personWeAreWorkingFor,
                        lstOfCashTrxVm2,
                        bsd,
                        bsdOwnerPerson,
                        personForSystem);


                }
            }


            return lstOfCashTrxVm2;
        }

        private void get_System_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdOwnerPerson, Person bsdSystemPerson)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return;
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
            //    return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;




            Owner owner;
            if (IsOwner(personWeAreWorkingFor.Id, out owner))
            {
                if (!bsdOwnerPerson.IsNull())
                {
                    if (personWeAreWorkingFor.Id == bsdOwnerPerson.Id)
                    {
                        string commentFixed = string.Format("{0} {1}",
                            "System Charges",
                            bsd.Comment);

                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.OrderConfirmedByCustomer.Date_NotNull_Min,
                            bsd.Total_Charged_To_Owner.Amount,
                            0,
                            commentFixed,
                            bsdOwnerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.SaleOrder,
                            bsd.IsCashAvailableTo_Owner());

                        lstOfCashTrxVm2.Add(cashTrx);
                    }
                }

            }
        }

        private Person get_OwnerSalesman_Person(BuySellDoc bsd)
        {
            Person bsdOwnerSalesmanPerson = null;
            if (!bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsdOwnerSalesmanPerson = SalesmanBiz.GetPersonForPlayer(bsd.OwnerSalesmanId);
            }
            return bsdOwnerSalesmanPerson;
        }

        private Person get_DeliverymanSalesman_Person(BuySellDoc bsd)
        {
            Person bsdDeliverymanSalesmanPerson = null;
            if (!bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsdDeliverymanSalesmanPerson = SalesmanBiz.GetPersonForPlayer(bsd.DeliverymanSalesmanId);
            }
            return bsdDeliverymanSalesmanPerson;
        }

        private Person get_CustomerSalesman_Person(BuySellDoc bsd)
        {
            Person bsdCustomerSalesmanPerson = null;
            if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsdCustomerSalesmanPerson = SalesmanBiz.GetPersonForPlayer(bsd.CustomerSalesmanId);
            }
            return bsdCustomerSalesmanPerson;
        }

        private Person get_Deliveryman_Person(BuySellDoc bsd)
        {
            Person bsdDeliverymanPerson = null;
            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
                bsdDeliverymanPerson = DeliverymanBiz.GetPersonForPlayer(bsd.DeliverymanId);
            return bsdDeliverymanPerson;
        }

        private void get_Salesmen_CashTrxVM2(Person personWeAreWorkingFor, Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerSalesmanPerson, Person bsdDeliverymanSalesmanPerson, Person bsdOwnerSalesmanPerson)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            Salesman salesman;
            if (IsSalesman(personWeAreWorkingFor.Id, out salesman))
            {
                get_CustomerSalesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdCustomerSalesmanPerson, salesman);
                get_OwnerSalesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdOwnerSalesmanPerson, salesman);
                get_DeliverymanSalesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdDeliverymanSalesmanPerson, salesman);
            }
        }

        private void get_DeliverymanSalesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdDeliverymanSalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.DeliverymanSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                                0,
                                bsd.DeliverymanSalesmanCommission.Amount,
                                "Percent: " + bsd.DeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliverymanSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SalesmanDeliverymanCommission,bsd.IsCashAvailableTo_Owner());

                    lstOfCashTrxVm2.Add(cashTrx);
                }
            }
        }

        private void get_OwnerSalesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdOwnerSalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.OwnerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.OrderConfirmedByCustomer.Date_NotNull_Min,
                                0,
                                bsd.OwnerSalesmanCommission.Amount,
                                "Percent: " + bsd.OwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwnerSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SalesmanOwnerCommission,
                                bsd.IsCashAvailableTo_Owner());

                    lstOfCashTrxVm2.Add(cashTrx);
                }
            }
        }

        private void get_CustomerSalesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerSalesmanPerson, Salesman salesman)
        {
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
            //    return;
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
            //    return;
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.CustomerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.OrderConfirmedByCustomer.Date_NotNull_Min,
                                0,
                                bsd.CustomerSalesmanCommission.Amount,
                                "Percent: " + bsd.CustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomerSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SalesmanCustomerCommission,
                                bsd.IsCashAvailableTo_Owner());

                    lstOfCashTrxVm2.Add(cashTrx);
                }
            }
        }

        private void get_Deliveryman_CashTrxVM2(Person personWeAreWorkingFor, Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdDeliverymanPerson, Person bsdOwnerPerson)
        {
            Deliveryman deliveryman;
            if (IsDeliveryman(personWeAreWorkingFor.Id, out deliveryman))
            {
                //there is a deliveryman in the doc
                if (!bsdDeliverymanPerson.IsNull())
                {
                    //the current person we are working for is the deliveryman
                    if (personWeAreWorkingFor.Id == bsdDeliverymanPerson.Id)
                    {
                        //the person we are working for is the deliveryman in the current bsd.
                        //which means that delivery has been accepted by the buyer.

                        //create a cashTrx if accepted by seller
                        string commentFixed = string.Format("{0}", bsd.FullName(), bsd.Comment);

                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                    bsd,
                                    bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                                    0,
                                    bsd.Freight_Accepted,
                                    commentFixed,
                                    personForSystem,
                                    bsdDeliverymanPerson,
                                    CashTrxVmDocumentTypeENUM.DeliveryOrderCharges,bsd.IsCashAvailableTo_Deliveryman());

                        lstOfCashTrxVm2.Add(cashTrx);


                        //also block the same amount of freight in case of failure
                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                        bsd,
                        bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                        bsd.Freight_Accepted,
                        0,
                        commentFixed,
                        bsdDeliverymanPerson,
                        personForSystem,
                        CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,
                        bsd.IsCashAvailableTo_Deliveryman());

                        lstOfCashTrxVm2.Add(cashTrx);

                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                        bsd,
                        bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                        bsd.InsuranceRequired,
                        0,
                        commentFixed,
                        bsdDeliverymanPerson,
                        personForSystem,
                        CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance,
                        bsd.IsCashAvailableTo_Deliveryman());

                        lstOfCashTrxVm2.Add(cashTrx);
                        //now locate all the freight trxs for the current buysellOrder
                        //there might be some canceled ones in which insurance or
                        //guarantee is due.

                        //List<FreightOfferTrx> list_FreightOfferTrx_For_BuySellOrder = bsd.FreightOfferTrxs.ToList();
                        //if (list_FreightOfferTrx_For_BuySellOrder.IsNullOrEmpty())
                        //    return;


                        ////if deliveryman has a insurance... allocate that money
                        //cashTrx = create_CashTrxVM2_From_BuySellDoc(
                        //            bsd,
                        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                        //            bsd.InsuranceRequired,
                        //            bsd.InsuranceRequired,
                        //            bsd.Comment + "Payable upon loss or damage to cargo.",
                        //            bsdDeliverymanPerson,
                        //            bsdOwnerPerson,
                        //            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance);

                        //lstOfCashTrxVm2.Add(cashTrx);

                        ////pickup guarantee today
                        //cashTrx = create_CashTrxVM2_From_BuySellDoc(
                        //            bsd,
                        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                        //            bsd.Freight_Accepted,
                        //            bsd.Freight_Accepted,
                        //            bsd.Comment + "Payable if pickup window is missed.",
                        //            bsdDeliverymanPerson,
                        //            bsdOwnerPerson,
                        //            CashTrxVmDocumentTypeENUM.DeliveryOrderPickupGuarantee);

                        //lstOfCashTrxVm2.Add(cashTrx);


                    }
                }
            }
        }

        private void get_Owner_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerPerson, Person bsdOwnerPerson, Person bsdDeliverymanPerson)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;




            Owner owner;
            if (IsOwner(personWeAreWorkingFor.Id, out owner))
            {
                if (!bsdOwnerPerson.IsNull())
                {
                    if (personWeAreWorkingFor.Id == bsdOwnerPerson.Id)
                    {
                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.OrderConfirmedByCustomer.Date_NotNull_Min,
                            0,
                            bsd.TotalInvoice,
                            bsd.Comment,
                            bsdCustomerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.SaleOrder, bsd.IsCashAvailableTo_Owner());

                        lstOfCashTrxVm2.Add(cashTrx);
                    }
                }

                //if (!bsdDeliverymanPerson.IsNull())
                //{
                //    //add insurance in case
                //    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                //        bsd,
                //        bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                //        bsd.Freight_Accepted,
                //        bsd.Freight_Accepted,
                //        bsd.Comment,
                //        bsdDeliverymanPerson,
                //        bsdOwnerPerson,
                //        CashTrxVmDocumentTypeENUM.DeliveryOrderPickupGuarantee);

                //    lstOfCashTrxVm2.Add(cashTrx);

                //    //add guarantee
                //    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                //        bsd,
                //        bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
                //        bsd.Freight_Accepted,
                //        bsd.Freight_Accepted,
                //        bsd.Comment,
                //        bsdDeliverymanPerson,
                //        bsdOwnerPerson,
                //        CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance);

                //    lstOfCashTrxVm2.Add(cashTrx);
                //}
            }



        }

        private void get_Customer_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerPerson, Person bsdOwnerPerson)
        {


            Customer customer;
            if (IsCustomer(personWeAreWorkingFor.Id, out customer))
            {
                if (!bsdCustomerPerson.IsNull())
                {
                    if (personWeAreWorkingFor.Id == bsdCustomerPerson.Id)
                    {
                        //create a trx for customer salesman
                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                                        bsd,
                                                        bsd.OrderConfirmedByCustomer.Date_NotNull_Min,
                                                        bsd.TotalInvoice,
                                                        0,
                                                        bsd.Comment,
                                                        bsdCustomerPerson,
                                                        bsdOwnerPerson,
                                                        CashTrxVmDocumentTypeENUM.PurchaseOrder,
                                                        bsd.IsCashAvailableTo_Customer());

                        lstOfCashTrxVm2.Add(cashTrx);
                    }
                }
            }
        }



        private List<CashTrxVM2> convertToCashTrxFor(
           List<CashEncashmentTrx> lst_CashEncashments,
           Person personWeAreWorkingFor,
           Person personForSystem)
        {
            List<CashTrxVM2> lstOfCashTrxVm2 = new List<CashTrxVM2>();

            if (lst_CashEncashments.IsNullOrEmpty())
                return lstOfCashTrxVm2;

            Person fakePerson = new Person();
            fakePerson.Name = "Bank";
            foreach (CashEncashmentTrx cet in lst_CashEncashments)
            {
                if (cet.PersonRequestingPaymentId == personWeAreWorkingFor.Id)
                {
                    string comment = "";
                    CashTrxVM2 cashTrx = cet.ToCashTrxVM2(comment, fakePerson);
                    lstOfCashTrxVm2.Add(cashTrx);
                }
            }

            return lstOfCashTrxVm2;


        }



        private CashTrxVM2 create_CashTrxVM2_From_BuySellDoc(BuySellDoc bsd, DateTime date, decimal paymentAmount, decimal receiptAmount, string comment, Person fromPerson, Person toPerson, CashTrxVmDocumentTypeENUM cashTrxVmDocumentTypeENUM, bool isCashAvailable)
        {
            CashStateENUM CashStateEnum = isCashAvailable ? CashStateENUM.Available : CashStateENUM.Allocated;
            //if from person is owner or customer or deliveryman.... the cash available status will be different
            CashTrxVM2 cashTrx = new CashTrxVM2(
                        bsd.Id,
                        date,                                  //*
                        paymentAmount,                         //*
                        receiptAmount,                         //*
                        comment,                               //*
                        fromPerson,                            //*
                        toPerson,                              //*
                        CashTypeENUM.Refundable,
                        CashStateEnum,
                        cashTrxVmDocumentTypeENUM,             //*
                        bsd.Name,
                        bsd.BuySellDocStateEnum);

            return cashTrx;

        }
        //private List<BuySellDoc> getLst_BuySellDocsFor(string personIdForWhomWeAreWorking)
        //{
        //    //get a list of all buySellDocs in which the personIdForWhomWeAreWorking is either a customer, seller, deliveryman, salesmanCustomer, salesmanSeller
        //    List<BuySellDoc> lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking = get_lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking(personIdForWhomWeAreWorking);
        //    ////now mark the cash availabl or not in these documents
        //    //List<BuySellDoc> lst_Buyselldocs_FilteredForPersonIdForWhomWeAreWorking_CashMarkedAvailable = markCashAvailableIn(lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking);
        //    return lst_Buyselldocs_FilteredForPersonIdForWhomWeAreWorking_CashMarkedAvailable;
        //}

        private List<CashEncashmentTrx> getListOfCashEncashmentsFor(string personIdForWhomWeAreWorking)
        {
            personIdForWhomWeAreWorking.IsNullOrWhiteSpaceThrowException();

            List<CashEncashmentTrx> lstCashEncashments = CashEncashmentTrxBiz
                .FindAll()
                .Where(x => x.PersonRequestingPaymentId == personIdForWhomWeAreWorking)
                .ToList();

            return lstCashEncashments;

        }

        private List<BuySellDoc> get_lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking(string personIdForWhomWeAreWorking)
        {
            //is the person a customer...
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsACustomer = getAllBuySellDocsInWhichPersonIsACustomer(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsASeller = getAllBuySellDocsInWhichPersonIsASeller(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsADeliveryman = getAllBuySellDocsInWhichPersonIsADeliveryman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsACustomerSalesman = getAllBuySellDocsInWhichPersonIsACustomerSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsASellerSalesman = getAllBuySellDocsInWhichPersonIsASellerSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsADeliverymanSalesman = getAllBuySellDocsInWhichPersonIsADeliverymanSalesman(personIdForWhomWeAreWorking);


            List<BuySellDoc> concatedList = new List<BuySellDoc>();


            if (!lstOfBuySellDocsWherePersonIsASeller.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsASeller).ToList();

            if (!lstOfBuySellDocsWherePersonIsADeliveryman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsADeliveryman).ToList();

            if (!lstOfBuySellDocsWherePersonIsACustomerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsACustomerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsASellerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsASellerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsADeliverymanSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsADeliverymanSalesman).ToList();

            //The buy sell docs aboove this need to have their freighttrxs checked for Insurance/Guarantee so
            //they can be recovered and commissions paid.

            if (!lstOfBuySellDocsWherePersonIsACustomer.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsACustomer).ToList();

            //Remove duplicates
            HashSet<BuySellDoc> hashsetBuySellDoc = new HashSet<BuySellDoc>(concatedList);
            return hashsetBuySellDoc.ToList();
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsADeliverymanSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.DeliverymanSalesmanId == salesman.Id).ToList();
                return lst;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsASellerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.OwnerSalesmanId == salesman.Id).ToList();
                return lst;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsACustomerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.CustomerSalesmanId == salesman.Id).ToList();
                return lst;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsADeliveryman(string personIdForWhomWeAreWorking)
        {
            Deliveryman deliveryman = DeliverymanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!deliveryman.IsNull())
            {
                List<BuySellDoc> allBuySellDocs = BuySellDocBiz.FindAll().ToList();

                if (allBuySellDocs.IsNullOrEmpty())
                    return null;

                List<BuySellDoc> selectedDocs = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in allBuySellDocs)
                {
                    if (bsd.DeliverymanId == deliveryman.Id)
                    {
                        selectedDocs.Add(bsd);
                    }
                }

                return selectedDocs;
            }
            return null;
        }
        private List<FreightOfferTrx> getAllFreightOfferTrxForSellerFrom(List<BuySellDoc> allBuySellDocsInWhichPersonIsASeller, string personIdForWhomWeAreWorking)
        {
            List<FreightOfferTrx> fots = new List<FreightOfferTrx>();
            if (allBuySellDocsInWhichPersonIsASeller.IsNullOrEmpty())
                return fots;

            foreach (BuySellDoc bds in allBuySellDocsInWhichPersonIsASeller)
            {
                List<FreightOfferTrx> fotsForBds = bds.FreightOfferTrxs.ToList();
                if (fotsForBds.IsNullOrEmpty())
                    continue;
                fots = fots.Concat(fotsForBds).ToList();
                ////remove the accepted one
                //if (!bds.FreightOfferTrxAccepted.IsNull())
                //    fots.Remove(bds.FreightOfferTrxAccepted);

            }
            return fots;
        }
        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsASeller(string personIdForWhomWeAreWorking)
        {
            Owner owner = OwnerBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!owner.IsNull())
            {
                //this person is a Owner
                //get all the buySellOrders in which he is a Owner
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.OwnerId == owner.Id).ToList();
                return lst;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsACustomer(string personIdForWhomWeAreWorking)
        {
            Customer customer = CustomerBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!customer.IsNull())
            {
                //this person is a customer
                //get all the buySellOrders in which he is a customer
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.CustomerId == customer.Id).ToList();
                return lst;
            }
            return null;
        }


        //private void add_Money_Back_Items_To_Lst_Of_BuySellDocs_When_Payment_Is_Allocated(List<BuySellDoc> lst_Of_BuySellDocs_When_Payment_Is_Allocated, List<BuySellDoc> listOfItemsWithinMoneyBackGuarantee)
        //{
        //    if (listOfItemsWithinMoneyBackGuarantee.IsNullOrEmpty())
        //        return;

        //    if (lst_Of_BuySellDocs_When_Payment_Is_Allocated.IsNull())
        //        lst_Of_BuySellDocs_When_Payment_Is_Allocated = new List<BuySellDoc>();

        //    foreach (BuySellDoc bsd in listOfItemsWithinMoneyBackGuarantee)
        //    {
        //        lst_Of_BuySellDocs_When_Payment_Is_Allocated.Add(bsd);
        //    }
        //}


        ///// <summary>
        ///// Marks cash available in the buyselldoc
        ///// </summary>
        ///// <param name="lstOfBuySellDocs"></param>
        ///// <returns></returns>
        //private List<BuySellDoc> markCashAvailableIn(List<BuySellDoc> lstOfBuySellDocs)
        //{
        //    if (lstOfBuySellDocs.IsNullOrEmpty())
        //        return lstOfBuySellDocs;
        //    int noofDaysGuarantee = GetNoOfDaysCashBackGuarantee();
        //    //mark the cash available or not
        //    foreach (BuySellDoc bsd in lstOfBuySellDocs)
        //    {
        //        bsd.IsCashAvailable = bsd.IsCashFromDocAvailableCalculator(noofDaysGuarantee);
        //    }


        //    return lstOfBuySellDocs;
        //}






        ///// <summary>
        ///// This removes those items from the delivered list that are still within moneyback guarantee. And then returns the list
        ///// of items that have been removed so that they can be added back to the allocated list.
        ///// </summary>
        ///// <param name="lst_Of_BuySellDocs_When_Payment_Is_Available"></param>
        ///// <returns></returns>
        //private List<BuySellDoc> remove_delivered_Items_Still_Within_Guarantee_Period(List<BuySellDoc> lst_Of_BuySellDocs_When_Payment_Is_Available)
        //{
        //    if (lst_Of_BuySellDocs_When_Payment_Is_Available.IsNullOrEmpty())
        //        return null;

        //    List<BuySellDoc> lstOfItemsToRemove = new List<BuySellDoc>();
        //    DateParameter dp = new DateParameter();
        //    foreach (BuySellDoc bsd in lst_Of_BuySellDocs_When_Payment_Is_Available)
        //    {
        //        if (getCashState(bsd) == CashStateENUM.Allocated)
        //            lstOfItemsToRemove.Add(bsd);
        //    }

        //    if (lstOfItemsToRemove.IsNullOrEmpty())
        //        return null;

        //    //now remove the items which are still within guarantee date
        //    //from the list
        //    foreach (BuySellDoc bsd in lstOfItemsToRemove)
        //    {
        //        lst_Of_BuySellDocs_When_Payment_Is_Available.Remove(bsd);
        //    }

        //    return lstOfItemsToRemove;
        //}

        //private List<CashTrxVM2> get_CashTrxVM2_For(string forPersonId, List<CashEncashmentTrx> cashEncashmentList, bool showAdminReports)
        //{
        //    if (cashEncashmentList.IsNullOrEmpty())
        //        return new List<CashTrxVM2>();

        //    List<CashEncashmentTrx> lstToConvert = new List<CashEncashmentTrx>();

        //    if (showAdminReports)
        //    {
        //        //this is the admin.
        //        //convert all
        //        lstToConvert = cashEncashmentList;
        //    }
        //    else
        //    {
        //        //convert for a specific person
        //        forPersonId.IsNullOrWhiteSpaceThrowArgumentException("forPersonId");
        //        lstToConvert = cashEncashmentList
        //            .Where(x => x.PersonRequestingPaymentId == forPersonId)
        //            .ToList();
        //    }

        //    List<CashTrxVM2> listCashTrxVM2 = new List<CashTrxVM2>();
        //    if (!lstToConvert.IsNullOrEmpty())
        //    {
        //        Person fakeBankPerson = new Person();
        //        fakeBankPerson.Id = "";
        //        fakeBankPerson.Name = "Bank";
        //        foreach (CashEncashmentTrx cashEncashmentTrx in lstToConvert)
        //        {
        //            CashTrxVM2 cashTrxVM2 = cashEncashmentTrx.ToCashTrxVM2(BuySellDocStateENUM.CashEncashment.ToString().ToTitleSentance(), fakeBankPerson);
        //            cashTrxVM2.IsNullThrowException();
        //            listCashTrxVM2.Add(cashTrxVM2);
        //        }
        //    }

        //    return listCashTrxVM2;
        //}




        //private List<CashTrx> get_Systems_Destroyed_Cash_Trx()
        //{
        //    List<CashTrx> destroyedTrx = CashTrxBiz.FindAll()
        //        .Where(x => x.PersonToId == null || x.PersonToId.Trim() == "")
        //        .ToList();

        //    return destroyedTrx;
        //}



        //private List<CashEncashmentTrx> get_CashEncashmentTrx_For(CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        //{
        //    List<CashEncashmentTrx> lstCashEncashments = new List<CashEncashmentTrx>();

        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //        throw new Exception("Cash Type Unknown");

        //    if (cashTypeEnum == CashTypeENUM.NonRefundable)
        //        return lstCashEncashments;



        //    switch (cashStateEnum)
        //    {
        //        case CashStateENUM.Available:
        //            lstCashEncashments = _cashEncashmentTrxBiz.FindAll().Where(x => x.IsPaymentMade.Selected == true).ToList();
        //            break;
        //        case CashStateENUM.Allocated:
        //            lstCashEncashments = _cashEncashmentTrxBiz.FindAll().Where(x => x.IsPaymentMade.Selected == false && x.IsApproved.Selected == true).ToList();
        //            break;
        //        case CashStateENUM.All:
        //            lstCashEncashments = _cashEncashmentTrxBiz.FindAll().Where(x => x.IsApproved.Selected == true).ToList();
        //            break;
        //        default:
        //            break;
        //    }
        //    return lstCashEncashments;

        //}












        //private List<CashTrxVM2> convert_BuySellDoc_To_CashTrx_Paid(string personIdForWhomWeAreWorking, List<BuySellDoc> lstBuySellDocs, CashStateENUM cashStateEnum, CashTypeENUM cashTypeEnum, bool isWantAdminReport)
        //{
        //    if (lstBuySellDocs.IsNullOrEmpty())
        //        return null;

        //    //this is where the report is filtered for the person, or complete data for the admin
        //    List<BuySellDoc> lstBuySellDocsOfPaidTrx = get_Payments_From_BuySellDocs_FilterForCustomer(personIdForWhomWeAreWorking, lstBuySellDocs, isWantAdminReport);

        //    if (lstBuySellDocsOfPaidTrx.IsNullOrEmpty())
        //        return null;

        //    List<CashTrxVM2> lstCashTrx = convert_BuySellDoc_To_CashTrxList(lstBuySellDocsOfPaidTrx, cashStateEnum, cashTypeEnum, personIdForWhomWeAreWorking, isWantAdminReport);
        //    return lstCashTrx;

        //}



        ///// <summary>
        ///// note. the cashtrx carries the id of the buyselldoc
        ///// </summary>
        ///// <param name="lstBuySellDocs"></param>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //private List<CashTrxVM2> convert_BuySellDoc_To_CashTrx_Receipts(string personIdForWhomWeAreWorking, List<BuySellDoc> lstBuySellDocs, CashStateENUM cashStateEnum, CashTypeENUM cashTypeEnum, bool isShowAdminReport)
        //{
        //    if (lstBuySellDocs.IsNullOrEmpty())
        //        return null;


        //    List<BuySellDoc> lstBuySellDocsTrx = get_BuySellDoc_Reciepts_FilterForSeller(personIdForWhomWeAreWorking, lstBuySellDocs, isShowAdminReport);

        //    if (lstBuySellDocsTrx.IsNullOrEmpty())
        //        return null;

        //    List<CashTrxVM2> lstCashTrx = convert_BuySellDoc_To_CashTrxList(lstBuySellDocsTrx, cashStateEnum, cashTypeEnum, personIdForWhomWeAreWorking, isShowAdminReport);
        //    return lstCashTrx;



        //    //now convert these to CashTrx
        //    //foreach (BuySellDoc item in lstBuySellDocsTrx)
        //    //{

        //    //    //string documentNo = getDocumentNumber(item);
        //    //    Person personFrom = CustomerBiz.GetPersonForPlayer(item.CustomerId);
        //    //    Person personTo = OwnerBiz.GetPersonForPlayer(item.OwnerId);
        //    //    //Person personFrom = OwnerBiz.GetPersonForPlayer(item.OwnerId);
        //    //    //Person personTo = CustomerBiz.GetPersonForPlayer(item.CustomerId);


        //    //    //string docNo = string.Format("Purchase Order #{0}",item.DocumentNumber);
        //    //    CashTrx cashTrx = new CashTrx(item.Id, item.DocumentNumber, item.MetaData.Created.Date_NotNull_Min, personFrom.Id, personTo.Id, item.TotalInvoice, CashStateENUM.Allocated, CashTypeENUM.Refundable);


        //    //    cashTrx.PersonFrom = personFrom;
        //    //    cashTrx.PersonTo = personTo;
        //    //    cashTrx.Id = item.Id;
        //    //    CashTrxBiz.Detach(cashTrx);
        //    //    lstCashTrx.Add(cashTrx);
        //    //}

        //    //return lstCashTrx;
        //}


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


        ///// <summary>
        ///// In the payments version we get the customer.
        ///// </summary>
        ///// <param name="lstBuySellDocs"></param>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //private List<BuySellDoc> get_Payments_From_BuySellDocs_FilterForCustomer(string personIdForWhomWeAreWorking, List<BuySellDoc> lstBuySellDocs, bool isWantAdminReport)
        //{
        //    List<BuySellDoc> lstBuySellDocsOfPaidTrx = new List<BuySellDoc>();
        //    if (isWantAdminReport)
        //    {
        //        //for Admin ... show him everything.
        //        lstBuySellDocsOfPaidTrx = lstBuySellDocs;
        //    }
        //    else
        //    {
        //        //see if the personIdForWhomWeAreWorking is a customer... everyone is....
        //        Customer customer = CustomerBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
        //        if (customer.IsNull())
        //            return null;

        //        string customerId = customer.Id;
        //        lstBuySellDocsOfPaidTrx = lstBuySellDocs.Where(x => x.CustomerId == customerId).ToList();

        //    }
        //    return lstBuySellDocsOfPaidTrx;
        //}



        //private List<BuySellDoc> get_BuySellDoc_Reciepts_FilterForSeller(string personIdForWhomWeAreWorking, List<BuySellDoc> lstBuySellDocs, bool isWantAdminReport)
        //{
        //    List<BuySellDoc> lstBuySellDocsTrx = new List<BuySellDoc>();

        //    if (isWantAdminReport)
        //    {
        //        //this is the admin... only show him everything
        //        lstBuySellDocsTrx = lstBuySellDocs;

        //    }
        //    else
        //    {
        //        Owner owner = OwnerBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
        //        if (owner.IsNull())
        //            return null;

        //        string ownerId = owner.Id;
        //        lstBuySellDocsTrx = lstBuySellDocs.Where(x => x.OwnerId == ownerId).ToList();


        //    }

        //    return lstBuySellDocsTrx;
        //}

        ///// <summary>
        ///// We are paying the system commissions to PersonAcceptingPaymentForSystemId. Note, lstBuySellDocsOfPaidTrx injected will either be all allocated or all available. It
        ///// Has been filtered already
        ///// </summary>
        ///// <param name="lstBuySellDocsOfPaidTrx"></param>
        ///// <param name="cashStateEnumWanted"></param>
        ///// <param name="cashTypeEnumWanted"></param>
        ///// <param name="isShowAdminReports"></param>
        ///// <returns></returns>

        //private List<CashTrxVM2> convert_BuySellDoc_To_CashTrxList(List<BuySellDoc> lstBuySellDocsOfPaidTrx, CashStateENUM cashStateEnumWanted, CashTypeENUM cashTypeEnumWanted, string personIdFor, bool isShowAdminReports)
        //{
        //    List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

        //    Person personAcceptingMoneyForSystem = PersonBiz.Find(CurrentUserParameter.PersonAcceptingPaymentForSystemId);
        //    personAcceptingMoneyForSystem.IsNullThrowException();

        //    foreach (BuySellDoc buySellDoc in lstBuySellDocsOfPaidTrx)
        //    {
        //        //if user is CustomerSalesman then he will want to see his commissions
        //        //if user is OwnerSalesman then he will want to see his commissions
        //        //if user is the Owner, then he will see the commission deducted.
        //        //if the user is the customer, he will see no commissions. He will only see any fines that have been charged.

        //        //Person personCustomerBuySellDoc = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
        //        //Person personOwnerBuySellDoc = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
        //        Person currPerson = null;
        //        if (isShowAdminReports)
        //        {

        //        }
        //        else
        //        {
        //            currPerson = PersonBiz.Find(personIdFor);
        //            currPerson.IsNullThrowException();

        //        }


        //        //personCustomerBuySellDoc.IsNullThrowException();
        //        //personOwnerBuySellDoc.IsNullThrowException();

        //        //customer, owner, deliveryman
        //        //create_Owner_CashtTrx_From_BuySellDoc(lstCashTrx, personAcceptingMoneyForSystem, buySellDoc, currPerson, isShowAdminReports);
        //        //create_Customer_CashtTrx_From_BuySellDoc(lstCashTrx, buySellDoc, currPerson, isShowAdminReports);
        //        //create_Deliveryman_CashtTrx_From_BuySellDoc(lstCashTrx, personAcceptingMoneyForSystem, buySellDoc, currPerson, isShowAdminReports);

        //        ////salesmen
        //        //create_CustomerSalesman_CashTrx_From_BuySellDoc(cashStateEnumWanted, lstCashTrx, personAcceptingMoneyForSystem, buySellDoc, currPerson, isShowAdminReports);
        //        //create_DeliverymanSalesman_CashTrx_From_BuySellDoc(cashStateEnumWanted, lstCashTrx, personAcceptingMoneyForSystem, buySellDoc, currPerson, isShowAdminReports);
        //        //create_OwnerSalesman_CashTrx_From_BuySellDoc(cashStateEnumWanted, lstCashTrx, personAcceptingMoneyForSystem, buySellDoc, currPerson, isShowAdminReports);

        //    }
        //    return lstCashTrx;
        //}







        //private void create_OwnerSalesman_CashTrx_From_BuySellDoc(CashStateENUM cashStateEnum, List<CashTrxVM2> lstCashTrx, Person personAcceptingMoneyForSystem, BuySellDoc buySellDoc, Person currPerson, bool isShowAdminReports)
        //{
        //    if (isShowAdminReports)
        //    {
        //        //what does admin want to see
        //        //throw new NotImplementedException("What does admin want to see?");

        //    }
        //    else
        //    {
        //        if (buySellDoc.IsCurrUserOwnerSalesman(CurrentUserParameter))
        //        {
        //            Person personDeliverymanSalesman = currPerson;
        //            personDeliverymanSalesman.IsNullThrowException();

        //            //if user is CustomerSalesman then he will want to see his commissions
        //            //he will get commission on the freight portion
        //            string comment = string.Format("Seller's Salesman Commission for {0} (Total Sale less freight ({1:N2}) Commission is {2:N2} ({3:N2}%) )",
        //                                            buySellDoc.FullName(),
        //                                            buySellDoc.TotalInvoiceLessFreight.ToString().ToRuppeeFormat(),
        //                                            buySellDoc.OwnerSalesmanCommission.Amount,
        //                                            buySellDoc.OwnerSalesmanCommission.Percent);

        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personAcceptingMoneyForSystem, personDeliverymanSalesman, buySellDoc.OwnerSalesmanCommission.Amount, comment, cashStateEnum, CashTrxVmDocumentTypeENUM.SalesmanOwnerCommission);


        //        }
        //    }
        //}

        //private void create_DeliverymanSalesman_CashTrx_From_BuySellDoc(CashStateENUM cashStateEnum, List<CashTrxVM2> lstCashTrx, Person personAcceptingMoneyForSystem, BuySellDoc buySellDoc, Person currPerson, bool isShowAdminReports)
        //{
        //    if (isShowAdminReports)
        //    {
        //        //what does admin want to see
        //        //throw new NotImplementedException("What does admin want to see?");

        //    }
        //    else
        //    {
        //        if (buySellDoc.IsCurrUserDeliverymanSalesman(CurrentUserParameter))
        //        {
        //            Person personDeliverymanSalesman = currPerson;
        //            personDeliverymanSalesman.IsNullThrowException();

        //            //if user is CustomerSalesman then he will want to see his commissions
        //            //he will get commission on the freight portion
        //            string comment = string.Format("Deliveryman Salesman Commission for {0} (Total Sale less freight ({1:N2}) Commission is {2:N2} ({3:N2}%) )",
        //                                            buySellDoc.FullName(),
        //                                            buySellDoc.TotalInvoiceLessFreight.ToString().ToRuppeeFormat(),
        //                                            buySellDoc.DeliverymanSalesmanCommission.Amount,
        //                                            buySellDoc.DeliverymanSalesmanCommission.Percent);

        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personAcceptingMoneyForSystem, personDeliverymanSalesman, buySellDoc.DeliverymanSalesmanCommission.Amount, comment, cashStateEnum, CashTrxVmDocumentTypeENUM.SalesmanDeliverymanCommission);

        //            //there are no system charges to the DeliverymanSalesman


        //        }
        //    }
        //}

        //private void create_CustomerSalesman_CashTrx_From_BuySellDoc(CashStateENUM cashStateEnum, List<CashTrxVM2> lstCashTrx, Person personAcceptingMoneyForSystem, BuySellDoc buySellDoc, Person currPerson, bool isShowAdminReports)
        //{
        //    if (isShowAdminReports)
        //    {
        //        //what does admin want to see
        //        //throw new NotImplementedException("What does admin want to see?");

        //    }
        //    else
        //    {
        //        if (buySellDoc.IsCurrUserCustomerSalesman(CurrentUserParameter))
        //        {
        //            Person personCustomerSalesman = currPerson;
        //            personCustomerSalesman.IsNullThrowException();

        //            //if user is CustomerSalesman then he will want to see his commissions
        //            //he will get commission on the freight portion
        //            string comment = string.Format("Customer Salesman Commission for {0} (Total Sale less freight ({1:N2}) Commission is {2:N2} ({3:N2}%) )",
        //                                            buySellDoc.FullName(),
        //                                            buySellDoc.TotalInvoiceLessFreight.ToString().ToRuppeeFormat(),
        //                                            buySellDoc.CustomerSalesmanCommission.Amount,
        //                                            buySellDoc.CustomerSalesmanCommission.Percent);

        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personAcceptingMoneyForSystem, personCustomerSalesman, buySellDoc.CustomerSalesmanCommission.Amount, comment, cashStateEnum, CashTrxVmDocumentTypeENUM.SalesmanCustomerCommission);

        //            //There are no system charges to Customer Salesman



        //        }
        //    }
        //}

        //private void create_Deliveryman_CashtTrx_From_BuySellDoc(List<CashTrxVM2> lstCashTrx, Person personAcceptingMoneyForSystem, BuySellDoc buySellDoc, Person currPerson, bool isShowAdminReports)
        //{
        //    if (isShowAdminReports)
        //    {
        //        //what does admin want to see
        //        //throw new NotImplementedException("What does admin want to see?");

        //    }
        //    else
        //    {

        //        if (buySellDoc.IsCurrUserDeliveryman(CurrentUserParameter))
        //        {

        //            CashStateENUM cashStateOfTrx = getCashState(buySellDoc);

        //            Person personDeliveryMan = currPerson;
        //            personDeliveryMan.IsNullThrowException();

        //            //if user is DeliverymanSalesman then he will want to see his revenue
        //            //he will get his freight portion less maximum commission allowed


        //            string comment = string.Format("Delivery Charges for {0} (Freight Amount {1})",
        //                                            buySellDoc.FullName(),
        //                                            buySellDoc.Freight_Accepted.ToString().ToRuppeeFormat());

        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personAcceptingMoneyForSystem, personDeliveryMan, buySellDoc.Freight_Accepted, comment, cashStateOfTrx, CashTrxVmDocumentTypeENUM.DeliveryOrderCharges);

        //            string commentSystem = string.Format("System Charges for {0} (Sys Charges {1})",
        //                                            buySellDoc.FullName(),
        //                                            buySellDoc.Get_Maximum_Commission_Chargeable_On_Freight_Amount().ToString().ToRuppeeFormat());
        //            //now get the system charges
        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personDeliveryMan, personAcceptingMoneyForSystem, buySellDoc.Get_Maximum_Commission_Chargeable_On_Freight_Amount(), commentSystem, cashStateOfTrx, CashTrxVmDocumentTypeENUM.DeliveryOrderCharges);


        //        }
        //    }
        //}

        //private void create_Customer_CashtTrx_From_BuySellDoc(List<CashTrxVM2> lstCashTrx, BuySellDoc buySellDoc, Person currPerson, bool isShowAdminReports)
        //{
        //    if (isShowAdminReports)
        //    {
        //        //what does admin want to see
        //        //throw new NotImplementedException("What does admin want to see?");

        //    }
        //    else
        //    {

        //        Customer customerCurrPerson = CustomerBiz.GetPlayerForPersonId(currPerson.Id);
        //        if (!customerCurrPerson.IsNull())
        //        {
        //            Person customerBuySellDocPerson = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);

        //            if (customerBuySellDocPerson.Id != currPerson.Id)
        //                return;

        //            Person ownerPerson = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
        //            CashStateENUM cashStateOfTrx = getCashState(buySellDoc);

        //            //if user is customer or owner, they will want this the total transaction

        //            string comment = string.Format("Payment - {0}", buySellDoc.FullName());
        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, customerBuySellDocPerson, ownerPerson, buySellDoc.TotalInvoice, comment, cashStateOfTrx, CashTrxVmDocumentTypeENUM.PurchaseOrder);

        //            //there are no system charges to the customer.
        //        }
        //    }
        //}


        ///// <summary>
        ///// If currPerson is null then this is an admin report
        ///// </summary>
        ///// <param name="lstCashTrx"></param>
        ///// <param name="personAcceptingMoneyForSystem"></param>
        ///// <param name="buySellDoc"></param>
        ///// <param name="currPerson"></param>
        //private void create_Owner_CashtTrx_From_BuySellDoc(List<CashTrxVM2> lstCashTrx, Person personAcceptingMoneyForSystem, BuySellDoc buySellDoc, Person currPerson, bool isShowAdminReports)
        //{
        //    //a single transaction has come for the owner
        //    //we have to see if it is allocated or available
        //    if (isShowAdminReports)
        //    {

        //        //what does admin want to see
        //        //throw new NotImplementedException("What does admin want to see?");

        //    }
        //    else
        //    {
        //        currPerson.IsNullThrowExceptionArgument();
        //        Owner currPersonOwner = OwnerBiz.GetPlayerForPersonId(currPerson.Id);
        //        if (!currPersonOwner.IsNull())
        //        {
        //            //if user is customer or owner, they will want this the total transaction

        //            Person personOwnerBuySellDoc = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
        //            personOwnerBuySellDoc.IsNullThrowException();

        //            if (personOwnerBuySellDoc.Id != currPerson.Id)
        //                return;

        //            Person personCustomerBuySellDoc = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
        //            CashStateENUM cashStateOfTrx = getCashState(buySellDoc);


        //            string comment = string.Format("Receipt - {0} (Freight included {1})",
        //                buySellDoc.FullName(),
        //                buySellDoc.Freight_Accepted.ToString().ToRuppeeFormat());


        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personCustomerBuySellDoc, personOwnerBuySellDoc, buySellDoc.TotalInvoiceLessFreight, comment, cashStateOfTrx, CashTrxVmDocumentTypeENUM.SaleOrder);

        //            //these are the system charges 
        //            string comment_Sys =
        //                string.Format("System Charges for {0} {1} ({2}%)",
        //                buySellDoc.FullName(),
        //                buySellDoc.Get_Maximum_Commission_Chargeable_On_TotalSale_Amount().ToString().ToRuppeeFormat(),
        //                buySellDoc.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent().ToString().ToRuppeeFormat());

        //            add_CashTrx_To_List_From_BuySellDoc(lstCashTrx, buySellDoc, personOwnerBuySellDoc, personAcceptingMoneyForSystem, buySellDoc.Get_Maximum_Commission_Chargeable_On_TotalSale_Amount(), comment, cashStateOfTrx, CashTrxVmDocumentTypeENUM.SaleOrderCharges);
        //        }
        //    }
        //}

        //private static CashStateENUM getCashState(BuySellDoc buySellDoc)
        //{
        //    CashStateENUM cashStateOfTrx = CashStateENUM.Allocated;
        //    if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
        //        cashStateOfTrx = buySellDoc.CashAvailableStatus();
        //    return cashStateOfTrx;
        //}







        //private void add_CashTrx_For_Salesman(List<CashTrx> lstCashTrx, BuySellDoc buySellDoc, Person personFrom, CashStateENUM cashStateEnum)
        //{
        //    if (buySellDoc.CustomerSalesmanId.IsNull())
        //    {
        //        //do nothing
        //    }
        //    else
        //    {
        //        Person salesmanPerson = SalesmanBiz.GetPersonForPlayer(buySellDoc.CustomerSalesmanId);
        //        salesmanPerson.IsNullThrowException();

        //        string comment = string.Format("Salesman-{0:N2}", buySellDoc.CustomerSalesmanCommission.Amount);

        //        add_CashTrx_To_List(lstCashTrx, buySellDoc, personFrom, salesmanPerson, buySellDoc.TotalInvoice, comment, cashStateEnum);

        //    }
        //}



        //private void add_CashTrx_For_Total_Commission_Payable_By_Owner(List<CashTrx> lstCashTrx, BuySellDoc buySellDoc, Person personFrom, CashStateENUM cashStateEnum)
        //{
        //    Person ownerPerson = PersonBiz.Find(CurrentUserParameter.PersonId);
        //    string comment = string.Format("Deliveryman for order");
        //    ownerPerson.IsNullThrowException();
        //    add_CashTrx_To_List(lstCashTrx, buySellDoc, personFrom, ownerPerson, buySellDoc.Freight_Accepted, comment, cashStateEnum);
        //}

        //private void add_CashTrx_For_Deliveryman(List<CashTrx> lstCashTrx, BuySellDoc buySellDoc, Person personFrom, CashStateENUM cashStateEnum)
        //{
        //    if (buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
        //    {
        //        //do nothing
        //    }
        //    else
        //    {
        //        Person deliveryManPerson = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
        //        string comment = string.Format("Deliveryman for order");
        //        deliveryManPerson.IsNullThrowException();
        //        add_CashTrx_To_List(lstCashTrx, buySellDoc, personFrom, deliveryManPerson, buySellDoc.Freight_Accepted, comment, cashStateEnum);
        //    }
        //}




        //private void add_CashTrx_To_List_From_BuySellDoc(
        //    List<CashTrxVM2> lstCashTrx,
        //    string id,
        //    DateTime date,
        //    string documentNumber,
        //    Person personFrom,
        //    Person personTo,
        //    decimal amountReceived,
        //    decimal amountPaid,
        //    string comment,
        //    CashStateENUM cashStateEnum,
        //    BuySellDocStateENUM buySellDocStateEnum,
        //    CashTrxVmDocumentTypeENUM cashTrxVmDocumentTypeEnum)
        //{


        //    CashTrxVM2 cashTrx = new CashTrxVM2(
        //        id,
        //        date,
        //        amountPaid,
        //        amountReceived,
        //        comment,
        //        personFrom,
        //        personTo,
        //        CashTypeENUM.Refundable,
        //        cashStateEnum,
        //        cashTrxVmDocumentTypeEnum,
        //        documentNumber,
        //        buySellDocStateEnum);

        //    lstCashTrx.Add(cashTrx);

        //}





        //private void create_CashTrxVM2_From_BuySellDoc(List<CashTrxVM2> lstCashTrx, BuySellDoc buySellDoc, CashTrxVmDocumentTypeENUM cashTrxVmDocumentTypeEnum)
        //{
        //    CashStateENUM cashStateEnum = CashStateENUM.Allocated;

        //    if (buySellDoc.IsCashAvailable)
        //        cashStateEnum = CashStateENUM.Available;


        //    CashTrxVM2 cashTrx = new CashTrxVM2(
        //        buySellDoc.Id,
        //        buySellDoc.MetaData.Created.Date_NotNull_Min,
        //        amount,
        //        0,
        //        "",
        //        personFrom,
        //        personTo,
        //        CashTypeENUM.Refundable,
        //        cashStateEnum,
        //        cashTrxVmDocumentTypeEnum,
        //        buySellDoc.DocumentNumber.ToString("N0"),
        //        buySellDoc.BuySellDocStateEnum);

        //    lstCashTrx.Add(cashTrx);

        //}








        ///// <summary>
        ///// This is injected into getCashFor
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //private IQueryable<CashTrx> cashTrxReceivedSql_For_Admin_Or_Normal(string personIdForWhomWeAreWorking, bool isShowAdminReports)
        //{
        //    IQueryable<CashTrx> cashTrxPaid;
        //    if (isShowAdminReports)
        //    {
        //        cashTrxPaid = CashTrxBiz.FindAll().Where(x => x.PersonToId == "" || x.PersonToId == null);
        //    }
        //    else
        //    {
        //        personIdForWhomWeAreWorking.IsNullOrWhiteSpaceThrowException();
        //        cashTrxPaid = CashTrxBiz.FindAll().Where(x => x.PersonToId == personIdForWhomWeAreWorking);
        //    }
        //    return cashTrxPaid;
        //}





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


        //private IQueryable<CashTrx> cashTrx_SQL_For(IQueryable<CashTrx> cashTrxSql, CashTypeENUM cashTypeEnum)
        //{
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //    {
        //        throw new Exception("Unknown Cash type");
        //    }

        //    //this will get all the cashTypeEnum transactions
        //    if (cashTypeEnum != CashTypeENUM.Total)
        //    {
        //        cashTrxSql = cashTrxSql.Where(x => x.CashTypeEnum == cashTypeEnum);

        //    }

        //    return cashTrxSql;

        //}





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
            Person systemPerson = PersonBiz.Find(CurrentUserParameter.PersonAcceptingPaymentForSystemId);
            createPenaltyTrx(buySellDoc, systemPerson, rcdbc.Comment);
            BuySellDocBiz.UpdateAndSave(buySellDoc);

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
            try
            {
                //save the item , 
                string poNumber = "";
                DateTime poDate = DateTime.MinValue;
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                productChildId.IsNullOrWhiteSpaceThrowArgumentException("Product not recieved.");

                message = AddToSale(UserId, productChildId, poNumber, poDate);
                success = true;

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                message = string.Format("Not saved. Error: {0}", ErrorsGlobal.ToString());

            }
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
        public void Deliveryman_Accepts_To_Deliver(string frtOfferId)
        {
            UserId.IsNullOrWhiteSpaceThrowException();

            //locate the freight trx and mark it true.
            FreightOfferTrx fot = FreightOfferTrxBiz.Find(frtOfferId);
            fot.IsNullThrowException();
            fot.BuySellDoc.IsNullThrowException();
            BuySellDoc buySellDoc = fot.BuySellDoc;

            does_Deliveryman_Have_Enough_Money_To_Continue(fot, buySellDoc);

            fot.IsOfferAccepted = true;
            fot.InsuranceAmount = buySellDoc.InsuranceRequired;

            //setup
            buySellDoc.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Accept;
            buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Delivery;
            buySellDoc.FreightOfferTrxAccepted = fot;
            buySellDoc.DeliverymanSalesmanId = GetSalesmanForDeliveryman(buySellDoc.DeliverymanId);

            FreightOfferTrxBiz.Update(fot);
            BuySellDocBiz.UpdateAndSave(buySellDoc);
        }

        private void does_Deliveryman_Have_Enough_Money_To_Continue(FreightOfferTrx fot, BuySellDoc buySellDoc)
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
        public string GetSalesmanIdForCustomer(string playerId)
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

        public string GetSalesmanIdForOwner(string playerId)
        {
            playerId.IsNullOrWhiteSpaceThrowException();

            Person person = OwnerBiz.GetPersonForPlayer(playerId);
            return getSalesmanIdForCommission_Algorithem(person);

        }

        public string GetSalesmanForDeliveryman(string playerId)
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

        private string getSalesmanIdForCommission_Algorithem(Person person)
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
            return salesmanForCommission.Id;

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


    }
}
