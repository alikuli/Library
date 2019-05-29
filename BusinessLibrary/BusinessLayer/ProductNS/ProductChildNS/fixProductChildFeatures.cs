
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {

        public void FixProductChildFeatures(ProductChild productChild)
        {
            productChild.IsNullThrowExceptionArgument("productChild");
            productChild.Product.IsNullThrowException("Product");
            IProduct iproduct = productChild.Product;

            //get all the product features.
            List<ProductFeature> list_Of_ProductFeatures =
                get_All_Product_Features_For(iproduct);

            List<ProductChildFeature> list_Of_ProductChild_Features =
                Get_All_ProductChild_Features_For(productChild);

            List<ProductChildFeature> list_Of_Missing_ProductChild_Features_In_ProductChild =
                get_List_Of_Missing_ProductChild_Features_In_ProductChild(list_Of_ProductFeatures, list_Of_ProductChild_Features);

            //add all missing features from the product to the product child.
            productChild = add_Missing_ProductChildFeatures_To_ProductChild(productChild, list_Of_Missing_ProductChild_Features_In_ProductChild);
        }



        ProductChild add_Missing_ProductChildFeatures_To_ProductChild(
            ProductChild productChild,
            List<ProductChildFeature> list_Of_Missing_ProductChild_Features_In_ProductChild)
        {
            if (list_Of_Missing_ProductChild_Features_In_ProductChild.IsNullOrEmpty())
                return productChild;

            if (productChild.ProductChildFeatures.IsNull())
                productChild.ProductChildFeatures = new List<ProductChildFeature>();

            foreach (ProductChildFeature pc in list_Of_Missing_ProductChild_Features_In_ProductChild)
            {
                productChild.ProductChildFeatures.Add(pc);
            }

            return productChild;

        }



        public List<ProductChildFeature> Get_All_ProductChild_Features_For(ProductChild productChild)
        {
            productChild.IsNullThrowExceptionArgument();

            if (productChild.ProductChildFeatures.IsNullOrEmpty())
                return null;

            List<ProductChildFeature> productChildFeatures = productChild.ProductChildFeatures.OrderBy(x => x.Name).ToList();
            return productChildFeatures;
        }



        List<ProductFeature> get_All_Product_Features_For(IProduct iproduct)
        {
            iproduct.IsNullThrowException();
            if (iproduct.ProductFeatures.IsNullOrEmpty())
                return null;

            List<ProductFeature> listProductFeatures = iproduct.ProductFeatures.Where(x=>x.MetaData.IsDeleted==false).ToList();
            return listProductFeatures;

        }



        List<ProductChildFeature> get_List_Of_Missing_ProductChild_Features_In_ProductChild(
            List<ProductFeature> listOfProductFeatures,
            List<ProductChildFeature> listOfProductChildFeatures)
        {
            if (listOfProductFeatures.IsNullOrEmpty())
                return null;

            List<ProductChildFeature> listMissingProductChildFeatures = new List<ProductChildFeature>();
            
            if(listOfProductChildFeatures.IsNull())
                listOfProductChildFeatures = new List<ProductChildFeature>();

            foreach (ProductFeature pf in listOfProductFeatures)
            {
                bool found = listOfProductChildFeatures.Any(x => x.Name.ToLower() == pf.Name.ToLower());
                if (found)
                    continue;

                bool foundInAddList = listMissingProductChildFeatures.Any(x => x.Name.ToLower() == pf.Name.ToLower());
                if (foundInAddList)
                    continue;

                ProductChildFeature pcf = ProductChildFeatureBiz.Factory() as ProductChildFeature;
                pcf.Name = pf.Name;
                pcf.Comment = pf.Comment;
                listMissingProductChildFeatures.Add(pcf);
            }
            return listMissingProductChildFeatures;
        }
    }
}
