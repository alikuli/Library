using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MyWorkClassesProduct(
            UomVolumeBiz uomVolumeBiz, 
            UomLengthBiz uomLengthBiz, 
            UomQuantityBiz uomQuantityBiz, 
            UomWeightBiz uomWeightBiz, 
            MenuPathMainBiz menuPathMainBiz, 
            ProductIdentifierBiz productIdentifierBiz, 
            ProductChildBiz productChildBiz)
        {
            _uomVolumeBiz = uomVolumeBiz;
            _uomLengthBiz = uomLengthBiz;
            _uomQuantityBiz = uomQuantityBiz;
            _uomWeightBiz = uomWeightBiz;
            _menuPathMainBiz = menuPathMainBiz;
            _productIdentifierBiz = productIdentifierBiz;
            _productChildBiz =  productChildBiz;



        }

        public UomVolumeBiz UomVolumeBiz { get { return _uomVolumeBiz; } }
        public UomLengthBiz UomLengthBiz { get { return _uomLengthBiz; } }
        public UomQuantityBiz UomQuantityBiz { get { return _uomQuantityBiz; } }
        public UomWeightBiz UomWeightBiz { get { return _uomWeightBiz; } }
        public MenuPathMainBiz MenuPathMainBiz { get { return _menuPathMainBiz; } }
        public ProductIdentifierBiz ProductIdentifierBiz { get { return _productIdentifierBiz; } }
        public ProductChildBiz ProductChildBiz { get { return _productChildBiz; } }
    }
}
