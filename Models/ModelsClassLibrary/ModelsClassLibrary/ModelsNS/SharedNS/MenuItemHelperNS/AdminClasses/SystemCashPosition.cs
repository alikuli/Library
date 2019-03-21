
namespace ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS.PersonClassesNS
{
    public class SystemCashPosition
    {
        public SystemCashPosition(
            decimal cashRefundable,
            decimal cashNonRefundable)
        {
            Initialize(
                cashRefundable,
                cashNonRefundable);
        }
        public void Initialize(
            decimal cashRefundable,
            decimal cashNonRefundable)
        {
            _refundable = cashRefundable;
            _nonRefundable = cashNonRefundable;
        }

        static decimal _refundable;
        public IMenuItemHelper Refundable
        {
            get
            {
                SystemCashRefundableClass mih = new SystemCashRefundableClass(_refundable);
                return mih;
            }
        }

        public IMenuItemHelper Refundable_AmountOnly
        {
            get
            {
                SystemCashRefundable_AmountOnlyClass mih = new SystemCashRefundable_AmountOnlyClass(_refundable);
                return mih;
            }
        }
        //----------------------------------------------------------------


        static decimal _nonRefundable;
        public IMenuItemHelper NonRefundable
        {
            get
            {
                IMenuItemHelper item = new SystemCashNonRefundableClass(_nonRefundable);
                return item;
            }
        }

        public IMenuItemHelper NonRefundable_AmountOnly
        {
            get
            {
                SystemCashRefundable_AmountOnlyClass mih = new SystemCashRefundable_AmountOnlyClass(_nonRefundable);
                return mih;
            }
        }


        public IMenuItemHelper Total
        {
            get
            {
                SystemTotalCashClass mih = new SystemTotalCashClass(_refundable, _nonRefundable);
                return mih;
            }
        }
        public IMenuItemHelper Total_AmountOnly
        {
            get
            {
                SystemTotalCashClass mih = new SystemTotalCashClass(_refundable, _nonRefundable);
                return mih;
            }
        }


    }
}
