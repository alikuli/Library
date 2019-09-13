using AliKuli.Extentions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace AliKuli.ToolsNS
{
    /// <summary>
    /// This holds the commission amounts that come from the WebConfig
    /// This also does a lot of the calculations
    /// </summary>
    [NotMapped]

    public class SalesCommissionClass
    {

        public static decimal CommissionPct_CustomerSalesman
        {
            get
            {
                return CommissionPct_CustomerSalesman_String().ToDecimal();
            }
        }
        static string CommissionPct_CustomerSalesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.Customer"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.Customer in WebConfig");
            }

            return commission;
        }


        public static decimal CommissionPct_OwnerSalesman
        {
            get
            {
                return CommissionPct_OwnerSalesman_String().ToDecimal();
            }
        }
        static string CommissionPct_OwnerSalesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.Owner"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.Owner in WebConfig");
            }

            return commission;
        }


        public static decimal CommissionPct_DeliverymanSalesman
        {
            get
            {
                return CommissionPct_DeliverymanSalesman_String().ToDecimal();
            }
        }

        static string CommissionPct_DeliverymanSalesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.Deliveryman"];

            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.Deliveryman in WebConfig");
            }

            return commission;
        }


        /// <summary>
        /// This is the percentage commission of the system for both delivery and sales
        /// </summary>
        public static decimal CommissionPct_System
        {
            get
            {
                return CommissionPct_System_String().ToDecimal();
            }
        }

        /// <summary>
        /// this is for both delivey and sales
        /// </summary>
        /// <returns></returns>
        static string CommissionPct_System_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.System"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.System in WebConfig");
            }

            return commission;
        }




        /// <summary>
        /// This calculates the commission for the OwnerSalesman
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="saleWithoutFreight"></param>
        /// <returns></returns>
        public static decimal CommissionAmount_OwnerSalesman_For(decimal saleWithoutFreight)
        {
            if (CommissionPct_OwnerSalesman == 0)
                return 0;

            if (saleWithoutFreight == 0)
                return 0;

            decimal commissionAmount = saleWithoutFreight * CommissionPct_OwnerSalesman / 100;
            return commissionAmount;
        }

        /// <summary>
        /// This is the total commission amount calculated for the customer salesman
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="saleWithoutFreight"></param>
        /// <returns></returns>
        public static decimal CommissionAmount_CustomerSalesman_For(decimal saleWithoutFreight)
        {
            if (CommissionPct_CustomerSalesman == 0)
                return 0;

            if (saleWithoutFreight == 0)
                return 0;

            decimal commissionAmount = saleWithoutFreight * CommissionPct_CustomerSalesman / 100;
            return commissionAmount;
        }

        /// <summary>
        /// This is the total commission amount calculated for the deliveryman salesman on freight
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="freightMoney"></param>
        /// <returns></returns>
        public static decimal CommissionAmount_DeliverySalesman_For(decimal freightMoney)
        {
            if (CommissionPct_DeliverymanSalesman == 0)
                return 0;

            if (freightMoney == 0)
                return 0;

            decimal commissionAmount = freightMoney * CommissionPct_DeliverymanSalesman / 100;
            return commissionAmount;
        }


        /// <summary>
        /// This is the commission amount calculated for the system. You need to add freight amount when calculating
        /// commission for the freight and the saleWithoutFrt amount when calculating for sale.
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="freightMoney_OR_SaleWithoutFreight"></param>
        /// <returns></returns>
        public static decimal CommissionAmount_ToSystem_For(decimal freightMoney_OR_SaleWithoutFreight)
        {
            if (CommissionPct_System == 0)
                return 0;

            if (freightMoney_OR_SaleWithoutFreight == 0)
                return 0;

            decimal commissionAmount = freightMoney_OR_SaleWithoutFreight * CommissionPct_System / 100;
            return commissionAmount;
        }



        /// <summary>
        /// It will give an amount regardless if the player exists or not
        /// 
        /// </summary>
        /// <returns></returns>
        public static decimal Commission_Payable_On_TotalSale_Pct(decimal saleWithoutFreightMoney, decimal freightMoney)
        {
            decimal totalSale = saleWithoutFreightMoney + freightMoney;

            if (totalSale == 0)
                return 0;

            decimal totalCommission = Commission_Payable_On_TotalSale_Amount(saleWithoutFreightMoney, freightMoney);

            if (totalCommission == 0)
                return 0;

            decimal totalCommission_Percentage = totalCommission / totalSale;
            return totalCommission_Percentage;
        }



        /// <summary>
        /// This is the total commission payable on the buyselldoc. (Comm on Net sale + comm on freight)
        /// It is not neccesary this amount will be met because, sometimes players are missing.
        /// It will give an amount regardless if the player exists or not
        /// 
        /// </summary>
        /// <param name="saleWithoutFreightMoney"></param>
        /// <param name="freightMoney"></param>
        /// <returns></returns>
        public static decimal Commission_Payable_On_TotalSale_Amount(decimal saleWithoutFreightMoney, decimal freightMoney)
        {
            decimal totalSale = saleWithoutFreightMoney + freightMoney;

            if (totalSale == 0)
                return 0;

            decimal totatCommission_On_NetSale_Money = Commission_Payable_On_TotalSaleWithoutFreight_Amount(saleWithoutFreightMoney);
            decimal totalCommission_On_Freight_Money = Commission_Payable_On_Freight_Amount(freightMoney);
            decimal totalCommission = totatCommission_On_NetSale_Money + totalCommission_On_Freight_Money;

            return totalCommission;
        }




        /// <summary>
        /// This is the total commission payable on freight (DeliverymanSalesman + system).
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="freightMoney"></param>
        /// <returns></returns>
        public static decimal Commission_Payable_On_Freight_Amount(decimal freightMoney)
        {

            if (freightMoney == 0)
                return 0;

            if ((CommissionPct_DeliverymanSalesman + CommissionPct_System) == 0)
                return 0;

            decimal totalCommission_On_Freight_Money =
                CommissionAmount_ToSystem_For(freightMoney) +
                CommissionAmount_DeliverySalesman_For(freightMoney);

            return totalCommission_On_Freight_Money;
        }



        /// <summary>
        /// This is the maximum commission  (Salesman + system) on the deliverymans amount shown as a percentage of the total sale.
        /// It is not neccesary this amount will be met because, sometimes players are missing.
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="freightMoney"></param>
        /// <param name="totalSale"></param>
        /// <returns></returns>
        public static decimal Commission_Payable_On_Freight_As_Pct_Of_TotalSale(decimal freightMoney, decimal totalSale)
        {
            if (totalSale == 0)
                return 0;

            decimal ttlCommPayableOnFrt_Amount = Commission_Payable_On_Freight_Amount(freightMoney);

            if (ttlCommPayableOnFrt_Amount == 0)
                return 0;

            decimal pctage = ttlCommPayableOnFrt_Amount / totalSale;
            return pctage;


        }



        /// <summary>
        /// This is the total commission payable only on the net sale as percent of the total sale
        /// It is not neccesary this amount will be met because, sometimes players are missing.
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="saleWithoutFreightMoney"></param>
        /// <returns></returns>
        public static decimal Commission_Payable_On_TotalSaleWithoutFreight_Amount(decimal saleWithoutFreightMoney)
        {

            if (saleWithoutFreightMoney == 0)
                return 0;

            decimal totalCommission_On_NetSale_Money = saleWithoutFreightMoney * (CommissionPct_CustomerSalesman + CommissionPct_OwnerSalesman + CommissionPct_System);

            if (totalCommission_On_NetSale_Money == 0)
                return 0;
            return totalCommission_On_NetSale_Money / 100;

        }



        /// <summary>
        /// This is the total commission payable on NetSale as a percentage of the total sale.
        /// It is not neccesary this amount will be met because, sometimes players are missing.
        /// It will give an amount regardless if the player exists or not
        /// 
        /// </summary>
        /// <param name="saleWithoutFreightMoney"></param>
        /// <param name="totalSale"></param>
        /// <returns></returns>
        public static decimal Commission_Payable_TotalSaleWithoutFreight_As_Percentage_Of_TotalSale(decimal saleWithoutFreightMoney, decimal totalSale)
        {
            if (saleWithoutFreightMoney == 0)
                return 0;

            if (totalSale == 0)
                return 0;

            decimal totalCommission_On_NetSale_Money = Commission_Payable_On_TotalSaleWithoutFreight_Amount(saleWithoutFreightMoney);

            if (totalCommission_On_NetSale_Money == 0)
                return 0;

            decimal percent = totalCommission_On_NetSale_Money / totalSale;

            return percent;

        }


        /// <summary>
        /// A simple error check
        /// </summary>
        /// <param name="freightMoney"></param>
        /// <param name="totalSale"></param>
        /// <param name="saleWithoutFreightMoney"></param>
        public static void SelfErrorCheck(decimal freightMoney, decimal totalSale, decimal saleWithoutFreightMoney)
        {
            //check amounts
            if (saleWithoutFreightMoney != totalSale - freightMoney)
                throw new Exception("There is an error in your max calculations for commissions.");

            decimal max_Comm_On_Frt_As_Pct_Of_TtlSale = Commission_Payable_On_Freight_As_Pct_Of_TotalSale(freightMoney, totalSale);
            decimal max_Comm_On_Net_Sale_As_Pct_Of_TtlSale = Commission_Payable_TotalSaleWithoutFreight_As_Percentage_Of_TotalSale(saleWithoutFreightMoney, totalSale);

            decimal sumOfcalcComm = max_Comm_On_Frt_As_Pct_Of_TtlSale + max_Comm_On_Net_Sale_As_Pct_Of_TtlSale;
            decimal comFromMethod = Commission_Payable_On_TotalSale_Pct(saleWithoutFreightMoney, freightMoney);

            //check calculated commission
            if (sumOfcalcComm != comFromMethod)
                throw new Exception("There is an error in your calculations for commissions.(2)");


        }
    }
}
