using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.AbstractNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Complex;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
//using ModelsClassLibrary.ModelsNS.BuySellDocNS;


namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    /// <summary>
    /// During sale the vendor / seller cannot update the CustomerId or the SellerId
    /// What will happen if the seller and the delivery man are the same. They could cheat the customer.
    /// The customer can always create a problem.
    /// </summary>
    public partial class BuySellDoc : DocumentAbstract
    {
        public BuySellDoc()
        {

            initializeComplexs();
            BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown;

        }


        void initializeComplexs()
        {
            //CourierSelected = new BoolDateAndByComplex();
            //OrderConfirmedByDeliveryman = new BoolDateAndByComplex();

            RequestUnconfirmed = new BoolDateAndByComplex();
            RequestConfirmed = new BoolDateAndByComplex();
            BeingPreparedForShipmentBySeller = new BoolDateAndByComplex();
            ReadyForPickup = new BoolDateAndByComplex();
            CourierAcceptedByBuyerAndSeller = new BoolDateAndByComplex();
            CourierComingToPickUp = new BoolDateAndByComplex();
            PickedUp = new BoolDateAndByComplex();
            Enroute = new BoolDateAndByComplex();
            Rejected = new BoolDateAndByComplex();
            Problem = new BoolDateAndByComplex();
            Delivered = new BoolDateAndByComplexWithConfirmationCode();

            //RequestConfirmed_Sign = new SignatureComplex();
            //ConfirmedBySeller_Sign = new SignatureComplex();
            //ReadyForShipment_Sign = new SignatureComplex();
            //ConfirmedByCourier_Sign = new SignatureComplex();
            //PickedUp_Sign = new SignatureComplex();
            //Delivered_Sign = new SignatureComplex();
            //Rejected_Sign = new SignatureComplex();
            //Problem_Sign = new SignatureComplex();

            AddressBillToComplex = new AddressComplex();
            AddressShipToComplex = new AddressComplex();
            AddressShipFromComplex = new AddressComplex();
            MoneyBackGuarantee = new BoolAndDaysDateAndByComplex();

            Total_Charged_To_Owner = new CommissionComplex();
            Total_Charged_To_Deliveryman = new CommissionComplex();

            CustomerSalesmanCommission = new CommissionComplex();
            SuperCustomerSalesmanCommission = new CommissionComplex();
            SuperSuperCustomerSalesmanCommission = new CommissionComplex();

            OwnerSalesmanCommission = new CommissionComplex();
            SuperOwnerSalesmanCommission = new CommissionComplex();
            SuperSuperOwnerSalesmanCommission = new CommissionComplex();

            DeliverymanSalesmanCommission = new CommissionComplex();
            SuperDeliverymanSalesmanCommission = new CommissionComplex();
            SuperSuperDeliverymanSalesmanCommission = new CommissionComplex();

            System_Commission_For_SaleWithoutFreight = new CommissionComplex();
            System_Commission_For_Freight = new CommissionComplex();




        }

        public void Initialize(string ownerId, string customerId, string addressBillToId, string addressShipToId, string poNumber, DateTime poDate, SelectList selectListOwner, SelectList selectListCustomer, SelectList selectListAddressBillTo, SelectList selectListAddressShipTo)
        {

            base.InitializeAbstract(ownerId, customerId, addressBillToId, addressShipToId, poNumber, poDate, selectListOwner, selectListCustomer, selectListAddressBillTo, selectListAddressShipTo);
            //BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;
            BuySellDocStateEnum = BuySellDocStateENUM.Unknown;
            FreightCustomerBudget = 1000;
            PleasePickupOnDate_Start = DateTime.Now;
            PleasePickupOnDate_End = DateTime.Now.AddDays(10);


        }
        ///// <summary>
        ///// This works out if the item has been delivered, is the cash available
        ///// </summary>
        ///// <returns></returns>
        //public CashStateENUM CashAvailableStatus(int cashBackGuaranteeNoOfDays)
        //{

        //    if (IsCashFromDocAvailableCalculator(cashBackGuaranteeNoOfDays))
        //        return CashStateENUM.Available;
        //    return CashStateENUM.Allocated;

        //}
        public static BuySellDoc UnBox(ICommonWithId icommonWithId)
        {
            BuySellDoc buySellDoc = icommonWithId as BuySellDoc;
            buySellDoc.IsNullThrowException();
            return buySellDoc;
        }

        /// <summary>
        /// The buySellDoc money is always not available, except 
        ///     while request is unconfirmed
        ///     and after delivery and after the cashbank guarantee period.
        /// </summary>
        /// <param name="cashBackGuaranteeNoOfDays"></param>
        /// <returns></returns>

        /// <summary>
        /// If true cash from this document is available.
        /// This is used in the cashTrx report
        /// </summary>
        [NotMapped]
        public bool IsCashAvailable { get; set; }
        /// <summary>
        /// This holds all the current users information
        /// </summary>
        [NotMapped]
        public UserParameter UserParameter { get; set; }

        [NotMapped]
        decimal CustomerBalanceRefundable { get; set; }

        [NotMapped]
        decimal CustomerBalanceNonRefundable { get; set; }


        /// <summary>
        /// This is used in View OrderList to determine if the current user is the deliveryman
        /// who is to accept the courier order request.
        /// </summary>
        [NotMapped]
        public string CurrentUser_DeliverymanId { get; set; }




        /// <summary>
        /// used in BuySellDocs.orderList.cshtml
        /// </summary>
        /// <returns></returns>
        public string PictureAddressPathAny()
        {
            string path = AliKuli.UtilitiesNS.ConfigManagerHelper.DefaultBlankPicture;
            UploadedFile uploadedFile = new UploadedFile();

            if (BuySellItems.IsNullOrEmpty())
                return path;

            if (BuySellItems.FirstOrDefault().ProductChild.IsNull())
                return path;

            if (BuySellItems.FirstOrDefault().ProductChild.Product.IsNull())
                return path;

            if (BuySellItems.FirstOrDefault().ProductChild.Product.MiscFiles.IsNull())
                return path;

            if (BuySellItems.FirstOrDefault().ProductChild.Product.MiscFiles.FirstOrDefault().IsNull())
                return path;


            path = BuySellItems.FirstOrDefault().ProductChild.Product.MiscFiles.FirstOrDefault().GetRelativePathWithFileName();
            return path;

        }

        #region Pickup and Delivery codes

        /// <summary>
        /// This is the code the deliveryman will give to the Seller. The seller will record the code here.
        /// </summary>
        [Display(Name = "Pickup Code Recorded by Seller")]
        public string PickupCode_Seller { get; set; }



        [Display(Name = "Deliveryman Pickup Code")]
        public string PickupCode_Deliveryman { get; set; }

        public string CombinedCode
        {
            get
            {
                return string.Format("{0}-{1}",
                    PickupCode_Deliveryman,
                    DeliveryCode_Customer);
            }
        }



        /// <summary>
        /// This is the code the deliveryman will get from the customer. It will be recorded here
        /// </summary>
        [Display(Name = "Delivery Code Recorded by Deliveryman")]
        public string DeliveryCode_Deliveryman_AsEntered { get; set; }

        [Display(Name = "Delivery Code Recorded by Deliveryman")]
        public string DeliveryCode_Deliveryman { get; set; }





        /// <summary>
        /// This is the delivery code the customer will give to the Deliveryman. It will be generated by the system and saved here.
        /// The deliveryman will save it in DeliveryCode_Deliveryman for onward recording in FreightOfferTrx
        /// </summary>
        [Display(Name = "Delivery Code with Customer")]
        public string DeliveryCode_Customer { get; set; }




        #endregion

        #region Insurance
        [Display(Name = "Package Delivery Insured For")]
        public decimal InsuranceRequired { get; set; }
        public decimal InsuranceRequiredCalculated()
        {
            if (buySellItemFixed.IsNullOrEmpty())
                return 0;
            decimal amt = buySellItemFixed.Sum(x => x.GuaranteeRequiredInRs);
            return amt;
        }

        public string InsuranceRequired_ToSring
        {
            get
            {
                if (InsuranceRequired.IsNull())
                    return "0";

                return InsuranceRequired.ToString("N2");
            }
        }


        [Display(Name = "Money back guarantee")]
        public BoolAndDaysDateAndByComplex MoneyBackGuarantee { get; set; }
        #endregion

        #region Address Ship From
        [Display(Name = "Ship FROM Address")]
        public string AddressShipFromId { get; set; }

        [Display(Name = "Ship FROM Address")]
        public virtual AddressMain AddressShipFrom { get; set; }

        [NotMapped]
        public SelectList SelectListAddressShipFrom { get; set; }

        [NotMapped]
        [Display(Name = "Ship From Address")]
        public virtual AddressComplex AddressShipFromComplex { get; set; }

        [NotMapped]
        [Display(Name = "Ship From Address")]
        public string AddressShipFromString { get; set; }


        #endregion




        /// <summary>
        /// All cash transaction applied to this document are saved here.
        /// </summary>
        //public virtual ICollection<BuySellDocCashTrxApplied> BuySellDocCashTrxApplieds { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        [NotMapped]
        public List<Message> Messages_Fixed
        {
            get
            {
                if (Messages.IsNull())
                    return null;

                return Messages.Where(x => x.MetaData.IsDeleted == false).OrderByDescending(x => x.MetaData.Created.Date).ToList();
            }
        }


        public void Add(BuySellItem buySellItem)
        {
            if (buySellItem.IsNull())
                return;

            if (BuySellItems.IsNull())
            {
                BuySellItems = new List<BuySellItem>();
                BuySellItems.Add(buySellItem);
                return;
            }

            //check to see if the item already exists.
            BuySellItem bsItemFound = BuySellItems.FirstOrDefault(x => x.ProductChildId == buySellItem.Id);
            if (bsItemFound.IsNull())
            {
                BuySellItems.Add(buySellItem);
                return;

            }
            //item was found already ordered... just its quantity is increased
            bsItemFound.Quantity.Order = buySellItem.Quantity.Order;
        }

        public void Add(List<BuySellItem> buySellItems)
        {
            if (buySellItems.IsNullOrEmpty())
                return;

            if (BuySellItems.IsNull())
                BuySellItems = new List<BuySellItem>();

            foreach (var item in buySellItems)
            {
                BuySellItems.Add(item);
            }
        }


        public virtual ICollection<BuySellDocHistory> BuySellDocHistorys { get; set; }



        [NotMapped]
        public List<BuySellDocHistory> BuySellDocHistorys_Fixed
        {
            get
            {
                if (BuySellDocHistorys.IsNull())
                    return null;

                return BuySellDocHistorys
                    .Where(x => x.MetaData.IsDeleted == false)
                    .OrderByDescending(x => x.MetaData.Created.Date)
                    .ToList();
            }
        }



        public virtual ICollection<BuySellItem> BuySellItems { get; set; }
        private ICollection<BuySellItem> buySellItemFixed
        {
            get
            {
                if (BuySellItems.IsNullOrEmpty())
                    return null;

                List<BuySellItem> withoutDeleted = BuySellItems.Where(x => x.MetaData.IsDeleted == false).ToList();
                if (withoutDeleted.IsNullOrEmpty())
                    return null;

                return withoutDeleted;
            }
        }

        public string AnchorDescriptionInOrderListItem()
        {
            string completeDescription = "ERROR";
            string ownersShipFromAddress = AddressShipFromComplex.ToStringOnlyNameAndCityAndCountry;
            string customersShipToAddress = AddressShipToComplex.ToStringOnlyNameAndCityAndCountry;
            string documentName = BuySellDocumentTypeEnum.ToString().ToTitleSentance();
            string documentNumber = string.Format("#{0}", DocumentNumber);
            string vehicalType = VehicalTypeRequested.IsNull() ? "" : "Vehical: " + VehicalTypeRequested.FullName();
            double ttlWt = TotalWeight;
            string ttlWtString = "(?)";
            double ttlQty = TotalQuantity;
            double ttlType = TotalItemTypes;


            if (customersShipToAddress.IsNullOrWhiteSpace())
                customersShipToAddress = "ERROR";

            if (ownersShipFromAddress.IsNullOrWhiteSpace())
                ownersShipFromAddress = "ERROR";

            if (documentName.IsNullOrWhiteSpace())
                documentName = "ERROR";

            if (documentNumber.IsNullOrWhiteSpace())
                documentNumber = "ERROR";
            if (ttlWt != 0)
                ttlWtString = string.Format("{0:N2}KG", ttlWt);

            if (vehicalType.IsNullOrWhiteSpace())
                vehicalType = "VEHICAL ERROR";




            completeDescription = string.Format("{1}  ===>>>  {2} Wt:{3} Qty:{4:N0} Type:{5:N0} {6}", documentNumber, ownersShipFromAddress, customersShipToAddress, ttlWtString, ttlQty, TotalItemTypes, vehicalType);

            return completeDescription;
        }


        public bool IsPickupLate
        {
            get
            {
                DateParameter dp = new DateParameter();
                dp.BeginDate = DateTime.Now;
                dp.EndDate = AgreedPickupDateByDeliveryman;

                return dp.BeginDateAfterEndDate;
            }
        }

        public bool IsDeliveryLate
        {
            get
            {
                DateParameter dp = new DateParameter();
                dp.BeginDate = DateTime.Now;
                dp.EndDate = ExpectedDeliveryDate;

                return dp.BeginDateAfterEndDate;
            }
        }
        //#region Vehical Type Requested
        //[Display(Name = "Vehical Type Requested")]
        //public string VehicalTypeRequestedId { get; set; }

        //[Display(Name = "Vehical Type Requested")]
        //public virtual VehicalType VehicalTypeRequested { get; set; }

        //[NotMapped]
        //public SelectList SelectListVehicalTypeRequested { get; set; }
        //#endregion

        //#region Vehical Type Offered

        //[NotMapped]
        //[Display(Name = "Vehical Type Offered")]
        //public string VehicalTypeOfferedId { get; set; }

        //[Display(Name = "Vehical Type Offered")]
        //[NotMapped]
        //public virtual VehicalType VehicalTypeOffered { get; set; }

        //[NotMapped]
        //public SelectList SelectListVehicalTypeOffered { get; set; }

        //#endregion

        //#region Vehical Type Accepted

        //[Display(Name = "Vehical Type Accepted")]
        //[NotMapped]
        //public string VehicalTypeAcceptedId
        //{
        //    get
        //    {
        //        if (VehicalTypeAccepted.IsNull())
        //            return null;

        //        return VehicalTypeAccepted.Id;
        //    }
        //}


        //[NotMapped]
        //[Display(Name = "Vehical Type Accepted")]
        //public virtual VehicalType VehicalTypeAccepted
        //{
        //    get
        //    {
        //        if (FreightOfferTrxAccepted.IsNull())
        //            return null;
        //        if (FreightOfferTrxAccepted.VehicalType.IsNull())
        //            return null;
        //        return FreightOfferTrxAccepted.VehicalType;
        //    }
        //}


        //[NotMapped]
        //public SelectList SelectListVehicalTypeAccepted { get; set; }

        //#endregion

        #region Totals
        public string TotalCompleted
        {
            get
            {
                if (buySellItemFixed.IsNullOrEmpty())
                    return "0.00";
                List<BuySellItem> deliveredBuySellDocs = buySellItemFixed.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered).ToList();

                if (deliveredBuySellDocs.IsNullOrEmpty())
                    return "0.00";

                decimal ttlSale = 0;
                foreach (var item in buySellItemFixed)
                {
                    ttlSale += item.OrderedRs;
                }

                string formatedStr = string.Format("{0:N2}", ttlSale);
                return formatedStr;

            }
        }
        public decimal TotalOrdered
        {
            get
            {
                if (buySellItemFixed.IsNullOrEmpty())
                    return 0;

                decimal ttlSale = 0;
                foreach (var item in buySellItemFixed)
                {
                    ttlSale += item.OrderedRs;
                }
                return ttlSale;
            }
        }


        public decimal TotalRs
        {
            get
            {
                if (buySellItemFixed.IsNullOrEmpty())
                    return 0;

                decimal totalRs = 0;
                foreach (var item in buySellItemFixed)
                {
                    totalRs += item.OrderedRs;
                }
                return totalRs;
            }
        }

        public double TotalWeight
        {
            get
            {
                double totalWt = 0;

                if (BuySellItems.IsNullOrEmpty())
                    return totalWt;

                foreach (BuySellItem item in BuySellItems)
                {
                    totalWt += item.ProductChild.Product.WeightActual;
                }
                return totalWt;
            }
        }


        public double TotalQuantity
        {
            get
            {
                double ttlQty = 0;
                if (BuySellItems.IsNullOrEmpty())
                    return ttlQty;

                foreach (BuySellItem item in BuySellItems)
                {
                    ttlQty += item.Quantity.Order;
                }


                return ttlQty;
            }
        }

        public int TotalItemTypes
        {
            get
            {
                int itemTypes = 0;

                if (BuySellItems.IsNullOrEmpty())
                    return itemTypes;

                return BuySellItems.Count();
            }
        }
        public decimal TotalInvoice
        {
            get
            {
                return TotalInvoiceLessFreight + Freight_Accepted;
            }
        }
        public decimal TotalInvoiceLessFreight
        {
            get
            {
                return TotalRs;
            }
        }

        #endregion


        #region Freight offered
        [NotMapped]
        [Display(Name = "Payment for Delivery")]
        public decimal Freight_Accepted
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return 0;

                return FreightOfferTrxAccepted.OfferAmount;
            }
        }
        public string Freight_Accepted_Formatted
        {
            get
            {
                return Freight_Accepted.ToString("N0");
            }
        }

        #endregion


        #region Order Status variables
        //public BoolDateAndByComplex CourierSelected { get; set; }
        //public BoolDateAndByComplex OrderConfirmedByDeliveryman { get; set; }
        public BoolDateAndByComplex RequestUnconfirmed { get; set; }
        public BoolDateAndByComplex RequestConfirmed { get; set; }
        public BoolDateAndByComplex BeingPreparedForShipmentBySeller { get; set; }
        public BoolDateAndByComplex ReadyForPickup { get; set; }
        public BoolDateAndByComplex CourierAcceptedByBuyerAndSeller { get; set; }
        public BoolDateAndByComplex CourierComingToPickUp { get; set; }
        public BoolDateAndByComplex PickedUp { get; set; }
        public BoolDateAndByComplex Enroute { get; set; }
        public BoolDateAndByComplex Rejected { get; set; }
        public BoolDateAndByComplex Problem { get; set; }
        public BoolDateAndByComplexWithConfirmationCode Delivered { get; set; }



        /// <summary>
        /// Used in ListOrder.cshtml
        /// </summary>
        /// <returns></returns>
        public string Get_BuySellDocState_Date_Controller()
        {
            string dateString = "";
            string timeString = "";
            switch (BuySellDocStateEnum)
            {
                case BuySellDocStateENUM.Unknown:
                    break;
                case BuySellDocStateENUM.RequestUnconfirmed:
                    dateString = RequestUnconfirmed.Date_NotNull_Min == DateTime.MinValue ? "" : RequestUnconfirmed.Date_NotNull_Min.ToShortDateString();
                    timeString = RequestUnconfirmed.Date_NotNull_Min == DateTime.MinValue ? "" : RequestUnconfirmed.Date_NotNull_Min.ToShortTimeString();
                    break;

                case BuySellDocStateENUM.RequestConfirmed:
                    dateString = RequestConfirmed.Date_NotNull_Min == DateTime.MinValue ? "" : RequestConfirmed.Date_NotNull_Min.ToShortDateString();
                    timeString = RequestConfirmed.Date_NotNull_Min == DateTime.MinValue ? "" : RequestConfirmed.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                    dateString = BeingPreparedForShipmentBySeller.Date_NotNull_Min == DateTime.MinValue ? "" : BeingPreparedForShipmentBySeller.Date_NotNull_Min.ToShortDateString();
                    timeString = BeingPreparedForShipmentBySeller.Date_NotNull_Min == DateTime.MinValue ? "" : BeingPreparedForShipmentBySeller.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.ReadyForPickup:
                    dateString = ReadyForPickup.Date_NotNull_Min == DateTime.MinValue ? "" : ReadyForPickup.Date_NotNull_Min.ToShortDateString();
                    timeString = ReadyForPickup.Date_NotNull_Min == DateTime.MinValue ? "" : ReadyForPickup.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                    dateString = CourierAcceptedByBuyerAndSeller.Date_NotNull_Min == DateTime.MinValue ? "" : CourierAcceptedByBuyerAndSeller.Date_NotNull_Min.ToShortDateString();
                    timeString = CourierAcceptedByBuyerAndSeller.Date_NotNull_Min == DateTime.MinValue ? "" : CourierAcceptedByBuyerAndSeller.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.CourierComingToPickUp:
                    dateString = CourierComingToPickUp.Date_NotNull_Min == DateTime.MinValue ? "" : CourierComingToPickUp.Date_NotNull_Min.ToShortDateString();
                    timeString = CourierComingToPickUp.Date_NotNull_Min == DateTime.MinValue ? "" : CourierComingToPickUp.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.PickedUp:
                    dateString = PickedUp.Date_NotNull_Min == DateTime.MinValue ? "" : PickedUp.Date_NotNull_Min.ToShortDateString();
                    timeString = PickedUp.Date_NotNull_Min == DateTime.MinValue ? "" : PickedUp.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.Enroute:
                    dateString = Enroute.Date_NotNull_Min == DateTime.MinValue ? "" : Enroute.Date_NotNull_Min.ToShortDateString();
                    timeString = Enroute.Date_NotNull_Min == DateTime.MinValue ? "" : Enroute.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.Delivered:
                    dateString = Delivered.Date_NotNull_Min == DateTime.MinValue ? "" : Delivered.Date_NotNull_Min.ToShortDateString();
                    timeString = Delivered.Date_NotNull_Min == DateTime.MinValue ? "" : Delivered.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.Rejected:
                    dateString = Rejected.Date_NotNull_Min == DateTime.MinValue ? "" : Rejected.Date_NotNull_Min.ToShortDateString();
                    timeString = Rejected.Date_NotNull_Min == DateTime.MinValue ? "" : Rejected.Date_NotNull_Min.ToShortTimeString();
                    break;
                case BuySellDocStateENUM.Problem:
                    dateString = Problem.Date_NotNull_Min == DateTime.MinValue ? "" : Problem.Date_NotNull_Min.ToShortDateString();
                    timeString = Problem.Date_NotNull_Min == DateTime.MinValue ? "" : Problem.Date_NotNull_Min.ToShortTimeString();
                    break;
                default:
                    break;
            }

            if (dateString.IsNullOrWhiteSpace())
            {

                dateString = "(Date Not Set)";
            }
            else
            {
                dateString = string.Format("on {0} {1}UTC", dateString, timeString);

                if (BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                {
                    string isPeriodOver = IsGuaranteePeriodIsOver() ? "Yes" : "No";
                    dateString += string.Format(" Guarantee Period ends {0}. Is it Over? {1}!.", GetDateGuaranteeExpires(), isPeriodOver);
                }
            }

            return dateString;

        }

        #endregion


        #region Penalties

        public virtual ICollection<PenaltyHeader> PenaltyHeaders { get; set; }
        #endregion






        [Display(Name = "Customer Salesman")]
        public string CustomerSalesmanId { get; set; }

        [Display(Name = "Customer Salesman")]
        public virtual Salesman CustomerSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListCustomerSalesman { get; set; }



        [Display(Name = "Super Customer Salesman")]
        public string SuperCustomerSalesmanId { get; set; }

        [Display(Name = "Super Customer Salesman")]
        public virtual Salesman SuperCustomerSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListSuperCustomerSalesman { get; set; }



        [Display(Name = "Super Super Customer Salesman")]
        public string SuperSuperCustomerSalesmanId { get; set; }

        [Display(Name = "Super Super Customer Salesman")]
        public virtual Salesman SuperSuperCustomerSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListSuperSuperCustomerSalesman { get; set; }











        [Display(Name = "Owner Salesman")]
        public string OwnerSalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public virtual Salesman OwnerSalesman { get; set; }

        [NotMapped]
        public SelectList SelectListOwnerSalesman { get; set; }


        [Display(Name = "Super Owner Salesman")]
        public string SuperOwnerSalesmanId { get; set; }

        [Display(Name = "Super Owner Salesman")]
        public virtual Salesman SuperOwnerSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListSuperOwnerSalesman { get; set; }





        [Display(Name = "Super Super Owner Salesman")]
        public string SuperSuperOwnerSalesmanId { get; set; }

        [Display(Name = "Super Super Owner Salesman")]
        public virtual Salesman SuperSuperOwnerSalesman { get; set; }

        [NotMapped]
        public SelectList SelectListSuperSuperOwnerSalesman { get; set; }










        [Display(Name = "Deliveryman Salesman")]
        public string DeliverymanSalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public virtual Salesman DeliverymanSalesman { get; set; }

        [NotMapped]
        public SelectList SelectListDeliverymanSalesman { get; set; }

        [Display(Name = "Super Deliveryman Salesman")]
        public string SuperDeliverymanSalesmanId { get; set; }

        [Display(Name = "Super Deliveryman Salesman")]
        public virtual Salesman SuperDeliverymanSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListSuperDeliverymanSalesman { get; set; }



        [Display(Name = "Super Super Deliveryman Salesman")]
        public string SuperSuperDeliverymanSalesmanId { get; set; }

        [Display(Name = "Super Super Deliveryman Salesman")]
        public virtual Salesman SuperSuperDeliverymanSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListSuperSuperDeliverymanSalesman { get; set; }






        #region Commissions


        public decimal Total_Commission_Amount
        {
            get
            {
                return Total_Charged_To_Owner.Amount + Total_Charged_To_Deliveryman.Amount;
            }
        }

        /// <summary>





        /// <summary>
        /// This is the commission paid to the CustomerSalesmanCommission
        /// </summary>
        public virtual CommissionComplex CustomerSalesmanCommission { get; set; }
        public virtual CommissionComplex SuperCustomerSalesmanCommission { get; set; }
        public virtual CommissionComplex SuperSuperCustomerSalesmanCommission { get; set; }


        /// <summary>
        /// This is the commission paid to the OwnerSalesmanCommission
        /// </summary>
        public virtual CommissionComplex OwnerSalesmanCommission { get; set; }
        public virtual CommissionComplex SuperOwnerSalesmanCommission { get; set; }
        public virtual CommissionComplex SuperSuperOwnerSalesmanCommission { get; set; }

        /// <summary>
        /// This is the commission paid to the DelivermanSalesman
        /// </summary>
        public virtual CommissionComplex DeliverymanSalesmanCommission { get; set; }
        public virtual CommissionComplex SuperDeliverymanSalesmanCommission { get; set; }
        public virtual CommissionComplex SuperSuperDeliverymanSalesmanCommission { get; set; }



        /// <summary>
        /// This is the commission paid to the System
        /// </summary>
        public virtual CommissionComplex System_Commission_For_SaleWithoutFreight { get; set; }
        public virtual CommissionComplex System_Commission_For_Freight { get; set; }

        /// This will always be the maximum commission
        /// </summary>
        public virtual CommissionComplex Total_Charged_To_Owner { get; set; }

        /// <summary>
        /// This will always be the maximum commission
        /// </summary>
        public virtual CommissionComplex Total_Charged_To_Deliveryman { get; set; }



        /// <summary>
        /// This is the maximum commission that is charged to the BuySellDoc. It
        /// is the sum of the commissions of SalesmanCustomer + OwnerSalesman + DeliverymanSalesman + System
        /// </summary>
        /// <returns></returns>
        public decimal Get_Maximum_Commission_Chargeable_On_TotalSale_Percent()
        {
            decimal ttlComm = SalesCommissionClass.Get_Maximum_Commission_Chargeable_On_TotalSale_Percent(TotalInvoiceLessFreight, Freight_Accepted);
            return ttlComm;

        }

        public decimal Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Amount()
        {
            decimal ttlComm = SalesCommissionClass.TotalCommissionOnSaleWithoutFreight_Amount(TotalInvoiceLessFreight);
            return ttlComm;

        }

        public decimal Get_Maximum_Commission_Chargeable_On_Freight_Amount()
        {
            decimal ttlComm = SalesCommissionClass.TotalCommissionOnFreight_Amount(Freight_Accepted);
            return ttlComm;

        }




        public decimal Get_Maximum_Commission_Chargeable_On_SaleWithoutFreight_To_SalesPeople_And_System_Percent()
        {
            decimal ttlComm =
                SalesCommissionClass.CommissionPct_OwnerSalesman +
                SalesCommissionClass.CommissionPct_CustomerSalesman +
                SalesCommissionClass.CommissionPct_System;
            return ttlComm;

        }

        public decimal Get_Maximum_Commission_Estimate_Chargeable_On_Total_Invoice_Percent()
        {
            decimal ttl =
                Get_Maximum_Commission_Chargeable_On_SaleWithoutFreight_To_SalesPeople_And_System_Percent() +
                Get_Maximum_Commission_Chargeable_On_Freight_TO_SalesPeople_And_System_Percent();
            return ttl;
        }


        /// <summary>
        /// This is the total commission actually payable by the BuySellDoc
        /// </summary>
        /// <returns></returns>
        public decimal Get_Actual_Commission_Payable_On_Freight_Amount()
        {
            decimal ttlComm = DeliverymanSalesmanCommission.Amount + System_Commission_For_Freight.Amount;
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


        /// <summary>
        /// This makes the commission amount payable based on total invoice
        /// </summary>
        /// <returns></returns>
        public decimal Get_OwnersSalesman_Commission_Based_On_TotalInvoice()
        {
            if (OwnerSalesmanCommission.Amount == 0)
                return 0;
            if (TotalInvoice == 0)
                return 0;
            decimal pct = OwnerSalesmanCommission.Amount / TotalInvoice;
            return pct;
        }
        /// <summary>
        /// This makes the commission amount payable based on total invoice
        /// </summary>
        /// <returns></returns>
        public decimal Get_CustomersSalesman_Commission_Based_On_TotalInvoice()
        {
            if (CustomerSalesmanCommission.Amount == 0)
                return 0;
            if (TotalInvoice == 0)
                return 0;
            decimal pct = CustomerSalesmanCommission.Amount / TotalInvoice;
            return pct;
        }


        /// <summary>
        /// This makes the commission amount payable based on total invoice
        /// </summary>
        /// <returns></returns>
        public decimal Get_DeliverymansSalesman_Commission_Based_On_TotalInvoice()
        {
            if (DeliverymanSalesmanCommission.Amount == 0)
                return 0;
            if (TotalInvoice == 0)
                return 0;
            decimal pct = DeliverymanSalesmanCommission.Amount / TotalInvoice;
            return pct;
        }


        /// <summary>
        /// This makes the commission amount payable based on total invoice
        /// </summary>
        /// <returns></returns>
        public decimal Get_System_Freight_Commission_Based_On_TotalInvoice()
        {
            if (System_Commission_For_Freight.Amount == 0)
                return 0;
            if (TotalInvoice == 0)
                return 0;
            decimal pct = System_Commission_For_Freight.Amount / TotalInvoice;
            return pct;
        }




        #endregion


        #region Commission checks

        public void ErrorCheckForCommission()
        {
            check_TotalCommission_Is_Greater_Than_Total_Allowed();
            //check_GetTotalCommissionEarnedBySystem_Amount_is_Greater_Than_Zero();
            check_If_OwnerSalesman_Commission_Pct__Zero_Then_Amount_Zero();
            check_If_CustomerSalesman_Commission_Pct__Zero_Then_Amount_Zero();
            check_If_DeliverymanSalesman_Commission_Pct__Zero_Then_Amount_Zero();
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
            //decimal totalCommissions = Get_Actual_Commission_Payable_On_TotalSale_Amount();
            decimal maxCommAllowed = SalesCommissionClass.Commission_Payable_On_Invoice_Amount(TotalInvoiceLessFreight, Freight_Accepted);
            if (Total_Commission_Amount > maxCommAllowed)
                throw new Exception("Commissions are less.");

        }

        private void check_If_OwnerSalesman_Commission_Pct__Zero_Then_Amount_Zero()
        {
            if (OwnerSalesmanCommission.Percent == 0)
            {
                if (OwnerSalesmanCommission.Amount != 0)
                {
                    throw new Exception("Owner Salesman Commission Pct is zero but ammount is not Zero.");
                }
            }
            else
            {
                if (OwnerSalesmanCommission.Amount != 0)
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
                if (CustomerSalesmanCommission.Amount != 0)
                {
                    throw new Exception("Customer Salesman Commission Pct is zero but ammount is not Zero.");
                }
            }
            else
            {
                if (CustomerSalesmanCommission.Amount != 0)
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
                if (DeliverymanSalesmanCommission.Amount != 0)
                {
                    throw new Exception("Deliveryman Salesman Commission Pct is zero but ammount is not Zero.");
                }
            }
            else
            {
                if (DeliverymanSalesmanCommission.Amount != 0)
                {
                    return;
                }
                //it is possible the amount is zero but the commission percent is not. Eg. if freight is zero
                //then the max commission amount will still be a number, but of zero.

            }


        }

        #endregion





        public bool IsAllItemPricesOriginal
        {
            get
            {
                if (BuySellItems.IsNullOrEmpty())
                    return true;

                foreach (BuySellItem item in BuySellItems)
                {
                    if (!item.IsSalePriceSame)
                        return false;
                }
                return true;
            }
        }


        #region BuySellDocStateEnum and BuySellDocumentTypeEnum


        //tells us if item is in proccess
        [Display(Name = "Document State")]
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }

        [NotMapped]
        public SelectList SelectListBuySellDocStateEnum { get { return EnumExtention.ToSelectListSorted<BuySellDocStateENUM>(BuySellDocStateENUM.All); } }


        [NotMapped]
        public BuySellDocStateModifierENUM BuySellDocStateModifierEnum { get; set; }




        //tells us if this is a sale or purchase. It could be either, depending how
        // you are looking at it.
        [NotMapped]
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }

        [NotMapped]
        public IBuySellDocViewState BuySellDocViewState
        {
            get
            {
                //Customer.IsNullThrowException();
                //Owner.IsNullThrowException();
                BuySellDocViewStateController buySellDocViewStateController = new BuySellDocViewStateController(BuySellDocStateEnum, BuySellDocumentTypeEnum, Customer, Owner, CustomerBalanceRefundable, CustomerBalanceNonRefundable);
                return buySellDocViewStateController.GetBuySellDocStateController();
            }
        }




        #endregion





        public IBuySellDocViewState GetBuySellDocViewState(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        {
            //note the customer and owner come from the BuysellDoc. It is in the abstract.
            BuySellDocViewStateController buySellDocViewStateController = new BuySellDocViewStateController(BuySellDocStateEnum, BuySellDocumentTypeEnum, Customer, Owner, CustomerBalanceRefundable, CustomerBalanceNonRefundable);
            return buySellDocViewStateController.GetBuySellDocStateController();

        }


        public override bool HideNameInView()
        {
            return true;
        }

        public override string FullName()
        {

            string statementType = "Error";
            string ownerName = "Owner not found";
            string customerName = "Customer not found";
            string vehicalType = "Vehical type not found";
            switch (BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Purchase:
                    statementType = "Purchase Order No: ";
                    break;

                case BuySellDocumentTypeENUM.Sale:
                    statementType = "Sale Order No: ";
                    break;

                case BuySellDocumentTypeENUM.Delivery:
                    statementType = "Delivery No: ";
                    break;

                case BuySellDocumentTypeENUM.Salesman:
                    statementType = "Document No: ";
                    break;

                default:
                    break;
            }


            if (!VehicalTypeRequested.IsNull())
                vehicalType = VehicalTypeRequested.Name;
            if (!Owner.IsNull())
                ownerName = Owner.Name;

            if (!Customer.IsNull())
                customerName = Customer.Name;

            string fullName = string.Format("{4} {0} of {1} By: {2} To: {3} ({5}) [Rs{6}] -{7}", DocumentNumber, MetaData.Created.Date_NotNull_Min.ToString("dd-MMM-yyyy"),
                ownerName,
                customerName,
                statementType,
                vehicalType,
                TotalInvoice.ToString("N2"),
                BuySellDocStateEnum.ToString().ToTitleSentance());

            return fullName;
        }


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDoc;
        }






        //private string parse_DeliveryCode_Deliveryman(string DeliveryCode_Deliveryman_AsEntered)
        //{

        //    string minimumNumberStr = BuySellDoc.GetRandomGeneratorMinimumNumber();
        //    minimumNumberStr.IsNullOrWhiteSpaceThrowException("The RandomNumberGenerator.MinimumNumber is null or empty");
        //    //get the digits from the right which are relevant
        //    string code = DeliveryCode_Deliveryman_AsEntered
        //        .Substring(DeliveryCode_Deliveryman_AsEntered.Length - minimumNumberStr.Length);


        //    return code;
        //    //DeliveryCode_Deliveryman = DeliveryCode_Deliveryman_AsEntered
        //    //    .Substring(DeliveryCode_Deliveryman_AsEntered.Length - minimumNumberStr.Length);


        //}

        //private void update_freightOfferAccepted(BuySellDoc buySellDoc)
        //{
        //    FreightOfferTrxAcceptedId = buySellDoc.FreightOfferTrxAcceptedId;
        //}

        //private void update_InsuranceRequired(BuySellDoc buySellDoc)
        //{
        //    InsuranceRequired = buySellDoc.InsuranceRequired;
        //}


        //private void update_Freight_Request_Variables(BuySellDoc buySellDoc)
        //{
        //    PleasePickupOnDate_Start = buySellDoc.PleasePickupOnDate_Start;
        //    PleasePickupOnDate_End = buySellDoc.PleasePickupOnDate_End;
        //    VehicalTypeRequestedId = buySellDoc.VehicalTypeRequestedId;
        //    FreightCustomerBudget_String = buySellDoc.FreightCustomerBudget_String;
        //}
        //private void update_VehicalType(BuySellDoc buySellDoc)
        //{
        //    VehicalTypeRequestedId = buySellDoc.VehicalTypeRequestedId;
        //}

        #region IQueriable


        public static IQueryable<BuySellDoc> IQuerable_Orders_For(BuySellDocStateENUM buySellDocStateEnum, IQueryable<BuySellDoc> filteredDocs)
        {
            switch (buySellDocStateEnum)
            {

                case BuySellDocStateENUM.RequestUnconfirmed:
                case BuySellDocStateENUM.RequestConfirmed:
                case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                case BuySellDocStateENUM.ReadyForPickup:
                case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                case BuySellDocStateENUM.CourierComingToPickUp:
                case BuySellDocStateENUM.PickedUp:
                case BuySellDocStateENUM.Enroute:
                case BuySellDocStateENUM.Delivered:
                case BuySellDocStateENUM.Rejected:
                case BuySellDocStateENUM.Problem:
                    return IQuerable_DocsIqFor(buySellDocStateEnum, filteredDocs);


                case BuySellDocStateENUM.InProccess:

                    return IQuerable_InProccess(filteredDocs); ;


                case BuySellDocStateENUM.All:
                    return IQuerable_DocsAllIQ(filteredDocs);


                case BuySellDocStateENUM.Unknown:

                default:
                    //https://stackoverflow.com/questions/13867794/returning-empty-iqueryable
                    //    return Enumerable.Empty<BuySellDoc>().AsQueryable();
                    return new List<BuySellDoc>().AsQueryable();
            }
        }
        static IQueryable<BuySellDoc> IQuerable_InProccess(IQueryable<BuySellDoc> filteredDocsByDate)
        {
            IQueryable<BuySellDoc> docs = filteredDocsByDate
               .Where(x =>
                   x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                   x.BuySellDocStateEnum == BuySellDocStateENUM.BeingPreparedForShipmentBySeller ||
                   x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup ||
                   x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                   x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                   x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                   x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute);
            return docs;
        }

        static IQueryable<BuySellDoc> IQuerable_DocsAllIQ(IQueryable<BuySellDoc> filteredDocsByDate)
        {
            IQueryable<BuySellDoc> docs = filteredDocsByDate
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.BeingPreparedForShipmentBySeller ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Rejected ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Problem);
            return docs;
        }

        static IQueryable<BuySellDoc> IQuerable_DocsIqFor(BuySellDocStateENUM buySellDocStateEnum, IQueryable<BuySellDoc> filteredDocsByDate)
        {
            IQueryable<BuySellDoc> docs = filteredDocsByDate
                            .Where(x => x.BuySellDocStateEnum == buySellDocStateEnum);
            return docs;
        }



        public static IQueryable<BuySellDoc> IQueryable_GetSaleDocs(IQueryable<BuySellDoc> allOrders, string ownerId)
        {
            if (ownerId.IsNullOrWhiteSpace())
                return allOrders;

            IQueryable<BuySellDoc> saleSql = allOrders
                .Where(x => x.OwnerId == ownerId);


            return saleSql;

        }


        public static IQueryable<BuySellDoc> IQueryable_GetPurchaseDocs(IQueryable<BuySellDoc> allOrders, string customerId)
        {
            if (customerId.IsNullOrWhiteSpace())
                return allOrders;

            IQueryable<BuySellDoc> purchaseSql = allOrders
                .Where(x => x.CustomerId == customerId);
            return purchaseSql;

        }

        public static IQueryable<BuySellDoc> IQueryable_GetDeliveryDocs(IQueryable<BuySellDoc> allOrders, string deliveryId)
        {
            //IQueryable<BuySellDoc> purchaseSql = allOrders
            //                               .Where(x =>
            //                                ((x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
            //                                x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
            //                                x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
            //                                x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered) && x.FreightOfferTrxAccepted.DeliverymanId == deliveryId)
            //                                || x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup);
            IQueryable<BuySellDoc> purchaseSql = allOrders
                                           .Where(x =>
                                            ((x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                                            x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                                            x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered) && x.FreightOfferTrxAccepted.DeliverymanId == deliveryId) || x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller || x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup);

            List<BuySellDoc> purchaseSql_2_DEBUG = purchaseSql.ToList();


            return purchaseSql;

        }


        public static IQueryable<BuySellDoc> IQueryable_GetSalesmanDocs(IQueryable<BuySellDoc> allOrders, string salesmanId)
        {

            if (salesmanId.IsNullOrWhiteSpace())
                return allOrders
                    .Where(x =>
                        (x.CustomerSalesmanId != null && x.CustomerSalesmanId.Trim() != "") ||
                        (x.CustomerSalesman.ParentSalesmanId != null && x.CustomerSalesman.ParentSalesmanId.Trim() != "") ||
                        (x.CustomerSalesman.ParentSalesman.ParentSalesmanId != null && x.CustomerSalesman.ParentSalesman.ParentSalesmanId.Trim() != "") ||
                        
                        (x.OwnerSalesman != null && x.OwnerSalesmanId.Trim() != "") ||
                        (x.OwnerSalesman.ParentSalesmanId != null && x.OwnerSalesman.ParentSalesmanId.Trim() != "") ||
                        (x.OwnerSalesman.ParentSalesman.ParentSalesmanId != null && x.OwnerSalesman.ParentSalesman.ParentSalesmanId.Trim() != "") ||

                        (x.DeliverymanSalesmanId != null && x.DeliverymanSalesmanId.Trim() != "") ||
                        (x.DeliverymanSalesman.ParentSalesmanId != null && x.DeliverymanSalesman.ParentSalesmanId.Trim() != "") ||
                        (x.DeliverymanSalesman.ParentSalesman.ParentSalesmanId != null && x.DeliverymanSalesman.ParentSalesman.ParentSalesmanId.Trim() != ""));

            IQueryable<BuySellDoc> purchaseSql = allOrders
                .Where(x =>
                    x.CustomerSalesmanId == salesmanId ||
                    x.SuperCustomerSalesmanId == salesmanId ||
                    x.SuperSuperCustomerSalesmanId == salesmanId ||
                    
                    x.OwnerSalesmanId == salesmanId ||
                    x.SuperOwnerSalesmanId == salesmanId ||
                    x.SuperSuperOwnerSalesmanId == salesmanId ||
                    
                    x.DeliverymanSalesmanId == salesmanId ||
                    x.SuperDeliverymanSalesmanId== salesmanId ||
                    x.SuperSuperDeliverymanSalesmanId == salesmanId);
            return purchaseSql;

        }


        #endregion


        #region GetMenuToolTip

        public static string GetMenuToolTip(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string money, string count)
        {
            string content = "Error in OperOrdersForPerson -Copy.getMenuToolTip";
            string str = content;

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:
                    break;
                case BuySellDocumentTypeENUM.Sale:
                    {

                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestUnconfirmed.ToolTip"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestConfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;
                        }

                        str = string.Format(content, count, money);
                        return str;

                    }
                case BuySellDocumentTypeENUM.Purchase:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                case BuySellDocumentTypeENUM.Delivery:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                case BuySellDocumentTypeENUM.Salesman:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                default:
                    break;
            }

            string docState = buySellDocStateEnum.ToString().ToTitleCase();
            string docType = buySellDocumentTypeEnum.ToString().ToTitleCase();

            string docName = string.Format("{0} {1}", docType, docState);
            return docName;
        }

        public static string GetMenuToolTip_Admin(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string money, string count)
        {
            string content = "Error in OperOrdersForPerson -Copy.getMenuToolTip";
            string str = content;

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:
                    break;
                case BuySellDocumentTypeENUM.Sale:
                    {

                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.RequestUnconfirmed.ToolTip"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.RequestConfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;
                        }

                        str = string.Format(content, count, money);
                        return str;

                    }
                case BuySellDocumentTypeENUM.Purchase:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                case BuySellDocumentTypeENUM.Delivery:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                case BuySellDocumentTypeENUM.Salesman:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.CourierAcceptedByBuyerAndSeller.ToolTip"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.CourierComingToPickUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Enroute.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                default:
                    break;
            }

            string docState = buySellDocStateEnum.ToString().ToTitleCase();
            string docType = buySellDocumentTypeEnum.ToString().ToTitleCase();

            string docName = string.Format("{0} {1}", docType, docState);
            return docName;
        }

        #endregion

        #region GetMenuItem

        public static string GetMenuItem(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string money, string count)
        {

            string content = "Error in OperOrdersForPerson -Copy.getMenuItem";
            string str = content;

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    {

                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;
                        }
                        str = string.Format(content, money, count);
                        return str;

                    }
                case BuySellDocumentTypeENUM.Purchase:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }
                case BuySellDocumentTypeENUM.Delivery:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.Deliveryman.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }
                case BuySellDocumentTypeENUM.Salesman:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.Salesman.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }

                case BuySellDocumentTypeENUM.Unknown:
                default:
                    throw new Exception("Unknown Document type");
            }

        }

        public static string GetMenuItem_Admin(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string money, string count)
        {

            string content = "Error in OperOrdersForPerson -Copy.getMenuItem";
            string str = content;

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    {

                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.SaleOrders.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;
                        }
                        str = string.Format(content, money, count);
                        return str;

                    }
                case BuySellDocumentTypeENUM.Purchase:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }
                case BuySellDocumentTypeENUM.Delivery:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.Deliveryman.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }
                case BuySellDocumentTypeENUM.Salesman:
                    {
                        switch (buySellDocStateEnum)
                        {
                            //case BuySellDocStateENUM.New:
                            //case BuySellDocStateENUM.Closed:
                            //case BuySellDocStateENUM.BackOrdered:
                            //case BuySellDocStateENUM.Quotation:
                            //case BuySellDocStateENUM.Credit:
                            //    throw new NotImplementedException();

                            //case BuySellDocStateENUM.Canceled:
                            //    break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForPickup:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.CourierAcceptedByBuyerAndSeller.MenuItem"];
                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.CourierComingToPickUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Enroute:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Enroute.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.system.Salesman.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }

                case BuySellDocumentTypeENUM.Unknown:
                default:
                    throw new Exception("Unknown Document type");
            }

        }

        #endregion

        public override void SelfErrorCheck()
        {

            if (BuySellDocStateEnum == BuySellDocStateENUM.Unknown)
                throw new Exception("Unknown Buy Sell Doc State");

            if (BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Unknown)
                throw new Exception("Unknown document type");

            MenuManager.IsNullThrowException("Menu Manager");

            base.SelfErrorCheck();

            ErrorCheckForCommission();
        }

        public bool IsCurrUserCustomer(UserParameter userParameter)
        {
            if (userParameter.CustomerId.IsNullOrWhiteSpace())
                return false;

            if (CustomerId.IsNullOrWhiteSpace())
                return false;
            return userParameter.CustomerId == CustomerId;
        }
        public bool IsCurrUserOwner(UserParameter userParameter)
        {
            if (userParameter.OwnerId.IsNullOrWhiteSpace())
                return false;

            if (OwnerId.IsNullOrWhiteSpace())
                return false;

            return userParameter.OwnerId == OwnerId;
        }
        public bool IsCurrUserDeliveryman(UserParameter userParameter)
        {
            if (userParameter.DeliverymanId.IsNullOrWhiteSpace())
                return false;

            if (DeliverymanId.IsNullOrWhiteSpace())
                return false;

            return userParameter.DeliverymanId == DeliverymanId;
        }

        public bool IsCurrUserCustomerSalesman(UserParameter userParameter)
        {
            if (userParameter.SalesmanId.IsNullOrWhiteSpace())
                return false;

            if (CustomerSalesmanId.IsNullOrWhiteSpace())
                return false;

            return userParameter.SalesmanId == CustomerSalesmanId;
        }


        public bool IsCurrUserOwnerSalesman(UserParameter userParameter)
        {
            if (userParameter.SalesmanId.IsNullOrWhiteSpace())
                return false;

            if (OwnerSalesmanId.IsNullOrWhiteSpace())
                return false;

            return userParameter.SalesmanId == OwnerSalesmanId;
        }

        public bool IsCurrUserDeliverymanSalesman(UserParameter userParameter)
        {
            if (userParameter.SalesmanId.IsNullOrWhiteSpace())
                return false;

            if (DeliverymanSalesmanId.IsNullOrWhiteSpace())
                return false;

            return userParameter.SalesmanId == DeliverymanSalesmanId;
        }



        public decimal GetPercentFreightToTotalInvoice(decimal totalPaymentAmount)
        {
            if (Freight_Accepted == 0)
                return 0;
            if (TotalInvoice == 0)
                return 0;
            if (totalPaymentAmount == 0)
                return 0;
            decimal pct = totalPaymentAmount / TotalInvoice;
            return pct;
        }

        public DateTime GetDateGuaranteeExpires()
        {
            if (Delivered.IsTrue)
            {
                DateTime orderDeliverDate = Delivered.Date_NotNull_Max;

                if (orderDeliverDate == DateTime.MaxValue)
                    return DateTime.MaxValue; ;

                int noOfDaysGuarantee = BuySellDoc.GetMoneyBackGuaranteeNumberOfDays();
                DateTime dateMoneyBackExpires = orderDeliverDate.AddDays(noOfDaysGuarantee);

                return dateMoneyBackExpires;
            }

            return DateTime.MaxValue; ;

        }
        public bool IsGuaranteePeriodIsOver()
        {
            bool afterGuaranteePeriod = false;
            DateParameter dp = new DateParameter();
            if (Delivered.IsTrue)
            {
                DateTime orderDeliverDate = Delivered.Date_NotNull_Max;

                if (orderDeliverDate == DateTime.MaxValue)
                    return false;

                DateTime dateMoneyBackExpires = GetDateGuaranteeExpires();

                afterGuaranteePeriod = dp.Date1AfterDate2(
                    DateTime.Now,
                    dateMoneyBackExpires);
            }
            return afterGuaranteePeriod;
        }

        public bool IsCashAvailableTo_Deliveryman()
        {


            if (BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return true;


            if (BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
            {
                if (IsGuaranteePeriodIsOver())
                    return true;
            }

            return false;
        }

        public bool IsCashAvailableTo_Customer()
        {

            if (BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return true;

            if (BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return true;

            return false;


        }

        public bool IsCashAvailableTo_Owner()
        {

            if (BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return true;


            if (BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
            {
                if (IsGuaranteePeriodIsOver())
                    return true;
            }
            return false;


        }


        //public bool IsCashFromDocAvailableCalculator(int cashBackGuaranteeNoOfDays)
        //{
        //    if (BuySellDocStateEnum == BuySellDocStateENUM.Problem)
        //        return false;

        //    if (BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
        //        return true;

        //    if (BuySellDocStateEnum != BuySellDocStateENUM.Delivered)
        //        return false;


        //    int cashBackNoOfDays = cashBackGuaranteeNoOfDays * (-1);
        //    string err = string.Format("Delivered order date is null for {0}", FullName());
        //    OrderDelivered.Date.IsNullThrowException(err);

        //    //get the date of the record and see if the window is passed
        //    DateTime dateOfDelivery = OrderDelivered.Date_NotNull_Min;

        //    DateParameter dp = new DateParameter();
        //    dp.BeginDate = DateTime.Now.AddDays(cashBackNoOfDays);
        //    dp.EndDate = dateOfDelivery;

        //    if (dp.BeginDateBeforeEndDate)
        //        return true;

        //    return false;

        //}


        public static int GetMoneyBackGuaranteeNumberOfDays()
        {
            string noOfDaysString = ConfigurationManager.AppSettings["MoneyBackGuarantee.NumberOfDays"];
            int noOfDays = noOfDaysString.ToInt();
            return noOfDays;

        }
        public static string GetRandomGeneratorMinimumNumber()
        {
            string minNumbStr = ConfigurationManager.AppSettings["PickupDelivery.RandomNumberGenerator.MinimumNumber"];

            if (minNumbStr.Trim() == "")
                throw new Exception("Unable to access minimum Random Number");

            return minNumbStr;
            //int minNumber = minNumbStr.ToInt();
            //return minNumber;

        }

        public decimal Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Based_On_TotalSale_Percent()
        {
            decimal commissionPct = SalesCommissionClass.TotalCommissionOnSaleWithoutFreight_Percent();
            return commissionPct;


        }

        public decimal Get_Maximum_Commission_Chargeable_On_Freight_TO_SalesPeople_And_System_Percent()
        {
            decimal ttlComm = SalesCommissionClass.TotalCommissionOnFreight_Precent();
            return ttlComm;

        }
    }
}
