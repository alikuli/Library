using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashsNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
            : BusinessLayer<CashTrx>
    {
        PersonBiz _personBiz;
        public CashTrxBiz(IRepositry<CashTrx> entityDal, BizParameters bizParameters, PersonBiz personBiz)
            : base(entityDal, bizParameters)
        {
            _personBiz = personBiz;
            _personBiz.UserId = UserId;
            _personBiz.UserName = UserName;
        }

        public PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException("PersonBiz");
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;
                return _personBiz;

            }
        }

        public UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }

        }
        public long GetNextDocNumber()
        {
            List<CashTrx> lst = FindAll().ToList();
            if (lst.IsNullOrEmpty())
                return 1;
            long maxExisting = lst.Max(x => x.DocNumber);
            return maxExisting + 1;

        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("User is not logged in");
            CashTrx paymentTrx = parm.Entity as CashTrx;
            paymentTrx.IsNullThrowException("Unable to unbox payment trx");


            if (paymentTrx.DocNumber == 0)
            {
                paymentTrx.DocNumber = GetNextDocNumber();
            }

            if (parm.Entity.Name.IsNullOrWhiteSpace())
            {
                parm.Entity.Name = UserName;
                parm.Entity.Name = parm.Entity.MakeUniqueName();

            }

            if (paymentTrx.PersonFromId.IsNullOrEmpty())
            {
                Person person = PersonBiz.GetPersonForUserId(UserId);
                person.IsNullThrowException("PersonFrom");

                paymentTrx.PersonFromId = person.Id;
                paymentTrx.PersonFromId.IsNullThrowException("PersonFromId");
                paymentTrx.PersonFrom = person;
            }

            paymentTrx.PersonToId.IsNullOrWhiteSpaceThrowException("You must declare who you are paying.");

            Person personTo = PersonBiz.Find(paymentTrx.PersonToId);
            personTo.IsNullThrowException("personTo");
            paymentTrx.PersonTo = personTo;

            base.Fix(parm);

        }


        public override ICommonWithId Factory()
        {

            CashTrx paymentTrx = base.Factory() as CashTrx;
            paymentTrx.CashTypeEnum = CashTypeENUM.Unknown;

            return paymentTrx as ICommonWithId;
        }

        /// <summary>
        /// This is the root of the payment trx from where payments will be added.
        /// We have to decide if non-refundable should only be ransacted by the bank.
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="amount"></param>
        /// <param name="cashTypeEnum"></param>
        /// <param name="comment"></param>
        /// <param name="isBank"></param>
        /// <returns></returns>
        bool addPayment(string fromId, string toId, decimal amount, CashTypeENUM cashTypeEnum, string comment, bool isBank)
        {
            fromId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
            toId.IsNullOrWhiteSpaceThrowArgumentException("toId");

            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");

            if (amount == 0)
                throw new Exception("Amount is zero!");


            if (!isBank)
            {
                decimal availableFund = BalanceForPerson(fromId, CashTypeENUM.Refundable);
                if (availableFund < amount)
                    throw new Exception(string.Format("Unavailable funds. Currently, you only have {0} which is less than your payment amount of {1}", availableFund, amount));
            }


            CashTrx paymentTrx = Factory() as CashTrx;

            paymentTrx.CashTypeEnum = cashTypeEnum;
            paymentTrx.PersonFromId = fromId;
            paymentTrx.PersonToId = toId;
            paymentTrx.Amount = amount;
            paymentTrx.Comment = comment;

            CreateAndSave(paymentTrx);
            return true;
        }

        public UserMoneyAccount UserMoneyAccountForUser(string userId)
        {
            Person person = UserBiz.GetPersonFor(userId);
            //person.IsNullThrowException("Person not found");
            if (person.IsNull()) return null;
            string personId = person.Id;

            return UserMoneyAccountForPerson(personId);
        }
        public UserMoneyAccount UserMoneyAccountForPerson(string personId)
        {
            personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
            UserMoneyAccount userMoneyAccount = new UserMoneyAccount();
            userMoneyAccount.AmountNonRefundable = BalanceForPerson(personId, CashTypeENUM.NonRefundable);
            userMoneyAccount.AmountRefundable = BalanceForPerson(personId, CashTypeENUM.Refundable);
            return userMoneyAccount;

        }



        //This calculates the person's available amount
        public decimal BalanceForPerson(string personId, CashTypeENUM cashTypeEnum)
        {
            personId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");


            //get all the person's cash trx
            List<CashTrx> cashTrxPaid = FindAll().Where(x => x.PersonFromId == personId && x.CashTypeEnum == cashTypeEnum).ToList();
            List<CashTrx> cashTrxReceived = FindAll().Where(x => x.PersonToId == personId && x.CashTypeEnum == cashTypeEnum).ToList();

            decimal totalPaid = 0;
            decimal totalReceived = 0;

            if (!cashTrxPaid.IsNullOrEmpty())
                totalPaid = cashTrxPaid.Sum(x => x.Amount);

            if (!cashTrxReceived.IsNullOrEmpty())
                totalReceived = cashTrxReceived.Sum(x => x.Amount);
            decimal availableAmount = totalReceived - totalPaid;

            return availableAmount;

        }


        /// <summary>
        /// Use this to add a refundable payment
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// 
        public bool Add_RefundablePayment(string fromId, string toId, decimal amount, string comment, bool isBank)
        {
            return addPayment(fromId, toId, amount, CashTypeENUM.Refundable, comment, isBank);
        }
        public bool Add_NON_RefundablePayment(string fromId, string toId, decimal amount, string comment, bool isBank)
        {
            return addPayment(fromId, toId, amount, CashTypeENUM.NonRefundable, comment, isBank);
        }


    }
}
