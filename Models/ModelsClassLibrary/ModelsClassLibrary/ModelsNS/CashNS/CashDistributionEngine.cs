using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.CashNS
{
    [NotMapped]
    public class CashDistributionEngine
    {

        /// <summary>
        /// This engine does not know what kind of amount it is dealing with. It just dirtributes it
        /// </summary>
        /// <param name="cashBalanceVm"></param>
        /// <param name="nonRefundable_Incoming"></param>
        /// <param name="amountPayable_InMoney"></param>
        /// <param name="commissionPercent"></param>

        public CashDistributionEngine(CashBalanceVM cashBalanceVm, decimal nonRefundable_Incoming, decimal amountPayable_InMoney, bool isTokenPaymentsAllowed)
        {
            Refundable_Incoming = amountPayable_InMoney;
            NonRefundable_Incoming = nonRefundable_Incoming;
            CashBalance = cashBalanceVm;
            IsTokenPaymentsAllowed = isTokenPaymentsAllowed;

            distributeEngine();
        }


        #region Public



        public decimal NonRefundable_Final { get; private set; }

        //public decimal NonRefundable_Less_Commission_Final()
        //{
        //    return NonRefundable_Final - NonRefundable_Commission_Final();
        //}





        /// <summary>
        /// This is the resul we want
        /// </summary>
        public decimal Refundable_Final { get; private set; }

        //public decimal Refundable_Less_Commission_Final() { return Refundable_Final - Refundable_Commission_Final(); }




        ///// <summary>
        ///// This is the totalfinal
        ///// </summary>
        ///// <returns></returns>
        //public decimal Total_Less_Commission_Final()
        //{
        //    return Refundable_Less_Commission_Final() - NonRefundable_Less_Commission_Final();
        //}


        /// <summary>
        /// This is a message that we want displayed.
        /// </summary>
        public string Message { get; set; }


        //cheks to if user can pay the required amount
        public bool CanBuy()
        {
            if (IsTokenPaymentsAllowed)
            {
                decimal ttl_CashBalance = CashBalance.Total();
                decimal ttl_Incoming = Total_Incoming();
                return ttl_CashBalance >= ttl_Incoming;

            }
            if (CashBalance.Refundable < Refundable_Incoming)
                return false;


            return true;
        }



        /// <summary>
        /// This is the cash balances of the User
        /// </summary>
        public CashBalanceVM CashBalance { get; set; }


        #endregion

        #region Parameters



        /// <summary>
        /// This is true if payment is allowed in tokens
        /// </summary>
        bool IsTokenPaymentsAllowed { get; set; }


        /// <summary>
        /// This is the commission amount expected.
        /// </summary>
        decimal CommissionPercent_Incoming { get; set; }



        /// <summary>
        /// This is the amount that is payable in cash
        /// </summary>
        decimal Refundable_Incoming { get; set; }



        /// <summary>
        /// This is the amount that is payable in tokens
        /// </summary>
        decimal NonRefundable_Incoming { get; set; }


        #endregion

        #region NonRefundable



        //decimal NonRefundable_Commission_Final()
        //{
        //    if (CommissionPercent_Incoming == 0)
        //        return 0;

        //    if (NonRefundable_Final == 0)
        //        return 0;

        //    decimal amnt = NonRefundable_Final * CommissionPercent_Incoming / 100;
        //    return amnt;
        //}

        //decimal NonRefundable_Commission_Incoming()
        //{
        //    if (CommissionPercent_Incoming == 0)
        //        return 0;
        //    if (NonRefundable_Incoming == 0)
        //        return 0;

        //    decimal amnt = NonRefundable_Incoming * CommissionPercent_Incoming / 100;
        //    return amnt;
        //}

        //decimal NonRefundable_Less_Commission_Incoming()
        //{
        //    decimal ttlCommissionAmount = 0;
        //    decimal ttl = 0;

        //    if (CommissionPercent_Incoming > 0 && NonRefundable_Incoming > 0)
        //        ttlCommissionAmount = NonRefundable_Incoming * CommissionPercent_Incoming / 100;

        //    ttl = NonRefundable_Incoming + ttlCommissionAmount;

        //    return ttl;
        //}




        #endregion

        #region Refundable




        /// <summary>
        /// This is what the commission is on the final amount
        /// </summary>decimal Refundable_Commission_Payable { get; set; }
        //decimal Refundable_Commission_Final()
        //{
        //    if (CommissionPercent_Incoming == 0)
        //        return 0;

        //    if (Refundable_Final == 0)
        //        return 0;

        //    decimal amnt = Refundable_Final * CommissionPercent_Incoming / 100;
        //    return amnt;
        //}


        //decimal Refundable_Commission_On_Incoming()
        //{
        //    if (CommissionPercent_Incoming == 0)
        //        return 0;

        //    if (Refundable_Incoming == 0)
        //        return 0;

        //    decimal amnt = Refundable_Incoming * CommissionPercent_Incoming / 100;
        //    return amnt;
        //}


        //decimal Refundable_Less_Commission_Incoming()
        //{
        //    decimal ttlCommissionAmount = 0;
        //    decimal ttl = 0;

        //    if (CommissionPercent_Incoming > 0 && Refundable_Incoming > 0)
        //        ttlCommissionAmount = Refundable_Incoming * CommissionPercent_Incoming / 100;

        //    ttl = Refundable_Incoming + ttlCommissionAmount;

        //    return ttl;
        //}



        #endregion

        #region Totals

        //decimal Total_Less_Commission_Incoming()
        //{
        //    return Refundable_Less_Commission_Incoming() + NonRefundable_Less_Commission_Incoming();
        //}

        //decimal Total_Less_Commission_Final()
        //{
        //    return Refundable_Less_Commission_Final() + NonRefundable_Less_Commission_Final();
        //}

        decimal Total_Final()
        {
            return Refundable_Final + NonRefundable_Final;
        }
        decimal Total_Incoming()
        {
            return Refundable_Incoming + NonRefundable_Incoming;
        }

        #endregion




        void distributeEngine()
        {
            if (CanBuy())
            {
                if (IsTokenPaymentsAllowed)
                {
                    //user has fewer token than required
                    if (CashBalance.NonRefundable < NonRefundable_Incoming)
                    {
                        if (CashBalance.NonRefundable > 0)
                        {
                            //use up all the users tokens
                            NonRefundable_Final = CashBalance.NonRefundable;

                        }
                        //NonRefundable_C
                        //get the balance from money
                        decimal refundable_payment = Total_Incoming() - NonRefundable_Final;
                        if(CashBalance.Refundable >= refundable_payment)
                        {
                            Refundable_Final = refundable_payment;

                        }

                    }
                    else
                    {
                        NonRefundable_Final = NonRefundable_Incoming;

                    }
                }
                else
                {
                    if (CashBalance.Refundable < Refundable_Incoming)
                    {
                        string err = string.Format("You do not have enough balance. You require Money + commission = Rs{0:N2} and you only have Rs{1:N2}",
                            Refundable_Incoming,
                            CashBalance.Refundable);
                        throw new Exception(err);
                    }
                    else
                    {
                        Refundable_Final = Refundable_Incoming;
                    }
                }
            }
        }
    }
}
