using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using AliKuli.Extentions;
using UowLibrary.CashTtxNS;

namespace UowLibrary.PlayersNS.CustomerNS
{
    public partial class CustomerBiz : BusinessLayerPlayer<Customer>
    {


        readonly CustomerCategoryBiz _customerCategoryBiz;

        public CustomerBiz(IRepositry<Customer> entityDal, BizParameters bizParameters, CustomerCategoryBiz customerCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {

            _customerCategoryBiz = customerCategoryBiz;
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
    }
}
