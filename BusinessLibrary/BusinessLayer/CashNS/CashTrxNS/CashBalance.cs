

namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {

        //this is used in the top menu
        //Cash that is created has no PersonFromId
        //private decimal TotalCash_System(CashTypeENUM cashTypeEnum)
        //{
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //        throw new Exception("Cash Type is unknown");



        //    List<CashTrx> cashTrxPaid = TrxPaidFor(cashTypeEnum, CashStateENUM.All, null);
        //    decimal totalPaid = 0;
        //    if (!cashTrxPaid.IsNullOrEmpty())
        //        totalPaid = cashTrxPaid.Sum(x => x.Amount);
        //    //this is basically total paid for the admin. All the rest would have a paid for from whom.
        //    //when cash is created, the PaidFrom is empty.
        //    return totalPaid;
        //}

        //public bool HasAvailableBalance(CashPaymentModel cashPaymentModel, string paymentFromUserId, bool isBanker)
        //{

        //    Person person = PersonBiz.GetPersonForUserId(paymentFromUserId);
        //    person.IsNullThrowException("Person not found!");
        //    return HasAvailableBalance(cashPaymentModel, person, isBanker);
        //}
        //public bool HasAvailableBalance(CashPaymentModel cashPaymentModel, Person person, bool isBanker)
        //{
        //    if (isBanker)
        //        return true;

        //    return BalanceForPerson(person.Id, cashPaymentModel.CashTypeEnum) >= cashPaymentModel.Amount;
        //}




        //public decimal BalanceForPerson(CashPaymentModel cashPaymentModel)
        //{
        //    return BalanceForPerson(cashPaymentModel.PersonToId, cashPaymentModel.CashTypeEnum);
        //}
        ////This calculates the person's available amount
        //public decimal BalanceForPerson(string personId, CashTypeENUM cashTypeEnum)
        //{
        //    throw new NotImplementedException();
        //    //personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
        //    //if (cashTypeEnum == CashTypeENUM.Unknown)
        //    //    throw new Exception("Cash Type is unknown");


        //    ////get all the person's cash trx
        //    //List<CashTrx> cashTrxPaid = TrxPaidFor(personId, cashTypeEnum, CashStateENUM.All);
        //    //List<CashTrx> cashTrxReceived = TrxRecievedFor(personId, cashTypeEnum, CashStateENUM.Available).ToList();

        //    //decimal totalPaid = 0;
        //    //decimal totalReceived = 0;

        //    //if (!cashTrxPaid.IsNullOrEmpty())
        //    //    totalPaid = cashTrxPaid.Sum(x => x.Amount);

        //    //if (!cashTrxReceived.IsNullOrEmpty())
        //    //    totalReceived = cashTrxReceived.Sum(x => x.Amount);
        //    //decimal availableAmount = totalReceived - totalPaid;

        //    //return availableAmount;

        //}




        //private IQueryable<CashTrx> TrxRecievedFor(string personId, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        //{
        //    return getCashTrxFor(cashTrxReceivedSql(personId), cashTypeEnum, cashStateEnum);
        //}








        //private List<CashTrx> TrxPaidFor(CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, List<BuySellDoc> lstBuySellDocs)
        //{

        //    return TrxPaidFor("", "", "", cashTypeEnum, cashStateEnum, lstBuySellDocs);
        //}





        ///// <summary>
        ///// If personId is null or empty, you will get the total cash of the system,
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <param name="cashTypeEnum"></param>
        ///// <param name="cashStateEnum"></param>
        ///// <returns></returns>
        //private List<CashTrx> TrxPaidFor(string personId, string customerId, string ownerId, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, List<BuySellDoc> lstBuySellDocs)
        //{
        //    List<CashTrx> lstTrxPaidFor = getCashTrxFor(cashTrxPaidSql(personId), cashTypeEnum, cashStateEnum).ToList();
        //    List<CashTrx> lstBuySellTrxsToCashTrx = getListOfCashTrxFromBuySellDocs_Paid(lstBuySellDocs, personId);
        //    lstTrxPaidFor = addBuySellDocTrxsT(lstTrxPaidFor, lstBuySellTrxsToCashTrx);

        //    return lstTrxPaidFor;
        //}

        //private List<CashTrx> addBuySellDocTrxsT(List<CashTrx> lstTrxPaidReceivedFor, List<CashTrx> lstBuySellTrxs)
        //{
        //    //Now we need to add the allocated transactions to the list. These will come from BuySellOrders of the person

        //    if (lstBuySellTrxs.IsNullOrEmpty())
        //        return lstTrxPaidReceivedFor;

        //    foreach (CashTrx item in lstBuySellTrxs)
        //    {
        //        lstTrxPaidReceivedFor.Add(item);
        //    }

        //    return lstTrxPaidReceivedFor;

        //}

        //private List<CashTrx> getListOfCashTrxFromBuySellDocs_Paid(List<BuySellDoc> lstBuySellDocs, string customerId)
        //{
        //    if (lstBuySellDocs.IsNullOrEmpty())
        //        return null;

        //    List<CashTrx> lstCashTrx = new List<CashTrx>();
        //    List<BuySellDoc> lstBuySellDocsOfPaidTrx = new List<BuySellDoc>();

        //    if (customerId.IsNullOrWhiteSpace())
        //    {
        //        lstBuySellDocsOfPaidTrx = lstBuySellDocs.Where(x => x.CustomerId == "" || x.CustomerId == null).ToList();

        //    }
        //    else
        //    {
        //        //get the customer
        //        lstBuySellDocsOfPaidTrx = lstBuySellDocs.Where(x => x.CustomerId == customerId).ToList();
        //    }

        //    if (lstBuySellDocsOfPaidTrx.IsNullOrEmpty())
        //        return null;

        //    //now convert these to CashTrx
        //    foreach (BuySellDoc item in lstBuySellDocsOfPaidTrx)
        //    {
        //        CashTrx cashTrx = new CashTrx();
        //    }

        //    return lstCashTrx;
        //}
















        //private IQueryable<CashTrx> getCashTrxFor(IQueryable<CashTrx> cashTrxSql, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        //{
        //    IQueryable<CashTrx> cashTrxPaid;
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //    {
        //        throw new Exception("Unknown Cash type");
        //    }

        //    cashTrxPaid = cashTrxSql;

        //    //this will get all the cashTypeEnum transactions
        //    if (cashTypeEnum != CashTypeENUM.Total)
        //    {
        //        if (cashStateEnum == CashStateENUM.All)
        //            cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum);
        //        else
        //            cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum && x.CashStateEnum == cashStateEnum);

        //    }

        //    //List<CashTrx> trxpaidFor_Debug = cashTrxPaid.ToList();



        //    return cashTrxPaid;

        //}






        ///// <summary>
        ///// This is injected into getCashFor
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //private IQueryable<CashTrx> cashTrxPaidSql(string personId)
        //{
        //    IQueryable<CashTrx> cashTrxPaid;
        //    if (personId == "" || personId == null)
        //    {
        //        cashTrxPaid = FindAll().Where(x => x.PersonFromId == "" || x.PersonFromId == null);

        //    }
        //    else
        //    {
        //        cashTrxPaid = FindAll().Where(x => x.PersonFromId == personId);
        //    }
        //    return cashTrxPaid;
        //}

        /// <summary>
        /// This is injected into getCashFor
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        //private IQueryable<CashTrx> cashTrxReceivedSql(string personId)
        //{
        //    IQueryable<CashTrx> cashTrxPaid;
        //    if (personId == "" || personId == null)
        //    {
        //        cashTrxPaid = FindAll().Where(x => x.PersonToId == "" || x.PersonToId == null);
        //    }
        //    else
        //    {

        //        cashTrxPaid = FindAll().Where(x => x.PersonToId == personId);
        //    }
        //    return cashTrxPaid;
        //}










        ///// <summary>
        ///// You can get 4 kind of reports from this
        ///// User Cash Transaction
        /////     Refundable -Available
        /////     Refundable -Allocatted
        /////     NON-Refundable -Available
        /////     NON-Refundable -Allocatted
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <param name="cashTypeEnum"></param>
        ///// <param name="cashStateEnum"></param>
        ///// <param name="fromDate"></param>
        ///// <param name="toDate"></param>
        ///// <param name="isAdmin"></param>
        ///// <returns></returns>
        //public CashTrxDbCrModel GetCashTrxDbCrModel(CustomerBiz CustomerBiz, List<BuySellDoc> lstBuySellDocs, string personId, string customerId, string ownerId, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, DateTime fromDate, DateTime toDate, bool isAdmin)
        //{
        //    throw new NotImplementedException();

        //    //List<CashTrx> cashTrxPaid = TrxPaidFor(personId, cashTypeEnum, cashStateEnum, lstBuySellDocs);
        //    //List<CashTrx> cashTrxReceived = TrxRecievedFor(personId, cashTypeEnum, cashStateEnum).ToList();

        //    ////personId can be null if we are trying to get system cash.
        //    //string personName = "";
        //    //if (!personId.IsNullOrWhiteSpace())
        //    //{
        //    //    Person person = PersonBiz.Find(personId);
        //    //    person.IsNullThrowException("Person");
        //    //    personName = person.FullName();
        //    //}

        //    //List<CashTrxVM> paidTrxVm = fixCashTrx(cashTrxPaid, "payment", cashTypeEnum);
        //    //List<CashTrxVM> receiptTrxVm = fixCashTrx(cashTrxReceived, "receipt", cashTypeEnum);

        //    //CashTrxDbCrModel cashTrxDbCrModel = new CashTrxDbCrModel(receiptTrxVm, paidTrxVm, fromDate, toDate, personName, cashTypeEnum, cashStateEnum, isAdmin);

        //    //return cashTrxDbCrModel;
        //}

        //private List<CashTrxVM> fixCashTrx(List<CashTrx> cashTrxs, string cashTTrxType, CashTypeENUM cashTypeEnum)
        //{
        //    if (cashTrxs.IsNullOrEmpty())
        //        return null;
        //    List<CashTrxVM> lst = new List<CashTrxVM>();
        //    foreach (var trx in cashTrxs)
        //    {

        //        decimal receiptAmount = 0;
        //        decimal paymentAmount = 0;
        //        switch (cashTTrxType)
        //        {
        //            case "receipt":
        //                receiptAmount = trx.Amount;
        //                break;
        //            case "payment":
        //                paymentAmount = trx.Amount;
        //                break;
        //            default:
        //                throw new Exception("Problem in switch");
        //        }

        //        string fromName = "Bank";
        //        string toName = "Bank";


        //        if (!trx.PersonFrom.IsNull())
        //            fromName = trx.PersonFrom.FullName();
        //        if (!trx.PersonTo.IsNull())
        //            toName = trx.PersonTo.FullName();

        //        CashTrxVM cVm = new CashTrxVM(trx.Id, trx.MetaData.Created.Date.Value, trx.Name, paymentAmount, receiptAmount, trx.Comment, fromName, toName, trx.CashTypeEnum, trx.CashStateEnum);

        //        lst.Add(cVm);
        //    }

        //    return lst;
        //}


        //public UserMoneyAccount MoneyAccountForUser(string userId, bool isAdmin)
        //{
        //    Person person = UserBiz.GetPersonFor(userId);
        //    person.IsNullThrowException("No person");

        //    string personId = person.Id;
        //    return MoneyAccountForPerson(personId, isAdmin);
        //}


        //public UserMoneyAccount MoneyAccountForPerson(string personId, bool isAdmin)
        //{
        //    personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");

        //    decimal amountRefundable = BalanceForPerson(personId, CashTypeENUM.Refundable);
        //    decimal amountNonRefundable = BalanceForPerson(personId, CashTypeENUM.NonRefundable); ;
        //    decimal totalCashCreated_Refundable = 0;
        //    decimal totalCashCreated_NonRefundable = 0;

        //    if (isAdmin)
        //    {
        //        totalCashCreated_Refundable = TotalCash_System(CashTypeENUM.Refundable);
        //        totalCashCreated_NonRefundable = TotalCash_System(CashTypeENUM.NonRefundable);

        //    }
        //    UserMoneyAccount userMoneyAccount = new UserMoneyAccount();
        //    userMoneyAccount.InitializeCash(
        //        amountRefundable,
        //        amountNonRefundable,
        //        totalCashCreated_Refundable,
        //        totalCashCreated_NonRefundable);
        //    return userMoneyAccount;
        //}



        //public MoneyType GetMoneyTypeForUser(string userId)
        //{
        //    if (userId.IsNullOrWhiteSpace())
        //        return new MoneyType();

        //    Person person = UserBiz.GetPersonFor(userId);
        //    person.IsNullThrowException("No person");

        //    string personId = person.Id;
        //    personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");


        //    //-------------------------------------------


        //    decimal amountRefundable = 0;
        //    decimal amountNonRefundable = 0;

        //    amountRefundable = BalanceForPerson(personId, CashTypeENUM.Refundable);
        //    amountNonRefundable = BalanceForPerson(personId, CashTypeENUM.NonRefundable);

        //    MoneyType moneyType = new MoneyType();

        //    moneyType.Refundable.MoneyAmount = amountRefundable;
        //    moneyType.Non_Refundable.MoneyAmount = amountNonRefundable;
        //    moneyType.TotalCash.MoneyAmount = amountNonRefundable + amountRefundable;

        //    string menuType_Refundable = ConfigurationManager.AppSettings["menu.person_CashRefundable_MenuItem"];
        //    string tooltip_Refundable = ConfigurationManager.AppSettings["menu.person_CashRefundable_ToolTip"];
        //    moneyType.Refundable.MenuName = string.Format(menuType_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());
        //    moneyType.Refundable.MenuToolTip = string.Format(tooltip_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());

        //    string menuType_NON_Refundable = ConfigurationManager.AppSettings["menu.person_CashNonRefundable_MenuItem"];
        //    string tooltip_NON_Refundable = ConfigurationManager.AppSettings["menu.person_CashNonRefundable_ToolTip"];
        //    moneyType.Non_Refundable.MenuName = string.Format(menuType_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());
        //    moneyType.Non_Refundable.MenuToolTip = string.Format(tooltip_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

        //    string menuType_AllCash = ConfigurationManager.AppSettings["menu.person_TotalCash_MenuItem"];
        //    string tooltip_AllCash = ConfigurationManager.AppSettings["menu.person_TotalCash_ToolTip"];
        //    moneyType.TotalCash.MenuName = string.Format(menuType_AllCash, moneyType.TotalCash.MoneyAmount.ToString().ToRuppeeFormat());
        //    moneyType.TotalCash.MenuToolTip = string.Format(
        //        tooltip_AllCash,
        //        moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat(),
        //        moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

        //    return moneyType;

        //}

        //public MoneyType GetMoneyForSystem(bool isAdmin)
        //{

        //    if (!isAdmin)
        //        return new MoneyType();

        //    decimal amountRefundable = 0;
        //    decimal amountNonRefundable = 0;

        //    amountRefundable = TotalCash_System(CashTypeENUM.Refundable);
        //    amountNonRefundable = TotalCash_System(CashTypeENUM.NonRefundable);


        //    MoneyType moneyType = new MoneyType();

        //    moneyType.Refundable.MoneyAmount = amountRefundable;
        //    moneyType.Non_Refundable.MoneyAmount = amountNonRefundable;
        //    moneyType.TotalCash.MoneyAmount = amountNonRefundable + amountRefundable;

        //    string menuType_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_Refundable_MenuItem"];
        //    string tooltip_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_Refundable_ToolTip"];

        //    string menuType_NON_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_NonRefundable_MenuItem"];
        //    string tooltip_NON_Refundable = ConfigurationManager.AppSettings["menu.system_Cash_NonRefundable_ToolTip"];

        //    string menuType_AllCash = ConfigurationManager.AppSettings["menu.system_TotalCash_MenuItem"];
        //    string tooltip_AllCash = ConfigurationManager.AppSettings["menu.system_TotalCash_ToolTip"];

        //    moneyType.Refundable.MenuName = string.Format(menuType_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());
        //    moneyType.Refundable.MenuToolTip = string.Format(tooltip_Refundable, moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat());

        //    moneyType.Non_Refundable.MenuName = string.Format(menuType_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());
        //    moneyType.Non_Refundable.MenuToolTip = string.Format(tooltip_NON_Refundable, moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

        //    moneyType.TotalCash.MenuName = string.Format(menuType_AllCash, moneyType.TotalCash.MoneyAmount.ToString().ToRuppeeFormat());
        //    moneyType.TotalCash.MenuToolTip = string.Format(
        //        tooltip_AllCash,
        //        moneyType.Refundable.MoneyAmount.ToString().ToRuppeeFormat(),
        //        moneyType.Non_Refundable.MoneyAmount.ToString().ToRuppeeFormat());

        //    return moneyType;
        //}
        //private IQueryable<CashTrx> TrxRecievedFor(string personId, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        //{

        //    IQueryable<CashTrx> cashTrxPaid;
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //    {
        //        throw new Exception("Unknown Cash type");
        //    }

        //    cashTrxPaid = cashTrxReceivedSql(personId);

        //    //this will get all the cashTypeEnum transactions
        //    if (cashTypeEnum != CashTypeENUM.Total)
        //    {
        //        if (cashStateEnum == CashStateENUM.All)
        //            cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum);
        //        else
        //            cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum && x.CashStateEnum == cashStateEnum);

        //    }

        //    List<CashTrx> trxpaidFor_Debug = cashTrxPaid.ToList();

        //    return cashTrxPaid;
        //}
        //private IQueryable<CashTrx> TrxPaidFor(string personId, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        //{
        //    if (cashTypeEnum == CashTypeENUM.Unknown)
        //    {
        //        throw new Exception("Unknown Cash type");
        //    }
        //    IQueryable<CashTrx> cashTrxPaid;

        //    //this is coming from Admin
        //    cashTrxPaid = cashTrxPaidSql(personId);
        //    List<CashTrx> trxpaidFor_Debug1 = cashTrxPaid.ToList();

        //    //this will get all the transactions
        //    if (cashTypeEnum != CashTypeENUM.Total)
        //    {
        //        if (cashStateEnum == CashStateENUM.All)
        //            cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum);
        //        else
        //            cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum && x.CashStateEnum == cashStateEnum);
        //    }

        //    List<CashTrx> trxpaidFor_Debug = cashTrxPaid.ToList();

        //    return cashTrxPaid;
        //}
        //public MoneyType GetMoneyTypeForPerson(string personId, bool isAdmin)
        //{
        //    decimal amountRefundable = 0;
        //    decimal amountNonRefundable = 0;

        //    if (!isAdmin && personId.IsNullOrEmpty())
        //    {
        //        return new MoneyType();
        //    }

        //    if (isAdmin && personId.IsNullOrEmpty())
        //    {
        //        amountRefundable = TotalCashInSystem(CashTypeENUM.Refundable);
        //        amountNonRefundable = TotalCashInSystem(CashTypeENUM.NonRefundable);

        //    }
        //    else
        //    {
        //        personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
        //        amountRefundable = BalanceForPerson(personId, CashTypeENUM.Refundable);
        //        amountNonRefundable = BalanceForPerson(personId, CashTypeENUM.NonRefundable);

        //    }


        //    MoneyType moneyType = new MoneyType();
        //    moneyType.Refundable.MoneyAmount = amountRefundable;
        //    //            moneyType.Refundable.MenuName = ConfigurationManager.AppSettings[];


        //    moneyType.Non_Refundable.MoneyAmount = amountNonRefundable;
        //    moneyType.Total.MoneyAmount = amountNonRefundable + amountRefundable;

        //    string totalMoneyContent = ConfigurationManager.AppSettings["menu.person_TotalCash_AmountOnly"];
        //    string totalMoneyMenuName = string.Format(totalMoneyContent, moneyType.Total.MoneyAmount.ToString().ToRuppeeFormat());
        //    string totalMoneyToolTipContent = ConfigurationManager.AppSettings["menu.person_TotalCash_ToolTip"];

        //    moneyType.Total.MenuName = totalMoneyMenuName;

        //    moneyType.Total.MenuToolTip = string.Format(
        //        totalMoneyToolTipContent,
        //        amountRefundable.ToString().ToRuppeeFormat(),
        //        amountNonRefundable.ToString().ToRuppeeFormat());
        //    return moneyType;
        //}

    }
}
