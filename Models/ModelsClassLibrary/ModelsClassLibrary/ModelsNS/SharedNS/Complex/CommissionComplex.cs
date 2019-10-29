using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS
{
    [ComplexType]
    public class PaymentsComplex : DateAndByComplex
    {
        public decimal Percent { get; set; }

        public decimal Amount_NonRefundable { get; set; }
        public string Amount_NonRefundable_Formatted
        {
            get
            {
                return string.Format("{0:N2}", Amount_NonRefundable);
            }
        }


        public decimal Amount_Refundable { get; set; }
        public string Amount_Formatted
        {
            get
            {
                return string.Format("{0:N2}", Amount_Refundable);
            }
        }

        public void SetToTodaysDate(decimal amount_Refundable, decimal amount_Non_Refundable, string userId, string userName)
        {
            Amount_NonRefundable = amount_Non_Refundable;
            Amount_Refundable = amount_Refundable;
            SetToTodaysDate(userName, userId);

        }

        

        public decimal Total
        {
            get
            {
                return Amount_NonRefundable + Amount_Refundable;
            }
        }

    }
}
