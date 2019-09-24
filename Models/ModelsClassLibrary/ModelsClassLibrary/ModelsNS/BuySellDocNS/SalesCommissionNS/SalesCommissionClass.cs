using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace AliKuli.ToolsNS
{
    /// <summary>
    /// This holds the commission amounts that come from the WebConfig
    /// This also does a lot of the calculations
    /// </summary>
    [NotMapped]

    public class SalesCommissionClass
    {

        #region Customer Salesmen Commission

        public static decimal CommissionPct_CustomerSalesman
        {
            get
            {
                return Salesman.CommissionPct_CustomerSalesman;
            }
        }
        public static decimal CommissionPct_Customer_Super_Salesman
        {
            get
            {
                return Salesman.CommissionPct_Customer_Super_Salesman;
            }
        }
        public static decimal CommissionPct_Customer_Super_Super_Salesman
        {
            get
            {
                return Salesman.CommissionPct_Customer_Super_Super_Salesman;
            }
        }



        #endregion

        #region Owner Salesman Commission

        public static decimal CommissionPct_OwnerSalesman
        {
            get
            {
                return Salesman.CommissionPct_OwnerSalesman;
            }
        }
        public static decimal CommissionPct_Owner_Super_Salesman
        {
            get
            {
                return Salesman.CommissionPct_Owner_Super_Salesman;
            }
        }
        public static decimal CommissionPct_Owner_Super_Super_Salesman
        {
            get
            {
                return Salesman.CommissionPct_Owner_Super_Super_Salesman;
            }
        }


        #endregion

        #region Deliveryman Sales Percent

        public static decimal CommissionPct_DeliverymanSalesman
        {
            get
            {
                return Deliveryman.CommissionPct_DeliverymanSalesman;
            }
        }

        public static decimal CommissionPct_Deliveryman_Super_Salesman
        {
            get
            {
                return Deliveryman.CommissionPct_Deliveryman_Super_Salesman;
            }
        }

        public static decimal CommissionPct_Deliveryman_Super_Super_Salesman
        {
            get
            {
                return Deliveryman.CommissionPct_Deliveryman_Super_Super_Salesman;
            }
        }


        #endregion

        #region System Commission



        /// <summary>
        /// This is the percentage commission of the system for both delivery and sales
        /// </summary>
        public static decimal CommissionPct_System
        {
            get
            {
                return Salesman.CommissionPct_System;
            }
        }




        #endregion



        /// <summary>
        /// It will give an amount regardless if the player exists or not
        /// 
        /// </summary>
        /// <returns></returns>
        public static decimal Get_Maximum_Commission_Chargeable_On_TotalSale_Percent(decimal saleWithoutFreightMoney, decimal freightMoney)
        {
            decimal totalSale = saleWithoutFreightMoney + freightMoney;

            if (totalSale == 0)
                return 0;

            decimal totalCommission = Commission_Payable_On_Invoice_Amount(saleWithoutFreightMoney, freightMoney);

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
        public static decimal Commission_Payable_On_Invoice_Amount(decimal saleWithoutFreightMoney, decimal freightMoney)
        {
            decimal totalSale = saleWithoutFreightMoney + freightMoney;

            if (totalSale == 0)
                return 0;

            decimal totatCommission_On_NetSale_Money = TotalCommissionOnSaleWithoutFreight_Amount(saleWithoutFreightMoney);
            decimal totalCommission_On_Freight_Money = TotalCommissionOnFreight_Amount(freightMoney);
            decimal totalCommission = totatCommission_On_NetSale_Money + totalCommission_On_Freight_Money;

            return totalCommission;
        }




        /// <summary>
        /// This is the total commission payable on freight (DeliverymanSalesman + system).
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="freightMoney"></param>
        /// <returns></returns>
        public static decimal TotalCommissionOnFreight_Amount(decimal freightMoney)
        {

            if (freightMoney == 0)
                return 0;

            if (TotalCommissionOnFreight_Precent() == 0)
                return 0;

            decimal totalCommission_On_Freight_Money =
                freightMoney * TotalCommissionOnFreight_Precent();

            return totalCommission_On_Freight_Money;
        }






        /// <summary>
        /// This is the total commission payable only on the net sale as percent of the total sale
        /// It is not neccesary this amount will be met because, sometimes players are missing.
        /// It will give an amount regardless if the player exists or not
        /// </summary>
        /// <param name="saleWithoutFreightMoney"></param>
        /// <returns></returns>
        public static decimal TotalCommissionOnSaleWithoutFreight_Amount(decimal saleWithoutFreightMoney)
        {

            if (saleWithoutFreightMoney == 0)
                return 0;

            decimal totalCommission_On_NetSale_Money = saleWithoutFreightMoney * TotalCommissionOnSaleWithoutFreight_Percent();

            if (totalCommission_On_NetSale_Money == 0)
                return 0;

            return totalCommission_On_NetSale_Money / 100;

        }

        public static decimal TotalCommissionOnSaleWithoutFreight_Percent()
        {
            decimal pct =
                CommissionPct_CustomerSalesman +
                CommissionPct_Customer_Super_Salesman +
                CommissionPct_Customer_Super_Super_Salesman +
                CommissionPct_OwnerSalesman +
                CommissionPct_Owner_Super_Salesman +
                CommissionPct_Owner_Super_Super_Salesman +
                CommissionPct_System;
            return pct;
        }

        public static decimal TotalCommissionOnFreight_Precent()
        {
            decimal pct =
                CommissionPct_DeliverymanSalesman +
                CommissionPct_Deliveryman_Super_Salesman +
                CommissionPct_Deliveryman_Super_Super_Salesman +
                CommissionPct_System;
            return pct;
        }
    }
}
