using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.AbstractNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
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
    public class BuySellDoc : DocumentAbstract
    {
        public BuySellDoc()
        {

            initializeComplexs();
            BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown;

        }


        void initializeComplexs()
        {
            CourierSelected = new BoolDateAndByComplex();
            OrderConfirmedByDeliveryman = new BoolDateAndByComplex();
            xyz = new BoolDateAndByComplex();
            OrderShipped = new BoolDateAndByComplexWithConfirmationCode();
            OrderDelivered = new BoolDateAndByComplexWithConfirmationCode();

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
            OwnerSalesmanCommission = new CommissionComplex();
            DeliverymanSalesmanCommission = new CommissionComplex();
            System_Commission_For_SaleWithoutFreight = new CommissionComplex();
            System_Commission_For_Freight = new CommissionComplex();

            OrderConfirmedByCustomer = new BoolDateAndByComplex();
            OrderConfirmedByOwner = new BoolDateAndByComplex();



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
        /// When accpeted, the document moves to the next level.
        /// This is not persisted
        /// </summary>

        //[NotMapped]
        //[Display(Name = "Accept and move to next step!")]
        //public string AcceptRejectOrEmpty { get; set; }

        //public string AcceptRejectOrEmpty_Accept = "accept";
        //public string AcceptRejectOrEmpty_Reject = "reject";
        //public string AcceptRejectOrEmpty_Empty = "";


        /// <summary>
        /// This is used in View OrderList to determine if the current user is the deliveryman
        /// who is to accept the courier order request.
        /// </summary>
        [NotMapped]
        public string CurrentUser_DeliverymanId { get; set; }


        public string CurrentUsers_FreightOfferTrx_String(string currUser_DeliverymanId)
        {
            if (currUser_DeliverymanId.IsNullOrWhiteSpace())
                return null;

            if (FreightOfferTrxs.IsNullOrEmpty())
                return null;
            //return " <<< NO OFFER >>";
            FreightOfferTrx frtTrx = FreightOfferTrxs.FirstOrDefault(x => x.DeliverymanId == currUser_DeliverymanId);
            if (frtTrx.IsNull())
                return null;

            string date = frtTrx.MetaData.Created.Date_NotNull_Min.ToShortDateString();
            string offerAmount = frtTrx.OfferAmount.ToString("N0");
            string transport = frtTrx.VehicalType.IsNull() ? "Unknown" : frtTrx.VehicalType.FullName();
            string insurance = InsuranceRequired_ToSring;
            string freightOffer = string.Format("On {0} You offered Rs{1} to pick. Vehical Type: {2}, Insurance {3}",
                date,
                offerAmount,
                transport,
                insurance);

            if (!FreightOfferTrxAcceptedId.IsNullOrWhiteSpace())
            {
                freightOffer += " *** Accepted By Seller ***";

            }

            if (frtTrx.IsOfferAccepted)
            {
                freightOffer += " *** Accepted By You ***";
            }
            return freightOffer;

        }

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

        #region Freight Offer



        [Display(Name = "Freight Offers")]
        public virtual ICollection<FreightOfferTrx> FreightOfferTrxs { get; set; }


        [NotMapped]
        public List<FreightOfferTrx> FreightOfferTrxs_Fixed
        {
            get
            {
                if (FreightOfferTrxs.IsNullOrEmpty())
                    return new List<FreightOfferTrx>();
                return FreightOfferTrxs.Where(x => x.MetaData.IsDeleted == false).OrderByDescending(x => x.IsOfferAccepted).OrderBy(x => x.OfferAmount).ToList();
            }
        }



        [Display(Name = "Accepted Trx")]
        public string FreightOfferTrxAcceptedId { get; set; }
        public virtual FreightOfferTrx FreightOfferTrxAccepted { get; set; }


        /// <summary>
        /// This is the offer made by the delivery man to pick and drop the item
        /// </summary>
        /// 
        [NotMapped]
        [Display(Name = "Offer")]
        public string FreightOffer { get; set; }

        [NotMapped]
        [Display(Name = "Freight Offer")]
        public decimal FreightOfferDecimal
        {
            get
            {
                decimal offer;
                bool success = decimal.TryParse(FreightOffer, out offer);
                if (success)
                    return offer;
                return 0;
            }
        }

        /// <summary>
        /// This it the amount the customer has budgeted for freight. Not neccassry he will
        /// get it but it will be the starting value for the deliveryman
        /// </summary>
        [Display(Name = "Customer Freight Budget")]

        public decimal FreightCustomerBudget { get; set; }

        [NotMapped]
        [Display(Name = "Customer Freight Budget")]

        public string FreightCustomerBudget_String { get; set; }



        [NotMapped]
        [Display(Name = "delivery man comments")]
        public string CommentByDeliveryman { get; set; }
















        [Display(Name = "Offered Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime OfferedPickupOnDate { get; set; }



        [Display(Name = "Offered Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime ExpectedDeliveryDate { get; set; }













        [NotMapped]
        [Display(Name = "Agreed Pick up Date")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AgreedPickupDateByDeliveryman
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return DateTime.MinValue;

                return FreightOfferTrxAccepted.PickupDate;
            }
        }



        /// <summary>
        /// This is the date that pick up is requested.
        /// </summary>
        [Display(Name = "Please pick up on ")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime PleasePickupOnDate_Start { get; set; }



        [Display(Name = "Last Day for Pick up")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PleasePickupOnDate_End { get; set; }


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

        #region Vehical Type Requested
        [Display(Name = "Vehical Type Requested")]
        public string VehicalTypeRequestedId { get; set; }

        [Display(Name = "Vehical Type Requested")]
        public virtual VehicalType VehicalTypeRequested { get; set; }

        [NotMapped]
        public SelectList SelectListVehicalTypeRequested { get; set; }
        #endregion

        #region Vehical Type Offered

        [NotMapped]
        [Display(Name = "Vehical Type Offered")]
        public string VehicalTypeOfferedId { get; set; }

        [Display(Name = "Vehical Type Offered")]
        [NotMapped]
        public virtual VehicalType VehicalTypeOffered { get; set; }

        [NotMapped]
        public SelectList SelectListVehicalTypeOffered { get; set; }

        #endregion

        #region Vehical Type Accepted

        [Display(Name = "Vehical Type Accepted")]
        [NotMapped]
        public string VehicalTypeAcceptedId
        {
            get
            {
                if (VehicalTypeAccepted.IsNull())
                    return null;

                return VehicalTypeAccepted.Id;
            }
        }


        [NotMapped]
        [Display(Name = "Vehical Type Accepted")]
        public virtual VehicalType VehicalTypeAccepted
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return null;
                if (FreightOfferTrxAccepted.VehicalType.IsNull())
                    return null;
                return FreightOfferTrxAccepted.VehicalType;
            }
        }


        [NotMapped]
        public SelectList SelectListVehicalTypeAccepted { get; set; }

        #endregion

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
        public BoolDateAndByComplex CourierSelected { get; set; }
        public BoolDateAndByComplex OrderConfirmedByDeliveryman { get; set; }
        public BoolDateAndByComplex xyz { get; set; }
        public BoolDateAndByComplex OrderConfirmedByCustomer { get; set; }
        public BoolDateAndByComplex OrderConfirmedByOwner { get; set; }


        public BoolDateAndByComplexWithConfirmationCode OrderShipped { get; set; }
        public BoolDateAndByComplexWithConfirmationCode OrderDelivered { get; set; }


        #endregion

        //#region Signatures
        ////public BoolDateAndByComplex RequestUnconfirmed_Sign { get; set; }

        ///// <summary>
        ///// When document is RequestUnconfirmed status, and it is signed,
        ///// the status is upgraded to RequestConfirmed status
        ///// 
        ///// Status              Signed  New Status
        ///// RequestUnconfirmed      =>  RequestConfirmed
        ///// RequestConfirmed        =>  ConfirmedBySeller
        ///// ConfirmedBySeller       =>  ReadyForShipment
        ///// ReadyForShipment        =>  ConfirmedByCourier
        ///// PickedUp                =>  Delivered
        ///// RequestConfirmed        =>  Rejected => RequestUnconfirmed      rejected by the seller for some reason
        ///// PickedUp                =>  Rejected => ReadyForShipment        Rejected by the courer for some reason
        ///// Rejected                =>  
        ///// 
        ///// </summary>
        ///// 
        //public SignatureComplex RequestConfirmed_Sign { get; set; }
        //public SignatureComplex ConfirmedBySeller_Sign { get; set; }
        //public SignatureComplex ReadyForShipment_Sign { get; set; }
        //public SignatureComplex ConfirmedByCourier_Sign { get; set; }
        //public SignatureComplex PickedUp_Sign { get; set; }
        //public SignatureComplex Delivered_Sign { get; set; }
        //public SignatureComplex Rejected_Sign { get; set; }
        //public SignatureComplex Problem_Sign { get; set; }

        //#endregion



        //These temp holders needed because we are unable to access cashTrx once we are in
        //BuySellDocBiz. We need it to find the salesperson for the document at a when the bu

        #region Penalties

        public virtual ICollection<PenaltyHeader> PenaltyHeaders { get; set; }
        #endregion

        [NotMapped]

        public string CustomerSalesmanId_TEMP { get; set; }
        [NotMapped]
        public string OwnerSalesmanId_TEMP { get; set; }
        [NotMapped]
        public string DeliverymanSalesmanId_TEMP { get; set; }




        [Display(Name = "Customer Salesman")]
        public string CustomerSalesmanId { get; set; }




        [Display(Name = "Salesman")]
        public Salesman CustomerSalesman { get; set; }

        //[Display(Name = "Owner Salesman Commission %")]
        //public decimal CustomerSalesmanCommission_Percent { get; set; }

        //[Display(Name = "Owner Salesman Commission Min Amount")]
        //public decimal CustomerSalesmanCommission_Amount { get; set; }

        [NotMapped]
        public SelectList SelectListCustomerSalesman { get; set; }






        [Display(Name = "Owner Salesman")]
        public string OwnerSalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public Salesman OwnerSalesman { get; set; }

        [NotMapped]
        public SelectList SelectListOwnerSalesman { get; set; }


        #region Commissions


        public decimal Total_Commission_Amount
        {
            get
            {
                return Total_Charged_To_Owner.Amount + Total_Charged_To_Deliveryman.Amount;
            }
        }

        /// <summary>
        /// This will always be the maximum commission
        /// </summary>
        public virtual CommissionComplex Total_Charged_To_Owner { get; set; }

        /// <summary>
        /// This will always be the maximum commission
        /// </summary>
        public virtual CommissionComplex Total_Charged_To_Deliveryman { get; set; }





        /// <summary>
        /// This is the commission paid to the CustomerSalesmanCommission
        /// </summary>
        public virtual CommissionComplex CustomerSalesmanCommission { get; set; }


        /// <summary>
        /// This is the commission paid to the OwnerSalesmanCommission
        /// </summary>
        public virtual CommissionComplex OwnerSalesmanCommission { get; set; }

        /// <summary>
        /// This is the commission paid to the DelivermanSalesman
        /// </summary>
        public virtual CommissionComplex DeliverymanSalesmanCommission { get; set; }



        /// <summary>
        /// This is the commission paid to the System
        /// </summary>
        public virtual CommissionComplex System_Commission_For_SaleWithoutFreight { get; set; }
        public virtual CommissionComplex System_Commission_For_Freight { get; set; }



        /// <summary>
        /// This is the maximum commission that is charged to the BuySellDoc. It
        /// is the sum of the commissions of SalesmanCustomer + OwnerSalesman + DeliverymanSalesman + System
        /// </summary>
        /// <returns></returns>
        public decimal Get_Maximum_Commission_Chargeable_On_TotalSale_Percent()
        {
            decimal ttlComm = SalesCommissionClass.Commission_Payable_On_TotalSale_Pct(TotalInvoiceLessFreight, Freight_Accepted);
            return ttlComm;

        }



        /// <summary>
        /// This is the maximum commission that is charged to the BuySellDoc. It
        /// is the sum of the commissions of SalesmanCustomer + OwnerSalesman + DeliverymanSalesman + System
        /// 
        /// However, the deliveryman's commission is based on freight, whereas the other commissions are based on
        /// Net sale
        /// </summary>
        public decimal Get_Maximum_Commission_Chargeable_On_TotalSale_Amount()
        {
            decimal ttlComm = SalesCommissionClass.Commission_Payable_On_TotalSale_Amount(TotalInvoiceLessFreight, Freight_Accepted);
            return ttlComm;

        }

        public decimal Get_Maximum_Commision_On_TotalSaleLessFreight_For_OwnerSalesman_Amount()
        {
            decimal commissionAmnt = SalesCommissionClass.CommissionAmount_OwnerSalesman_For(TotalInvoiceLessFreight);
            return commissionAmnt;
        }

        public decimal Get_Maximum_Commision_On_TotalSaleLessFreight_For_OwnerSalesman_Percent()
        {
            decimal commissionPercent = SalesCommissionClass.CommissionPct_OwnerSalesman;
            return commissionPercent;
        }

        public decimal Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Amount()
        {
            decimal ttlComm = SalesCommissionClass.Commission_Payable_On_TotalSaleWithoutFreight_Amount(TotalInvoiceLessFreight);
            return ttlComm;

        }
        public decimal Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Based_On_TotalSale_Percent()
        {
            if (Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Amount() == 0)
                return 0;
            if (TotalInvoice == 0)
                return 0;
            decimal commissionPct = Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Amount() / TotalInvoice * 100;
            return commissionPct;

        }


        public decimal Get_Maximum_Commission_Chargeable_On_Freight_Amount()
        {
            decimal ttlComm = SalesCommissionClass.Commission_Payable_On_Freight_Amount(Freight_Accepted);
            return ttlComm;

        }




        public decimal Get_Maximum_Commission_Chargeable_On_Freight_TO_SalesPeople_And_System_Percent()
        {
            decimal ttlComm =
                SalesCommissionClass.CommissionPct_DeliverymanSalesman +
                SalesCommissionClass.CommissionPct_System;
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


        /// <summary>
        /// This makes the commission amount payable based on total invoice
        /// </summary>
        /// <returns></returns>
        //public decimal Get_System_Sale_Commission_Based_On_TotalInvoice()
        //{
        //    if (System_Commission_For_SaleWithoutFreight.Amount == 0)
        //        return 0;
        //    if (TotalInvoice == 0)
        //        return 0;
        //    decimal pct = System_Commission_For_SaleWithoutFreight.Amount / TotalInvoice;
        //    return pct;
        //}



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
            decimal maxCommAllowed = SalesCommissionClass.Commission_Payable_On_TotalSale_Amount(TotalInvoiceLessFreight, Freight_Accepted);
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



        [Display(Name = "Deliveryman Salesman")]
        public string DeliverymanSalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public Salesman DeliverymanSalesman { get; set; }

        [NotMapped]
        public SelectList SelectListDeliverymanSalesman { get; set; }


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







        [NotMapped]
        public Deliveryman Deliveryman
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return null;

                FreightOfferTrxAccepted.Deliveryman.IsNullThrowException();
                return FreightOfferTrxAccepted.Deliveryman;
            }
        }

        [NotMapped]
        public string DeliverymanId
        {
            get
            {
                if (Deliveryman.IsNull())
                    return "";
                return Deliveryman.Id;
            }
        }
        /// <summary>
        /// If true then the deliveryman has accepted to deliver the products
        /// </summary>
        [NotMapped]
        public bool IsDeliveryman_Accepted_By_Courier
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return false;

                if (FreightOfferTrxAccepted.IsOfferAccepted)
                    return true;
                return false;
            }
        }

        public string GetDeliverymanId()
        {
            if (Deliveryman.IsNull())
                return "";
            return Deliveryman.Id;
        }

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
            //Owner.IsNullThrowException("Owner");
            //Customer.IsNullThrowException("Customer");
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
            string fullName = "Error";

            switch (BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:
                    break;
                case BuySellDocumentTypeENUM.Purchase:
                    fullName = string.Format("{4} {0} of {1} By: {3} To: {2} ({5}) [Rs{6}]", DocumentNumber, MetaData.Created.Date_NotNull_Min.ToString("dd-MMM-yyyy"),
                        ownerName,
                        customerName,
                        statementType,
                        vehicalType,
                        TotalInvoice.ToString("N2"));
                    break;
                case BuySellDocumentTypeENUM.Sale:
                case BuySellDocumentTypeENUM.Delivery:
                default:
                    fullName = string.Format("{4} {0} of {1} By: {2} To: {3} ({5}) [Rs{6}] -{7}", DocumentNumber, MetaData.Created.Date_NotNull_Min.ToString("dd-MMM-yyyy"),
                        ownerName,
                        customerName,
                        statementType,
                        vehicalType,
                        TotalInvoice.ToString("N2"),
                        BuySellDocStateEnum.ToString().ToTitleSentance());
                    break;
            }
            return fullName;
        }


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDoc;
        }
        public void UpdateFreightBidVars(FreightOfferTrx earlierOfferByUser)
        {
            FreightOffer = string.Format("{0:N0}", earlierOfferByUser.OfferAmount);
            CommentByDeliveryman = earlierOfferByUser.Comment;
            OfferedPickupOnDate = earlierOfferByUser.PickupDate;
            VehicalTypeOfferedId = earlierOfferByUser.VehicalTypeId;

        }


        /// <summary>
        /// This is only concerned with updating information
        /// do not add any logic here.
        /// Add logic in Business rules.
        /// </summary>
        /// <param name="icommonWithId"></param>
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            BuySellDoc buySellDoc = BuySellDoc.UnBox(icommonWithId);

            CustomerSalesmanId_TEMP = buySellDoc.CustomerSalesmanId_TEMP;
            OwnerSalesmanId_TEMP = buySellDoc.OwnerSalesmanId_TEMP;
            DeliverymanSalesmanId_TEMP = buySellDoc.DeliverymanSalesmanId_TEMP;

            update_DocumentTypeAndState(buySellDoc);

            switch (buySellDoc.BuySellDocumentTypeEnum)
            {

                //*******************************************************************************
                //*     DELIVERY
                //*******************************************************************************
                case BuySellDocumentTypeENUM.Delivery:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.ReadyForPickup:
                            //update the courier offer
                            update_OfferVariables(buySellDoc);
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;
                        case BuySellDocStateENUM.Enroute:

                            DeliveryCode_Deliveryman_AsEntered = buySellDoc.DeliveryCode_Deliveryman_AsEntered;

                            string minimumNumberStr = ConfigurationManager.AppSettings["PickupDelivery.RandomNumberGenerator.MinimumNumber"];
                            minimumNumberStr.IsNullOrWhiteSpaceThrowException("The RandomNumberGenerator.MinimumNumber is null or empty");

                            DeliveryCode_Deliveryman = DeliveryCode_Deliveryman_AsEntered
                                .Substring(DeliveryCode_Deliveryman_AsEntered.Length - minimumNumberStr.Length);

                            break;

                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                            globalUpdate(buySellDoc);
                            break;
                        case BuySellDocStateENUM.Unknown:
                            break;
                        default:
                            break;
                    }
                    break;


                //*******************************************************************************
                //*     SALE
                //*******************************************************************************
                case BuySellDocumentTypeENUM.Sale:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestConfirmed:
                        //Insurance can be added.
                        //the quantity can be reduced.
                        //a message to the purchaser can be sent.
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            globalUpdate(buySellDoc);
                            break;

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            PickupCode_Seller = buySellDoc.PickupCode_Seller;
                            break;



                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            FreightOfferTrxAcceptedId = buySellDoc.FreightOfferTrxAcceptedId;
                            //CourierSelected = buySellDoc.CourierSelected;
                            break;

                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Delivered:
                        default:
                            break;
                    }
                    break;


                //*******************************************************************************
                //*     PURCHASE
                //*******************************************************************************

                case BuySellDocumentTypeENUM.Purchase:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            //update_Accept(buySellDoc);
                            update_Freight_Request_Variables(buySellDoc);
                            update_VehicalType(buySellDoc);
                            update_Customer_Comment(buySellDoc);
                            update_BillToAddress(buySellDoc);
                            update_ShipToAddress(buySellDoc);
                            //update_CustomerBalanceRefundable(buySellDoc);
                            //update_InsuranceRequired(buySellDoc);
                            //update_OfferVariables(buySellDoc);

                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:

                            update_Customer_Comment(buySellDoc);
                            update_Freight_Request_Variables(buySellDoc);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            update_freightOfferAccepted(buySellDoc);
                            break;

                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    }
                    break;

                //*******************************************************************************
                //*     PURCHASE
                //*******************************************************************************

                case BuySellDocumentTypeENUM.Salesman:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }



        }

        private void update_freightOfferAccepted(BuySellDoc buySellDoc)
        {
            FreightOfferTrxAcceptedId = buySellDoc.FreightOfferTrxAcceptedId;
        }
        private void update_DocumentTypeAndState(BuySellDoc buySellDoc)
        {
            BuySellDocStateEnum = buySellDoc.BuySellDocStateEnum;
            BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;
        }

        private void update_BillToAddress(BuySellDoc buySellDoc)
        {
            AddressBillToComplex = buySellDoc.AddressBillToComplex;
            AddressBillToId = buySellDoc.AddressBillToId;
        }

        private void update_ShipToAddress(BuySellDoc buySellDoc)
        {
            AddressShipToId = buySellDoc.AddressShipToId;
            AddressShipToComplex = buySellDoc.AddressShipToComplex;
        }

        private void update_OfferVariables(BuySellDoc buySellDoc)
        {
            OfferedPickupOnDate = buySellDoc.OfferedPickupOnDate;
            VehicalTypeOfferedId = buySellDoc.VehicalTypeOfferedId;
            FreightOffer = buySellDoc.FreightOffer;
            CommentByDeliveryman = buySellDoc.CommentByDeliveryman;
        }

        private void globalUpdate(BuySellDoc buySellDoc)
        {
            //CourierAccepts = buySellDoc.CourierAccepts;
            //VendorAccepts = buySellDoc.VendorAccepts;

            //update_Accept(buySellDoc);
            update_DocumentTypeAndState(buySellDoc);
            update_OfferVariables(buySellDoc);
            update_InsuranceRequired(buySellDoc);
            update_CustomerBalanceRefundable(buySellDoc);

        }

        //private void update_Accept(BuySellDoc buySellDoc)
        //{
        //    AcceptRejectOrEmpty = buySellDoc.AcceptRejectOrEmpty;
        //}

        private void update_CustomerBalanceRefundable(BuySellDoc buySellDoc)
        {

            //DANGEROUS... why are we doing this? This can be solved by Superbiz.
            CustomerBalanceRefundable = buySellDoc.CustomerBalanceRefundable;
        }

        private void update_InsuranceRequired(BuySellDoc buySellDoc)
        {
            InsuranceRequired = buySellDoc.InsuranceRequired;
        }


        private void update_Freight_Request_Variables(BuySellDoc buySellDoc)
        {
            PleasePickupOnDate_Start = buySellDoc.PleasePickupOnDate_Start;
            PleasePickupOnDate_End = buySellDoc.PleasePickupOnDate_End;
            VehicalTypeRequestedId = buySellDoc.VehicalTypeRequestedId;
            FreightCustomerBudget_String = buySellDoc.FreightCustomerBudget_String;
        }
        private void update_VehicalType(BuySellDoc buySellDoc)
        {
            VehicalTypeRequestedId = buySellDoc.VehicalTypeRequestedId;
        }

        private void update_Customer_Comment(BuySellDoc buySellDoc)
        {
            Comment = buySellDoc.Comment;

        }

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
            IQueryable<BuySellDoc> purchaseSql = allOrders
                                           .Where(x =>
                                            ((x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                                            x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                                            x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                                            x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered) && x.FreightOfferTrxAccepted.DeliverymanId == deliveryId)
                                            || x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup);

            if (deliveryId.IsNullOrWhiteSpace())
            {
                purchaseSql = allOrders
                                               .Where(x =>
                                                ((x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                                                x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                                                x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                                                x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered) &&
                                                (x.FreightOfferTrxAccepted.DeliverymanId != "" || x.FreightOfferTrxAccepted.DeliverymanId != null))
                                                || x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup);
            }

            return purchaseSql;

        }


        public static IQueryable<BuySellDoc> IQueryable_GetSalesmanDocs(IQueryable<BuySellDoc> allOrders, string salesmanId)
        {

            if (salesmanId.IsNullOrWhiteSpace())
                return allOrders
                    .Where(x =>
                        (x.CustomerSalesmanId != null && x.CustomerSalesmanId.Trim() != "") ||
                        (x.OwnerSalesmanId != null && x.OwnerSalesmanId.Trim() != "") ||
                        (x.DeliverymanSalesmanId != null && x.DeliverymanSalesmanId.Trim() != ""));

            IQueryable<BuySellDoc> purchaseSql = allOrders
                .Where(x => x.CustomerSalesmanId == salesmanId || x.OwnerSalesmanId == salesmanId || x.DeliverymanSalesmanId == salesmanId);
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

        public static int GetMoneyBackGuaranteeNumberOfDays()
        {
            string noOfDaysString = ConfigurationManager.AppSettings["MoneyBackGuarantee.NumberOfDays"];
            int noOfDays = noOfDaysString.ToInt();
            return noOfDays;

        }

        public bool IsGuaranteePeriodIsOver()
        {
            bool afterGuaranteePeriod = false;
            DateParameter dp = new DateParameter();
            if (OrderDelivered.IsTrue)
            {
                DateTime orderDeliverDate = OrderDelivered.Date_NotNull_Max;

                if (orderDeliverDate == DateTime.MaxValue)
                    return false;

                int noOfDaysGuarantee = BuySellDoc.GetMoneyBackGuaranteeNumberOfDays();
                DateTime dateMoneyBackExpires = orderDeliverDate.AddDays(noOfDaysGuarantee);

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
    }
}
