using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;

namespace UowLibrary.SuperLayerNS
{


    public partial class SuperBiz
    {


        private void createPenaltyTrx(BuySellDoc bsd, Person systemPerson, string reason)
        {
            PersonPayingPenalty ppp;
            IPenaltyClass penalty = PenaltyController.GetPenalty(bsd, out ppp);

            if (penalty.IsNull())
                return;

            //we want to delete the maximum commission.
            //payments will be made to people who exist.
            //balance will go to the system
            decimal totalPaymentAmount = penalty.PenaltyAmount();

            if (totalPaymentAmount == 0)
                return;

            
            
            switch (bsd.BuySellDocStateModifierEnum)
            {
                case BuySellDocStateModifierENUM.Unknown:
                    break;
                case BuySellDocStateModifierENUM.Reject:
                    break;
                case BuySellDocStateModifierENUM.Cancel:
                    break;
                case BuySellDocStateModifierENUM.Accept:
                    break;
                case BuySellDocStateModifierENUM.SeeAddress:

                    if (bsd.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Sale)
                    {
                        bsd.IsShowFullAddressTo_Seller = true;
                    }
                    if (bsd.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Purchase)
                    {
                        bsd.IsShowFullAddressTo_Customer = true;
                    }
                    
                    break;

                case BuySellDocStateModifierENUM.OptOutOfSystem:
                    {
                        if (bsd.BuySellDocumentTypeEnum != BuySellDocumentTypeENUM.Purchase)
                            return;

                        if (bsd.BuySellDocStateEnum != BuySellDocStateENUM.RequestUnconfirmed)
                            return;

                        bsd.OptedOutOfSystem.SetToTodaysDate(UserName, UserId);

                    }
                    break;
                default:
                    break;
            }

            calculateAndDistributePenaltyPayments_Controller(bsd, systemPerson, ppp, penalty, totalPaymentAmount);
            createAndSavePenalty(bsd, reason, ppp, totalPaymentAmount, GetGlobalObject());


            //if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel || bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.SeeAddress)
            //{
                //PersonPayingPenalty ppp;
                //IPenaltyClass penalty = PenaltyController.GetPenalty(bsd, out ppp);

                //if (penalty.IsNull())
                //    return;

                ////we want to delete the maximum commission.
                ////payments will be made to people who exist.
                ////balance will go to the system
                //decimal totalPaymentAmount = penalty.PenaltyAmount();

                //if (totalPaymentAmount == 0)
                //    return;

                //get all the persons involved
                //create a cash transaction
                //here we should know who is paying
                //if(bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.SeeAddress)
                //{
                //    //update the bsd
                //    ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
                //    parm.Entity = bsd as ICommonWithId;

                //    if (bsd.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Sale)
                //    {
                //        bsd.IsShowFullAddressTo_Seller = true;
                //        parm.GlobalObject = GetGlobalObject();
                //        //for some reason I am losing the value of bsd.BuySellDocumentTypeEnum... it becomes
                //        //purchase when we do GetGlobalObject.
                //        //so I  am fixing it here.
                //        bsd.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Sale;
                //        BuySellDocBiz.Update(parm);
                //    }
                //    if (bsd.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Purchase)
                //    {
                //        parm.GlobalObject = GetGlobalObject();

                //        bsd.IsShowFullAddressTo_Customer = true;
                //        //for some reason I am losing the value of bsd.BuySellDocumentTypeEnum... it becomes
                //        //purchase when we do GetGlobalObject.
                //        //so I  am fixing it here.
                //        bsd.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Purchase;
                //        BuySellDocBiz.Update(parm);
                //    }
                //}
                //calculateAndDistributePenaltyPayments_Controller(bsd, systemPerson, ppp, penalty, totalPaymentAmount);
                //createAndSavePenalty(bsd, reason, ppp, totalPaymentAmount, GetGlobalObject());
            //}

        }
        /// <summary>
        /// This is the controller that directs how the payments are made.
        /// </summary>
        /// <param name="bsd"></param>
        /// <param name="systemPerson"></param>
        /// <param name="ppp"></param>
        /// <param name="penalty"></param>
        /// <param name="totalPaymentAmount"></param>
        private void calculateAndDistributePenaltyPayments_Controller(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, IPenaltyClass penalty, decimal totalPaymentAmount)
        {
            if (totalPaymentAmount == 0)
                return;

            switch (penalty.WhoPaysWhoEnum)
            {
                case WhoPaysWhoENUM.OwnerPaysCustomer:
                    ownerPaysCustomer_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.OwnerPaysDeliveryMan:
                    ownerPaysDeliveryMan_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.OwnerPaysSystem: //used for optingOutOfSystem
                    Owner_Pays_System(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;




                case WhoPaysWhoENUM.CustomerPaysOwner:
                    customerPaysOwner_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.CustomerPaysDeliveryman:
                    customerPaysDeliveryman_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.CustomerPaysSystem:
                    customer_Pays_System(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.DeliverymanPaysOwner:
                    deliverymanPaysOwner_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;

                case WhoPaysWhoENUM.DeliverymanPaysCustomer:
                    deliverymanPaysCustomer_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
                    break;


                case WhoPaysWhoENUM.Unknown:
                default:
                    throw new Exception("I Dont know who to pay!");
            }
        }

        private void customer_Pays_System(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Customer_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);
        }

        private void Owner_Pays_System(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Owner_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);
        }

