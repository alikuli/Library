using AliKuli.Extentions;
using EnumLibrary.EnumNS;
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
        public BuySellStatementModel(List<BuySellDoc> sellDocs, DateTime fromDate, DateTime toDate, BuySellDocumentTypeENUM buySellDocumentTypeEnum, bool isAdmin, BuySellDocStateENUM buySellDocStateEnum = BuySellDocStateENUM.Unknown)
        {
            //Person = person;
            Docs = sellDocs;
            //BuyDocs = buyDocs;
            FromDate = fromDate;
            ToDate = toDate;
            //SaleOrderTypeEnum = saleOrderTypeEnum;
            BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            BuySellDocStateEnum = buySellDocStateEnum;
            IsAdmin = isAdmin;
        }
        /// <summary>
        /// if true then emit admin reports 
        /// </summary>
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

                return "Name... need to fix";

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
                    case BuySellDocStateENUM.ConfirmedBySeller:
                    case BuySellDocStateENUM.ReadyForShipment:
                    case BuySellDocStateENUM.ConfirmedByCourier:
                    case BuySellDocStateENUM.PickedUp:
                    case BuySellDocStateENUM.Delivered:
                    case BuySellDocStateENUM.Rejected:
                    case BuySellDocStateENUM.Problem:
                        minorHeading = " - " + BuySellDocStateEnum.ToString().ToTitleSentance();
                        break;
                    case BuySellDocStateENUM.New:
                    case BuySellDocStateENUM.Closed:
                    case BuySellDocStateENUM.Canceled:
                    case BuySellDocStateENUM.Quotation:
                    case BuySellDocStateENUM.Credit:
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
        //{
        //    string heading = string.Format("Sale Orders between {0:dd-MMM-yyyy} and {1:dd-MMM-yyyy}", FromDate, ToDate);

        //    if (SellerName.IsNullOrEmpty())
        //        return heading;

        //    heading = string.Format("Sale Orders for Seller: {0} between (and including) {1:dd-MMM-yyyy} and {2:dd-MMM-yyyy}", SellerName, FromDate, ToDate);
        //    return heading;
        //}

        //public string HeadingPurchaseOrders()
        //{
        //    string heading = string.Format("Purchase Orders between {0} and {1}", PurchaserName, FromDate, ToDate);

        //    if (SellerName.IsNullOrEmpty())
        //        return heading;

        //    heading = string.Format("Sale Orders for Purchaser: {0} between (and including) {1} and {2}", PurchaserName, FromDate, ToDate);
        //    return heading;
        //}
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
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Delivered);


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
                        ttlBf += item.TotalRemaining;
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
                        ttlBf += item.TotalRemaining;
                }
                return ttlBf;
            }
        }

        public decimal GrandTotal_Live
        {
            get
            {
                if (Docs.IsNullOrEmpty())
                    return 0;

                decimal ttlBf = 0;
                foreach (var item in Docs)
                {
                    if (IsShippable(item))
                        ttlBf += item.TotalRemaining;
                }
                return ttlBf;
            }
        }
        public decimal GrandTotal_Dead
        {
            get
            {
                if (Docs.IsNullOrEmpty())
                    return 0;

                decimal ttlBf = 0;
                foreach (var item in Docs)
                {
                    if (!IsShippable(item))
                        ttlBf += item.TotalRemaining;
                }
                return ttlBf;
            }
        }
    }
}
