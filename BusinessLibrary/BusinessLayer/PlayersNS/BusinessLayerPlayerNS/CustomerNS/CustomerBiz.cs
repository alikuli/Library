using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;

namespace UowLibrary.PlayersNS.CustomerNS
{
    public partial class CustomerBiz : BusinessLayerPlayer<Customer>
    {


        readonly CustomerCategoryBiz _customerCategoryBiz;
        CashTrxBiz _cashTrxBiz;
        public CustomerBiz(IRepositry<Customer> entityDal, BizParameters bizParameters, CustomerCategoryBiz customerCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {

            _customerCategoryBiz = customerCategoryBiz;
            _cashTrxBiz = cashTrxBiz;
        }

        public CustomerCategoryBiz CustomerCategoryBiz
        {
            get
            {
                _customerCategoryBiz.IsNullThrowException("_customerCategoryBiz");
                _customerCategoryBiz.UserId = UserId;
                _customerCategoryBiz.UserName = UserName;
                return _customerCategoryBiz;
            }
        }

        //public CashTrxBiz CashTrxBiz
        //{
        //    get
        //    {
        //        _cashTrxBiz.IsNullThrowException("_cashTrxBiz");
        //        _cashTrxBiz.UserId = UserId;
        //        _cashTrxBiz.UserName = UserName;
        //        return _cashTrxBiz;
        //    }
        //}
        //public PersonBiz PersonBiz
        //{
        //    get
        //    {
        //        return CashTrxBiz.PersonBiz;
        //    }
        //}
    }
}
