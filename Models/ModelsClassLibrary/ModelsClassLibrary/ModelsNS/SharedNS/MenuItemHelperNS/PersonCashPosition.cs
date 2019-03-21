
namespace ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS.PersonClassesNS
{
    public class PersonCashPosition
    {
        public PersonCashPosition(
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
                PersonCashRefundableClass mih = new PersonCashRefundableClass(_refundable);
                return mih;
            }
        }

        public IMenuItemHelper Refundable_AmountOnly
        {
            get
            {
                PersonCashRefundable_AmountOnlyClass mih = new PersonCashRefundable_AmountOnlyClass(_refundable);
                return mih;
            }
        }
        //----------------------------------------------------------------


        static decimal _nonRefundable;
        public IMenuItemHelper NonRefundable
        {
            get
            {
                IMenuItemHelper item = new PersonCashNonRefundableClass(_nonRefundable);
                return item;
            }
        }

        public IMenuItemHelper NonRefundable_AmountOnly
        {
            get
            {
                PersonCashRefundable_AmountOnlyClass mih = new PersonCashRefundable_AmountOnlyClass(_nonRefundable);
                return mih;
            }
        }


        //static decimal _total = _refundable + _nonRefundable;
        public IMenuItemHelper Total
        {
            get
            {
                PersonTotalCashClass mih = new PersonTotalCashClass(_refundable, _nonRefundable);
                return mih;
            }
        }
        public IMenuItemHelper Total_AmountOnly
        {
            get
            {
                PersonCash_AmountOnly mih = new PersonCash_AmountOnly(_refundable, _nonRefundable);
                return mih;
            }
        }


    }
}
