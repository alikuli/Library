using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.MenuNS;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.MenuNS
{
    public partial class MenuBiz : BusinessLayer<MenuPathMain>
    {
        private MenuPathMainBiz _menuPathMainBiz;
        private ProductBiz _productBiz;
        private ProductChildBiz _productChildBiz;
        //private LikeUnlikeBiz _likeUnlikeBiz;
        private CashTrxBiz _cashTrxBiz;
        OwnerBiz _ownerBiz;
        //AddressBiz _addressBiz;
        BankBiz _bankBix;
        AddressBiz _addressBiz;
        public MenuBiz(MenuPathMainBiz menuPathMainBiz, ProductBiz productBiz, IRepositry<MenuPathMain> entityDal, BizParameters bizParameters, CashTrxBiz cashTrxBiz, BankBiz bankBiz, OwnerBiz ownerBiz,  AddressBiz addressBiz)
            : base(entityDal, bizParameters)
        {

            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _productChildBiz = productBiz.ProductChildBiz;
            //_likeUnlikeBiz = likeUnlikeBiz;
            _cashTrxBiz = cashTrxBiz;
            _bankBix = bankBiz;
            _ownerBiz = ownerBiz;
            //_addressBiz = addressBiz;
            _addressBiz = addressBiz;

        }

        public AddressBiz addressBiz
        {
            get
            {
                _addressBiz.IsNullThrowExceptionArgument();
                _addressBiz.UserId = UserId;
                _addressBiz.UserName = UserName;
                return _addressBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }
        BankBiz BankBiz
        {
            get
            {
                _bankBix.UserId = UserId;
                _bankBix.UserName = UserName;
                return _bankBix;
            }
        }
        public CashTrxBiz CashTrxBiz
        {
            get
            {
                _cashTrxBiz.IsNullThrowExceptionArgument();
                _cashTrxBiz.UserId = UserId;
                _cashTrxBiz.UserName = UserName;
                return _cashTrxBiz;
            }
        }

        public UserBiz UserBiz
        {
            get
            {
                return CashTrxBiz.PersonBiz.UserBiz;
            }
        }
        public override string SelectListCacheKey
        {
            get { return "MenuPathMainSelectListData"; }
        }

        public LikeUnlikeBiz LikeUnlikeBiz
        {
            get
            {
                return ProductBiz.LikeUnlikeBiz;
            }
        }

        public ProductChildBiz ProductChildBiz
        {
            get
            {
                _productChildBiz.IsNullThrowExceptionArgument();
                _productChildBiz.UserId = UserId;
                _productChildBiz.UserName = UserName;
                return _productChildBiz;
            }
        }


        public ProductBiz ProductBiz
        {
            get
            {
                _productBiz.IsNullThrowExceptionArgument();
                _productBiz.UserId = UserId;
                _productBiz.UserName = UserName;
                return _productBiz;
            }
        }
        public MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                _menuPathMainBiz.IsNullThrowExceptionArgument();
                _menuPathMainBiz.UserId = UserId;
                _menuPathMainBiz.UserName = UserName;
                return _menuPathMainBiz;
            }
        }


        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        _userBiz.IsNullThrowExceptionArgument();
        //        _userBiz.UserId = UserId;
        //        _userBiz.UserName = UserName;
        //        return _userBiz;
        //    }
        //}


    }
}
