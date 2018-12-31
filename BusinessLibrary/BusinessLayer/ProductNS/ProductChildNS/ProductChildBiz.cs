using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz : BusinessLayer<ProductChild>
    {

        /// <summary>
        /// All product children will be owned by Owners who will have a person who will have a user
        /// </summary>
        OwnerBiz _ownerBiz;
        ProductChildFeatureBiz _productChildFeatureBiz;
        public ProductChildBiz(IRepositry<ProductChild> entityDal, BizParameters bizParameters, OwnerBiz ownerBiz, ProductChildFeatureBiz productChildFeatureBiz)
            : base(entityDal, bizParameters)
        {
            _ownerBiz = ownerBiz;
            _productChildFeatureBiz = productChildFeatureBiz;
        }


        ProductChildFeatureBiz ProductChildFeatureBiz
        {
            get
            {
                _productChildFeatureBiz.UserId = UserId;
                _productChildFeatureBiz.UserName = UserName;
                return _productChildFeatureBiz;
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

    }
}
