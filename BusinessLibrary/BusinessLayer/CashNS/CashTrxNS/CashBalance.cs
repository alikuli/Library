using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {
        private decimal TotalCashInSystem(CashTypeENUM cashTypeEnum)
        {
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");


            //get all the person's cash trx
            List<CashTrx> cashTrxPaid = FindAll().Where(x => x.CashTypeEnum == cashTypeEnum && x.PersonFromId == null).ToList();
            //List<CashTrx> cashTrxReceived = FindAll().Where(x => x.CashTypeEnum == cashTypeEnum && x.PersonFromId == null).ToList();

            decimal totalPaid = 0;
            //decimal totalReceived = 0;

            if (!cashTrxPaid.IsNullOrEmpty())
                totalPaid = cashTrxPaid.Sum(x => x.Amount);

            //if (!cashTrxReceived.IsNullOrEmpty())
            //    totalReceived = cashTrxReceived.Sum(x => x.Amount);
            //decimal availableAmount = totalReceived - totalPaid;

            return totalPaid;
        }

        public bool HasAvailableBalance(CashPaymentModel cashPaymentModel, string paymentFromUserId, bool isBanker)
        {

            Person person = PersonBiz.GetPersonForUserId(paymentFromUserId);
            person.IsNullThrowException("Person not found!");
            return HasAvailableBalance(cashPaymentModel, person, isBanker);
        }
        public bool HasAvailableBalance(CashPaymentModel cashPaymentModel, Person person, bool isBanker)
        {
            if (isBanker)
                return true;

            return BalanceForPerson(person.Id, cashPaymentModel.CashTypeEnum) >= cashPaymentModel.Amount;
        }




        public decimal BalanceForPerson(CashPaymentModel cashPaymentModel)
        {
            return BalanceForPerson(cashPaymentModel.PersonToId, cashPaymentModel.CashTypeEnum);
        }
        //This calculates the person's available amount
        public decimal BalanceForPerson(string personId, CashTypeENUM cashTypeEnum)
        {
            personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");


            //get all the person's cash trx
            List<CashTrx> cashTrxPaid = TrxPaidFor(personId, cashTypeEnum).ToList();
            List<CashTrx> cashTrxReceived = TrxRecievedFor(personId, cashTypeEnum).ToList();

            decimal totalPaid = 0;
            decimal totalReceived = 0;

            if (!cashTrxPaid.IsNullOrEmpty())
                totalPaid = cashTrxPaid.Sum(x => x.Amount);

            if (!cashTrxReceived.IsNullOrEmpty())
                totalReceived = cashTrxReceived.Sum(x => x.Amount);
            decimal availableAmount = totalReceived - totalPaid;

            return availableAmount;

        }

        private IQueryable<CashTrx> TrxRecievedFor(string personId, CashTypeENUM cashTypeEnum)
        {

            //            IQueryable<CashTrx> cashTrxPaid = FindAll().Where(x => x.PersonToId == personId);

            IQueryable<CashTrx> cashTrxPaid;
            if (cashTypeEnum == CashTypeENUM.Unknown)
            {
                throw new Exception("Unknown Cash type");
            }

            if (personId == "" || personId == null)
            {
                cashTrxPaid = FindAll().Where(x => x.PersonToId == "" || x.PersonToId == null);
            }
            else
            {

                cashTrxPaid = FindAll().Where(x => x.PersonToId == personId);
            }

            //this will get all the cashTypeEnum transactions
            if (cashTypeEnum != CashTypeENUM.Total)
            {
                cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum);
            }
            return cashTrxPaid;
        }

        private IQueryable<CashTrx> TrxPaidFor(string personId, CashTypeENUM cashTypeEnum)
        {
            if (cashTypeEnum == CashTypeENUM.Unknown)
            {
                throw new Exception("Unknown Cash type");
            }
            IQueryable<CashTrx> cashTrxPaid;

            //this is coming from Admin
            if (personId == "" || personId == null)
            {
                cashTrxPaid = FindAll().Where(x => x.PersonFromId == "" || x.PersonFromId == null);

            }
            else
            {
                cashTrxPaid = FindAll().Where(x => x.PersonFromId == personId);
            }

            //this will get all the transactions
            if (cashTypeEnum != CashTypeENUM.Total)
            {
                cashTrxPaid = cashTrxPaid.Where(x => x.CashTypeEnum == cashTypeEnum);
            }


            return cashTrxPaid;
        }


        public CashTrxDbCrModel GetCashTrxDbCrModel(string personId, CashTypeENUM cashTypeEnum, DateTime fromDate, DateTime toDate, bool isAdmin)
        {

            List<CashTrx> cashTrxPaid = TrxPaidFor(personId, cashTypeEnum).Where(x => x.MetaData.Created.Date >= fromDate && x.MetaData.Created.Date <= toDate).ToList();
            List<CashTrx> cashTrxReceived = TrxRecievedFor(personId, cashTypeEnum).Where(x => x.MetaData.Created.Date >= fromDate && x.MetaData.Created.Date <= toDate).ToList();

            //personId can be null if we are trying to get system cash.
            string personName = "";
            if (!personId.IsNullOrWhiteSpace())
            {
                Person person = PersonBiz.Find(personId);
                person.IsNullThrowException("Person");
                personName = person.FullName();
            }

            List<CashTrxVM> paidTrxVm = fixCastTrx(cashTrxPaid, "payment", cashTypeEnum);
            List<CashTrxVM> receiptTrxVm = fixCastTrx(cashTrxReceived, "receipt", cashTypeEnum);
            CashTrxDbCrModel cashTrxDbCrModel = new CashTrxDbCrModel(receiptTrxVm, paidTrxVm, fromDate, toDate, personName, cashTypeEnum, isAdmin);

            return cashTrxDbCrModel;
        }

        private List<CashTrxVM> fixCastTrx(List<CashTrx> cashTrxs, string cashTTrxType, CashTypeENUM cashTypeEnum)
        {
            if (cashTrxs.IsNullOrEmpty())
                return null;
            List<CashTrxVM> lst = new List<CashTrxVM>();
            foreach (var trx in cashTrxs)
            {

                decimal receiptAmount = 0;
                decimal paymentAmount = 0;
                switch (cashTTrxType)
                {
                    case "receipt":
                        receiptAmount = trx.Amount;
                        break;
                    case "payment":
                        paymentAmount = trx.Amount;
                        break;
                    default:
                        throw new Exception("Problem in switch");
                }

                string fromName = "Bank";
                string toName = "Bank";


                if (!trx.PersonFrom.IsNull())
                    fromName = trx.PersonFrom.FullName();
                if (!trx.PersonTo.IsNull())
                    toName = trx.PersonTo.FullName();

                CashTrxVM cVm = new CashTrxVM(trx.Id, trx.MetaData.Created.Date.Value, trx.Name, paymentAmount, receiptAmount, trx.Comment, fromName, toName, trx.CashTypeEnum);

                lst.Add(cVm);
            }

            return lst;
        }


        public UserMoneyAccount MoneyAccountForUser(string userId, bool isAdmin)
        {
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("No person");

            string personId = person.Id;
            return MoneyAccountForPerson(personId, isAdmin);
        }


        public UserMoneyAccount MoneyAccountForPerson(string personId, bool isAdmin)
        {
            personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");

            decimal amountRefundable = BalanceForPerson(personId, CashTypeENUM.Refundable);
            decimal amountNonRefundable = BalanceForPerson(personId, CashTypeENUM.NonRefundable); ;
            decimal totalCashCreated_Refundable = 0;
            decimal totalCashCreated_NonRefundable = 0;

            if (isAdmin)
            {
                totalCashCreated_Refundable = TotalCashInSystem(CashTypeENUM.Refundable);
                totalCashCreated_NonRefundable = TotalCashInSystem(CashTypeENUM.NonRefundable);

            }
            UserMoneyAccount userMoneyAccount = new UserMoneyAccount();
            userMoneyAccount.InitializeCash(
                amountRefundable,
                amountNonRefundable,
                totalCashCreated_Refundable,
                totalCashCreated_NonRefundable);
            return userMoneyAccount;
        }



        public MoneyType GetMoneyTypeForUser(string userId)
        {
            if (userId.IsNullOrWhiteSpace())
                return new MoneyType();

            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("No person");

            string personId = person.Id;
            return GetMoneyTypeForPerson(personId, false);
        }

        public MoneyType GetMoneyTypeForPerson(string personId, bool isAdmin)
        {
            decimal amountRefundable = 0;
            decimal amountNonRefundable = 0;

            if (!isAdmin && personId.IsNullOrEmpty())
            {
                return new MoneyType();
            }

            if (isAdmin && personId.IsNullOrEmpty())
            {
                amountRefundable = TotalCashInSystem(CashTypeENUM.Refundable);
                amountNonRefundable = TotalCashInSystem(CashTypeENUM.NonRefundable);

            }
            else
            {
                personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
                amountRefundable = BalanceForPerson(personId, CashTypeENUM.Refundable);
                amountNonRefundable = BalanceForPerson(personId, CashTypeENUM.NonRefundable);

            }


            MoneyType moneyType = new MoneyType();
            moneyType.Refundable.MoneyAmount = amountRefundable;
            moneyType.Non_Refundable.MoneyAmount = amountNonRefundable;

            return moneyType;
        }

    }
}
