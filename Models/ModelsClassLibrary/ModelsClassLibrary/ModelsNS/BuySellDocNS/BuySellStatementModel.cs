using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    /// <summary>
    /// Note. The transactions are already filtered. The date is only used to create the heading. This only shows the documents, not the account.
    /// </summary>
    [NotMapped]
    public class BuySellStatementModel
    {
        public BuySellStatementModel()
        {

        }
        public BuySellStatementModel(List<BuySellDoc> sellDocs, DateTime fromDate, DateTime toDate, BuySellDocumentTypeENUM buySellDocumentTypeEnum, bool isAdmin, decimal customerBalanceRefundable, decimal customerBalanceNonRefundable, Deliveryman currentUsers_Deliveryman, BuySellDocStateENUM buySellDocStateEnum = BuySellDocStateENUM.Unknown)
        {
            Docs = sellDocs;
            FromDate = fromDate;
            ToDate = toDate;
            BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            BuySellDocStateEnum = buySellDocStateEnum;
            IsAdmin = isAdmin;

            CustomerBalanceNonRefundable = customerBalanceNonRefundable;
            CustomerBalanceRefundable = customerBalanceRefundable;
            CurrentUsers_Deliveryman = currentUsers_Deliveryman;

            //add the document type to the docs
            add_documentType_to_SellDocs();
        }

        private void add_documentType_to_SellDocs()
        {
            if (!Docs.IsNullOrEmpty())
            {
                foreach (BuySellDoc item in Docs)
                {
                    item.BuySellDocumentTypeEnum = BuySellDocumentTypeEnum;
                }
            }

        }
        /// <summary>
        /// if true then emit admin reports 
        /// </summary>



        decimal CustomerBalanceRefundable { get; set; }

        decimal CustomerBalanceNonRefundable { get; set; }

        //[NotMapped]
        //Customer Customer { get; set; }


        //[NotMapped]
        //Owner Owner { get; set; }

        public Deliveryman CurrentUsers_Deliveryman { get; set; }
        public string CurrentUsers_DeliverymanId
        {
            get
            {
                if (CurrentUsers_Deliveryman.IsNull())
                    return "";
                return CurrentUsers_Deliveryman.Id;
            }
        }

        public bool IsAdmin { get; private set; }
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; private set; }
        public BuySellDocStateENUM BuySellDocStateEnum { get; private set; }
        //public ApplicationUser User { get; set; }



        /// <summary>
        /// These can be sales orders or Invoices or sale Request
        /// </summary>
        public List<BuySellDoc> Docs { get; set; }
  
        public List<BuySellDoc> Docs_BF
        {
            get
            {
                if (Docs.IsNullOrEmpty())
                    return null;
                List<BuySellDoc> sellDocs_BF = Docs.Where(x => x.MetaData.Created.Date < FromDate).ToList();

                return sellDocs_BF;
            }
        }


        /// <summary>
        /// The date is used to create the heading only
        /// </summary>
        public DateTime FromDate { get; private set; }
        DateTime ToDate { get; set; }

        public string Name
        {

            get
            {
                //if (IsAdmin)
                //    return "Administrator";

                if (Docs.IsNull())
                    return "";

                BuySellDoc sellDoc = Docs.FirstOrDefault();

                if (sellDoc.IsNull())
                    return "";

                switch (BuySellDocumentTypeEnum)
                {
                    case BuySellDocumentTypeENUM.Sale:
                        return sellDoc.Owner.FullName();
                    case BuySellDocumentTypeENUM.Purchase:
                        return sellDoc.Customer.FullName();
                    case BuySellDocumentTypeENUM.Unknown:
                    default:
                        return "ERROR";
                }


            }
        }        //public SaleOrderTypeENUM SaleOrderTypeEnum { get; set; }
        public string MainHeading
        {
            get
            {
                string heading = "";
                switch (BuySellDocumentTypeEnum)
                {
                    case BuySellDocumentTypeENUM.Sale:
                        heading = string.Format("Sales for {0}", Name);
                        break;

                    case BuySellDocumentTypeENUM.Purchase:
                        heading = string.Format("Purchases for {0}", Name);
                        break;
                    case BuySellDocumentTypeENUM.Delivery:
                        heading = string.Format("Deliveries Available");
                        break;

                    case BuySellDocumentTypeENUM.Unknown:
                    default:
                        heading = "ERROR";
                        break;
                }

                string minorHeading = "";
                switch (BuySellDocStateEnum)
                {
                    case BuySellDocStateENUM.Unknown:
                        break;
                    case BuySellDocStateENUM.BackOrdered:
                        break;
                    case BuySellDocStateENUM.InProccess:
                    case BuySellDocStateENUM.All:
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
                        minorHeading = " - " + BuySellDocStateEnum.ToString().ToTitleSentance();
                        break;

                    default:
                        break;
                }
                if (!minorHeading.IsNullOrWhiteSpace())
                    heading += " " + minorHeading;

                if (IsAdmin)
                    heading += " -Admin Screen";

                return heading;
            }
        }


        public string SubHeading
        {
            get
            {
                string heading = string.Format("Between {0:dd-MMM-yyyy} and {1:dd-MMM-yyyy}", FromDate, ToDate);

                //if (SellerName.IsNullOrEmpty())
                //    return heading;

                //heading = string.Format("Sale Orders for Seller: {0} between (and including) {1:dd-MMM-yyyy} and {2:dd-MMM-yyyy}", SellerName, FromDate, ToDate);
                return heading;

            }
        }


        public bool IsShippable(BuySellDoc buySellDoc)
        {
            bool isShippable = (
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.BeingPreparedForShipmentBySeller ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp);


            return isShippable;
        }

        public bool IsDelivered(BuySellDoc buySellDoc)
        {
            bool isShippable = (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Delivered);
            return isShippable;
        }
        public decimal Live_BF
        {
            get
            {
                if (Docs_BF.IsNullOrEmpty())
                    return 0;
                decimal ttlBf = 0;
                foreach (var item in Docs_BF)
                {
                    if (IsShippable(item))
                        ttlBf += item.TotalOrdered;
                }
                return ttlBf;
            }
        }
        public decimal Dead_BF
        {
            get
            {
                if (Docs_BF.IsNullOrEmpty())
                    return 0;
                decimal ttlBf = 0;
                foreach (var item in Docs_BF)
                {
                    if (!IsShippable(item))
                        ttlBf += item.TotalOrdered;
                }
                return ttlBf;
            }
        }

        public decimal GrandTotal_OpenOrders
        {
            get
            {
                if (Docs.IsNullOrEmpty())
                    return 0;

                decimal ttlBf = 0;
                foreach (var item in Docs)
                {
                    if (IsShippable(item))
                        ttlBf += item.TotalOrdered;
                }
                return ttlBf;
            }
        }
        public decimal GrandTotal_ClosedOrders
        {
            get
            {
                if (Docs.IsNullOrEmpty())
                    return 0;

                decimal ttlBf = 0;
                foreach (var item in Docs)
                {
                    if (IsDelivered(item))
                        ttlBf += item.TotalOrdered;
                }
                return ttlBf;
            }
        }


        public IBuySellDocViewState BuySellDocViewState
        {
            get
            {
                BuySellDocViewStateController buySellDocViewStateController = new BuySellDocViewStateController(BuySellDocStateEnum, BuySellDocumentTypeEnum, null, null, CustomerBalanceRefundable, CustomerBalanceNonRefundable);
                return buySellDocViewStateController.GetBuySellDocStateController();
            }
        }

        public IBuySellDocViewState GetBuySellDocViewState(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, Customer customer, Owner owner)
        {
            BuySellDocViewStateController buySellDocViewStateController = new BuySellDocViewStateController(buySellDocStateEnum, buySellDocumentTypeEnum, customer, owner, CustomerBalanceRefundable, CustomerBalanceNonRefundable);
            return buySellDocViewStateController.GetBuySellDocStateController();

        }


    }
}
