using System;

namespace DatastoreNS
{
    public class ProductChildInitializer
    {
        public ProductChildInitializer(string productname, string username, double saleprice, double buyprice, DateTime expirydate, string serialnumber, string parentname)
        {
            
            ProductName = productname;
            UserName = username;
            SalePrice = saleprice;
            BuyPrice = buyprice;
            ExpiryDate = expirydate;
            SerialNumber = serialnumber;
            //PictureRelativeAddress = picturerelativeaddress;
            ParentName = parentname;

        }
        /// <summary>
        /// This will be the name of the product.
        /// </summary>
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        //public string PictureRelativeAddress { get; set; }
        public string ParentName { get; set; }
    }
}
