using System;

namespace DatastoreNS
{
    public class ProductChildInitializer
    {
        public ProductChildInitializer(string productname, string username, decimal saleprice, decimal buyprice, DateTime expirydate, string serialnumber, string parentname, bool isNonRefundablePaymentAccepted)
        {
            
            ProductName = productname;
            UserName = username;
            SalePrice = saleprice;
            BuyPrice = buyprice;
            ExpiryDate = expirydate;
            SerialNumber = serialnumber;
            //PictureRelativeAddress = picturerelativeaddress;
            ParentName = parentname;
            IsNonRefundablePaymentAccepted = isNonRefundablePaymentAccepted;

        }
        /// <summary>
        /// This will be the name of the product.
        /// </summary>
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        //public string PictureRelativeAddress { get; set; }
        public string ParentName { get; set; }
        public bool IsNonRefundablePaymentAccepted { get; set; }
    }
}
