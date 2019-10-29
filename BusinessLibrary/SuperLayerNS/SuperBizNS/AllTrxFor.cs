
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.SuperLayerNS
{
    public partial class SuperBiz
    {
        /// <summary>
        /// If personId is null or empty, you will get the total cash of the system.
        /// The cash allocated for payment is different from cash recieved. In Payment,
        /// cash will get allocated at RequestConfirmed. Whereas in Receiving, the cash
        /// will get allocated at SellerConfirms
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cashTypeEnum"></param>
        /// <param name="cashStateEnum"></param>
        /// <returns></returns>
        private List<CashTrxVM2> allTrxFor(string personIdForWhomWeAreWorking, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, bool isShowAdminReports)
        {

            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("CashTypeENUM.Unknown");
            if (personIdForWhomWeAreWorking.IsNullOrWhiteSpace())
                return null;

            Person personAcceptingMoneyForSystem = PersonBiz.Find(CurrentUserParameter.SystemPersonId);
            personAcceptingMoneyForSystem.IsNullThrowException();

            Person personForWhomWeAreWorking = PersonBiz.Find(personIdForWhomWeAreWorking);
            personForWhomWeAreWorking.IsNullThrowException();

            //when I am a seller I want a list of all freight trx where insurance is due or payment is due to me. Sometimes, these will not have completed
            //the transaction.

            List<BuySellDoc> lst_BuySellDocsForPerson = get_lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking(personIdForWhomWeAreWorking);
            List<CashEncashmentTrx> listOfCashEncashmentsForPerson = getListOfCashEncashmentsFor(personIdForWhomWeAreWorking);
            List<CashTrx> lst_Of_CashTrxs_Payments = cashTrx_SQL_For(personIdForWhomWeAreWorking).ToList();

            //List<FreightOfferTrx> lstOfFreightOfferTrx = getFreightOfferTrxListFromBuySellDocs(lst_BuySellDocsForPerson, personIdForWhomWeAreWorking);
            List<PenaltyFlatFile> penaltyTrxFlatFile = convert_PenaltyHeader_And_Trx_To_PenaltyFlatFile(personForWhomWeAreWorking, personAcceptingMoneyForSystem);

            List<CashTrxVM2> cashTrxFromBuySellDoc = convertTo_CashTrx2For_BuySellDocs(
                lst_BuySellDocsForPerson,
                personForWhomWeAreWorking,
                personAcceptingMoneyForSystem);

            List<CashTrxVM2> cashTrxFromEncashment = convertToCashTrxFor(
                listOfCashEncashmentsForPerson,
                personForWhomWeAreWorking,
                personAcceptingMoneyForSystem);


            List<CashTrxVM2> lst_CashTrxVM2FromCashTrx = CashTrxVM2.ConvertCashTrxListToCashVM2List(
                lst_Of_CashTrxs_Payments,
                personIdForWhomWeAreWorking);


            List<CashTrxVM2> lst_CashTrx2FromPenaltyTrxs = convert_ToCashTrx_For_PenaltyFlatTrx(penaltyTrxFlatFile);



            //List<CashTrxVM2> lstFromFreightOfferTrx = convertToCashTrxlistFromFreightOfferTrxList(
            //    lstOfFreightOfferTrx,
            //    personForWhomWeAreWorking,
            //    personAcceptingMoneyForSystem);

            //joining lists
            List<CashTrxVM2> lstCompletePayment = new List<CashTrxVM2>();

            if (!lst_CashTrx2FromPenaltyTrxs.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(lst_CashTrx2FromPenaltyTrxs).ToList();

            if (!lst_CashTrxVM2FromCashTrx.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(lst_CashTrxVM2FromCashTrx).ToList();

            if (!cashTrxFromBuySellDoc.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(cashTrxFromBuySellDoc).ToList();

            if (!cashTrxFromEncashment.IsNullOrEmpty())
                lstCompletePayment = lstCompletePayment.Concat(cashTrxFromEncashment).ToList();

            //if (!lstFromFreightOfferTrx.IsNullOrEmpty())
            //    lstCompletePayment = lstCompletePayment.Concat(lstFromFreightOfferTrx).ToList();

            //Now filter the cash for cash Type

            List<CashTrxVM2> lstCompletePayment_Filtered_For_CashType = filterForCashType(cashTypeEnum, lstCompletePayment);


            return lstCompletePayment_Filtered_For_CashType;
        }

        private List<CashTrxVM2> filterForCashType(CashTypeENUM cashTypeEnum, List<CashTrxVM2> lstCompletePayment)
        {

            List<CashTrxVM2> lstCompletePayment_Filtered_For_CashType = new List<CashTrxVM2>();

            switch (cashTypeEnum)
            {
                case CashTypeENUM.Refundable:
                    lstCompletePayment_Filtered_For_CashType = lstCompletePayment.Where(x => x.CashTypeEnum == CashTypeENUM.Refundable).ToList();

                    break;
                case CashTypeENUM.NonRefundable:
                    lstCompletePayment_Filtered_For_CashType = lstCompletePayment.Where(x => x.CashTypeEnum == CashTypeENUM.NonRefundable).ToList();
                    break;
                case CashTypeENUM.Total:
                    lstCompletePayment_Filtered_For_CashType = lstCompletePayment;
                    break;
                case CashTypeENUM.Unknown:
                default:
                    throw new Exception("Cash type Unknown! Tell your administrator.");
            }


            return lstCompletePayment_Filtered_For_CashType;
        }

        private List<CashTrxVM2> convert_ToCashTrx_For_PenaltyFlatTrx(List<PenaltyFlatFile> penaltyTrxFlatFile)
        {
            List<CashTrxVM2> cashTrxList = new List<CashTrxVM2>();

            if (penaltyTrxFlatFile.IsNullOrEmpty())
                return null;

            foreach (PenaltyFlatFile pf in penaltyTrxFlatFile)
            {
                CashTrxVM2 csh = pf.ConvertTo_CashTrxVM2();
                if(csh.HasValue)
                    cashTrxList.Add(csh);
            }
            return cashTrxList;
        }


        private List<PenaltyFlatFile> convert_PenaltyHeader_And_Trx_To_PenaltyFlatFile(Person personForWhomWeAreWorking, Person systemPerson)
        {
            List<PenaltyHeader> lstOfPenaltyHeaders = getPenaltyHeadersFor(personForWhomWeAreWorking);
            List<PenaltyTrx> lstOfPenaltyTrxs = getPenaltyTransactionsFor(personForWhomWeAreWorking);

            List<PenaltyFlatFile> fromHdr = convert_PenaltyHeader_To_PenaltyFlatFile(lstOfPenaltyHeaders, systemPerson);
            List<PenaltyFlatFile> fromTrx = convert_PenaltyTrx_To_PenaltyFlatFile(lstOfPenaltyTrxs);

            if (fromHdr.IsNullOrEmpty())
            {
                if (fromTrx.IsNullOrEmpty())
                    return null;
                else
                    return fromTrx;
            }
            else
            {
                if (fromTrx.IsNullOrEmpty())
                {
                    return fromHdr;
                }
                else
                {
                    List<PenaltyFlatFile> concatedList = fromHdr.Concat(fromTrx).ToList();
                    return concatedList;
                }
            }
        }
        private List<PenaltyFlatFile> convert_PenaltyHeader_To_PenaltyFlatFile(List<PenaltyHeader> lstOfPenaltyHeaders, Person toSystemPerson)
        {
            if (lstOfPenaltyHeaders.IsNullOrEmpty())
                return null;
            List<PenaltyFlatFile> listPenaltyFlatFile = new List<PenaltyFlatFile>();
            foreach (PenaltyHeader ph in lstOfPenaltyHeaders)
            {
                if (ph.PenaltyTrxs_Fixed.IsNullOrEmpty())
                    continue;

                PenaltyFlatFile pff = new PenaltyFlatFile(
                    ph.Id,
                    ph.MetaData.Created.Date_NotNull_Min,
                    ph.Amount,
                    0,
                    ph.Comment,
                    "",
                    ph.FromPerson,
                    toSystemPerson,
                    ph.Name,
                    ph.BuySellDocStateEnum,
                    ph.BuySellDocumentTypeEnum,
                    ph.BuySellDocStateModifierEnum);

                listPenaltyFlatFile.Add(pff);
            }
            return listPenaltyFlatFile;
        }

        private List<PenaltyFlatFile> convert_PenaltyTrx_To_PenaltyFlatFile(List<PenaltyTrx> lstOfPenaltyTrxs)
        {
            if (lstOfPenaltyTrxs.IsNullOrEmpty())
                return null;
            List<PenaltyFlatFile> listPenaltyFlatFile = new List<PenaltyFlatFile>();
            foreach (PenaltyTrx pt in lstOfPenaltyTrxs)
            {
                string name = pt.PenaltyHeader.IsNull() ? "Error" : pt.PenaltyHeader.Name;
                PenaltyFlatFile pff = new PenaltyFlatFile(
                    pt.Id,
                    pt.MetaData.Created.Date_NotNull_Min,
                    0,
                    pt.Amount,
                    pt.PenaltyHeader.Comment,
                    pt.Comment,
                    pt.PenaltyHeader.FromPerson,
                    pt.Person,
                    name,
                    pt.PenaltyHeader.BuySellDocStateEnum,
                    pt.PenaltyHeader.BuySellDocumentTypeEnum,
                    pt.PenaltyHeader.BuySellDocStateModifierEnum);
                listPenaltyFlatFile.Add(pff);
            }
            return listPenaltyFlatFile;
        }


        /// <summary>
        /// These are all the payment tranactions for the penalties
        /// </summary>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <returns></returns>
        private List<PenaltyHeader> getPenaltyHeadersFor(Person personForWhomWeAreWorking)
        {
            List<PenaltyHeader> penaltyHeaders = PenaltyHeaderBiz.FindAll().Where(x => x.FromPersonId == personForWhomWeAreWorking.Id).ToList();
            return penaltyHeaders;
        }

        /// <summary>
        /// These are all the received payments
        /// </summary>
        /// <param name="personForWhomWeAreWorking"></param>
        /// <returns></returns>
        private List<PenaltyTrx> getPenaltyTransactionsFor(Person personForWhomWeAreWorking)
        {
            List<PenaltyTrx> penaltyTrxs = PenaltyTrxBiz.FindAll().Where(x => x.PersonId == personForWhomWeAreWorking.Id).ToList();
            return penaltyTrxs;
        }

        ///// <summary>
        ///// Convert the FreightOfferTrxs to cash Entries.
        ///// Keep provision for 
        /////     Insurance
        ///// Guarantee
        ///// 
        ///// </summary>
        ///// <param name="lstOfFreightOfferTrx"></param>
        ///// <param name="personForWhomWeAreWorking"></param>
        ///// <param name="personForSystem"></param>
        ///// <returns></returns>
        //private List<CashTrxVM2> convertToCashTrxlistFromFreightOfferTrxList(
        //    List<FreightOfferTrx> lstOfFreightOfferTrx,
        //    Person personForWhomWeAreWorking,
        //    Person personForSystem)
        //{


        //    if (lstOfFreightOfferTrx.IsNullOrEmpty())
        //        return null;

        //    List<CashTrxVM2> allCashTrx = new List<CashTrxVM2>();
        //    foreach (FreightOfferTrx fot in lstOfFreightOfferTrx)
        //    {
        //        //get seller/owner trx
        //        List<CashTrxVM2> cashTrx = get_Owners_Insurance_Guarantee_TrxFromFreightOfferTrx(fot, personForWhomWeAreWorking, personForSystem);
        //        if (!cashTrx.IsNullOrEmpty())
        //        {
        //            allCashTrx = allCashTrx.Concat(cashTrx).ToList();
        //        }


        //        //get customerSalesManCommission
        //        cashTrx = get_CustomerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personForWhomWeAreWorking, personForSystem);

        //        if (!cashTrx.IsNullOrEmpty())
        //        {
        //            allCashTrx = allCashTrx.Concat(cashTrx).ToList();
        //        }


        //        ////get customerSalesManCommission
        //        //cashTrx = get_CustomerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personIdForWhomWeAreWorking);
        //        //if (!cashTrx.IsNullOrEmpty())
        //        //{
        //        //    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
        //        //}

        //        ////get ownerSalesManCommission
        //        //cashTrx = get_OwnerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personIdForWhomWeAreWorking);
        //        //if (!cashTrx.IsNullOrEmpty())
        //        //{
        //        //    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
        //        //}

        //        ////get the deliverySalesmanCommission
        //        //cashTrx = get_DeliverymanSalesMan_Insurance_Guarantee_FromFreightOfferTrx(fot, personIdForWhomWeAreWorking);
        //        //if (!cashTrx.IsNullOrEmpty())
        //        //{
        //        //    allCashTrx = allCashTrx.Concat(cashTrx).ToList();
        //        //}



        //    }

        //    return allCashTrx;
        //}

        //private List<CashTrxVM2> get_DeliverymanSalesMan_Insurance_Guarantee_FromFreightOfferTrx(List<FreightOfferTrx> lstOfFreightOfferTrx, string personIdForWhomWeAreWorking)
        //{
        //    throw new NotImplementedException();
        //}

        //private List<CashTrxVM2> get_OwnerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(FreightOfferTrx fot, Person personIdForWhomWeAreWorking, Person personForSystem)
        //{
        //    fot.BuySellDoc.IsNullThrowException();

        //    if (fot.IsNull())
        //        return null;

        //    List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

        //    Customer customer;
        //    if (IsCustomer(personIdForWhomWeAreWorking.Id, out customer))
        //    {
        //        if (fot.BuySellDoc.CustomerId == customer.Id)
        //        {
        //            BuySellDoc bsd = fot.BuySellDoc;

        //            //the person is an customer in this buySellDoc -for insurance
        //            decimal insuranaceReceived = 0;

        //            //insurance payable by deliveryman
        //            Person deliverymanPerson = DeliverymanBiz.GetPersonForPlayer(fot.DeliverymanId);
        //            deliverymanPerson.IsNullThrowException();

        //            if (fot.IsPayInsurance)
        //            {
        //                insuranaceReceived = fot.InsuranceAmount;
        //            }

        //            if (insuranaceReceived != 0)
        //            {

        //                if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
        //                {
        //                    decimal commissionAmount =
        //                        insuranaceReceived * bsd.CustomerSalesmanCommission.Percent;

        //                    string commentFixed = string.Format("Commission on Insurance. {0} {1} {2}",
        //                        bsd.CustomerSalesmanCommission.Percent,
        //                        bsd.FullName(),
        //                        bsd.Comment);

        //                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //                            bsd,
        //                            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //                            0,
        //                            commissionAmount,
        //                            commentFixed,
        //                            personIdForWhomWeAreWorking,
        //                            personForSystem,
        //                            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance,
        //                            bsd.IsCashAvailableTo_Customer());

        //                    lstCashTrx.Add(cashTrx);
        //                }
        //            }
        //        }
        //    }
        //    return lstCashTrx;
        //}

        //private List<CashTrxVM2> get_CustomerSalesMan_Insurance_Guarantee_FromFreightOfferTrx(FreightOfferTrx fot, Person personIdForWhomWeAreWorking, Person personForSystem)
        //{
        //    fot.BuySellDoc.IsNullThrowException();


        //    List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

        //    Salesman salesman;
        //    if (IsSalesman(personIdForWhomWeAreWorking.Id, out salesman))
        //    {
        //        BuySellDoc bsd = fot.BuySellDoc;

        //        if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
        //        {
        //            if (fot.BuySellDoc.CustomerSalesmanId == salesman.Id)
        //            {

        //                //the person is a customersalesman in this buySellDoc -for insurance
        //                decimal insuranaceReceived = 0;
        //                insuranaceReceived = fot.InsuranceAmount;

        //                //if product has been delivered and Pay insurance is false...
        //                if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
        //                {
        //                    if (!fot.IsPayInsurance)
        //                    {
        //                        insuranaceReceived = 0;
        //                    }
        //                }

        //                if (insuranaceReceived != 0)
        //                {
        //                    decimal commissionAmount =
        //                        insuranaceReceived * bsd.CustomerSalesmanCommission.Percent;

        //                    string commentFixed =
        //                        string.Format("Commission on Insurance. {0} {1} {2}",
        //                        bsd.CustomerSalesmanCommission.Percent,
        //                        bsd.FullName(),
        //                        bsd.Comment);

        //                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //                            bsd,
        //                            bsd.CourierComingToPickUp.Date_NotNull_Min,
        //                            0,
        //                            commissionAmount,
        //                            commentFixed,
        //                            personIdForWhomWeAreWorking,
        //                            personForSystem,
        //                            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance, 
        //                            bsd.IsCashAvailableTo_Owner());

        //                    lstCashTrx.Add(cashTrx);
        //                }
        //            }
        //        }
        //    }
        //    return lstCashTrx;
        //}

        //private List<CashTrxVM2> get_Owners_Insurance_Guarantee_TrxFromFreightOfferTrx(
        //    FreightOfferTrx fot,
        //    Person personForWhomWeAreWorking,
        //    Person personForSystem)
        //{
        //    fot.BuySellDoc.IsNullThrowException();

        //    if (fot.IsNull())
        //        return null;

        //    List<CashTrxVM2> lstCashTrx = new List<CashTrxVM2>();

        //    Owner owner;
        //    if (IsOwner(personForWhomWeAreWorking.Id, out owner))
        //    {
        //        if (fot.BuySellDoc.OwnerId == owner.Id)
        //        {
        //            BuySellDoc bsd = fot.BuySellDoc;

        //            //the person is an owner in this buySellDoc -for insurance
        //            //insurance payable by deliveryman

        //            Person deliverymanPerson = DeliverymanBiz.GetPersonForPlayer(fot.DeliverymanId);
        //            deliverymanPerson.IsNullThrowException();

        //            create_Guarantee_Entry_From_FreightTrx_For_Owner(fot, personForWhomWeAreWorking, personForSystem, lstCashTrx, bsd, deliverymanPerson);
        //            create_Insurance_Entry_From_FreightTrx_For_Owner(fot, personForWhomWeAreWorking, personForSystem, lstCashTrx, bsd, deliverymanPerson);
        //        }
        //    }
        //    return lstCashTrx;
        //}

        ///// <summary>
        ///// The deliveryman belongs to the FreightTrx. The owner is paid the full amount if the deliveryman cancels after
        ///// agreeing pickup
        ///// This guarantee entry is not required. This will be taken care of through cancelations
        ///// </summary>
        ///// <param name="fot"></param>
        ///// <param name="personForWhomWeAreWorking"></param>
        ///// <param name="personForSystem"></param>
        ///// <param name="lstCashTrx"></param>
        ///// <param name="bsd"></param>
        ///// <param name="deliverymanPerson"></param>
        //private void create_Guarantee_Entry_From_FreightTrx_For_Owner(FreightOfferTrx fot, Person personForWhomWeAreWorking, Person personForSystem, List<CashTrxVM2> lstCashTrx, BuySellDoc bsd, Person deliverymanPerson)
        //{

        //    decimal guaranteeAmount = 0;
        //    string amountPaidComment = string.Format("payable only if Deliveryman does not deliver (Penalty {0}%), or delivers late/early (Penalty {1}%)",
        //        GetPenaltyPercentageForDeliverymanQuitting(),
        //        GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly());


        //    guaranteeAmount = calculateDeliverymanGuaranteeAmount(fot, bsd, guaranteeAmount);

        //    if (guaranteeAmount == 0)
        //        return;

        //    //guarantee is always payable

        //    string commentFixed = string.Format("{0} {1} {2}",
        //        amountPaidComment,
        //        bsd.FullName(),
        //        bsd.Comment);

        //    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            0,
        //            guaranteeAmount,
        //            commentFixed,
        //            deliverymanPerson,
        //            personForWhomWeAreWorking,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,
        //            bsd.IsCashAvailableTo_Owner());

        //    lstCashTrx.Add(cashTrx);

        //    //we have to put this otherwise, the amount is deducted from the owner
        //    //and there might be complaints later.... ie they have not received the 
        //    //amount and this is already deducted.
        //    if (bsd.BuySellDocStateEnum != BuySellDocStateENUM.Delivered)
        //        return;

        //    //The above total amount does not belong to the owner completely. Some is Commission and system charges
        //    //those are taken once bsd is delivered.

        //    decimal maxCommissionForInsurance =
        //        guaranteeAmount * bsd.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent();

        //    cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            maxCommissionForInsurance,
        //            0,
        //            "System charges on Insurance due. " + bsd.FullName() + bsd.Comment,
        //            personForWhomWeAreWorking,
        //            personForSystem,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,
        //            bsd.IsCashAvailableTo_Owner());

        //    lstCashTrx.Add(cashTrx);

        //}


        ///// <summary>
        ///// In case the deliveryman damages or loses the cargo.
        ///// </summary>
        ///// <param name="fot"></param>
        ///// <param name="personForWhomWeAreWorking"></param>
        ///// <param name="personForSystem"></param>
        ///// <param name="lstCashTrx"></param>
        ///// <param name="bsd"></param>
        ///// <param name="deliverymanPerson"></param>
        //private void create_Insurance_Entry_From_FreightTrx_For_Owner(FreightOfferTrx fot, Person personForWhomWeAreWorking, Person personForSystem, List<CashTrxVM2> lstCashTrx, BuySellDoc bsd, Person deliverymanPerson)
        //{
        //    //this assumes that the personForWhomWeAreWorking is the owner in the bsd
        //    decimal insuranceAmount = 0;
        //    string amountPaidComment = "Only payable by Deliveryman if goods damaged/lost during delivery.";


        //    insuranceAmount = fot.InsuranceAmount;

        //    if (insuranceAmount == 0)
        //        return;

        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
        //        if (!fot.IsPayInsurance)
        //            return;

        //    string commentFixed = string.Format("{0} {1} {2}",
        //        amountPaidComment,
        //        bsd.FullName(),
        //        bsd.Comment);

        //    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            0,
        //            insuranceAmount,
        //            commentFixed,
        //            deliverymanPerson,
        //            personForWhomWeAreWorking,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance, bsd.IsCashAvailableTo_Owner());

        //    lstCashTrx.Add(cashTrx);

        //    //we have to put this otherwise, the amount is deducted from the owner
        //    //and there might be complaints later.... ie they have not received the 
        //    //amount and this is already deducted.
        //    if (bsd.BuySellDocStateEnum != BuySellDocStateENUM.Delivered)
        //        return;

        //    //The above total amount does not belong to the owner completely. Some is Commission and system charges
        //    //those are taken once bsd is delivered.

        //    decimal maxCommissionForInsurance =
        //        insuranceAmount * bsd.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent();

        //    cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            maxCommissionForInsurance,
        //            0,
        //            "System charges on Insurance due. " + bsd.FullName() + bsd.Comment,
        //            personForWhomWeAreWorking,
        //            personForSystem,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,bsd.IsCashAvailableTo_Owner());

        //    lstCashTrx.Add(cashTrx);
        //}





        //private decimal create_Insusrance_Entry_From_FreightTrx(FreightOfferTrx fot, Person personForWhomWeAreWorking, Person personForSystem, List<CashTrxVM2> lstCashTrx, BuySellDoc bsd, Person deliverymanPerson)
        //{
        //    decimal insuranaceReceived = 0;

        //    if (fot.IsPayInsurance)
        //    {
        //        insuranaceReceived = fot.InsuranceAmount;
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            0,
        //            insuranaceReceived,
        //            "Insurance due. " + bsd.FullName() + bsd.Comment,
        //            deliverymanPerson,
        //            personForWhomWeAreWorking,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance);

        //    lstCashTrx.Add(cashTrx);

        //    decimal maxCommissionForInsurance =
        //        insuranaceReceived * bsd.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent();

        //    cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //            bsd,
        //            bsd.OrderConfirmedByDeliveryman.Date_NotNull_Min,
        //            0,
        //            maxCommissionForInsurance,
        //            "System charges on Insurance due. " + bsd.FullName() + bsd.Comment,
        //            personForWhomWeAreWorking,
        //            personForSystem,
        //            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance);

        //    lstCashTrx.Add(cashTrx);

        //    return insuranaceReceived;
        //}



        ////this returns all the freight trxs for.
        //private List<FreightOfferTrx> getFreightOfferTrxListFromBuySellDocs(List<BuySellDoc> lstOfBuySellDocsFor, string personIdForWhomWeAreWorking)
        //{
        //    if (lstOfBuySellDocsFor.IsNullOrEmpty())
        //        return null;

        //    List<FreightOfferTrx> allFrtTrx = new List<FreightOfferTrx>();

        //    //now get all frtTrx
        //    foreach (BuySellDoc bds in lstOfBuySellDocsFor)
        //    {
        //        if (bds.FreightOfferTrxs.IsNull())
        //            continue;

        //        List<FreightOfferTrx> bdsFrtTrx = bds.FreightOfferTrxs.ToList();
        //        if (bdsFrtTrx.IsNullOrEmpty())
        //            continue;

        //        allFrtTrx = allFrtTrx.Concat(bdsFrtTrx)
        //            .ToList();

        //    }
        //    if (allFrtTrx.IsNull())
        //        return null;

        //    List<FreightOfferTrx> allFrtTrxDistinct = new HashSet<FreightOfferTrx>(allFrtTrx).ToList();
        //    return allFrtTrxDistinct;
        //}


        private List<CashTrxVM2> convertTo_CashTrx2For_BuySellDocs(
            List<BuySellDoc> lst_BuySellDocsForPerson,
            Person personWeAreWorkingFor,
            Person personForSystem)
        {
            List<CashTrxVM2> lstOfCashTrxVm2 = new List<CashTrxVM2>();

            if (lst_BuySellDocsForPerson.IsNullOrEmpty())
            {

            }
            else
            {
                foreach (BuySellDoc bsd in lst_BuySellDocsForPerson)
                {
                    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                        continue;

                    //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                    //    continue;

                    Person bsdCustomerPerson = CustomerBiz.GetPersonForPlayer(bsd.CustomerId);
                    Person bsdOwnerPerson = OwnerBiz.GetPersonForPlayer(bsd.OwnerId);
                    Person bsdDeliverymanPerson = get_Deliveryman_Person(bsd);

                    Person bsdCustomerSalesmanPerson = get_CustomerSalesman_Person(bsd);
                    Person bsdCustomer_Super_SalesmanPerson = get_Customer_Super_Salesman_Person(bsd);
                    Person bsdCustomer_Super_Super_SalesmanPerson = get_Customer_Super_Super_Salesman_Person(bsd);

                    Person bsdDeliverymanSalesmanPerson = get_DeliverymanSalesman_Person(bsd);
                    Person bsdDeliveryman_Super_SalesmanPerson = get_Deliveryman_Super_Salesman_Person(bsd);
                    Person bsdDeliveryman_Super_Super_SalesmanPerson = get_Deliveryman_Super_Super_Salesman_Person(bsd);

                    Person bsdOwnerSalesmanPerson = get_OwnerSalesman_Person(bsd);
                    Person bsdOwner_Super_SalesmanPerson = get_Owner_Super_Salesman_Person(bsd);
                    Person bsdOwner_Super_Super_SalesmanPerson = get_Owner_Super_Super_Salesman_Person(bsd);



                    if (bsd.OptedOutOfSystem.IsSelected)
                    {
                        //get_Customer_NoNReturnable_CashTrxVM2(
                        //    personWeAreWorkingFor,
                        //    lstOfCashTrxVm2,
                        //    bsd,
                        //    personForSystem,
                        //    bsdOwnerPerson);
                    }
                    else
                    {
                        get_Customer_CashTrxVM2(
                            personWeAreWorkingFor,
                            lstOfCashTrxVm2,
                            bsd,
                            bsdCustomerPerson,
                            bsdOwnerPerson);


                        if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
                            continue;


                        get_Owner_CashTrxVM2(
                            personWeAreWorkingFor,
                            lstOfCashTrxVm2,
                            bsd,
                            bsdCustomerPerson,
                            bsdOwnerPerson,
                            bsdDeliverymanPerson);


                        get_Deliveryman_CashTrxVM2(
                            personWeAreWorkingFor,
                            personForSystem,
                            lstOfCashTrxVm2,
                            bsd,
                            bsdDeliverymanPerson,
                            bsdOwnerPerson);


                        get_Salesmen_CashTrxVM2(
                            personWeAreWorkingFor,
                            personForSystem,
                            lstOfCashTrxVm2,
                            bsd,
                            bsdCustomerSalesmanPerson,
                            bsdDeliverymanSalesmanPerson,
                            bsdOwnerSalesmanPerson,
                            bsdCustomer_Super_SalesmanPerson,
                            bsdCustomer_Super_Super_SalesmanPerson,
                            bsdDeliveryman_Super_SalesmanPerson,
                            bsdDeliveryman_Super_Super_SalesmanPerson,
                            bsdOwner_Super_SalesmanPerson,
                            bsdOwner_Super_Super_SalesmanPerson);


                        get_System_CashTrxVM2(
                            personWeAreWorkingFor,
                            lstOfCashTrxVm2,
                            bsd,
                            bsdOwnerPerson,
                            personForSystem);
                    }


                }
            }

            return lstOfCashTrxVm2;
        }

        //private void get_Customer_NoNReturnable_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person personForSystem, Person bsdOwnerPerson)
        //{
        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
        //        return;

        //    if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
        //        return;


        //    //Non Returnable expenses are always payable only by the seller/owner

        //    Owner owner;
        //    if (IsOwner(personWeAreWorkingFor.Id, out owner))
        //    {
        //        if (!bsdOwnerPerson.IsNull())
        //        {
        //            if (personWeAreWorkingFor.Id == bsdOwnerPerson.Id)
        //            {
        //                if (bsd.NonReturnableTrxs.IsNullOrEmpty())
        //                { }
        //                else
        //                {
        //                    foreach (NonReturnableTrx nrt in bsd.NonReturnableTrxs)
        //                    {
        //                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
        //                            bsd,
        //                            bsd.RequestConfirmed.Date_NotNull_Min,
        //                            nrt.Amount,
        //                            0,
        //                            nrt.FullName(),
        //                            bsdOwnerPerson,
        //                            personForSystem,
        //                            CashTrxVmDocumentTypeENUM.SaleOrder, 
        //                            bsd.IsCashAvailableTo_Owner());

        //                        lstOfCashTrxVm2.Add(cashTrx);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        private void get_System_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdOwnerPerson, Person bsdSystemPerson)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return;
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
            //    return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;


            Owner owner;
            if (IsOwner(personWeAreWorkingFor.Id, out owner))
            {
                if (!bsdOwnerPerson.IsNull())
                {
                    if (personWeAreWorkingFor.Id == bsdOwnerPerson.Id)
                    {
                        string commentFixed = string.Format("{0} {1}",
                            "System Charges",
                            bsd.Comment);

                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            bsd.Total_Commissions_Payable_By_Owner.Amount_Refundable,
                            0,
                            commentFixed,
                            bsdOwnerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.SaleOrder,
                            bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                        if(cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);

                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            bsd.Total_Commissions_Payable_By_Owner.Amount_NonRefundable,
                            0,
                            commentFixed,
                            bsdOwnerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.SaleOrder,
                            bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);
                    }
                }

            }
        }

        private Person get_OwnerSalesman_Person(BuySellDoc bsd)
        {
            Person bsdOwnerSalesmanPerson = null;
            if (!bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsdOwnerSalesmanPerson = SalesmanBiz.GetPersonForPlayer(bsd.OwnerSalesmanId);
            }
            return bsdOwnerSalesmanPerson;
        }

        private Person get_DeliverymanSalesman_Person(BuySellDoc bsd)
        {
            Person bsdDeliverymanSalesmanPerson = null;
            if (!bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsdDeliverymanSalesmanPerson = SalesmanBiz.GetPersonForPlayer(bsd.DeliverymanSalesmanId);
            }
            return bsdDeliverymanSalesmanPerson;
        }

        private Person get_CustomerSalesman_Person(BuySellDoc bsd)
        {
            Person bsdCustomerSalesmanPerson = null;
            if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsdCustomerSalesmanPerson = SalesmanBiz.GetPersonForPlayer(bsd.CustomerSalesmanId);
            }
            return bsdCustomerSalesmanPerson;
        }

        private Person get_Deliveryman_Person(BuySellDoc bsd)
        {
            Person bsdDeliverymanPerson = null;
            if (!bsd.DeliverymanId.IsNullOrWhiteSpace())
                bsdDeliverymanPerson = DeliverymanBiz.GetPersonForPlayer(bsd.DeliverymanId);
            return bsdDeliverymanPerson;
        }


        private Person get_Deliveryman_Super_Super_Salesman_Person(BuySellDoc bsd)
        {
            Person person = null;
            if (!bsd.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                person = SalesmanBiz.GetPersonForPlayer(bsd.DeliverymanSalesmanId);
            }
            return person;
        }

        private Person get_Deliveryman_Super_Salesman_Person(BuySellDoc bsd)
        {
            Person person = null;
            if (!bsd.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                person = SalesmanBiz.GetPersonForPlayer(bsd.DeliverymanSalesmanId);
            }
            return person;
        }

        private Person get_Customer_Super_Super_Salesman_Person(BuySellDoc bsd)
        {
            Person person = null;
            if (!bsd.SuperSuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                person = SalesmanBiz.GetPersonForPlayer(bsd.CustomerSalesmanId);
            }
            return person;
        }

        private Person get_Customer_Super_Salesman_Person(BuySellDoc bsd)
        {
            Person person = null;
            if (!bsd.SuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                person = SalesmanBiz.GetPersonForPlayer(bsd.CustomerSalesmanId);
            }
            return person;
        }

        private Person get_Owner_Super_Super_Salesman_Person(BuySellDoc bsd)
        {
            Person person = null;
            if (!bsd.SuperSuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                person = SalesmanBiz.GetPersonForPlayer(bsd.OwnerSalesmanId);
            }
            return person;
        }

        private Person get_Owner_Super_Salesman_Person(BuySellDoc bsd)
        {
            Person person = null;
            if (!bsd.SuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                person = SalesmanBiz.GetPersonForPlayer(bsd.OwnerSalesmanId);
            }
            return person;
        }


        private void get_Salesmen_CashTrxVM2(
            Person personWeAreWorkingFor,
            Person personForSystem,
            List<CashTrxVM2> lstOfCashTrxVm2,
            BuySellDoc bsd,
            Person bsdCustomerSalesmanPerson,
            Person bsdDeliverymanSalesmanPerson,
            Person bsdOwnerSalesmanPerson,
            Person bsdCustomer_Super_SalesmanPerson,
            Person bsdCustomer_Super_Super_SalesmanPerson,
            Person bsdDeliveryman_Super_SalesmanPerson,
            Person bsdDeliveryman_Super_Super_SalesmanPerson,
            Person bsdOwner_Super_SalesmanPerson,
            Person bsdOwner_Super_Super_SalesmanPerson)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            Salesman salesman;
            if (IsSalesman(personWeAreWorkingFor.Id, out salesman))
            {
                get_CustomerSalesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdCustomerSalesmanPerson, salesman);
                get_Customer_Super_Salesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdCustomer_Super_SalesmanPerson, salesman);
                get_Customer_Super_Super_Salesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdCustomer_Super_Super_SalesmanPerson, salesman);

                get_OwnerSalesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdOwnerSalesmanPerson, salesman);
                get_Owner_Super_Salesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdOwner_Super_SalesmanPerson, salesman);
                get_Owner_Super_Super_Salesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdOwner_Super_Super_SalesmanPerson, salesman);

                get_DeliverymanSalesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdDeliverymanSalesmanPerson, salesman);
                get_Deliveryman_Super_Salesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdDeliveryman_Super_SalesmanPerson, salesman);
                get_Deliveryman_Super_Super_Salesman_CashTrxVM2(personForSystem, lstOfCashTrxVm2, bsd, bsdDeliveryman_Super_Super_SalesmanPerson, salesman);


            }
        }

        private void get_Deliveryman_Super_Super_Salesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdDeliveryman_Super_Super_SalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.SuperSuperDeliverymanSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperSuperDeliverymanSalesmanCommission.Amount_Refundable,
                                "Deliveryman Super Super Salesman Commission - Percent: " + bsd.SuperSuperDeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliveryman_Super_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperSuperDeliverymanSalesmanCommission,
                                bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                
                
                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperSuperDeliverymanSalesmanCommission.Amount_NonRefundable,
                                "Deliveryman Super Super Salesman Commission - Percent: " + bsd.SuperSuperDeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliveryman_Super_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperSuperDeliverymanSalesmanCommission,
                                bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

                }
            }
        }

        private void get_Deliveryman_Super_Salesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdDeliveryman_Super_SalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.SuperDeliverymanSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperDeliverymanSalesmanCommission.Amount_Refundable,
                                "Deliveryman Super Salesman Commission - Percent: " + bsd.SuperDeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliveryman_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperDeliverymanSalesmanCommission,
                                bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);



                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperDeliverymanSalesmanCommission.Amount_NonRefundable,
                                "Deliveryman Super Salesman Commission - Percent: " + bsd.SuperDeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliveryman_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperDeliverymanSalesmanCommission,
                                bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

 
                }
            }
        }

        private void get_Owner_Super_Super_Salesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdOwner_Super_Super_SalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.SuperSuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.SuperSuperOwnerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperSuperOwnerSalesmanCommission.Amount_Refundable,
                                "Seller Super Super Salesman Commission - Percent: " + bsd.SuperSuperOwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwner_Super_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperSuperOwnerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperSuperOwnerSalesmanCommission.Amount_NonRefundable,
                                "Seller Super Super Salesman Commission - Percent: " + bsd.SuperSuperOwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwner_Super_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperSuperOwnerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

                
                
                }
            }
        }

        private void get_Owner_Super_Salesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdOwner_Super_SalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.SuperOwnerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.SuperOwnerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperOwnerSalesmanCommission.Amount_Refundable,
                                "Seller Super Salesman Commission - Percent: " + bsd.SuperOwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwner_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperOwnerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);



                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperOwnerSalesmanCommission.Amount_NonRefundable,
                                "Seller Super Salesman Commission - Percent: " + bsd.SuperOwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwner_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperOwnerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                }
            }
        }

        private void get_Customer_Super_Super_Salesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomer_Super_Super_SalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.SuperSuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.SuperSuperCustomerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperSuperCustomerSalesmanCommission.Amount_Refundable,
                                "Customer Super Super Salesman Commission - Percent: " + bsd.SuperSuperCustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomer_Super_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperSuperCustomerSalesmanCommission,
                                bsd.IsCashAvailableTo_Customer(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperSuperCustomerSalesmanCommission.Amount_NonRefundable,
                                "Customer Super Super Salesman Commission - Percent: " + bsd.SuperSuperCustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomer_Super_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperSuperCustomerSalesmanCommission,
                                bsd.IsCashAvailableTo_Customer(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                }
            }
        }

        private void get_Customer_Super_Salesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomer_Super_SalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.SuperCustomerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.SuperCustomerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperCustomerSalesmanCommission.Amount_Refundable,
                                "Customer Super Salesman Commission - Percent: " + bsd.SuperCustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomer_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperCustomerSalesmanCommission,
                                bsd.IsCashAvailableTo_Customer(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.SuperCustomerSalesmanCommission.Amount_NonRefundable,
                                "Customer Super Salesman Commission - Percent: " + bsd.SuperCustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomer_Super_SalesmanPerson,
                                CashTrxVmDocumentTypeENUM.SuperCustomerSalesmanCommission,
                                bsd.IsCashAvailableTo_Customer(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                }
            }
        }

        private void get_DeliverymanSalesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdDeliverymanSalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.DeliverymanSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.DeliverymanSalesmanCommission.Amount_Refundable,
                                "Deliveryman Salesman Commission - Percent: " + bsd.DeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliverymanSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.DeliverymanSalesmanCommission, bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.DeliverymanSalesmanCommission.Amount_NonRefundable,
                                "Deliveryman Salesman Commission - Percent: " + bsd.DeliverymanSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdDeliverymanSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.DeliverymanSalesmanCommission, bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);


                }
            }
        }

        private void get_OwnerSalesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdOwnerSalesmanPerson, Salesman salesman)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.OwnerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.OwnerSalesmanCommission.Amount_Refundable,
                                "Owner Salesman Commission - Percent: " + bsd.OwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwnerSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.OwnerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.OwnerSalesmanCommission.Amount_NonRefundable,
                                "Owner Salesman Commission - Percent: " + bsd.OwnerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdOwnerSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.OwnerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

                }
            }
        }

        private void get_CustomerSalesman_CashTrxVM2(Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerSalesmanPerson, Salesman salesman)
        {
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
            //    return;
            //if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
            //    return;
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            if (!bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                if (salesman.Id == bsd.CustomerSalesmanId)
                {
                    CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.CustomerSalesmanCommission.Amount_Refundable,
                                " - Percent: " + bsd.CustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomerSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.CustomerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

                    cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                bsd,
                                bsd.MetaData.Created.Date_NotNull_Min,
                                0,
                                bsd.CustomerSalesmanCommission.Amount_NonRefundable,
                                " - Percent: " + bsd.CustomerSalesmanCommission.Percent.ToString("N2") + "%. " + bsd.Comment,
                                personForSystem,
                                bsdCustomerSalesmanPerson,
                                CashTrxVmDocumentTypeENUM.CustomerSalesmanCommission,
                                bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                    if (cashTrx.HasValue)
                        lstOfCashTrxVm2.Add(cashTrx);

                }
            }
        }

        private void get_Deliveryman_CashTrxVM2(Person personWeAreWorkingFor, Person personForSystem, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdDeliverymanPerson, Person bsdOwnerPerson)
        {
            Deliveryman deliveryman;
            if (IsDeliveryman(personWeAreWorkingFor.Id, out deliveryman))
            {
                //there is a deliveryman in the doc
                if (!bsdDeliverymanPerson.IsNull())
                {
                    //the current person we are working for is the deliveryman
                    if (personWeAreWorkingFor.Id == bsdDeliverymanPerson.Id)
                    {
                        //the person we are working for is the deliveryman in the current bsd.
                        //which means that delivery has been accepted by the buyer.

                        //create a cashTrx if accepted by seller
                        string commentFixed = string.Format("{0}", bsd.FullName(), bsd.Comment);

                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                                    bsd,
                                    bsd.MetaData.Created.Date_NotNull_Min,
                                    0,
                                    bsd.Freight_Accepted_Refundable,
                                    commentFixed,
                                    personForSystem,
                                    bsdDeliverymanPerson,
                                    CashTrxVmDocumentTypeENUM.DeliveryOrderCharges, bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.Refundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);


                        //also block the same amount of freight in case of failure
                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            bsd.Freight_Accepted_Refundable,
                            0,
                            commentFixed,
                            bsdDeliverymanPerson,
                            personForSystem,
                            CashTrxVmDocumentTypeENUM.DeliveryOrderExecutionGuarantee,
                            bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.Refundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);




                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            bsd.InsuranceRequired,
                            0,
                            commentFixed,
                            bsdDeliverymanPerson,
                            personForSystem,
                            CashTrxVmDocumentTypeENUM.DeliveryOrderInsurance,
                            bsd.IsCashAvailableTo_Deliveryman(),
                            CashTypeENUM.Refundable);


                        if(cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);


                    }
                }
            }
        }

        private void get_Owner_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerPerson, Person bsdOwnerPerson, Person bsdDeliverymanPerson)
        {
            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return;

            if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;




            Owner owner;
            if (IsOwner(personWeAreWorkingFor.Id, out owner))
            {
                if (!bsdOwnerPerson.IsNull())
                {
                    if (personWeAreWorkingFor.Id == bsdOwnerPerson.Id)
                    {
                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            0,
                            bsd.Total_Payment_For_Invoice_Refundable(),
                            bsd.Comment,
                            bsdCustomerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.SaleOrder,
                            bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.Refundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);


                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            0,
                            bsd.Total_Payment_For_Invoice_NonRefundable(),
                            bsd.Comment,
                            bsdCustomerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.SaleOrder,
                            bsd.IsCashAvailableTo_Owner(),
                            CashTypeENUM.NonRefundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);


                    }
                }

            }



        }

        private void get_Customer_CashTrxVM2(Person personWeAreWorkingFor, List<CashTrxVM2> lstOfCashTrxVm2, BuySellDoc bsd, Person bsdCustomerPerson, Person bsdOwnerPerson)
        {


            Customer customer;
            if (IsCustomer(personWeAreWorkingFor.Id, out customer))
            {
                if (!bsdCustomerPerson.IsNull())
                {
                    if (personWeAreWorkingFor.Id == bsdCustomerPerson.Id)
                    {
                        //create a trx for customer salesman
                        CashTrxVM2 cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            bsd.Total_Payment_For_Invoice_Refundable(),
                            0,
                            bsd.Comment,
                            bsdCustomerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.PurchaseOrder,
                            bsd.IsCashAvailableTo_Customer(),
                            CashTypeENUM.Refundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);



                        //create a trx for customer salesman
                        cashTrx = create_CashTrxVM2_From_BuySellDoc(
                            bsd,
                            bsd.MetaData.Created.Date_NotNull_Min,
                            bsd.Total_Payment_For_Invoice_NonRefundable(),
                            0,
                            bsd.Comment,
                            bsdCustomerPerson,
                            bsdOwnerPerson,
                            CashTrxVmDocumentTypeENUM.PurchaseOrder,
                            bsd.IsCashAvailableTo_Customer(),
                            CashTypeENUM.NonRefundable);

                        if (cashTrx.HasValue)
                            lstOfCashTrxVm2.Add(cashTrx);

                    }
                }
            }
        }



        private List<CashTrxVM2> convertToCashTrxFor(
           List<CashEncashmentTrx> lst_CashEncashments,
           Person personWeAreWorkingFor,
           Person personForSystem)
        {
            List<CashTrxVM2> lstOfCashTrxVm2 = new List<CashTrxVM2>();

            if (lst_CashEncashments.IsNullOrEmpty())
                return lstOfCashTrxVm2;

            Person fakePerson = new Person();
            fakePerson.Name = "Bank";
            foreach (CashEncashmentTrx cet in lst_CashEncashments)
            {
                if (cet.PersonRequestingPaymentId == personWeAreWorkingFor.Id)
                {
                    string comment = "";
                    CashTrxVM2 cashTrx = cet.ToCashTrxVM2(comment, fakePerson);

                    if (!cashTrx.IsNull())
                        lstOfCashTrxVm2.Add(cashTrx);
                }
            }

            return lstOfCashTrxVm2;


        }



        private CashTrxVM2 create_CashTrxVM2_From_BuySellDoc(BuySellDoc bsd, DateTime date, decimal paymentAmount, decimal receiptAmount, string comment, Person fromPerson, Person toPerson, CashTrxVmDocumentTypeENUM cashTrxVmDocumentTypeENUM, bool isCashAvailable, CashTypeENUM cashTypeEnum)
        {
            CashStateENUM CashStateEnum = isCashAvailable ? CashStateENUM.Available : CashStateENUM.Allocated;
            //if from person is owner or customer or deliveryman.... the cash available status will be different
            if(bsd.IsShop)
            {
                if(bsd.Shop.IsNull())
                {
                    comment = "(Shop) " + comment;

                }
                else
                {
                    comment = "(Shop: " + bsd.Shop.FullName() + ") " + comment;

                }
            }
            CashTrxVM2 cashTrx = new CashTrxVM2(
                        bsd.Id,
                        date,                                  //*
                        paymentAmount,                         //*
                        receiptAmount,                         //*
                        comment,                               //*
                        fromPerson,                            //*
                        toPerson,                              //*
                        cashTypeEnum,
                        CashStateEnum,
                        cashTrxVmDocumentTypeENUM,             //*
                        bsd.Name,
                        bsd.BuySellDocStateEnum);

            return cashTrx;

        }


        private List<CashEncashmentTrx> getListOfCashEncashmentsFor(string personIdForWhomWeAreWorking)
        {
            personIdForWhomWeAreWorking.IsNullOrWhiteSpaceThrowException();

            List<CashEncashmentTrx> lstCashEncashments = CashEncashmentTrxBiz
                .FindAll()
                .Where(x => x.PersonRequestingPaymentId == personIdForWhomWeAreWorking)
                .ToList();

            return lstCashEncashments;

        }

        private List<BuySellDoc> get_lst_Of_BuySellDocs_For_personIdForWhomWeAreWorking(string personIdForWhomWeAreWorking)
        {
            //is the person a customer...
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsACustomer = getAllBuySellDocsInWhichPersonIsACustomer(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsASeller = getAllBuySellDocsInWhichPersonIsASeller(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsADeliveryman = getAllBuySellDocsInWhichPersonIsADeliveryman(personIdForWhomWeAreWorking);

            List<BuySellDoc> lstOfBuySellDocsWherePersonIsACustomerSalesman = getAllBuySellDocsInWhichPersonIsACustomerSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsA_Super_CustomerSalesman = getAllBuySellDocsInWhichPersonIsA_Super_CustomerSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsA_Super_Super_CustomerSalesman = getAllBuySellDocsInWhichPersonIsA_Super_Super_CustomerSalesman(personIdForWhomWeAreWorking);

            List<BuySellDoc> lstOfBuySellDocsWherePersonIsASellerSalesman = getAllBuySellDocsInWhichPersonIsASellerSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsA_Super_SellerSalesman = getAllBuySellDocsInWhichPersonIsA_Super_SellerSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsA_Super_Super_SellerSalesman = getAllBuySellDocsInWhichPersonIsA_Super_Super_SellerSalesman(personIdForWhomWeAreWorking);

            List<BuySellDoc> lstOfBuySellDocsWherePersonIsADeliverymanSalesman = getAllBuySellDocsInWhichPersonIsADeliverymanSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsA_Super_DeliverymanSalesman = getAllBuySellDocsInWhichPersonIsA_Super_DeliverymanSalesman(personIdForWhomWeAreWorking);
            List<BuySellDoc> lstOfBuySellDocsWherePersonIsA_Super_Super_DeliverymanSalesman = getAllBuySellDocsInWhichPersonIsA_Super_Super_DeliverymanSalesman(personIdForWhomWeAreWorking);


            List<BuySellDoc> concatedList = new List<BuySellDoc>();


            if (!lstOfBuySellDocsWherePersonIsASeller.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsASeller).ToList();

            if (!lstOfBuySellDocsWherePersonIsADeliveryman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsADeliveryman).ToList();

            if (!lstOfBuySellDocsWherePersonIsACustomerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsACustomerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsASellerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsASellerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsADeliverymanSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsADeliverymanSalesman).ToList();


            if (!lstOfBuySellDocsWherePersonIsA_Super_CustomerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsA_Super_CustomerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsA_Super_Super_CustomerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsA_Super_Super_CustomerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsA_Super_SellerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsA_Super_SellerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsA_Super_Super_SellerSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsA_Super_Super_SellerSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsA_Super_DeliverymanSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsA_Super_DeliverymanSalesman).ToList();

            if (!lstOfBuySellDocsWherePersonIsA_Super_Super_DeliverymanSalesman.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsA_Super_Super_DeliverymanSalesman).ToList();






            //The buy sell docs aboove this need to have their freighttrxs checked for Insurance/Guarantee so
            //they can be recovered and commissions paid.

            if (!lstOfBuySellDocsWherePersonIsACustomer.IsNullOrEmpty())
                concatedList = concatedList.Concat(lstOfBuySellDocsWherePersonIsACustomer).ToList();

            //Remove duplicates
            HashSet<BuySellDoc> hashsetBuySellDoc = new HashSet<BuySellDoc>(concatedList);
            return hashsetBuySellDoc.ToList();
        }





        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsA_Super_DeliverymanSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a super Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().ToList();

                if (lst.IsNullOrEmpty())
                    return lst;

                List<BuySellDoc> lstBstContainingSalesman = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in lst)
                {
                    if (bsd.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
                        continue;

                    if (bsd.SuperDeliverymanSalesmanId == salesman.Id)
                        lstBstContainingSalesman.Add(bsd);
                }
                return lstBstContainingSalesman;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsA_Super_Super_DeliverymanSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a super Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().ToList();

                if (lst.IsNullOrEmpty())
                    return lst;

                List<BuySellDoc> lstBstContainingSalesman = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in lst)
                {
                    if (bsd.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
                        continue;

                    if (bsd.SuperSuperDeliverymanSalesmanId == salesman.Id)
                        lstBstContainingSalesman.Add(bsd);
                }
                return lstBstContainingSalesman;
            }
            return null;
        }


        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsADeliverymanSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x =>
                    x.DeliverymanSalesmanId == salesman.Id).ToList();
                return lst;
            }
            return null;
        }





        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsA_Super_SellerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a super Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().ToList();

                if (lst.IsNullOrEmpty())
                    return lst;

                List<BuySellDoc> lstBstContainingSalesman = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in lst)
                {
                    if (bsd.SuperOwnerSalesmanId.IsNullOrWhiteSpace())
                        continue;

                    if (bsd.SuperOwnerSalesmanId == salesman.Id)
                        lstBstContainingSalesman.Add(bsd);
                }
                return lstBstContainingSalesman;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsA_Super_Super_SellerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a super Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().ToList();

                if (lst.IsNullOrEmpty())
                    return lst;

                List<BuySellDoc> lstBstContainingSalesman = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in lst)
                {
                    if (bsd.SuperSuperOwnerSalesmanId.IsNullOrWhiteSpace())
                        continue;

                    if (bsd.SuperSuperOwnerSalesmanId == salesman.Id)
                        lstBstContainingSalesman.Add(bsd);
                }
                return lstBstContainingSalesman;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsASellerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.OwnerSalesmanId == salesman.Id).ToList();
                return lst;
            }
            return null;
        }






        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsA_Super_CustomerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a super Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().ToList();

                if (lst.IsNullOrEmpty())
                    return lst;

                List<BuySellDoc> lstBstContainingSalesman = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in lst)
                {
                    if (bsd.SuperOwnerSalesmanId.IsNullOrWhiteSpace())
                        continue;

                    if (bsd.SuperOwnerSalesmanId == salesman.Id)
                        lstBstContainingSalesman.Add(bsd);
                }
                return lstBstContainingSalesman;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsA_Super_Super_CustomerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a super Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().ToList();

                if (lst.IsNullOrEmpty())
                    return lst;

                List<BuySellDoc> lstBstContainingSalesman = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in lst)
                {
                    if (bsd.SuperSuperOwnerSalesmanId.IsNullOrWhiteSpace())
                        continue;

                    if (bsd.SuperSuperOwnerSalesmanId == salesman.Id)
                        lstBstContainingSalesman.Add(bsd);
                }
                return lstBstContainingSalesman;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsACustomerSalesman(string personIdForWhomWeAreWorking)
        {
            Salesman salesman = SalesmanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!salesman.IsNull())
            {
                //this person is a Salesman
                //get all the buySellOrders in which he is a Salesman
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.CustomerSalesmanId == salesman.Id).ToList();
                return lst;
            }
            return null;
        }






        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsADeliveryman(string personIdForWhomWeAreWorking)
        {
            Deliveryman deliveryman = DeliverymanBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!deliveryman.IsNull())
            {
                List<BuySellDoc> allBuySellDocs = BuySellDocBiz.FindAll().ToList();

                if (allBuySellDocs.IsNullOrEmpty())
                    return null;

                List<BuySellDoc> selectedDocs = new List<BuySellDoc>();
                foreach (BuySellDoc bsd in allBuySellDocs)
                {
                    if (bsd.DeliverymanId == deliveryman.Id)
                    {
                        selectedDocs.Add(bsd);
                    }
                }

                return selectedDocs;
            }
            return null;
        }
        private List<FreightOfferTrx> getAllFreightOfferTrxForSellerFrom(List<BuySellDoc> allBuySellDocsInWhichPersonIsASeller, string personIdForWhomWeAreWorking)
        {
            List<FreightOfferTrx> fots = new List<FreightOfferTrx>();
            if (allBuySellDocsInWhichPersonIsASeller.IsNullOrEmpty())
                return fots;

            foreach (BuySellDoc bds in allBuySellDocsInWhichPersonIsASeller)
            {
                List<FreightOfferTrx> fotsForBds = bds.FreightOfferTrxs.ToList();
                if (fotsForBds.IsNullOrEmpty())
                    continue;
                fots = fots.Concat(fotsForBds).ToList();
                ////remove the accepted one
                //if (!bds.FreightOfferTrxAccepted.IsNull())
                //    fots.Remove(bds.FreightOfferTrxAccepted);

            }
            return fots;
        }
        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsASeller(string personIdForWhomWeAreWorking)
        {
            Owner owner = OwnerBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!owner.IsNull())
            {
                //this person is a Owner
                //get all the buySellOrders in which he is a Owner
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.OwnerId == owner.Id).ToList();
                return lst;
            }
            return null;
        }

        private List<BuySellDoc> getAllBuySellDocsInWhichPersonIsACustomer(string personIdForWhomWeAreWorking)
        {
            Customer customer = CustomerBiz.GetPlayerForPersonId(personIdForWhomWeAreWorking);
            if (!customer.IsNull())
            {
                //this person is a customer
                //get all the buySellOrders in which he is a customer
                List<BuySellDoc> lst = BuySellDocBiz.FindAll().Where(x => x.CustomerId == customer.Id).ToList();
                return lst;
            }
            return null;
        }


    }
}
