using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
            : BusinessLayer<BuySellDoc>
    {
        OwnerBiz _ownerBiz;
        CustomerBiz _customerBiz;
        ProductBiz _productBiz;
        BuySellDocBiz _buySellBiz;
        BuySellItemBiz _buySellItemBiz;

        public BuySellDocBiz(IRepositry<BuySellDoc> entityDal, BuySellItemBiz buySellItemBiz, BizParameters bizParameters, OwnerBiz ownerBiz, CustomerBiz customerBiz, ProductBiz productBiz)
            : base(entityDal, bizParameters)
        {
            _ownerBiz = ownerBiz;
            _customerBiz = customerBiz;
            _productBiz = productBiz;
            _buySellBiz = entityDal as BuySellDocBiz;
            _buySellItemBiz = buySellItemBiz;

        }


        public BuySellItemBiz BuySellItemBiz
        {
            get
            {
                _buySellItemBiz.IsNullThrowException("_buySellItemBiz");
                _buySellItemBiz.UserId = UserId;
                _buySellItemBiz.UserName = UserName;
                return _buySellItemBiz;
            }

        }
        public ProductBiz ProductBiz
        {
            get
            {
                _productBiz.IsNullThrowException("_productBiz");
                _productBiz.UserId = UserId;
                _productBiz.UserName = UserName;
                return _productBiz;
            }
        }


        public ProductChildBiz ProductChildBiz
        {
            get
            {
                return ProductBiz.ProductChildBiz;
            }
        }
        public AddressBiz AddressBiz
        {
            get
            {
                return OwnerBiz.AddressBiz;
            }
        }

        public CustomerBiz CustomerBiz
        {
            get
            {
                _customerBiz.IsNullThrowException("_customerBiz");
                _customerBiz.UserId = UserId;
                _customerBiz.UserName = UserName;
                return _customerBiz;
            }
        }

        public OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.IsNullThrowException("_ownerBiz");
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }
        public PersonBiz PersonBiz
        {
            get
            {

                return OwnerBiz.PersonBiz;

            }
        }

        public UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }

        }








    }


}


