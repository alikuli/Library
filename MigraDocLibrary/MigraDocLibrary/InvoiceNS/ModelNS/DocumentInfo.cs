using EnumLibrary.EnumNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InvoiceNS
{
    public class DocumentInfo
    {
        public DocumentInfo()
        {
            DocType = DocumentTypeENUM.Unknown;
        }

        public DocumentInfo(long documentNo, DateTime date, DocumentTypeENUM docType, string purchaseOrderNumber, string purchaseOrderDate, string shipDate, string shipWeight, string shippingCarrier, string comment, List<LineItem> lineItems, decimal tax, decimal shippingHandling, decimal advancePayment, decimal misc)
        {
            DocumentNo = documentNo.ToString("N0");
            Date = date.ToString("dd-mmm-yyyyy");
            DocType = docType;
        }

        public DocumentInfo(DocumentInfo docInfo)
            :this(
            long.Parse(docInfo.DocumentNo.ToNumericString()), 
            DateTime.Parse(docInfo.Date), 
            docInfo.DocType, 
            docInfo.PurchaseOrderNumber, 
            docInfo.PurchaseOrderDate, 
            docInfo.ShipDate, 
            docInfo.ShipWeight, 
            docInfo.ShippingCarrier,
            docInfo.Comment, 
            docInfo.LineItems, 
            docInfo.Tax, 
            docInfo.ShippingHandling, 
            docInfo.AdvancePayment, 
            docInfo.Misc)
        {

        }
        public string DocumentNo { get; set; }
        public string Date { get; set; }

        public DocumentTypeENUM DocType { get; set; }

        public string DocumentNoWithTitle
        {
            get
            {
                long n = 0;
                bool success = long.TryParse(DocumentNo, out n);

                if (DocumentNo.IsNullOrWhiteSpace())
                    return (DocType.ToString().ToTitleSentance() + "#: NIL");

                if (success)
                    return (DocType.ToString().ToTitleSentance() + "#: " + n.ToString("N0"));

                return (DocType.ToString().ToTitleSentance() + "#: " + DocumentNo);
            }
        }

        public string PurchaseOrderNumber { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string ShipDate { get; set; }
        public string ShipWeight { get; set; }
        public string ShippingCarrier { get; set; }

        public string Comment { get; set; }

        public List<LineItem> LineItems { get; set; }

        public decimal Tax { get; set; }
        public decimal ShippingHandling { get; set; }
        public decimal Misc { get; set; }
        public decimal AdvancePayment { get; set; }

        public double TotalShipped
        {
            get
            {
                if(LineItems.IsNullOrEmpty())
                    return 0;
                var ttl = LineItems.Sum(x => x.Shipped);

                return ttl;
            }
        }

        public double TotalOrdered
        {
            get
            {
                if(LineItems.IsNullOrEmpty())
                    return 0;
                var ttl = LineItems.Sum(x => x.Ordered);

                return ttl;
            }
        }

        public decimal TotalExtended
        {
            get
            {
                if(LineItems.IsNullOrEmpty())
                    return 0;

                var ttl = LineItems.Sum(x => x.Extended());

                return ttl;
            }
        }
    
        public string AvgPrice
        {
            get
            {
                if(LineItems.IsNullOrEmpty())
                    return " ";
                
                if (TotalExtended == 0)
                    return "No Value";

                var ttlExtended = TotalExtended;

                if(TotalShipped == 0)
                    return "No Value";
                
                var ttlShipped = TotalShipped;


                var avg = (double)ttlExtended/ttlShipped;

                return avg.ToString("N2");
            }
        }


        public string GrandTotal
        {
            get
            {
                decimal ttl = Tax + ShippingHandling + Misc + TotalExtended - AdvancePayment;
                return ttl.ToString("N2");  
            }
        }


    }

    
}
