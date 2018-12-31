using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ProductChildNS;

namespace UowLibrary.ProductNS
{
    public class MyWorkClassesProduct
    {
        readonly UomWeightBiz _uomWeightBiz;
        readonly UomVolumeBiz _uomVolumeBiz;
        readonly UomLengthBiz _uomLengthBiz;
        readonly UomQuantityBiz _uomQuantityBiz;
        readonly MenuPathMainBiz _menuPathMainBiz;
        readonly ProductIdentifierBiz _productIdentifierBiz;
        //readonly ProductChildBiz _productChildBiz;
        readonly ProductFeatureBiz _productFeatureBiz;
        readonly MenuFeatureBiz _menuFeatureBiz;
        public MyWorkClassesProduct(
            UomVolumeBiz uomVolumeBiz,
            UomLengthBiz uomLengthBiz,
            UomQuantityBiz uomQuantityBiz,
            UomWeightBiz uomWeightBiz,
            MenuPathMainBiz menuPathMainBiz,
            ProductIdentifierBiz productIdentifierBiz,
            //ProductChildBiz productChildBiz,
            ProductFeatureBiz productFeatureBiz,
            MenuFeatureBiz menuFeatureBiz)
        {
            _uomVolumeBiz = uomVolumeBiz;
            _uomLengthBiz = uomLengthBiz;
            _uomQuantityBiz = uomQuantityBiz;
            _uomWeightBiz = uomWeightBiz;
            _menuPathMainBiz = menuPathMainBiz;
            _productIdentifierBiz = productIdentifierBiz;
            //_productChildBiz = productChildBiz;
            _productFeatureBiz = productFeatureBiz;
            _menuFeatureBiz = menuFeatureBiz;
        }


        public string UserId { get; set; }
        public string UserName { get; set; }

        public MenuFeatureBiz MenuFeatureBiz 
        { 
            get 
            {
                _menuFeatureBiz.UserId = UserId;
                _menuFeatureBiz.UserName = UserName;
                return _menuFeatureBiz; 
            } 
        }
        public ProductFeatureBiz ProductFeatureBiz 
        { 
            get 
            {
                _productFeatureBiz.UserId = UserId;
                _productFeatureBiz.UserName = UserName;
                return _productFeatureBiz; 
            } 
        }
        public UomVolumeBiz UomVolumeBiz 
        { 
            get 
            {
                _uomVolumeBiz.UserId = UserId;
                _uomVolumeBiz.UserName = UserName;
                return _uomVolumeBiz; 
            } 
        }
        public UomLengthBiz UomLengthBiz 
        { 
            get 
            {
                _uomLengthBiz.UserId = UserId;
                _uomLengthBiz.UserName = UserName;
                return _uomLengthBiz; 
            } 
        }
        public UomQuantityBiz UomQuantityBiz 
        { 
            get 
            {
                _uomQuantityBiz.UserId = UserId;
                _uomQuantityBiz.UserName = UserName;
                return _uomQuantityBiz; 
            } 
        }
        public UomWeightBiz UomWeightBiz 
        { 
            get 
            {
                _uomWeightBiz.UserId = UserId;
                _uomWeightBiz.UserName = UserName;
                return _uomWeightBiz; 
            } 
        }
        public MenuPathMainBiz MenuPathMainBiz 
        { 
            get 
            {
                _menuPathMainBiz.UserId = UserId;
                _menuPathMainBiz.UserName = UserName;
                return _menuPathMainBiz; 
            } 
        }
        public ProductIdentifierBiz ProductIdentifierBiz 
        { 
            get 
            {
                _productIdentifierBiz.UserId = UserId;
                _productIdentifierBiz.UserName = UserName;
                return _productIdentifierBiz; 
            } 
        }
        //public ProductChildBiz ProductChildBiz 
        //{ 
        //    get 
        //    {
        //        _productChildBiz.UserId = UserId;
        //        _productChildBiz.UserName = UserName;
        //        return _productChildBiz; 
        //    } 
        //}
    }
}
