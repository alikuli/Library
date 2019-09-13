using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS
{
    public class CashBalance
    {
        public decimal Refundable { get; set; }
        public decimal NonRefundable { get; set; }
        public decimal Total { get { return Refundable + NonRefundable; } }

        public bool CanBuy(decimal itemCost)
        {
            return Refundable >= itemCost;
        }
    }
}
