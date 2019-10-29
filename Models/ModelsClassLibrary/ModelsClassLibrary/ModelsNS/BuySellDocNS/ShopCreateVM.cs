using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS
{
    /// <summary>
    /// This is used to create a shop.
    /// </summary>
    [NotMapped]
    public class ShopVM : Product
    {
        public ShopVM()
        {

        }

        public ShopVM(string id, string shopName, string explaination, int noOfMonths, decimal ratePerMonth, string returnUrl, List<string> previousStores, MenuPathMain menuPathMain, AddressStringWithNames customerAddress)
        {
            ShopName = shopName;
            Explaination = explaination;
            NoOfMonths = noOfMonths;
            RatePerMonth = ratePerMonth;
            ReturnUrl = returnUrl;
            PreviousStores = previousStores;
            MenuPathMain = menuPathMain;
            MenuPathMainId = menuPathMain.Id;
            CustomerAddress = customerAddress;
            
            if(!id.IsNullOrWhiteSpace())
            {
                Id = id;
            }
        }
        public ShopVM(string id, string shopName, string explaination, int noOfMonths, decimal ratePerMonth, string returnUrl, List<string> previousStores, MenuPathMain menuPathMain, AddressStringWithNames customerAddress, List<UploadedFile> miscFiles):
            this(id,shopName, explaination, noOfMonths, ratePerMonth, returnUrl, previousStores, menuPathMain, customerAddress)
        {
            MiscFiles = miscFiles;
        }

        [Display(Name = "Suggested Shop Name")]
        [Required]
        public string ShopName { get; set; }
        public string Explaination { get; set; }

        [Display(Name = "No of months")]
        [Range(1, 12)]
        public int NoOfMonths { get; set; }

        [Display(Name = "Rate/Month")]
        public decimal RatePerMonth { get; set; }
        public string RatePerMonth_Formatted { get { return "Rs. " + RatePerMonth.ToString("N2"); } }
        public string ReturnUrl { get; set; }
        public string MenuPathMainId { get; set; }
        public MenuPathMain MenuPathMain { get; set; }
        /// <summary>
        /// This is a product Id
        /// </summary>
        public string ShopId { get; set; }
        public List<string> PreviousStores { get; set; }

        public AddressStringWithNames CustomerAddress { get; set; }

        public decimal Total()
        {
            decimal ttl = NoOfMonths * RatePerMonth;
            return ttl;
        }

        public string ShopPath()
        {
            string path = string.Format("Path: {0} - {1} - {2}",
                MenuPathMain.MenuPath1.FullName(),
                MenuPathMain.MenuPath2.FullName(),
                MenuPathMain.MenuPath3.FullName());
            return path;
        }





    }
}
