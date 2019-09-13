using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using System;
using System.Collections.Generic;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
            : BusinessLayer<CashTrx>
    {
        PersonBiz _personBiz;
        CashTrxDistributionBiz _cashTrxDistributionBiz;
        public CashTrxBiz(IRepositry<CashTrx> entityDal, BizParameters bizParameters, PersonBiz personBiz, CashTrxDistributionBiz cashTrxDistributionBiz)
            : base(entityDal, bizParameters)
        {
            _personBiz = personBiz;
            _personBiz.UserId = UserId;
            _personBiz.UserName = UserName;
            _cashTrxDistributionBiz = cashTrxDistributionBiz;
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

        public CashTrxDistributionBiz CashTrxDistributionBiz
        {
            get
            {
                _cashTrxDistributionBiz.IsNullThrowException("CashTrxDistributionBiz");
                _cashTrxDistributionBiz.UserId = UserId;
                _cashTrxDistributionBiz.UserName = UserName;
                return _cashTrxDistributionBiz;
            }
        }


        public bool Add_RefundablePayment(string fromId, string toId, decimal amount, string comment, bool isBank, decimal availableFunds)
        {
            return AddPayment(fromId, toId, amount, CashTypeENUM.Refundable, comment, isBank, availableFunds);
        }
        public bool Add_NON_RefundablePayment(string fromId, string toId, decimal amount, string comment, bool isBank, decimal availableFunds)
        {
            return AddPayment(fromId, toId, amount, CashTypeENUM.NonRefundable, comment, isBank, availableFunds);
        }


        /// <summary>
        /// This is the root of the payment trx from where payments will be added.
        /// We have to decide if non-refundable should only be ransacted by the bank.
        /// </summary>
        /// <param name="fromPersonId"></param>
        /// <param name="personToId"></param>
        /// <param name="amount"></param>
        /// <param name="cashTypeEnum"></param>
        /// <param name="comment"></param>
        /// <param name="isBank"></param>
        /// <returns></returns>
        public bool AddPayment(string fromPersonId, string personToId, decimal amount, CashTypeENUM cashTypeEnum, string comment, bool isBank, decimal availableFund)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");

            fromPersonId.IsNullOrWhiteSpaceThrowArgumentException("fromId");
            personToId.IsNullOrWhiteSpaceThrowArgumentException("toId");

            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is unknown");

            if (amount == 0)
                throw new Exception("Amount is zero!");

            if (isBank)
            {
                if (availableFund < amount)
                {
                    string adminComment = "Cash Creation!";
                    //create cash for that amount
                    createCashTransaction(null, fromPersonId, amount - availableFund, cashTypeEnum, adminComment);

                }
            }
            else
            {
                if (availableFund < amount)
                    throw new Exception(string.Format("Unavailable funds. Currently, you only have {0} which is less than your payment amount of {1}", availableFund, amount));

            }


            createCashTransaction(fromPersonId, personToId, amount, cashTypeEnum, comment);

            return true;
        }

        private void createCashTransaction(string personFromId, string personToId, decimal amount, CashTypeENUM cashTypeEnum, string comment)
        {
            CashTrx paymentTrx = Factory() as CashTrx;

            paymentTrx.CashTypeEnum = cashTypeEnum;
            paymentTrx.PersonFromId = personFromId;
            paymentTrx.PersonToId = personToId;
            paymentTrx.Amount = amount;
            paymentTrx.Comment = comment;

            CreateAndSave(paymentTrx);
        }




        public List<CashTrxVM> JoinLists(List<CashTrxVM> lstTrxPaidReceivedFor, List<CashTrxVM> lstBuySellTrxs)
        {
            if (lstTrxPaidReceivedFor.IsNullOrEmpty())
                return lstBuySellTrxs;

            if (lstBuySellTrxs.IsNullOrEmpty())
                return lstTrxPaidReceivedFor;

            foreach (CashTrxVM item in lstBuySellTrxs)
            {
                lstTrxPaidReceivedFor.Add(item);
            }

            return lstTrxPaidReceivedFor;

        }







    }
}
