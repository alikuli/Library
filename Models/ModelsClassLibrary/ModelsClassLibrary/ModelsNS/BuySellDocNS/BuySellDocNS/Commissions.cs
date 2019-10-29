using AliKuli.ToolsNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using System;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    /// <summary>
    /// During sale the vendor / seller cannot update the CustomerId or the SellerId
    /// What will happen if the seller and the delivery man are the same. They could cheat the customer.
    /// The customer can always create a problem.
    /// </summary>
    public partial class BuySellDoc
    {





        #region Total Product Commission - Expected
        public decimal Get_Total_Commission_Product_Percent_Expected()
        {
            decimal total = 0;
            total += Get_Total_Commission_Customer_Percent();
            total += Get_Total_Commission_Seller_Percent();
            total += Get_Total_Commission_System_Percent();

            return total;
        }

        public decimal Get_Total_Commission_Product_Amount_Refundable_Expected()
        {
            decimal total = 0;
            total += Get_Total_Commission_Customer_Amount_Refundable_Expected();
            total += Get_Total_Commission_Seller_Amount_Refundable_Expected();
            total += Get_Total_Commission_System_Amount_Refundable_Expected();

            return total;
        }

        public decimal Get_Total_Commission_Product_Amount_NonRefundable_Expected()
        {
            decimal total = 0;
            total += Get_Total_Commission_Customer_Amount_NonRefundable_Expected();
            total += Get_Total_Commission_Seller_Amount_NonRefundable_Expected();
            total += Get_Total_Commission_System_Amount_NonRefundable_Expected();

            return total;
        }

        private decimal Get_Total_Commission_System_Amount_NonRefundable_Expected()
        {
            decimal ttl = Get_Total_Commission_System_Percent() * Total_Product_Payment_For_Invoice.Amount_NonRefundable;
            return ttl;
        }

        private decimal Get_Total_Commission_Seller_Amount_NonRefundable_Expected()
        {
            decimal ttl = Get_Total_Commission_Seller_Percent() * Total_Product_Payment_For_Invoice.Amount_NonRefundable;
            return ttl;
        }

        private decimal Get_Total_Commission_Customer_Amount_NonRefundable_Expected()
        {
            decimal ttl = Get_Total_Commission_Customer_Percent() * Total_Product_Payment_For_Invoice.Amount_NonRefundable;
            return ttl;

        }
        //THIS THE MAX COMMISSION PAYABLE
        private decimal Get_Total_Commission_System_Percent()
        {
            decimal ttlCommissionPct =
                    Get_System_Commission_Percent();

            return ttlCommissionPct;
        }

        private decimal Get_Total_Commission_Seller_Percent()
        {
            decimal ttlCommissionPct =
                Get_OwnerSalesman_Commission_Percent() +
                Get_OwnerSuperSalesman_Commission_Percent() +
                Get_OwnerSuperSuperSalesman_Commission_Percent();
            return ttlCommissionPct;
        }

        //this is the expected commission if all salesmen were available
        private decimal Get_Total_Commission_Customer_Percent()
        {
            decimal ttlCommissionPct =
                Get_OwnerSalesman_Commission_Percent() +
                Get_CustomerSuperSalesman_Commission_Percent() +
                Get_CustomerSuperSuperSalesman_Commission_Percent();

            return ttlCommissionPct;

        }







        //THIS THE MAX COMMISSION PAYABLE
        private decimal Get_Total_Commission_System_Amount_Refundable_Expected()
        {
            decimal ttl = Get_Total_Commission_System_Percent() * Total_Product_Payment_For_Invoice.Amount_Refundable;
            return ttl;
        }

        private decimal Get_Total_Commission_Seller_Amount_Refundable_Expected()
        {
            decimal ttl = Get_Total_Commission_Seller_Percent() * Total_Product_Payment_For_Invoice.Amount_Refundable;
            return ttl;
        }

        private decimal Get_Total_Commission_Customer_Amount_Refundable_Expected()
        {
            decimal ttl = Get_Total_Commission_Customer_Percent() * Total_Product_Payment_For_Invoice.Amount_Refundable;
            return ttl;
        }


        #endregion



        #region Total Product Commission Refundable




        public decimal Get_Total_Commission_Product_Amount_Refundable()
        {
            decimal total = 0;
            total += Get_Total_Commission_Customer_Amount_Refundable();
            total += Get_Total_Commission_Seller_Amount_Refundable();
            total += Get_Total_Commission_System_Amount_Refundable();

            return total;
        }

        public decimal Get_Total_Commission_Customer_Amount_Refundable()
        {
            decimal total = 0;
            total += CustomerSalesmanCommission.Amount_Refundable;
            total += SuperCustomerSalesmanCommission.Amount_Refundable;
            total += SuperSuperCustomerSalesmanCommission.Amount_Refundable;
            return total;

        }
        public decimal Get_Total_Commission_Seller_Amount_Refundable()
        {
            decimal total = 0;
            total += OwnerSalesmanCommission.Amount_Refundable;
            total += SuperOwnerSalesmanCommission.Amount_Refundable;
            total += SuperSuperOwnerSalesmanCommission.Amount_Refundable;
            return total;
        }


        public decimal Get_Total_Commission_System_Amount_Refundable()
        {
            decimal total = 0;
            total += System_Commission_For_Product.Amount_Refundable;
            return total;
        }

        public static decimal Get_Maximum_Commission_Product_Percent()
        {
            decimal ttlComm =
                BuySellDoc.Get_OwnerSalesman_Commission_Percent() +
                BuySellDoc.Get_OwnerSuperSalesman_Commission_Percent() +
                BuySellDoc.Get_OwnerSuperSuperSalesman_Commission_Percent() +
                BuySellDoc.Get_CustomerSalesman_Commission_Percent() +
                BuySellDoc.Get_CustomerSuperSalesman_Commission_Percent() +
                BuySellDoc.Get_CustomerSuperSuperSalesman_Commission_Percent() +
                BuySellDoc.Get_System_Commission_Percent();
            return ttlComm;

        }



        #endregion


        #region Get Total Product Commission Non-Refundable


        public decimal Get_Total_Commission_Product_Amount_NonRefundable()
        {
            decimal total = 0;
            total += Get_Total_Commission_Customer_Amount_NonRefundable();
            total += Get_Total_Commission_Seller_Amount_NonRefundable();
            total += Get_Total_Commission_System_Amount_NonRefundable();




            return total;
        }

        public decimal Get_Total_Commission_Customer_Amount_NonRefundable()
        {
            decimal total = 0;
            total += CustomerSalesmanCommission.Amount_NonRefundable;
            total += SuperCustomerSalesmanCommission.Amount_NonRefundable;
            total += SuperSuperCustomerSalesmanCommission.Amount_NonRefundable;
            return total;

        }
        public decimal Get_Total_Commission_Seller_Amount_NonRefundable()
        {
            decimal total = 0;
            total += OwnerSalesmanCommission.Amount_NonRefundable;
            total += SuperOwnerSalesmanCommission.Amount_NonRefundable;
            total += SuperSuperOwnerSalesmanCommission.Amount_NonRefundable;
            return total;
        }


        public decimal Get_Total_Commission_System_Amount_NonRefundable()
        {
            decimal total = 0;
            total += System_Commission_For_Product.Amount_NonRefundable;
            return total;
        }



        #endregion



        #region Total Payments for Product and Delivery


        /// <summary>
        /// this stores the total payment accepted for the product. Some is Refundable and some is non refundable
        /// this will be set when the buysell order is confirmed. All commissions will then be paid from here in the
        /// same ratio of the Refundable and nonrefundable amounts.
        /// </summary>
        public virtual PaymentsComplex Total_Product_Payment_For_Invoice { get; set; }
        public virtual PaymentsComplex Total_Delivery_Payment_For_Invoice { get; set; }



        public decimal Total_Payment_For_Invoice_Refundable() { return Total_Product_Payment_For_Invoice.Amount_Refundable + Total_Delivery_Payment_For_Invoice.Amount_Refundable; }
        public decimal Total_Payment_For_Invoice_NonRefundable() { return Total_Product_Payment_For_Invoice.Amount_NonRefundable + Total_Delivery_Payment_For_Invoice.Amount_NonRefundable; }
        public decimal Total_Payment_For_Invoice_Both() { return Total_Payment_For_Invoice_Refundable() + Total_Payment_For_Invoice_NonRefundable(); }


        /// This will always be the maximum commission
        /// </summary>
        public virtual PaymentsComplex Total_Commissions_Payable_By_Owner { get; set; }

        /// <summary>
        /// This will always be the maximum commission
        /// </summary>
        public virtual PaymentsComplex Total_Commissions_Payable_By_Deliveryman { get; set; }



        #endregion


        #region Customer Salesmen


        /// <summary>
        /// This is the commission paid to the CustomerSalesmanCommission
        /// </summary>
        public virtual PaymentsComplex CustomerSalesmanCommission { get; set; }
        public virtual PaymentsComplex SuperCustomerSalesmanCommission { get; set; }
        public virtual PaymentsComplex SuperSuperCustomerSalesmanCommission { get; set; }

        public static decimal Get_CustomerSalesman_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_CustomerSalesman;
        }
        public static decimal Get_CustomerSuperSalesman_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_Customer_Super_Salesman;
        }
        public static decimal Get_CustomerSuperSuperSalesman_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_Customer_Super_Super_Salesman;
        }

        #endregion

        #region Owner Salesmen

        /// <summary>
        /// This is the commission paid to the OwnerSalesmanCommission
        /// </summary>
        public virtual PaymentsComplex OwnerSalesmanCommission { get; set; }
        public virtual PaymentsComplex SuperOwnerSalesmanCommission { get; set; }
        public virtual PaymentsComplex SuperSuperOwnerSalesmanCommission { get; set; }

        public static decimal Get_OwnerSalesman_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_OwnerSalesman;
        }
        public static decimal Get_OwnerSuperSalesman_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_Owner_Super_Salesman;
        }
        public static decimal Get_OwnerSuperSuperSalesman_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_Owner_Super_Super_Salesman;
        }


        #endregion

        #region Deliveryman Salesmen


        /// <summary>
        /// This is the commission paid to the DelivermanSalesman
        /// </summary>
        public virtual PaymentsComplex DeliverymanSalesmanCommission { get; set; }
        public virtual PaymentsComplex SuperDeliverymanSalesmanCommission { get; set; }
        public virtual PaymentsComplex SuperSuperDeliverymanSalesmanCommission { get; set; }


        public decimal Get_DeliverymanSalesman_Commission_Percent()
        {
            decimal ttlComm = SalesCommissionClass.CommissionPct_DeliverymanSalesman;
            return ttlComm;

        }

        public decimal Get_DeliverymanSuperSalesman_Commission_Percent()
        {
            decimal ttlComm = SalesCommissionClass.CommissionPct_Owner_Super_Salesman;
            return ttlComm;

        }

        public decimal Get_DeliverymanSuperSuperSalesman_Commission_Percent()
        {
            decimal ttlComm = SalesCommissionClass.CommissionPct_Owner_Super_Super_Salesman;
            return ttlComm;

        }


        public decimal Get_Total_Commission_Freight_Percent_Expected()
        {
            decimal total = 0;
            total += Get_Total_Commission_Deliveryman_Percent();
            total += Get_Total_Commission_System_Percent();

            return total;
        }

        private decimal Get_Total_Commission_Deliveryman_Percent()
        {
            decimal total = 0;
            total += Get_DeliverymanSalesman_Commission_Percent();
            total += Get_DeliverymanSuperSalesman_Commission_Percent();
            total += Get_DeliverymanSuperSuperSalesman_Commission_Percent();

            return total;
        }





        #endregion


        #region System commissions


        /// <summary>
        /// This is the commission paid to the System
        /// </summary>
        public virtual PaymentsComplex System_Commission_For_Product { get; set; }
        public virtual PaymentsComplex System_Commission_For_Freight { get; set; }

        public static decimal Get_System_Commission_Percent()
        {
            return SalesCommissionClass.CommissionPct_System;
        }

        #endregion


        #region Total Invoice


        /// <summary>
        /// this is the product commission + freight commission
        /// </summary>
        public decimal Get_Total_Commission_Invoice_Amount_Refundable()
        {

            return Get_Total_Commission_Product_Amount_Refundable() + Total_Delivery_Payment_For_Invoice.Amount_Refundable;
        }



        #endregion

        #region Freight
        /// <summary>
        /// This is the total commission actually payable by the BuySellDoc
        /// </summary>
        /// <returns></returns>
        public decimal Get_Actual_Commission_Payable_On_Freight_Amount()
        {
            decimal ttlComm = DeliverymanSalesmanCommission.Amount_Refundable + System_Commission_For_Freight.Amount_Refundable;
            return ttlComm;

        }


        public decimal Get_Actual_Extra_Commission_Earned_By_System_On_Freight_Amount()
        {
            decimal maxComm = Get_Maximum_Commission_Chargeable_On_Freight_Amount();
            decimal actualComm = Get_Actual_Commission_Payable_On_Freight_Amount();

            if (maxComm == 0)
                throw new Exception("Maximum Commission Chargeable On Freight Amount is 0");

            return maxComm;

        }



        public decimal Get_Maximum_Commission_Chargeable_On_Freight_Amount()
        {
            decimal ttlComm = SalesCommissionClass.TotalCommissionOnFreight_Amount(Freight_Accepted_Refundable);
            return ttlComm;

        }

        #endregion


        #region Commission checks

        public void ErrorCheckForCommission()
        {
            //check_TotalCommission_Is_Greater_Than_Total_Allowed();
            ////check_GetTotalCommissionEarnedBySystem_Amount_is_Greater_Than_Zero();
            //check_If_OwnerSalesman_Commission_Pct__Zero_Then_Amount_Zero();
            //check_If_CustomerSalesman_Commission_Pct__Zero_Then_Amount_Zero();
            //check_If_DeliverymanSalesman_Commission_Pct__Zero_Then_Amount_Zero();
        }

        private void calculateCommissions()
        {
            throw new NotImplementedException();
        }

        //private void check_GetTotalCommissionEarnedBySystem_Amount_is_Greater_Than_Zero()
        //{
        //    if (Get_Actual_Extra_Commission_Earned_By_System_On_TotalSale_Amount() > 0)
        //        return;

        //    throw new Exception("Total Commission earned by system is zero.");

        //}

        private void check_TotalCommission_Is_Greater_Than_Total_Allowed()
        {
            ////decimal totalCommissions = Get_Actual_Commission_Payable_On_TotalSale_Amount();
            //decimal maxCommAllowed = SalesCommissionClass.Commission_Payable_On_Invoice_Amount(Total_Product_Refundable, Freight_Accepted_Refundable);
            //if (Get_Total_Commission_Invoice_Amount_Refundable() > maxCommAllowed)
            //    throw new Exception("Commissions are less.");

        }

        private void check_If_OwnerSalesman_Commission_Pct__Zero_Then_Amount_Zero()
        {
            if (OwnerSalesmanCommission.Percent == 0)
            {
                if (OwnerSalesmanCommission.Amount_Refundable != 0)
                {
                    throw new Exception("Owner Salesman Commission Pct is zero but ammount is not Zero.");
                }
            }
            else
            {
                if (OwnerSalesmanCommission.Amount_Refundable != 0)
                {
                    return;
                }
                else
                {
                    //it is possible the amount is zero but the commission percent is not. Eg. if freight is zero
                    //then the max commission amount will still be a number, but of zero.
                }


            }


        }

        private void check_If_CustomerSalesman_Commission_Pct__Zero_Then_Amount_Zero()
        {
            if (CustomerSalesmanCommission.Percent == 0)
            {
                if (CustomerSalesmanCommission.Amount_Refundable != 0)
                {
                    throw new Exception("Customer Salesman Commission Pct is zero but ammount is not Zero.");
                }
            }
            else
            {
                if (CustomerSalesmanCommission.Amount_Refundable != 0)
                {
                    return;
                }
                //it is possible the amount is zero but the commission percent is not. Eg. if freight is zero
                //then the max commission amount will still be a number, but of zero.

            }


        }

        private void check_If_DeliverymanSalesman_Commission_Pct__Zero_Then_Amount_Zero()
        {
            if (DeliverymanSalesmanCommission.Percent == 0)
            {
                if (DeliverymanSalesmanCommission.Amount_Refundable != 0)
                {
                    throw new Exception("Deliveryman Salesman Commission Pct is zero but ammount is not Zero.");
                }
            }
            else
            {
                if (DeliverymanSalesmanCommission.Amount_Refundable != 0)
                {
                    return;
                }
                //it is possible the amount is zero but the commission percent is not. Eg. if freight is zero
                //then the max commission amount will still be a number, but of zero.

            }


        }

        #endregion




    }



}
