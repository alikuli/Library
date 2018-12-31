using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
using UowLibrary.ParametersNS;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public class ProductFeatureBiz : BusinessLayer<ProductFeature>
    {

        public ProductFeatureBiz(IRepositry<ProductFeature> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }

        public override string SelectListCacheKey
        {
            get { return "ProductFeatureSelectListData"; }
        }

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;
        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            ProductFeature pf = parm.Entity as ProductFeature;
            pf.IsNullThrowException("Unable to unbox product features");

            //we need to have the product and Menufeatures available in the model
            //so as to make the unique name

            //we do this in Event_BeforeSaveInCreateAndEdit in the controller
            //make sure its done!
            pf.Product.IsNullThrowException("Product");
            pf.MenuFeature.IsNullThrowException("MenuFeature");



            pf.Name = pf.MakeUniqueName();
            base.Fix(parm);


        }


        /// <summary>
        /// This ensures that the duplicate name is checked only in this product
        /// </summary>
        /// <param name="productFeature"></param>
        /// <returns></returns>
        public override IQueryable<ProductFeature> GetDataToCheckDuplicateName(ProductFeature productFeature)
        {
            IQueryable<ProductFeature> productFeatureIQ = base.GetDataToCheckDuplicateName(productFeature).Where(x => x.ProductId == productFeature.ProductId);
            return productFeatureIQ;
        }
    }
}
