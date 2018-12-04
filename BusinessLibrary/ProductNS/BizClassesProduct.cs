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
        readonly ProductChildBiz _productChildBiz;
        readonly ProductFeatureBiz _productFeatureBiz;
        readonly MenuFeatureBiz _menuFeatureBiz;
        public MyWorkClassesProduct(
            UomVolumeBiz uomVolumeBiz,
            UomLengthBiz uomLengthBiz,
            UomQuantityBiz uomQuantityBiz,
            UomWeightBiz uomWeightBiz,
            MenuPathMainBiz menuPathMainBiz,
            ProductIdentifierBiz productIdentifierBiz,
            ProductChildBiz productChildBiz,
            ProductFeatureBiz productFeatureBiz,
            MenuFeatureBiz menuFeatureBiz)
        {
            _uomVolumeBiz = uomVolumeBiz;
            _uomLengthBiz = uomLengthBiz;
            _uomQuantityBiz = uomQuantityBiz;
            _uomWeightBiz = uomWeightBiz;
            _menuPathMainBiz = menuPathMainBiz;
            _productIdentifierBiz = productIdentifierBiz;
            _productChildBiz = productChildBiz;
            _productFeatureBiz = productFeatureBiz;
            _menuFeatureBiz = menuFeatureBiz;
        }

        public MenuFeatureBiz MenuFeatureBiz { get { return _menuFeatureBiz; } }
        public ProductFeatureBiz ProductFeatureBiz { get { return _productFeatureBiz; } }
        public UomVolumeBiz UomVolumeBiz { get { return _uomVolumeBiz; } }
        public UomLengthBiz UomLengthBiz { get { return _uomLengthBiz; } }
        public UomQuantityBiz UomQuantityBiz { get { return _uomQuantityBiz; } }
        public UomWeightBiz UomWeightBiz { get { return _uomWeightBiz; } }
        public MenuPathMainBiz MenuPathMainBiz { get { return _menuPathMainBiz; } }
        public ProductIdentifierBiz ProductIdentifierBiz { get { return _productIdentifierBiz; } }
        public ProductChildBiz ProductChildBiz { get { return _productChildBiz; } }
    }
}
