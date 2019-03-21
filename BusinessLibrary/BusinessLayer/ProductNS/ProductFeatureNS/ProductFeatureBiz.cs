using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using UowLibrary.ParametersNS;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public class ProductFeatureBiz : BusinessLayer<ProductFeature>
    {
        readonly MenuFeatureBiz _menuFeatureBiz;
        public ProductFeatureBiz(IRepositry<ProductFeature> entityDal, BizParameters bizParameters, MenuFeatureBiz menuFeatureBiz)
            : base(entityDal, bizParameters)
        {
            _menuFeatureBiz = menuFeatureBiz;
        }

        MenuFeatureBiz MenuFeatureBiz
        {
            get
            {
                return _menuFeatureBiz;
            }
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

        public void CreateNewFeature(CreateNewFeatureModel model)
        {
            model.SelfCheck();
            MenuFeature menuFeature = MenuFeatureBiz.FindByName(model.FeatureName);
            if (menuFeature.IsNull())
            {
                menuFeature = MenuFeatureBiz.Factory() as MenuFeature;
                menuFeature.IsNullThrowException("menuFeature");

                menuFeature.Name = model.FeatureName;
                MenuFeatureBiz.CreateAndSave(menuFeature);
                return;

            }
            //if you are here then the feature already exists
            ErrorsGlobal.Add(string.Format("'{0}' already exists!", model.FeatureName), MethodBase.GetCurrentMethod());
            throw new Exception(ErrorsGlobal.ToString());
        }

    }
}
