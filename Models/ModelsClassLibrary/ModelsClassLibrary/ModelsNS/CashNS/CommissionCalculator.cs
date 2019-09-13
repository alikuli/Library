using AliKuli.ToolsNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.CashNS
{
    /// <summary>
    /// This calculates the commissions for the entry.
    /// </summary>
    [NotMapped]
    public class CommissionCalculator
    {
        public CommissionCalculator(decimal totalAmount)
        {
            TotalAmount = totalAmount;
        }

        decimal TotalAmount { get; set; }

        public decimal commission_CustomerSalesman_Pct { get { return SalesCommissionClass.CommissionPct_CustomerSalesman; } }
        public decimal commission_CustomerSalesman_Amount
        {
            get
            {
                if (commission_CustomerSalesman_Pct * TotalAmount == 0)
                    return 0;
                return commission_CustomerSalesman_Pct * TotalAmount / 100;

            }
        }

        public decimal commission_OwnerSalesman_Pct { get { return SalesCommissionClass.CommissionPct_OwnerSalesman; } }
        public decimal commission_OwnerSalesman_Amount
        {
            get
            {
                if (commission_OwnerSalesman_Pct * TotalAmount == 0)
                    return 0;
                return commission_OwnerSalesman_Pct * TotalAmount / 100;

            }
        }

        public decimal commission_DeliverymanSalesman_Pct { get { return SalesCommissionClass.CommissionPct_DeliverymanSalesman; } }
        public decimal commission_DeliverymanSalesman_Amount
        {
            get
            {
                if (commission_DeliverymanSalesman_Pct * TotalAmount == 0)
                    return 0;
                return commission_DeliverymanSalesman_Pct * TotalAmount / 100;

            }
        }


        public decimal commission_SystemFreight_Pct { get { return SalesCommissionClass.CommissionPct_System; } }
        public decimal commission_SystemFreight_Amount
        {
            get
            {
                if (commission_DeliverymanSalesman_Pct * TotalAmount == 0)
                    return 0;
                return commission_DeliverymanSalesman_Pct * TotalAmount / 100;

            }
        }

        public decimal commission_SystemSaleWithoutFreight_Pct { get { return SalesCommissionClass.CommissionPct_System; } }
        public decimal commission_SystemSaleWithoutFreight_Amount
        {
            get
            {
                if (commission_DeliverymanSalesman_Pct * TotalAmount == 0)
                    return 0;
                return commission_DeliverymanSalesman_Pct * TotalAmount / 100;

            }
        }


        public decimal TotalCommissions_Pct
        {
            get
            {
                decimal ttl =
                    commission_CustomerSalesman_Pct +
                    commission_OwnerSalesman_Pct +
                    commission_DeliverymanSalesman_Pct +
                    commission_SystemFreight_Pct +
                    commission_SystemSaleWithoutFreight_Pct;
                return ttl;
            }
        }

        public decimal TotalCommissions_Amount
        {
            get
            {
                decimal ttl =
                    commission_CustomerSalesman_Amount +
                    commission_OwnerSalesman_Amount +
                    commission_DeliverymanSalesman_Amount +
                    commission_SystemFreight_Amount +
                    commission_SystemSaleWithoutFreight_Amount;
                return ttl;
            }
        }

        public decimal TotalAmount_Less_Commissions
        {
            get
            {
                decimal ttl = TotalAmount - TotalCommissions_Amount;
                return ttl;
            }
        }
    }
}
