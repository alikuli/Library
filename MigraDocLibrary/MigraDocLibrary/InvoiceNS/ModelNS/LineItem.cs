
namespace InvoiceNS
{
    public class LineItem
    {
        public LineItem(string itemNo, string description, double ordered, double shipped, decimal price, double taxpercent, string comment = "")
        {
            ItemNo = itemNo;
            Description = description;
            Ordered = ordered;
            Shipped = shipped;
            Price = price;
            TaxPercent = taxpercent;
            Comment = comment;
        }
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public double Ordered { get; set; }
        public double Shipped { get; set; }
        public decimal Price { get; set; }
        public double TaxPercent { get; set; }
        public decimal Extended()
        {
            double goods = (Shipped * ((double)Price));
            double tax = (Shipped * ((double)Price)) * TaxPercent;
            double ttl = goods + tax;
            return (decimal) ttl;

        }
        public string Comment { get; set; }


    }
}
