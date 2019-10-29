using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.CashNS
{
    [NotMapped]
    public class CashBalanceVM
    {
        public CashBalanceVM()
        {

        }

        public CashBalanceVM(decimal refundable, decimal nonRefundable)
        {
            Refundable = refundable;
            NonRefundable = nonRefundable;
        }
        public decimal Refundable { get; set; }
        public decimal NonRefundable { get; set; }

        public decimal Total()
        {
            return Refundable + NonRefundable;
        }
    }
}