        private void ownerPaysDeliveryMan_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Owner_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_Penalty_When_Deliveryman_Is_Receiving(bsd, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);

        }

        private void ownerPaysCustomer_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            calculate_Owner_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_Customer_Amount_When_Receiving_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);


        }

        private void customerPaysOwner_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Customer_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_Owner_Amount_When_Receiving_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);
        }

        private void deliverymanPaysCustomer_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Deliveryman_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_Customer_Amount_When_Receiving_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);

        }

        private void customerPaysDeliveryman_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Customer_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_Penalty_When_Deliveryman_Is_Receiving(bsd, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);


        }

        private void deliverymanPaysOwner_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_Deliveryman_Payment(bsd, ppp, totalPaymentAmount);
            calculateCommissions(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_Owner_Amount_When_Receiving_Penalty(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_ExtraCommission(bsd, systemPerson, ppp, totalPaymentAmount);
        }


        private void calculateCommissions(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            calculate_OwnerSalesmanPenalty(bsd, ppp, totalPaymentAmount);
            calculate_Super_OwnerSalesmanPenalty(bsd, ppp, totalPaymentAmount);
            calculate_Super_Super_OwnerSalesmanPenalty(bsd, ppp, totalPaymentAmount);

            calculate_CustomerSalesmanPenalty(bsd, ppp, totalPaymentAmount);
            calculate_Super_CustomerSalesmanPenalty(bsd, ppp, totalPaymentAmount);
            calculate_Super_Super_CustomerSalesmanPenalty(bsd, ppp, totalPaymentAmount);

            calculate_DeliverySalesman_Penalty(bsd, ppp, totalPaymentAmount);
            calculate_Super_DeliverySalesman_Penalty(bsd, ppp, totalPaymentAmount);
            calculate_Super_Super_DeliverySalesman_Penalty(bsd, ppp, totalPaymentAmount);

            calculate_SystemCommission_OnSale(bsd, systemPerson, ppp, totalPaymentAmount);
            calculate_SystemCommission_OnFreight(bsd, systemPerson, ppp, totalPaymentAmount);
        }


        private void calculate_Penalty_When_Deliveryman_Is_Receiving(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            decimal maxPctCommissionChargeableToFreight_Pct = bsd.Get_Total_Commission_Freight_Percent_Expected();
            decimal maxCommission_Amount = totalPaymentAmount * maxPctCommissionChargeableToFreight_Pct / 100;
            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
            {

                ppp.Deliveryman.Person = DeliverymanBiz.GetPersonForPlayer(bsd.DeliverymanId);
                ppp.Deliveryman.IsNullThrowException();
                ppp.Deliveryman.Comment = "Deliveryman";

                ppp.Deliveryman.Amount = totalPaymentAmount - maxCommission_Amount;

                if (ppp.Deliveryman.Amount > 0)
                    ppp.Deliveryman.Percent = Math.Round(ppp.Deliveryman.Amount / totalPaymentAmount * 100, 2);

            }
        }

        private decimal getFreightAndProductCommissionAdded(BuySellDoc bsd)
        {
            decimal ttl = (bsd.Get_Total_Commission_Freight_Percent_Expected() + bsd.Get_Total_Commission_Product_Percent_Expected());
            return ttl;
        }

        private void calculate_Customer_Amount_When_Receiving_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            //get the maximum commission payable on the freight and sale
            decimal maxPayableAccordingToMaxCommission_Amount = getFreightAndProductCommissionAdded(bsd) * totalPaymentAmount;

            decimal totalPaidOutToCustomerDeliverymanOwner = get_TotalPaidOut_To_Deliveryman_Owner_Customer(ppp);

            ppp.Customer.Person = CustomerBiz.GetPersonForPlayer(bsd.CustomerId);
            ppp.Customer.IsNullThrowException();
            ppp.Customer.Comment = "Customer";
            ppp.Customer.Amount = totalPaymentAmount - maxPayableAccordingToMaxCommission_Amount;

            if (ppp.Customer.Amount != 0)
                ppp.Customer.Percent = ppp.Customer.Amount / totalPaymentAmount;

        }





        private void calculate_Owner_Payment(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.From.Person = OwnerBiz.GetPersonForPlayer(bsd.OwnerId);
            ppp.From.Person.IsNullThrowException();
            ppp.From.Amount = totalPaymentAmount;
            ppp.From.Comment = "Owner";

        }





        private void calculate_Customer_Payment(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.From.Person = CustomerBiz.GetPersonForPlayer(bsd.CustomerId);
            ppp.From.Person.IsNullThrowException();
            ppp.From.Amount = totalPaymentAmount;
            ppp.From.Comment = "Customer";

        }

        private void calculate_Deliveryman_Payment(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            bsd.DeliverymanId.IsNullOrWhiteSpaceThrowException();
            ppp.From.Person = DeliverymanBiz.GetPersonForPlayer(bsd.DeliverymanId);
            ppp.From.Person.IsNullThrowException();
            ppp.From.Amount = totalPaymentAmount;
            ppp.From.Comment = "Deliveryman";

        }









        private void calculate_Owner_Amount_When_Receiving_Penalty(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            ppp.Owner.Clear();
            //get the maximum commission payable on the freight and sale
            decimal maxPayableAccordingToMaxCommission_Amount = getFreightAndProductCommissionAdded(bsd) * totalPaymentAmount;


            ppp.Owner.Person = OwnerBiz.GetPersonForPlayer(bsd.OwnerId);
            ppp.Owner.Person.IsNullThrowException();
            ppp.Owner.Comment = "Owner";
            ppp.Owner.Amount = totalPaymentAmount - maxPayableAccordingToMaxCommission_Amount;

            if (ppp.Owner.Amount != 0)
                ppp.Owner.Percent = ppp.Owner.Amount / totalPaymentAmount;
        }




        private void createAndSavePenalty(BuySellDoc bsd, string reason, PersonPayingPenalty ppp, decimal totalPaymentAmount, GlobalObject globalObject)
        {
            ppp.SelfErrorCheck();

            PenaltyHeader ph = new PenaltyHeader(bsd, reason, totalPaymentAmount, ppp.From.Person, true);
            PenaltyHeaderBiz.Create(ph);

            if (!ppp.Owner.Person.IsNull())
            {
                PenaltyTrx owner = new PenaltyTrx(ppp.Owner.Person.Id, ppp.Owner.Amount, ppp.Owner.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, owner);

            }


            if (!ppp.Deliveryman.Person.IsNull())
            {
                PenaltyTrx deliveryman = new PenaltyTrx(ppp.Deliveryman.Person.Id, ppp.Deliveryman.Amount, ppp.Deliveryman.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, deliveryman);

            }


            if (!ppp.Salesman_Owner.Person.IsNull())
            {
                PenaltyTrx salesman_Owner = new PenaltyTrx(ppp.Salesman_Owner.Person.Id, ppp.Salesman_Owner.Amount, ppp.Salesman_Owner.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Owner);

            }


            if (!ppp.Salesman_Customer.Person.IsNull())
            {
                PenaltyTrx salesman_Customer = new PenaltyTrx(ppp.Salesman_Customer.Person.Id, ppp.Salesman_Customer.Amount, ppp.Salesman_Customer.Comment, false);
                PenaltyHeaderBiz.AddPenaltyTrx(ph, salesman_Customer);

            }


            if (!ppp.Salesman_Deliveryman.Person.IsNull())
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

        private static decimal calculate_ExtraCommission(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
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

        private static decimal get_Expected_Product_And_Freight_Commissions_Pct_Added(BuySellDoc bsd, decimal totalPaymentAmount)
        {
            decimal maxCommissionPayable_Pct =
                    bsd.Get_Total_Commission_Product_Percent_Expected() +
                    bsd.Get_Total_Commission_Freight_Percent_Expected();

            decimal maxCommissionPayable_Amount = Math.Round(totalPaymentAmount * maxCommissionPayable_Pct / 100, 2);
            return maxCommissionPayable_Amount;
        }

        private void calculate_Deliveryman_Penalty_Payment(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            decimal maxPctCommissionChargeableToFreight_Pct = bsd.Get_Total_Commission_Freight_Percent_Expected();
            decimal maxCommission_Amount = totalPaymentAmount * maxPctCommissionChargeableToFreight_Pct;

            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
            {

                ppp.Deliveryman.Person = DeliverymanBiz.GetPersonForPlayer(bsd.DeliverymanId);
                ppp.Deliveryman.IsNullThrowException();
                ppp.Deliveryman.Comment = "Deliveryman";

                ppp.Deliveryman.Amount = totalPaymentAmount - maxCommission_Amount;

                if (ppp.Deliveryman.Amount > 0)
                    ppp.Deliveryman.Percent = Math.Round(ppp.Deliveryman.Amount / totalPaymentAmount * 100, 2);

            }
        }

        private void calculate_SystemCommission_OnSale(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            ppp.System_Sale.Clear();
            ppp.System_Sale.Person = systemPerson;
            ppp.System_Sale.IsNullThrowException();
            ppp.System_Sale.Comment = "System Commission (Sale)";
            ppp.System_Sale.Percent = bsd.System_Commission_For_Product.Percent;

            if (ppp.System_Sale.Percent == 0)
                return;

            ppp.System_Sale.Amount = Math.Round(totalPaymentAmount * ppp.System_Sale.Percent / 100, 2);

        }

        private void calculate_SystemCommission_OnFreight(BuySellDoc bsd, Person systemPerson, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            ppp.System_Freight.Clear();
            ppp.System_Freight.Person = systemPerson;
            ppp.System_Freight.IsNullThrowException();
            ppp.System_Freight.Comment = "System Commission (Freight)";
            ppp.System_Freight.Percent = bsd.System_Commission_For_Freight.Percent;

            if (ppp.System_Freight.Percent == 0)
                return;

            ppp.System_Freight.Amount = Math.Round(totalPaymentAmount * ppp.System_Freight.Percent / 100, 2);
        }




        private void calculate_Super_Super_DeliverySalesman_Penalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!bsd.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.Super_Super_Salesman_Deliveryman.Clear();
                    ppp.Super_Super_Salesman_Deliveryman.Person = SalesmanBiz.GetPersonForPlayer(bsd.SuperSuperDeliverymanSalesmanId);
                    ppp.Super_Super_Salesman_Deliveryman.IsNullThrowException();
                    ppp.Super_Super_Salesman_Deliveryman.Comment = "Super Super Deliveryman Salesman";
                    ppp.Super_Super_Salesman_Deliveryman.Percent = bsd.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Super_Super_Salesman_Deliveryman.Percent == 0)
                        return;

                    ppp.Super_Super_Salesman_Deliveryman.Amount = Math.Round(ppp.Super_Super_Salesman_Deliveryman.Percent * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_Super_DeliverySalesman_Penalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!bsd.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.Super_Salesman_Deliveryman.Clear();
                    ppp.Super_Salesman_Deliveryman.Person = SalesmanBiz.GetPersonForPlayer(bsd.SuperDeliverymanSalesmanId);
                    ppp.Super_Salesman_Deliveryman.IsNullThrowException();
                    ppp.Super_Salesman_Deliveryman.Comment = "Super Deliveryman Salesman";
                    ppp.Super_Salesman_Deliveryman.Percent = bsd.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Super_Salesman_Deliveryman.Percent == 0)
                        return;

                    ppp.Super_Salesman_Deliveryman.Amount = Math.Round(ppp.Super_Salesman_Deliveryman.Percent * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_Super_Super_CustomerSalesmanPenalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.SuperSuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Super_Salesman_Customer.Clear();
                ppp.Super_Super_Salesman_Customer.Person = SalesmanBiz.GetPersonForPlayer(bsd.SuperSuperCustomerSalesmanId);
                ppp.Super_Super_Salesman_Customer.Person.IsNullThrowException();
                ppp.Super_Super_Salesman_Customer.Comment = "Super Super Customer Salesman";
                ppp.Super_Super_Salesman_Customer.Percent = bsd.CustomerSalesmanCommission.Percent;

                if (ppp.Super_Super_Salesman_Customer.Percent == 0)
                    return;
                ppp.Super_Super_Salesman_Customer.Amount = Math.Round(totalPaymentAmount * ppp.Super_Super_Salesman_Customer.Percent / 100, 2);
            }
        }

        private void calculate_Super_CustomerSalesmanPenalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.SuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Salesman_Customer.Clear();
                ppp.Super_Salesman_Customer.Person = SalesmanBiz.GetPersonForPlayer(bsd.SuperCustomerSalesmanId);
                ppp.Super_Salesman_Customer.Person.IsNullThrowException();
                ppp.Super_Salesman_Customer.Comment = "Super Customer Salesman";
                ppp.Super_Salesman_Customer.Percent = bsd.CustomerSalesmanCommission.Percent;

                if (ppp.Super_Salesman_Customer.Percent == 0)
                    return;

                ppp.Super_Salesman_Customer.Amount = Math.Round(totalPaymentAmount * ppp.Super_Salesman_Customer.Percent / 100, 2);
            }
        }

        private void calculate_Super_Super_OwnerSalesmanPenalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.SuperSuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Super_Salesman_Owner.Clear();
                ppp.Super_Super_Salesman_Owner.Person = SalesmanBiz.GetPersonForPlayer(bsd.SuperSuperOwnerSalesmanId);
                ppp.Super_Super_Salesman_Owner.IsNullThrowException();
                ppp.Super_Super_Salesman_Owner.Comment = "Super Super Owner Salesman";
                ppp.Super_Super_Salesman_Owner.Percent = bsd.OwnerSalesmanCommission.Percent;

                if (ppp.Super_Super_Salesman_Owner.Percent == 0)
                    return;

                ppp.Super_Super_Salesman_Owner.Amount = Math.Round(totalPaymentAmount * ppp.Super_Super_Salesman_Owner.Percent / 100, 2);
            }
        }

        private void calculate_Super_OwnerSalesmanPenalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.SuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Super_Salesman_Owner.Clear();
                ppp.Super_Salesman_Owner.Person = SalesmanBiz.GetPersonForPlayer(bsd.SuperOwnerSalesmanId);
                ppp.Super_Salesman_Owner.IsNullThrowException();
                ppp.Super_Salesman_Owner.Comment = "Super Owner Salesman";
                ppp.Super_Salesman_Owner.Percent = bsd.OwnerSalesmanCommission.Percent;

                if (ppp.Super_Salesman_Owner.Percent == 0)
                    return;

                ppp.Super_Salesman_Owner.Amount = Math.Round(totalPaymentAmount * ppp.Super_Salesman_Owner.Percent / 100, 2);
            }
        }



        private void calculate_DeliverySalesman_Penalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
            {
                if (!bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
                {
                    ppp.Salesman_Deliveryman.Clear();
                    ppp.Salesman_Deliveryman.Person = SalesmanBiz.GetPersonForPlayer(bsd.DeliverymanSalesmanId);
                    ppp.Salesman_Deliveryman.IsNullThrowException();
                    ppp.Salesman_Deliveryman.Comment = "Deliveryman Salesman";
                    ppp.Salesman_Deliveryman.Percent = bsd.DeliverymanSalesmanCommission.Percent;

                    if (ppp.Salesman_Deliveryman.Percent == 0)
                        return;

                    ppp.Salesman_Deliveryman.Amount = Math.Round(ppp.Salesman_Deliveryman.Percent * totalPaymentAmount / 100, 2);
                }
            }
        }

        private void calculate_CustomerSalesmanPenalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {
            if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Salesman_Customer.Clear();
                ppp.Salesman_Customer.Person = SalesmanBiz.GetPersonForPlayer(bsd.CustomerSalesmanId);
                ppp.Salesman_Customer.Person.IsNullThrowException();
                ppp.Salesman_Customer.Comment = "Customer Salesman";
                ppp.Salesman_Customer.Percent = bsd.CustomerSalesmanCommission.Percent;

                if (ppp.Salesman_Customer.Percent == 0)
                    return;
                ppp.Salesman_Customer.Amount = Math.Round(totalPaymentAmount * ppp.Salesman_Customer.Percent / 100, 2);
            }
        }

        private void calculate_OwnerSalesmanPenalty(BuySellDoc bsd, PersonPayingPenalty ppp, decimal totalPaymentAmount)
        {

            if (!bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                ppp.Salesman_Owner.Clear();
                ppp.Salesman_Owner.Person = SalesmanBiz.GetPersonForPlayer(bsd.OwnerSalesmanId);
                ppp.Salesman_Owner.IsNullThrowException();
                ppp.Salesman_Owner.Comment = "Owner Salesman";
                ppp.Salesman_Owner.Percent = bsd.OwnerSalesmanCommission.Percent;

                if (ppp.Salesman_Owner.Percent == 0)
                    return;

                ppp.Salesman_Owner.Amount = Math.Round(totalPaymentAmount * ppp.Salesman_Owner.Percent / 100, 2);
            }
        }





    }
}
