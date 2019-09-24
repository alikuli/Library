using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;

namespace UowLibrary.SuperLayerNS
{


    public partial class SuperBiz
    {
        #region Penalty


        private void createPenaltyTrx(BuySellDoc buySellDoc, Person systemPerson, string reason)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                PersonPayingPenalty ppp;
                IPenaltyClass penalty = PenaltyController.GetPenalty(buySellDoc, out ppp);

                if (penalty.IsNull())
                    return;

                //we want to delete the maximum commission.
                //payments will be made to people who exist.
                //balance will go to the system
                decimal totalPaymentAmount = penalty.PenaltyAmount();

                if (totalPaymentAmount == 0)
                    return;

                //get all the persons involved
                //create a cash transaction
                //here we should know who is paying

                calculateAndDistributePenaltyPayments_Controller(buySellDoc, systemPerson, ppp, penalty, totalPaymentAmount);
                createAndSavePenalty(buySellDoc, reason, ppp, totalPaymentAmount);


            }

        }
        /// <summary>
        /// This is the controller that directs how the payments are made.
        /// </summary>
        /// <param name="buySellDoc"></param>
        /// <param name="systemPerson"></param>
        /// <param name="ppp"></param>
        /// <param name="penalty"></param>
        /// <param name="totalPaymentAmount"></param>
        private void calculateAndDistributePenaltyPayments_Controller(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, IPenaltyClass penalty, decimal totalPaymentAmount)
        {
            if (totalPaymentAmount == 0)
                return;

            switch (penalty.WhoPaysWhoEnum)
            {
                case WhoPaysWhoENUM.OwnerPaysCustomer:
                    ownerPaysCustomer_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.OwnerPaysDeliveryMan:
                    ownerPaysDeliveryMan_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.CustomerPaysOwner:
                    customerPaysOwner_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.CustomerPaysDeliveryman:
                    customerPaysDeliveryman_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.DeliverymanPaysOwner:
                    deliverymanPaysOwner_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.DeliverymanPaysCustomer:
                    deliverymanPaysCustomer_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.Unknown:
                default:
                    throw new Exception("I Dont know who to pay!");
            }
        }

        private void ownerPaysDeliveryMan_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Owner_Payment(buySellDoc, ppp, totalPaymentAmount);
            calculateCommissions(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_Penalty_When_Deliveryman_Is_Receiving(buySellDoc, ppp, totalPaymentAmount);
            calculate_ExtraCommission(buySellDoc, systemPerson, ppp, totalPaymentAmount);

        }

        private void ownerPaysCustomer_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            calculate_Owner_Payment(buySellDoc, ppp, totalPaymentAmount);
            calculateCommissions(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_Customer_Amount_When_Receiving_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(buySellDoc, systemPerson, ppp, totalPaymentAmount);


        }

        private void customerPaysOwner_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Customer_Payment(buySellDoc, ppp, totalPaymentAmount);
            calculateCommissions(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_Owner_Amount_When_Receiving_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(buySellDoc, systemPerson, ppp, totalPaymentAmount);
        }

        private void deliverymanPaysCustomer_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Deliveryman_Payment(buySellDoc, ppp, totalPaymentAmount);
            calculateCommissions(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_Customer_Amount_When_Receiving_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(buySellDoc, systemPerson, ppp, totalPaymentAmount);

        }

        private void customerPaysDeliveryman_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Customer_Payment(buySellDoc, ppp, totalPaymentAmount);
            calculateCommissions(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_Penalty_When_Deliveryman_Is_Receiving(buySellDoc, ppp, totalPaymentAmount);
            calculate_ExtraCommission(buySellDoc, systemPerson, ppp, totalPaymentAmount);


        }

        private void deliverymanPaysOwner_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Deliveryman_Payment(buySellDoc, ppp, totalPaymentAmount);
            calculateCommissions(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_Owner_Amount_When_Receiving_Penalty(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(buySellDoc, systemPerson, ppp, totalPaymentAmount);
        }


        private void calculateCommissions(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_OwnerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_Super_OwnerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_Super_Super_OwnerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);

            calculate_CustomerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_Super_CustomerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_Super_Super_CustomerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);

            calculate_DeliverySalesman_Penalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_Super_DeliverySalesman_Penalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_Super_Super_DeliverySalesman_Penalty(buySellDoc, ppp, totalPaymentAmount);

            calculate_SystemCommission_OnSale(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_SystemCommission_OnFreight(buySellDoc, systemPerson, ppp, totalPaymentAmount);
        }


        private void calculate_Penalty_When_Deliveryman_Is_Receiving(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            decimal maxPctCommissionChargeableToFreight_Pct = buySellDoc.Get_Maximum_Commission_Estimate_Chargeable_On_Total_Invoice_Percent();
            decimal maxCommission_Amount = totalPaymentAmount * maxPctCommissionChargeableToFreight_Pct / 100;
            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {

                ppp.Deliveryman.Person = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
                ppp.Deliveryman.IsNullThrowException();
                ppp.Deliveryman.Comment = "Deliveryman";

                ppp.Deliveryman.Amount = totalPaymentAmount - maxCommission_Amount;

                if (ppp.Deliveryman.Amount > 0)
                    ppp.Deliveryman.Percent = Math.Round(ppp.Deliveryman.Amount / totalPaymentAmount * 100, 2);

            }
        }



        private void calculate_Customer_Amount_When_Receiving_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            //get the maximum commission payable on the freight and sale
            decimal maxPayableAccordingToMaxCommission_Amount = getMaxCommissionPayable_Amount(buySellDoc, totalPaymentAmount);

            decimal totalPaidOutToCustomerDeliverymanOwner = get_TotalPaidOut_To_Deliveryman_Owner_Customer(ppp);

            ppp.Customer.Person = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
            ppp.Customer.IsNullThrowException();
            ppp.Customer.Comment = "Customer";
            ppp.Customer.Amount = totalPaymentAmount - maxPayableAccordingToMaxCommission_Amount;

            if (ppp.Customer.Amount != 0)
                ppp.Customer.Percent = ppp.Customer.Amount / totalPaymentAmount;

        }





        private void calculate_Owner_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.From.Person = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
            ppp.From.Person.IsNullThrowException();
            ppp.From.Amount = totalPaymentAmount;
            ppp.From.Comment = "Owner";

        }



        //private static decimal calculate_AdjustedFreightAmountAgainstWhichFreightCommissionWillBeCharged(BuySellDoc buySellDoc, decimal totalPaymentAmount)
        //{
        //    decimal totalAmountFixedForFreight_Percent = buySellDoc.GetPercentFreightToTotalInvoice(totalPaymentAmount);
        //    decimal adjustedFreightAmountAgainstWhichCommissionWillBeCharged = totalAmountFixedForFreight_Percent * totalPaymentAmount;
        //    return adjustedFreightAmountAgainstWhichCommissionWillBeCharged;
        //}

        private void calculate_Customer_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.From.Person = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
            ppp.From.Person.IsNullThrowException();
            ppp.From.Amount = totalPaymentAmount;
            ppp.From.Comment = "Customer";

        }

        private void calculate_Deliveryman_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            buySellDoc.DeliverymanId.IsNullOrWhiteSpaceThrowException();
            ppp.From.Person = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
            ppp.From.Person.IsNullThrowException();
            ppp.From.Amount = totalPaymentAmount;
            ppp.From.Comment = "Deliveryman";

        }









        private void calculate_Owner_Amount_When_Receiving_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            ppp.Owner.Clear();
            //get the maximum commission payable on the freight and sale
            decimal maxPayableAccordingToMaxCommission_Amount = getMaxCommissionPayable_Amount(buySellDoc, totalPaymentAmount);


            ppp.Owner.Person = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
            ppp.Owner.Person.IsNullThrowException();
            ppp.Owner.Comment = "Owner";
            ppp.Owner.Amount = totalPaymentAmount - maxPayableAccordingToMaxCommission_Amount;

            if (ppp.Owner.Amount != 0)
                ppp.Owner.Percent = ppp.Owner.Amount / totalPaymentAmount;
        }




        private void createAndSavePenalty(BuySellDoc buySellDoc, string reason, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.SelfErrorCheck();

            PenaltyHeader ph = new PenaltyHeader(buySellDoc, reason, totalPaymentAmount, ppp.From.Person, true);
            PenaltyHeaderBiz.Create(ph);

            if (!ppp.Owner.IsNull())
            {
                PenaltyTrx owner = new PenaltyTrx(ppp.Owner.Person.Id, ppp.Owner.Amount, ppp.Owner.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, owner);

            }


            if (!ppp.Deliveryman.IsNull())
            {
                PenaltyTrx deliveryman = new PenaltyTrx(ppp.Deliveryman.Person.Id, ppp.Deliveryman.Amount, ppp.Deliveryman.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, deliveryman);

            }


            if (!ppp.Salesman_Owner.IsNull())
            {
                PenaltyTrx salesman_Owner = new PenaltyTrx(ppp.Salesman_Owner.Person.Id, ppp.Salesman_Owner.Amount, ppp.Salesman_Owner.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Owner);

            }


            if (!ppp.Salesman_Customer.IsNull())
            {
                PenaltyTrx salesman_Customer = new PenaltyTrx(ppp.Salesman_Customer.Person.Id, ppp.Salesman_Customer.Amount, ppp.Salesman_Customer.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Customer);

            }


            if (!ppp.Salesman_Deliveryman.IsNull())
            {
                PenaltyTrx salesman_Deliveryman = new PenaltyTrx(ppp.Salesman_Deliveryman.Person.Id, ppp.Salesman_Deliveryman.Amount, ppp.Salesman_Deliveryman.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Deliveryman);

            }


            if (!ppp.System_Freight.IsNull())
            {
                PenaltyTrx system_Freight = new PenaltyTrx(ppp.System_Freight.Person.Id, ppp.System_Freight.Amount, ppp.System_Freight.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, system_Freight);

            }


            if (!ppp.System_Sale.IsNull())
            {
                PenaltyTrx system_Sale = new PenaltyTrx(ppp.System_Sale.Person.Id, ppp.System_Sale.Amount, ppp.System_Sale.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, system_Sale);

            }


            if (!ppp.System_ExtraCommission.IsNull())
            {
                PenaltyTrx system_ExtraCommission = new PenaltyTrx(ppp.System_ExtraCommission.Person.Id, ppp.System_ExtraCommission.Amount, ppp.System_ExtraCommission.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, system_ExtraCommission);

            }


            PenaltyHeaderBiz.SaveChanges();
        }



        private static decimal get_TotalPaidOutToSystem(PersonPayingPenalty ppp)
        {

            decimal totalPaidOut = ppp.System_Freight.Amount +
                                   ppp.System_Sale.Amount;

            return totalPaidOut;
        }



        private static decimal get_TotalPaidOut(PersonPayingPenalty ppp)
        {

            decimal totalPaidOut = get_TotalPaidOutToSalesMen(ppp) +
                                   get_TotalPaidOut_To_Deliveryman_Owner_Customer(ppp) +
                                   get_TotalPaidOutToSystem(ppp);

            return totalPaidOut;
        }
        private static decimal get_TotalPaidOutToSalesMen(PersonPayingPenalty ppp)
        {

            decimal totalPaidOut = ppp.Salesman_Customer.Amount +
                                   ppp.Salesman_Deliveryman.Amount +
                                   ppp.Salesman_Owner.Amount;

            return totalPaidOut;
        }

        private static decimal get_TotalPaidOut_To_Deliveryman_Owner_Customer(PersonPayingPenalty ppp)
        {

            decimal totalPaidOut = ppp.Deliveryman.Amount +
                                   ppp.Customer.Amount +
                                   ppp.Owner.Amount;
            return totalPaidOut;
        }

        private static decimal calculate_ExtraCommission(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            decimal totalPaidOut = get_TotalPaidOut(ppp);

            decimal extraCommission = totalPaymentAmount - totalPaidOut;


            if (extraCommission < 0)
                throw new Exception("Somthing wrong with your commission calcultions");

            if (extraCommission > 0)
            {
                ppp.System_ExtraCommission.Person = systemPerson;
                ppp.System_ExtraCommission.IsNullThrowException();

                ppp.System_ExtraCommission.Comment = "This is Extra Left Over Money";
                ppp.System_ExtraCommission.Amount = extraCommission;
                ppp.System_ExtraCommission.Percent = extraCommission / totalPaymentAmount * 100;

            }

            return extraCommission;
        }

        private static decimal get_ActualCommissionPayableForFreight_Amount(PersonPayingPenalty ppp)
        {
            decimal Actual_CommissionPayableForFreight_Amount =
                ppp.Salesman_Deliveryman.Amount +
                ppp.Deliveryman.Amount +
                ppp.System_Freight.Amount;

            return Actual_CommissionPayableForFreight_Amount;
        }

        private static decimal get_ActualCommissionPayableOnSaleFreight_Amount(PersonPayingPenalty ppp)
        {
            decimal Actual_CommissionPayableForSaleFreight_Amount =
                ppp.Salesman_Owner.Amount +
                ppp.Salesman_Customer.Amount +
                ppp.System_Sale.Amount;

            return Actual_CommissionPayableForSaleFreight_Amount;
        }

        private static decimal getMaxCommissionPayable_Amount(BuySellDoc buySellDoc, decimal totalPaymentAmount)
        {
            decimal maxCommissionPayable_Pct =
                    buySellDoc.Get_Maximum_Commission_Chargeable_On_SaleWithoutFreight_To_SalesPeople_And_System_Percent() +
                    buySellDoc.Get_Maximum_Commission_Chargeable_On_Freight_TO_SalesPeople_And_System_Percent();

            decimal maxCommissionPayable_Amount = Math.Round(totalPaymentAmount * maxCommissionPayable_Pct / 100, 2);
            return maxCommissionPayable_Amount;
        }

        private void calculate_Deliveryman_Penalty_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            decimal maxPctCommissionChargeableToFreight_Pct = buySellDoc.Get_Maximum_Commission_Chargeable_On_Freight_TO_SalesPeople_And_System_Percent();
            decimal maxCommission_Amount = totalPaymentAmount * maxPctCommissionChargeableToFreight_Pct;

            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {

                ppp.Deliveryman.Person = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
                ppp.Deliveryman.IsNullThrowException();
                ppp.Deliveryman.Comment = "Deliveryman";

                ppp.Deliveryman.Amount = totalPaymentAmount - maxCommission_Amount;

                if (ppp.Deliveryman.Amount > 0)
                    ppp.Deliveryman.Percent = Math.Round(ppp.Deliveryman.Amount / totalPaymentAmount * 100, 2);

            }
        }

        private void calculate_SystemCommission_OnSale(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.System_Sale.Clear();
            ppp.System_Sale.Person = systemPerson;
            ppp.System_Sale.IsNullThrowException();
            ppp.System_Sale.Comment = "System Commission (Sale)";
            ppp.System_Sale.Percent = buySellDoc.System_Commission_For_SaleWithoutFreight.Percent;

            if (ppp.System_Sale.Percent == 0)
                return;

            ppp.System_Sale.Amount = Math.Round(totalPaymentAmount * ppp.System_Sale.Percent / 100, 2);

        }

        private void calculate_SystemCommission_OnFreight(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            ppp.System_Freight.Clear();
            ppp.System_Freight.Person = systemPerson;
            ppp.System_Freight.IsNullThrowException();
            ppp.System_Freight.Comment = "System Commission (Freight)";
            ppp.System_Freight.Percent = buySellDoc.System_Commission_For_Freight.Percent;

            if (ppp.System_Freight.Percent == 0)
                return;

            ppp.System_Freight.Amount = Math.Round(totalPaymentAmount * ppp.System_Freight.Percent / 100, 2);
        }




        private void calculate_Super_Super_DeliverySalesman_Penalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!buySellDoc.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.Super_Super_Salesman_Deliveryman.Clear();
                    ppp.Super_Super_Salesman_Deliveryman.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.SuperSuperDeliverymanSalesmanId);
                    ppp.Super_Super_Salesman_Deliveryman.IsNullThrowException();
                    ppp.Super_Super_Salesman_Deliveryman.Comment = "Super Super Deliveryman Salesman";
                    ppp.Super_Super_Salesman_Deliveryman.Percent = buySellDoc.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Super_Super_Salesman_Deliveryman.Percent == 0)
                        return;

                    ppp.Super_Super_Salesman_Deliveryman.Amount = Math.Round(ppp.Super_Super_Salesman_Deliveryman.Percent * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_Super_DeliverySalesman_Penalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!buySellDoc.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.Super_Salesman_Deliveryman.Clear();
                    ppp.Super_Salesman_Deliveryman.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.SuperDeliverymanSalesmanId);
                    ppp.Super_Salesman_Deliveryman.IsNullThrowException();
                    ppp.Super_Salesman_Deliveryman.Comment = "Super Deliveryman Salesman";
                    ppp.Super_Salesman_Deliveryman.Percent = buySellDoc.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Super_Salesman_Deliveryman.Percent == 0)
                        return;

                    ppp.Super_Salesman_Deliveryman.Amount = Math.Round(ppp.Super_Salesman_Deliveryman.Percent * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_Super_Super_CustomerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.SuperSuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Super_Salesman_Customer.Clear();
                ppp.Super_Super_Salesman_Customer.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.SuperSuperCustomerSalesmanId);
                ppp.Super_Super_Salesman_Customer.Person.IsNullThrowException();
                ppp.Super_Super_Salesman_Customer.Comment = "Super Super Customer Salesman";
                ppp.Super_Super_Salesman_Customer.Percent = buySellDoc.CustomerSalesmanCommission.Percent;

                if (ppp.Super_Super_Salesman_Customer.Percent == 0)
                    return;
                ppp.Super_Super_Salesman_Customer.Amount = Math.Round(totalPaymentAmount * ppp.Super_Super_Salesman_Customer.Percent / 100, 2);
            }
        }

        private void calculate_Super_CustomerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.SuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Salesman_Customer.Clear();
                ppp.Super_Salesman_Customer.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.SuperCustomerSalesmanId);
                ppp.Super_Salesman_Customer.Person.IsNullThrowException();
                ppp.Super_Salesman_Customer.Comment = "Super Customer Salesman";
                ppp.Super_Salesman_Customer.Percent = buySellDoc.CustomerSalesmanCommission.Percent;

                if (ppp.Super_Salesman_Customer.Percent == 0)
                    return;

                ppp.Super_Salesman_Customer.Amount = Math.Round(totalPaymentAmount * ppp.Super_Salesman_Customer.Percent / 100, 2);
            }
        }

        private void calculate_Super_Super_OwnerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.SuperSuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Super_Salesman_Owner.Clear();
                ppp.Super_Super_Salesman_Owner.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.SuperSuperOwnerSalesmanId);
                ppp.Super_Super_Salesman_Owner.IsNullThrowException();
                ppp.Super_Super_Salesman_Owner.Comment = "Super Super Owner Salesman";
                ppp.Super_Super_Salesman_Owner.Percent = buySellDoc.OwnerSalesmanCommission.Percent;

                if (ppp.Super_Super_Salesman_Owner.Percent == 0)
                    return;

                ppp.Super_Super_Salesman_Owner.Amount = Math.Round(totalPaymentAmount * ppp.Super_Super_Salesman_Owner.Percent / 100, 2);
            }
        }

        private void calculate_Super_OwnerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.SuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Salesman_Owner.Clear();
                ppp.Super_Salesman_Owner.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.SuperOwnerSalesmanId);
                ppp.Super_Salesman_Owner.IsNullThrowException();
                ppp.Super_Salesman_Owner.Comment = "Super Owner Salesman";
                ppp.Super_Salesman_Owner.Percent = buySellDoc.OwnerSalesmanCommission.Percent;

                if (ppp.Super_Salesman_Owner.Percent == 0)
                    return;

                ppp.Super_Salesman_Owner.Amount = Math.Round(totalPaymentAmount * ppp.Super_Salesman_Owner.Percent / 100, 2);
            }
        }



        private void calculate_DeliverySalesman_Penalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!buySellDoc.DeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.Salesman_Deliveryman.Clear();
                    ppp.Salesman_Deliveryman.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.DeliverymanSalesmanId);
                    ppp.Salesman_Deliveryman.IsNullThrowException();
                    ppp.Salesman_Deliveryman.Comment = "Deliveryman Salesman";
                    ppp.Salesman_Deliveryman.Percent = buySellDoc.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Salesman_Deliveryman.Percent == 0)
                        return;

                    ppp.Salesman_Deliveryman.Amount = Math.Round(ppp.Salesman_Deliveryman.Percent * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_CustomerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Salesman_Customer.Clear();
                ppp.Salesman_Customer.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.CustomerSalesmanId);
                ppp.Salesman_Customer.Person.IsNullThrowException();
                ppp.Salesman_Customer.Comment = "Customer Salesman";
                ppp.Salesman_Customer.Percent = buySellDoc.CustomerSalesmanCommission.Percent;

                if (ppp.Salesman_Customer.Percent == 0)
                    return;
                ppp.Salesman_Customer.Amount = Math.Round(totalPaymentAmount * ppp.Salesman_Customer.Percent / 100, 2);
            }
        }

        private void calculate_OwnerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            if (!buySellDoc.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Salesman_Owner.Clear();
                ppp.Salesman_Owner.Person = SalesmanBiz.GetPersonForPlayer(buySellDoc.OwnerSalesmanId);
                ppp.Salesman_Owner.IsNullThrowException();
                ppp.Salesman_Owner.Comment = "Owner Salesman";
                ppp.Salesman_Owner.Percent = buySellDoc.OwnerSalesmanCommission.Percent;

                if (ppp.Salesman_Owner.Percent == 0)
                    return;

                ppp.Salesman_Owner.Amount = Math.Round(totalPaymentAmount * ppp.Salesman_Owner.Percent / 100, 2);
            }
        }



        #endregion


    }
}
