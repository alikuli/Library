using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductChildNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz : BusinessLayer<Product>
    {


        MyWorkClassesProduct _myWorkClassesProduct;
        UserBiz _userBiz;
        ProductChildBiz _productChildBiz;
        LikeUnlikeBiz _likeUnlikeBiz;
        public ProductBiz(UserBiz userBiz, IRepositry<Product> entityDal, MyWorkClassesProduct myWorkClassesProduct, ProductChildBiz productChildBiz, BizParameters bizParameters, LikeUnlikeBiz likeUnlikeBiz)
            : base(entityDal, bizParameters)
        {
            _myWorkClassesProduct = myWorkClassesProduct;
            _userBiz = userBiz;
            _productChildBiz = productChildBiz;
            _likeUnlikeBiz = likeUnlikeBiz;
        }


        public LikeUnlikeBiz LikeUnlikeBiz
        {
            get
            {
                _likeUnlikeBiz.UserId = UserId;
                _likeUnlikeBiz.UserName = UserName;
                return _likeUnlikeBiz;
            }
        }
        public UserBiz UserBiz
        {
            get 
            {
                _userBiz.UserId = UserId;
                _userBiz.UserName = UserName;
                return _userBiz; 
            }
        }

        MyWorkClassesProduct MyWorkClassesProduct
        {
            get
            {
                _myWorkClassesProduct.UserId = UserId;
                _myWorkClassesProduct.UserName = UserName;
                return _myWorkClassesProduct;
            }
        }
        public MenuFeatureBiz MenuFeatureBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.MenuFeatureBiz; 
            } 
        }
        public ProductFeatureBiz ProductFeatureBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.ProductFeatureBiz; 
            } 
        }

        public UomVolumeBiz UomVolumeBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.UomVolumeBiz; 
            } 
        }
        public UomLengthBiz UomLengthBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.UomLengthBiz; 
            } 
        }
        public UomQuantityBiz UomQuantityBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.UomQuantityBiz; 
            } 
        }
        public UomWeightBiz UomWeightBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.UomWeightBiz; 
            } 
        }
        public MenuPathMainBiz MenuPathMainBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.MenuPathMainBiz; 
            } 
        }
        public ProductIdentifierBiz ProductIdentifierBiz 
        { 
            get 
            {
                return MyWorkClassesProduct.ProductIdentifierBiz; 
            } 
        }

        public ProductChildBiz ProductChildBiz
        {
            get
            {
                _productChildBiz.UserId = UserId;
                _productChildBiz.UserName = UserName;
                return _productChildBiz;
            }
        }



        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _myWorkClassesProduct.MenuPathMainBiz.MenuPath1Biz;
            }
        }
        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _myWorkClassesProduct.MenuPathMainBiz.MenuPath2Biz;
            }
        }
        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _myWorkClassesProduct.MenuPathMainBiz.MenuPath3Biz;
            }
        }



    }
}
