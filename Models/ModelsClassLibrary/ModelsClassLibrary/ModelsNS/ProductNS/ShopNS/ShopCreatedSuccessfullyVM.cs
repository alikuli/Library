using ModelsClassLibrary.ModelsNS.CashNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS.ShopNS
{
    [NotMapped]
    public class ShopCreatedSuccessfullyVM : CashBalanceVM
    {
        public ShopCreatedSuccessfullyVM(decimal refundable, decimal nonRefundable, string shopName, string mp1Name, string mp2Name, string mp3Name, int numberOfMonths, DateTime expiryDate, string returnUrl)
            : base(refundable, nonRefundable)
        {
            ShopName = shopName;
            Mp1Name = mp1Name;
            Mp2Name = mp2Name;
            Mp3Name = mp3Name;
            NumberOfMonths = numberOfMonths;
            ExpiryDate = expiryDate;
            ReturnUrl = returnUrl;
        }


        public string ShopName { get; set; }
        public string Mp1Name { get; set; }
        public string Mp2Name { get; set; }
        public string Mp3Name { get; set; }
        public int NumberOfMonths { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ReturnUrl { get; set; }
        public override string ToString()
        {
            string str = string.Format("A shop named '{0}' has been created in area '{1} - {2} - {3}'. Total spent - Money = Rs{4:N2}, Token = Rs{5:N2}, Total = {6:N2}. Number of months = {7}. Expiry date is {8}. GOOD LUCK!", ShopName, Mp1Name, Mp2Name, Mp3Name, Refundable, NonRefundable, Total(), NumberOfMonths, ExpiryDate);



            return str;
        }

        public string ExpiryDateInNoOfDays()
        {
            string expiryDateInNoOfDays = new DateParameter().ToNoOfDaysAway(ExpiryDate);
            return expiryDateInNoOfDays;
        }
    }
}
