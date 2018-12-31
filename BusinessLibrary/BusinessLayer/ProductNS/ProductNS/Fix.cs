using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            Product product = parm.Entity as Product;
            product.IsNullThrowException("Unable to unbox product");

            //purge out all the productFeatures that do not have
            //productId of ProductFeatureId.
            //this is required for create because all of them are empty with no id's coming back
            //from the view into create. It works in Update because in update
            //if (!product.ProductFeatures.IsNullOrEmpty())
            //{
            //    List<ProductFeature> pfRemovalList = new List<ProductFeature>();
            //    foreach (ProductFeature pf in product.ProductFeatures)
            //    {
            //        if (pf.ProductId.IsNullOrEmpty())
            //            pfRemovalList.Add(pf);

            //    }

            //    foreach (ProductFeature pf in pfRemovalList)
            //    {
            //        product.ProductFeatures.Remove(pf);
            //    }
            //}

            if (product.UomDimensionsId.IsNullOrWhiteSpace())
                product.UomDimensionsId = null;

            if (product.UomPurchaseId.IsNullOrWhiteSpace())
                product.UomPurchaseId = null;

            if (product.UomSaleId.IsNullOrWhiteSpace())
                product.UomSaleId = null;


            if (product.UomVolumeId.IsNullOrWhiteSpace())
                product.UomVolumeId = null;


            if (product.UomWeightActualId.IsNullOrWhiteSpace())
                product.UomWeightActualId = null;

            if (product.UomWeightListedId.IsNullOrWhiteSpace())
                product.UomWeightListedId = null;


            //fixTheMenuFeautreKeys(product);
        }

        private void fixTheMenuFeautreKeys(Product product)
        {
            if (product.ProductFeatures.IsNullOrEmpty())
                return;

            List<ProductFeature> productFeatures = product.ProductFeatures.ToList();

            foreach (ProductFeature item in productFeatures)
            {
                item.ProductId = product.Id;
            }

        }


    }
}
