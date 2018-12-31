using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CashierCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using AliKuli.Extentions;
using UowLibrary.CashTtxNS;

namespace UowLibrary.PlayersNS.CashierNS
{
    public partial class CashierBiz : BusinessLayerPlayer<Cashier>
    {
        readonly CashierCategoryBiz _cashierCategoryBiz;
        //PersonBiz _personBiz;

        public CashierBiz(IRepositry<Cashier> entityDal, BizParameters bizParameters, CashierCategoryBiz cashierCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {
            //_personBiz = personBiz;
            _cashierCategoryBiz = cashierCategoryBiz;
        }

        public CashierCategoryBiz CashierCategoryBiz 
        { 
            get 
            {
                _cashierCategoryBiz.IsNullThrowException("_cashierCategoryBiz");
                _cashierCategoryBiz.UserId = UserId;
                _cashierCategoryBiz.UserName = UserName;
                return _cashierCategoryBiz; 
            }
        }

    }
}
