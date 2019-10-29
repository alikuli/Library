
using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.CashNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Complex;
using System;


namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {

        #region Deliveryman

        /// <summary>
        /// This is always max.
        /// Not the percent is of the deliveryman's sale amount
        /// use calculate_Commissions
        /// </summary>
        /// <param name="bsd"></param>
        private void set_Deliveryman_Commission(BuySellDoc bsd)
        {
            bsd.Total_Delivery_Payment_For_Invoice.Amount_Refundable = bsd.Freight_Accepted_Refundable;
            bsd.Total_Delivery_Payment_For_Invoice.Percent = bsd.Get_Total_Commission_Freight_Percent_Expected();

        }



        private void reset_DeliverymansCommissionAndFields(BuySellDoc bsd)
        {
            if (!bsd.FreightOfferTrxAcceptedId.IsNullOrWhiteSpace())
            {
                FreightOfferTrx freightOfferTrx = FreightOfferTrxBiz.Find(bsd.FreightOfferTrxAcceptedId);
                freightOfferTrx.IsNullThrowException();
                //freightOfferTrx.OfferAcceptedByDeliveryman = new BoolDateAndByComplex();
                freightOfferTrx.OfferAcceptedByOwner = new BoolDateAndByComplex();
                FreightOfferTrxBiz.Update(freightOfferTrx);

                bsd.FreightOfferTrxAcceptedId = null;
                bsd.FreightOfferTrxAccepted = null;

            }

            bsd.DeliverymanSalesman = null;
            bsd.DeliverymanSalesmanId = null;
            bsd.DeliverymanSalesmanCommission = new PaymentsComplex();

            bsd.SuperDeliverymanSalesmanId = null;
            bsd.SuperDeliverymanSalesman = null;
            bsd.SuperDeliverymanSalesmanCommission = new PaymentsComplex();

            bsd.SuperSuperDeliverymanSalesmanId = null;
            bsd.SuperSuperDeliverymanSalesman = null;
            bsd.SuperSuperDeliverymanSalesmanCommission = new PaymentsComplex();

            //bsd.Total_Charged_To_Deliveryman = new PaymentsComplex();
            bsd.Total_Delivery_Payment_For_Invoice = new PaymentsComplex();

            bsd.CourierAcceptedByBuyerAndSeller = new BoolDateAndByComplex();
            bsd.CourierComingToPickUp = new BoolDateAndByComplex();

        }

        private void setBuySellPayment_Delivery(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            decimal currBalance_NonRefundable = parm.GlobalObject.Money_User.Non_Refundable.MoneyAmount;
            decimal currBalance_Refundable = parm.GlobalObject.Money_User.Refundable.MoneyAmount;

        }

        private void set_Commissions_For_DeliverySalesman(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            throw new NotImplementedException();
        }
        private void set_Payment_For_Deliveryman(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            decimal currBalance_NonRefundable = parm.GlobalObject.Money_User.Non_Refundable.MoneyAmount;
            decimal currBalance_Refundable = parm.GlobalObject.Money_User.Refundable.MoneyAmount;

            decimal commissionAmount_Refundable = SalesCommissionClass.TotalCommissionOnFreight_Amount(bsd.Freight_Accepted_Refundable);

            IsEnoughBalanceForUser(bsd, parm, commissionAmount_Refundable, 0);

            bsd.Total_Delivery_Payment_For_Invoice.Amount_Refundable = commissionAmount_Refundable;



        }
        private void checkBalanceOfDeliveryman_Delivery(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {

            //decimal commissionAmount_NonRefundable = SalesCommissionClass.TotalCommissionOnFreight_Amount(bsd.TotalRs_Non_Refundable);
            //decimal currBalance_NonRefundable = parm.GlobalObject.Money_User.Non_Refundable.MoneyAmount;

            decimal currBalance_Refundable = parm.GlobalObject.Money_User.Refundable.MoneyAmount;
            decimal totalBalanceRequired = bsd.Freight_Accepted_Refundable + bsd.InsuranceRequired;
            decimal amountShort = totalBalanceRequired - currBalance_Refundable;

            if (amountShort > 0)
            {
                string err = string.Format("You do not have enough amount to continue. You require Rs{0:N2}, your refundable balance is Rs{1:N2}, and you are short by Rs{2:N2}. Please refill your account.",
                    totalBalanceRequired,
                    currBalance_Refundable,
                    amountShort);
                throw new Exception(err);
            }




        }

        private void set_Super_DeliverymansSalesMan_Commission(BuySellDoc bsd)
        {


            if (bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperDeliverymanSalesmanCommission = new PaymentsComplex();
                return;
            }

            //check to see if Super Deliveryman Salesman Exists
            if (bsd.SuperDeliverymanSalesmanId.IsNull())
            {
                bsd.SuperDeliverymanSalesmanCommission = new PaymentsComplex();
                return;
            }

            //reset if ownerSalesmanId is null


            decimal commissionPct = bsd.Get_DeliverymanSuperSalesman_Commission_Percent();
            bsd.SuperDeliverymanSalesmanCommission.Percent = commissionPct;
            if (bsd.Total_Product_Refundable * commissionPct == 0)
                return;

            bsd.SuperDeliverymanSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Delivery_Payment_For_Invoice.Amount_Refundable / 100;


        }

        private void set_Super_Super_DeliverymansSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperDeliverymanSalesmanCommission = new PaymentsComplex();
                return;
            }

            if (bsd.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperDeliverymanSalesmanCommission = new PaymentsComplex();
                return;
            }

            //check to see if Super Deliveryman Salesman Exists
            if (bsd.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperDeliverymanSalesmanCommission = new PaymentsComplex();
                return;
            }



            decimal commissionPct = bsd.Get_DeliverymanSuperSuperSalesman_Commission_Percent();

            bsd.SuperSuperDeliverymanSalesmanCommission.Percent = commissionPct;

            if (bsd.Total_Product_Refundable * commissionPct == 0)
                return;

            bsd.SuperSuperDeliverymanSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Delivery_Payment_For_Invoice.Amount_Refundable / 100;


        }
        /// use calculate_Commissions
        private void set_DeliverymansSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.DeliverymanSalesmanCommission = new PaymentsComplex();
                return;

            }

            decimal commissionPct = bsd.Get_DeliverymanSalesman_Commission_Percent();
            bsd.DeliverymanSalesmanCommission.Percent = bsd.DeliverymanSalesmanCommission.Percent;

            if (commissionPct == 0)
                return;

            if (bsd.Total_Delivery_Payment_For_Invoice.Total == 0)
                return;


            bsd.DeliverymanSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Delivery_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.DeliverymanSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Delivery_Payment_For_Invoice.Amount_NonRefundable / 100;

        }


        #endregion




        #region Customer
        private void set_Super_Super_CustomersSalesMan_Commission(BuySellDoc bsd)
        {
            //check to see if Customer Salesman Exists
            if (bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperCustomerSalesmanCommission = new PaymentsComplex();
                return;
            }

            //check to see if Super Customer Salesman Exists
            if (bsd.SuperCustomerSalesmanId.IsNull())
            {
                bsd.SuperSuperCustomerSalesmanCommission = new PaymentsComplex();
                return;
            }

            if (bsd.SuperSuperCustomerSalesmanId.IsNull())
            {
                bsd.SuperSuperCustomerSalesmanCommission = new PaymentsComplex();
                return;
            }

            decimal commissionPct = BuySellDoc.Get_CustomerSuperSuperSalesman_Commission_Percent();
            bsd.SuperSuperCustomerSalesmanCommission.Percent = commissionPct;

            if (bsd.Total_Product_Refundable * commissionPct == 0)
                return;

            bsd.SuperSuperCustomerSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.SuperSuperCustomerSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;



        }

        private void set_Super_CustomersSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperCustomerSalesmanCommission = new PaymentsComplex();
                return;
            }

            //check to see if Super Customer Salesman Exists
            if (bsd.SuperCustomerSalesman.IsNull())
            {
                bsd.SuperCustomerSalesmanCommission = new PaymentsComplex();
                return;
            }

            //reset if ownerSalesmanId is null

            decimal commissionPct = BuySellDoc.Get_CustomerSuperSalesman_Commission_Percent();
            bsd.SuperCustomerSalesmanCommission.Percent = commissionPct;
            if (bsd.Total_Product_Refundable * commissionPct == 0)
                return;

            bsd.SuperCustomerSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.SuperCustomerSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;


        }

        private void reset_CustomersCommissionAndFields(BuySellDoc bsd)
        {
            bsd.CustomerSalesmanCommission = new PaymentsComplex();
            bsd.SuperCustomerSalesmanCommission = new PaymentsComplex();
            bsd.SuperSuperCustomerSalesmanCommission = new PaymentsComplex();
        }

        private void set_CustomersSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.CustomerSalesmanCommission = new PaymentsComplex();
                return;
            }

            bsd.CustomerSalesmanCommission.Percent = BuySellDoc.Get_CustomerSalesman_Commission_Percent();
            decimal commissionPct = bsd.CustomerSalesmanCommission.Percent;

            if (commissionPct == 0)
                return;

            if (bsd.Total_Product_Payment_For_Invoice.Total == 0)
                return;

            //bsd.Total_Product_Payment.Amount_Refundable  contains the total commission. So we need to divide by total commission percent
            //and then multiply with commission for customer salesmamn
            bsd.CustomerSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.CustomerSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;


        }



        #endregion



        #region Owner/Seller

        private void reset_OwnersCommissionAndFields(BuySellDoc bsd)
        {

            bsd.OwnerSalesmanCommission = new PaymentsComplex();
            bsd.SuperOwnerSalesmanCommission = new PaymentsComplex();
            bsd.SuperSuperOwnerSalesmanCommission = new PaymentsComplex();
            //bsd.Total_Charged_To_Owner = new PaymentsComplex();


        }


        private void set_Super_OwnersSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperOwnerSalesmanCommission = new PaymentsComplex();
                return;
            }

            //check to see if Super Owner Salesman Exists
            if (bsd.SuperOwnerSalesmanId.IsNull())
            {
                bsd.SuperOwnerSalesmanCommission = new PaymentsComplex();
                return;
            }

            //reset if ownerSalesmanId is null

            decimal commissionPct = BuySellDoc.Get_OwnerSuperSalesman_Commission_Percent();
            bsd.SuperOwnerSalesmanCommission.Percent = commissionPct;
            if (bsd.Total_Product_Refundable * commissionPct == 0)
                return;

            bsd.SuperOwnerSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.SuperOwnerSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;


        }


        //**********************************************************************************************************
        // Commission for Sales
        //**********************************************************************************************************


        //this will always be the max commission chargeable
        //use calculate_Commissions
        private void set_Owners_MaxCommission(BuySellDoc bsd)
        {
            //this should be on the TotalSaleLessFreight
            bsd.Total_Commissions_Payable_By_Owner.Percent = BuySellDoc.Get_Maximum_Commission_Product_Percent();

            if (bsd.Total_Commissions_Payable_By_Owner.Percent == 0)
                return;
            if (bsd.Total_Product_Payment_For_Invoice.Amount_Refundable == 0)
                return;

            bsd.Total_Commissions_Payable_By_Owner.Amount_Refundable = bsd.Total_Product_Payment_For_Invoice.Amount_Refundable * bsd.Total_Commissions_Payable_By_Owner.Percent / 100;
            bsd.Total_Commissions_Payable_By_Owner.Amount_NonRefundable = bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable * bsd.Total_Commissions_Payable_By_Owner.Percent / 100;
        }


        //use calculate_Commissions

        private void set_OwnersSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.OwnerSalesmanCommission = new PaymentsComplex();
                return;
            }

            bsd.OwnerSalesmanCommission.Percent = BuySellDoc.Get_OwnerSalesman_Commission_Percent();
            //bsd.OwnerSalesmanCommission.Percent = SalesCommissionClass.CommissionPct_OwnerSalesman;
            decimal commissionPct = bsd.OwnerSalesmanCommission.Percent;


            if (commissionPct == 0)
                return;

            if (bsd.Total_Product_Payment_For_Invoice.Total == 0)
                return;

            bsd.OwnerSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.OwnerSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;

        }


        private void set_Super_Super_OwnersSalesMan_Commission(BuySellDoc bsd)
        {
            //check to see if Super Owner Salesman Exists
            if (bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperOwnerSalesmanCommission = new PaymentsComplex();
                return;
            }

            //check to see if Super Owner Salesman Exists
            if (bsd.SuperOwnerSalesmanId.IsNull())
            {
                bsd.SuperSuperOwnerSalesmanCommission = new PaymentsComplex();
                return;
            }

            if (bsd.SuperSuperOwnerSalesmanId.IsNull())
            {
                bsd.SuperSuperOwnerSalesmanCommission = new PaymentsComplex();
                return;
            }


            decimal commissionPct = BuySellDoc.Get_OwnerSuperSuperSalesman_Commission_Percent();
            bsd.SuperSuperOwnerSalesmanCommission.Percent = commissionPct;

            if (bsd.Total_Product_Refundable * commissionPct == 0)
                return;

            bsd.SuperSuperOwnerSalesmanCommission.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.SuperSuperOwnerSalesmanCommission.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;


        }




        #endregion


        #region Misc

        #endregion


        #region System

        /// use calculate_Commissions
        private void set_System_Commission_Product(BuySellDoc bsd)
        {
            bsd.System_Commission_For_Product.Percent = SalesCommissionClass.CommissionPct_System;
            decimal commissionPct = bsd.System_Commission_For_Product.Percent;

            if (commissionPct == 0)
                return;

            if (bsd.Total_Product_Payment_For_Invoice.Total == 0)
                return;


            bsd.System_Commission_For_Product.Amount_Refundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.System_Commission_For_Product.Amount_NonRefundable = commissionPct * bsd.Total_Product_Payment_For_Invoice.Amount_NonRefundable / 100;

        }




        private void set_System_Commission_Freight(BuySellDoc bsd)
        {

            bsd.System_Commission_For_Freight.Percent = SalesCommissionClass.CommissionPct_System;
            decimal commissionPct = bsd.System_Commission_For_Freight.Percent;

            if (commissionPct == 0)
                return;

            if (bsd.Total_Delivery_Payment_For_Invoice.Total == 0)
                return;


            bsd.System_Commission_For_Freight.Amount_Refundable = commissionPct * bsd.Total_Delivery_Payment_For_Invoice.Amount_Refundable / 100;
            bsd.System_Commission_For_Freight.Amount_NonRefundable = commissionPct * bsd.Total_Delivery_Payment_For_Invoice.Amount_NonRefundable / 100;

        }

        private void reset_SystemsCommission(BuySellDoc buySellDoc)
        {
            buySellDoc.System_Commission_For_Product = new PaymentsComplex();
            buySellDoc.System_Commission_For_Freight = new PaymentsComplex();


        }

        #endregion


        /// <summary>
        /// Use this so that all the amounts are in sync.
        /// </summary>
        /// <param name="bsd"></param>
        private void set_Commissions(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
            //    return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
                return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.OptedOutOfSystem)
            {
                //see sale ConfirmRequest
            }
            else
            {
                set_Owners_MaxCommission(bsd);

                set_OwnersSalesMan_Commission(bsd);
                set_Super_OwnersSalesMan_Commission(bsd);
                set_Super_Super_OwnersSalesMan_Commission(bsd);

                set_CustomersSalesMan_Commission(bsd);
                set_Super_CustomersSalesMan_Commission(bsd);
                set_Super_Super_CustomersSalesMan_Commission(bsd);

                set_Deliveryman_Commission(bsd);

                set_DeliverymansSalesMan_Commission(bsd);
                set_Super_DeliverymansSalesMan_Commission(bsd);
                set_Super_Super_DeliverymansSalesMan_Commission(bsd);

                set_System_Commission_Freight(bsd);
                set_System_Commission_Product(bsd);


            }


        }

        private void set_Payment_For_Product(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            //first check to see if product can be paid for in token, if yes, then how much?


            decimal currBalance_NonRefundable = parm.GlobalObject.Money_User.Non_Refundable.MoneyAmount;
            decimal currBalance_Refundable = parm.GlobalObject.Money_User.Refundable.MoneyAmount;
            decimal payment_Refundable = bsd.Total_Product_Refundable;
            decimal payment_NonRefundable = bsd.Total_Product_Non_Refundable;
            bool isNonRefundablePaymentAllowed = payment_NonRefundable > 0;

            CashBalanceVM cashBalance = new CashBalanceVM(currBalance_Refundable,currBalance_NonRefundable);
            CashDistributionEngine cde = new CashDistributionEngine(
                cashBalance,
                payment_NonRefundable,
                payment_Refundable,
                isNonRefundablePaymentAllowed);


            if (cde.CanBuy())
            {
                string msg = string.Format("You have a total balance of: Rs{0:N2}. Money = {1:N2}, Non Refundable Tokens {2:N2}. The expected spending will be as follows: Money = Rs{3:N2}, Tokens = Rs{4:N2}",
                    cde.CashBalance.Total(),
                    cde.CashBalance.Refundable,
                    cde.CashBalance.NonRefundable,
                    cde.Refundable_Final,
                    cde.NonRefundable_Final);

                cde.Message = msg;
            }
            else
            {
                throw new Exception("You have a total balance of: Rs" + cde.CashBalance.Total().ToString("N2") + ". You do not have sufficent money to buy.");

            }


            decimal total_Non_Refundable_Payment = cde.NonRefundable_Final;
            decimal total_Refundable_Payment = cde.Refundable_Final;


            bsd.Total_Product_Payment_For_Invoice.SetToTodaysDate(
                total_Refundable_Payment,
                total_Non_Refundable_Payment,
                UserId,
                UserName);
        }



        private bool IsEnoughBalanceForUser(BuySellDoc bsd, ControllerCreateEditParameter parm, decimal amountRqrd_Refundable, decimal amountRqrd_NonRefundable)
        {
            if (bsd.BuySellDocumentTypeEnum != BuySellDocumentTypeENUM.Purchase)
                return true;

            decimal currBalance_NonRefundable = parm.GlobalObject.Money_User.Non_Refundable.MoneyAmount;
            decimal currBalance_Refundable = parm.GlobalObject.Money_User.Refundable.MoneyAmount;

            decimal amntShort_NonRefundable = 0;
            decimal additional_Refundable_Amount_required = 0;
            decimal amountRqrd_Refundable_Plus_ShortNonRefundable = amountRqrd_Refundable;

            if (amountRqrd_NonRefundable > 0)
            {
                amntShort_NonRefundable = amountRqrd_NonRefundable - currBalance_NonRefundable;
            }

            if (amntShort_NonRefundable > 0)
            {
                amountRqrd_Refundable_Plus_ShortNonRefundable += amntShort_NonRefundable;
            }
            additional_Refundable_Amount_required = amountRqrd_Refundable_Plus_ShortNonRefundable - currBalance_Refundable;

            if (additional_Refundable_Amount_required > 0)
            {

                string err = string.Format("You do not have enough balance. Your refundable balance is Rs{0:N2}, but you need Rs{1:N2}. You require an additional Rs{2:N2}",
                    currBalance_Refundable,
                    amountRqrd_Refundable_Plus_ShortNonRefundable,
                    additional_Refundable_Amount_required);
                throw new Exception(err);
            }

            return true;



        }
        /// <summary>
        /// this is checked when Seller accepts the courier
        /// </summary>
        /// <param name="bsd"></param>
        /// <param name="parm"></param>




        private void if_Reject_reset_Commissions_And_Fields(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
            {
                reset_CustomersCommissionAndFields(buySellDoc);
                reset_OwnersCommissionAndFields(buySellDoc);
                reset_DeliverymansCommissionAndFields(buySellDoc);
                reset_SystemsCommission(buySellDoc);
            }

        }










        //use calculate_Commissions













    }
}
