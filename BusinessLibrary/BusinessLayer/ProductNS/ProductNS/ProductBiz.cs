using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductChildNS;

namespace UowLibrary.ProductNS
{

    /// <summary>
    /// You can controll the list of products to the index by marking ShowInactive true or false.
    /// </summary>
    public partial class ProductBiz : BusinessLayer<Product>
    {


        MyWorkClassesProduct _myWorkClassesProduct;
        UserBiz _userBiz;
        ProductChildBiz _productChildBiz;
        LikeUnlikeBiz _likeUnlikeBiz;
        OwnerBiz _ownerBiz;
        BizParameters _bizParameters;
        public ProductBiz(UserBiz userBiz, IRepositry<Product> entityDal, MyWorkClassesProduct myWorkClassesProduct, ProductChildBiz productChildBiz, BizParameters bizParameters, LikeUnlikeBiz likeUnlikeBiz, OwnerBiz ownerBiz)
            : base(entityDal, bizParameters)
        {
            _myWorkClassesProduct = myWorkClassesProduct;
            _userBiz = userBiz;
            _productChildBiz = productChildBiz;
            _likeUnlikeBiz = likeUnlikeBiz;
            _ownerBiz = ownerBiz;
            _bizParameters = bizParameters;
        }
        public OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }

        public BizParameters BizParameters
        {
            get
            {
                return _bizParameters;
            }
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

        public MyWorkClassesProduct MyWorkClassesProduct
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

        public void ApproveAllProductsAndSaveUtility()
        {
            List<Product> productLst = FindAll().ToList();
            if (productLst.IsNullOrEmpty())
                return;

            foreach (Product product in productLst)
            {
                product.IsUnApproved = false;
            }

            SaveChanges();
        }



    }
}
