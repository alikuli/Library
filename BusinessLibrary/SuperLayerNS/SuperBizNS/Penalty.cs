using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PeopleMessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;

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

                calculateAndDistributePenaltyPayments_Controller(buySellDoc, systemPerson, ppp, penalty, totalPaymentAmount);
                createAndSavePenalty(buySellDoc, reason, ppp, totalPaymentAmount);

                //get all the persons involved
                //create a cash transaction
                //here we should know who is paying

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
            calculate_CustomerSalesmanPenalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_DeliverySalesman_Penalty(buySellDoc, ppp, totalPaymentAmount);
            calculate_SystemCommission_OnSale(buySellDoc, systemPerson, ppp, totalPaymentAmount);
            calculate_SystemCommission_OnFreight(buySellDoc, systemPerson, ppp, totalPaymentAmount);
        }



        private void calculate_Penalty_When_Deliveryman_Is_Receiving(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            decimal maxPctCommissionChargeableToFreight_Pct = buySellDoc.Get_Maximum_Commission_Estimate_Chargeable_On_Total_Invoice_Percent();
            decimal maxCommission_Amount = totalPaymentAmount * maxPctCommissionChargeableToFreight_Pct / 100;
            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {

                ppp.Deliveryman = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
                ppp.Deliveryman.IsNullThrowException();
                ppp.Deliveryman_Comment = "You are the Deliveryman";

                ppp.Deliveryman_RecivedAmount = totalPaymentAmount - maxCommission_Amount;

                if (ppp.Deliveryman_RecivedAmount > 0)
                    ppp.Deliveryman_Pct_Of_Total = Math.Round(ppp.Deliveryman_RecivedAmount / totalPaymentAmount * 100, 2);

            }
        }



        private void calculate_Customer_Amount_When_Receiving_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            //get the maximum commission payable on the freight and sale
            decimal maxPayableAccordingToMaxCommission_Amount = getMaxCommissionPayable_Amount(buySellDoc, totalPaymentAmount);

            decimal totalPaidOutToCustomerDeliverymanOwner = get_TotalPaidOut_To_Deliveryman_Owner_Customer(ppp);

            ppp.Customer = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
            ppp.Customer.IsNullThrowException();
            ppp.Customer_Comment = "You are the Customer";
            ppp.Customer_RecivedAmount = totalPaymentAmount - maxPayableAccordingToMaxCommission_Amount;

            if (ppp.Customer_RecivedAmount != 0)
                ppp.Customer_Pct_Of_Total = ppp.Customer_RecivedAmount / totalPaymentAmount;

        }





        private void calculate_Owner_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.PersonFrom = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
            ppp.PersonFrom.IsNullThrowException();
            ppp.PersonFrom_PaymentAmount = totalPaymentAmount;
            ppp.PersonFrom_Comment = "You are the Seller";

        }



        //private static decimal calculate_AdjustedFreightAmountAgainstWhichFreightCommissionWillBeCharged(BuySellDoc buySellDoc, decimal totalPaymentAmount)
        //{
        //    decimal totalAmountFixedForFreight_Percent = buySellDoc.GetPercentFreightToTotalInvoice(totalPaymentAmount);
        //    decimal adjustedFreightAmountAgainstWhichCommissionWillBeCharged = totalAmountFixedForFreight_Percent * totalPaymentAmount;
        //    return adjustedFreightAmountAgainstWhichCommissionWillBeCharged;
        //}

        private void calculate_Customer_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.PersonFrom = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
            ppp.PersonFrom.IsNullThrowException();
            ppp.PersonFrom_PaymentAmount = totalPaymentAmount;
            ppp.PersonFrom_Comment = "You are the Customer";

        }

        private void calculate_Deliveryman_Payment(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            buySellDoc.DeliverymanSalesmanId.IsNullOrWhiteSpaceThrowException();
            ppp.PersonFrom = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
            ppp.PersonFrom.IsNullThrowException();
            ppp.PersonFrom_PaymentAmount = totalPaymentAmount;
            ppp.PersonFrom_Comment = "You are the Customer";

        }









        private void calculate_Owner_Amount_When_Receiving_Penalty(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            ppp.ClearOwner();
            //get the maximum commission payable on the freight and sale
            decimal maxPayableAccordingToMaxCommission_Amount = getMaxCommissionPayable_Amount(buySellDoc, totalPaymentAmount);


            ppp.Owner = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
            ppp.Owner.IsNullThrowException();
            ppp.Owner_Comment = "You are the Owner";
            ppp.Owner_RecivedAmount = totalPaymentAmount - maxPayableAccordingToMaxCommission_Amount;

            if (ppp.Owner_RecivedAmount != 0)
                ppp.Owner_Pct_Of_Total = ppp.Owner_RecivedAmount / totalPaymentAmount;
        }




        private void createAndSavePenalty(BuySellDoc buySellDoc, string reason, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.SelfErrorCheck();

            PenaltyHeader ph = new PenaltyHeader(buySellDoc, reason, totalPaymentAmount, ppp.PersonFrom, true);
            PenaltyHeaderBiz.Create(ph);

            //if (!ppp.PersonFrom.IsNull())
            //{
            //    PenaltyTrx personFrom = new PenaltyTrx(ppp.PersonFrom.Id, ppp.PersonFrom_PaymentAmount, ppp.PersonFrom_Comment, true);
            //    PenaltyHeaderBiz.AddPenaltyTrx(ph, personFrom);

            //}
            if (!ppp.Owner.IsNull())
            {
                PenaltyTrx owner = new PenaltyTrx(ppp.Owner.Id, ppp.Owner_RecivedAmount, ppp.Owner_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, owner);

            }
            if (!ppp.Deliveryman.IsNull())
            {
                PenaltyTrx deliveryman = new PenaltyTrx(ppp.Deliveryman.Id, ppp.Deliveryman_RecivedAmount, ppp.Deliveryman_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, deliveryman);

            }
            if (!ppp.Salesman_Owner.IsNull())
            {
                PenaltyTrx salesman_Owner = new PenaltyTrx(ppp.Salesman_Owner.Id, ppp.Salesman_Owner_RecivedAmount, ppp.Salesman_Owner_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Owner);

            }
            if (!ppp.Salesman_Customer.IsNull())
            {
                PenaltyTrx salesman_Customer = new PenaltyTrx(ppp.Salesman_Customer.Id, ppp.Salesman_Customer_RecivedAmount, ppp.Salesman_Customer_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Customer);

            }
            if (!ppp.Salesman_Deliveryman.IsNull())
            {
                PenaltyTrx salesman_Deliveryman = new PenaltyTrx(ppp.Salesman_Deliveryman.Id, ppp.Salesman_Deliveryman_RecivedAmount, ppp.Salesman_Deliveryman_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Deliveryman);

            }
            if (!ppp.System_Freight.IsNull())
            {
                PenaltyTrx system_Freight = new PenaltyTrx(ppp.System_Freight.Id, ppp.System_Freight_RecivedAmount, ppp.System_Freight_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, system_Freight);

            }
            if (!ppp.System_Sale.IsNull())
            {
                PenaltyTrx system_Sale = new PenaltyTrx(ppp.System_Sale.Id, ppp.System_Sale_RecivedAmount, ppp.System_Sale_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, system_Sale);

            }
            if (!ppp.System_ExtraCommission.IsNull())
            {
                PenaltyTrx system_ExtraCommission = new PenaltyTrx(ppp.System_ExtraCommission.Id, ppp.System_ExtraCommission_RecivedAmount, ppp.System_ExtraCommission_Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, system_ExtraCommission);

            }


            PenaltyHeaderBiz.SaveChanges();
        }



        private static decimal get_TotalPaidOutToSystem(PersonPayingPenalty ppp)
        {

            decimal totalPaidOut = ppp.System_Freight_RecivedAmount +
                                   ppp.System_Sale_RecivedAmount;

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

            decimal totalPaidOut = ppp.Salesman_Customer_RecivedAmount +
                                   ppp.Salesman_Deliveryman_RecivedAmount +
                                   ppp.Salesman_Owner_RecivedAmount;

            return totalPaidOut;
        }

        private static decimal get_TotalPaidOut_To_Deliveryman_Owner_Customer(PersonPayingPenalty ppp)
        {

            decimal totalPaidOut = ppp.Deliveryman_RecivedAmount +
                                   ppp.Customer_RecivedAmount +
                                   ppp.Owner_RecivedAmount;
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
                ppp.System_ExtraCommission = systemPerson;
                ppp.System_ExtraCommission.IsNullThrowException();

                ppp.System_ExtraCommission_Comment = "This is Extra Left Over Money";
                ppp.System_ExtraCommission_RecivedAmount = extraCommission;
                ppp.System_ExtraCommission_Pct_Of_Total = extraCommission / totalPaymentAmount * 100;

            }

            return extraCommission;
        }

        private static decimal get_ActualCommissionPayableForFreight_Amount(PersonPayingPenalty ppp)
        {
            decimal Actual_CommissionPayableForFreight_Amount =
                ppp.Salesman_Deliveryman_RecivedAmount +
                ppp.Deliveryman_RecivedAmount +
                ppp.System_Freight_RecivedAmount;

            return Actual_CommissionPayableForFreight_Amount;
        }

        private static decimal get_ActualCommissionPayableOnSaleFreight_Amount(PersonPayingPenalty ppp)
        {
            decimal Actual_CommissionPayableForSaleFreight_Amount =
                ppp.Salesman_Owner_RecivedAmount +
                ppp.Salesman_Customer_RecivedAmount +
                ppp.System_Sale_RecivedAmount;

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

                ppp.Deliveryman = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
                ppp.Deliveryman.IsNullThrowException();
                ppp.Deliveryman_Comment = "You are the Deliveryman";

                ppp.Deliveryman_RecivedAmount = totalPaymentAmount - maxCommission_Amount;

                if (ppp.Deliveryman_RecivedAmount > 0)
                    ppp.Deliveryman_Pct_Of_Total = Math.Round(ppp.Deliveryman_RecivedAmount / totalPaymentAmount * 100, 2);

            }
        }

        private void calculate_SystemCommission_OnSale(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.ClearSystem_Sale();
            ppp.System_Sale = systemPerson;
            ppp.System_Sale.IsNullThrowException();
            ppp.System_Sale_Comment = "This is System Commission (Sale)";
            ppp.System_Sale_Pct_Of_Total = buySellDoc.System_Commission_For_SaleWithoutFreight.Percent;

            if (ppp.System_Sale_Pct_Of_Total == 0)
                return;

            ppp.System_Sale_RecivedAmount = Math.Round(totalPaymentAmount * ppp.System_Sale_Pct_Of_Total / 100, 2);

        }

        private void calculate_SystemCommission_OnFreight(BuySellDoc buySellDoc, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            ppp.ClearSystem_Freight();
            ppp.System_Freight = systemPerson;
            ppp.System_Freight.IsNullThrowException();
            ppp.System_Freight_Comment = "This Is System Commission (Freight)";
            ppp.System_Freight_Pct_Of_Total = buySellDoc.System_Commission_For_Freight.Percent;

            if (ppp.System_Freight_Pct_Of_Total == 0)
                return;

            ppp.System_Freight_RecivedAmount = Math.Round(totalPaymentAmount * ppp.System_Freight_Pct_Of_Total / 100, 2);
        }





        private void calculate_DeliverySalesman_Penalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!buySellDoc.DeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.ClearSalesman_Deliveryman();
                    ppp.Salesman_Deliveryman = SalesmanBiz.GetPersonForPlayer(buySellDoc.DeliverymanSalesmanId);
                    ppp.Salesman_Deliveryman.IsNullThrowException();
                    ppp.Salesman_Deliveryman_Comment = "You are the Deliveryman Salesman";
                    ppp.Salesman_Deliveryman_Pct_Of_Total = buySellDoc.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Salesman_Deliveryman_Pct_Of_Total == 0)
                        return;

                    ppp.Salesman_Deliveryman_RecivedAmount = Math.Round(ppp.Salesman_Deliveryman_Pct_Of_Total * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_CustomerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!buySellDoc.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.ClearSalesman_Customer();
                ppp.Salesman_Customer = SalesmanBiz.GetPersonForPlayer(buySellDoc.CustomerSalesmanId);
                ppp.Salesman_Customer.IsNullThrowException();
                ppp.Salesman_Customer_Comment = "You are the Customer Salesman";
                ppp.Salesman_Customer_Pct_Of_Total = buySellDoc.CustomerSalesmanCommission.Percent;

                if (ppp.Salesman_Customer_Pct_Of_Total == 0)
                    return;
                ppp.Salesman_Customer_RecivedAmount = Math.Round(totalPaymentAmount * ppp.Salesman_Customer_Pct_Of_Total / 100, 2);
            }
        }

        private void calculate_OwnerSalesmanPenalty(BuySellDoc buySellDoc, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            if (!buySellDoc.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.ClearSalesman_Owner();
                ppp.Salesman_Owner = SalesmanBiz.GetPersonForPlayer(buySellDoc.OwnerSalesmanId);
                ppp.Salesman_Owner.IsNullThrowException();
                ppp.Salesman_Owner_Comment = "You are the Owner Salesman";
                ppp.Salesman_Owner_Pct_Of_Total = buySellDoc.OwnerSalesmanCommission.Percent;

                if (ppp.Salesman_Owner_Pct_Of_Total == 0)
                    return;

                ppp.Salesman_Owner_RecivedAmount = Math.Round(totalPaymentAmount * ppp.Salesman_Owner_Pct_Of_Total / 100, 2);
            }
        }

        private void createMessageFor(RejectCancelDeleteInbetweenClass rcdbc, BuySellDoc buySellDoc)
        {
            string fromPersonId = "";
            string toPersonId = "";
            Person fromPerson = null;
            List<string> listOfToPeople = new List<string>();

            string buySellDocId = buySellDoc.Id;
            Person ownerPerson = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
            Person customerPerson = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);

            ownerPerson.IsNullThrowException();
            customerPerson.IsNullThrowException();
            switch (buySellDoc.BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    fromPersonId = ownerPerson.Id;
                    fromPerson = ownerPerson;
                    toPersonId = customerPerson.Id;
                    listOfToPeople.Add(toPersonId);
                    break;

                case BuySellDocumentTypeENUM.Purchase:
                    toPersonId = ownerPerson.Id;
                    fromPersonId = customerPerson.Id;
                    fromPerson = customerPerson;
                    listOfToPeople.Add(toPersonId);

                    break;
                case BuySellDocumentTypeENUM.Delivery:
                    Person deliveryPerson = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
                    deliveryPerson.IsNullThrowException();

                    fromPersonId = deliveryPerson.Id;
                    fromPerson = deliveryPerson;
                    listOfToPeople.Add(ownerPerson.Id);
                    listOfToPeople.Add(customerPerson.Id);
                    break;

                case BuySellDocumentTypeENUM.Unknown:
                default:
                    throw new Exception("Unknown document type");
            }

            Message message = new Message(
                fromPersonId,
                fromPerson,
                listOfToPeople,
                rcdbc.Subject,
                rcdbc.Comment,
                MessageENUM.Free,
                null);

            if (buySellDoc.Messages.IsNull())
                buySellDoc.Messages = new List<Message>();

            buySellDoc.Messages.Add(message);
            message.BuySellDocId = buySellDoc.Id;

            message.ListOfToPeopleId.IsNullOrEmptyThrowException();
            foreach (var pplId in message.ListOfToPeopleId)
            {
                Person person = PersonBiz.Find(pplId);
                person.IsNullThrowException();

                PeopleMessage pplMsg = new PeopleMessage();
                pplMsg.IsNullThrowException();
                pplMsg.PersonId = pplId;
                pplMsg.Person = person;
                pplMsg.MessageId = message.Id;
                PeopleMessageBiz.Create(pplMsg);

            }
            MessageBiz.Create(message);
        }


        #endregion


    }
}
