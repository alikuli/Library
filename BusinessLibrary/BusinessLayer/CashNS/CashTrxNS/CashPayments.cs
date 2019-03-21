using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using System;


namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {
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

            decimal availableFund = BalanceForPerson(fromId, cashTypeEnum);

            if (isBank)
            {
                if (availableFund < amount)
                {
                    string adminComment = "Cash Creation!";
                    //create cash for that amount
                    createCashTransaction(null, fromId, amount - availableFund, cashTypeEnum, adminComment);

                }
            }
            else
            {
                if (availableFund < amount)
                    throw new Exception(string.Format("Unavailable funds. Currently, you only have {0} which is less than your payment amount of {1}", availableFund, amount));

            }


            createCashTransaction(fromId, toId, amount, cashTypeEnum, comment);

            return true;
        }

        private void createCashTransaction(string fromId, string toId, decimal amount, CashTypeENUM cashTypeEnum, string comment)
        {
            CashTrx paymentTrx = Factory() as CashTrx;

            paymentTrx.CashTypeEnum = cashTypeEnum;
            paymentTrx.PersonFromId = fromId;
            paymentTrx.PersonToId = toId;
            paymentTrx.Amount = amount;
            paymentTrx.Comment = comment;

            CreateAndSave(paymentTrx);
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
