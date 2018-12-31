using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{

    public partial class Product
    {

        /// <summary>
        /// This makes the name with the features listed in alphabetical order.
        /// </summary>
        /// <returns></returns>
        public override string FullName()
        {
            if (ProductFeatures.IsNullOrEmpty())
                return Name;

            //get all the product features and put them in alphabetical order
            List<ProductFeature> productFeatures = ProductFeatures.ToList();
            if (productFeatures.IsNullOrEmpty())
                return Name;

            List<ProductFeature> productFeaturesOrdered = productFeatures.OrderBy(x => x.Name).ToList();
            productFeaturesOrdered.IsNullOrEmptyThrowException("Unable to sort MenuFeature list");


            StringBuilder sb = new StringBuilder();


            foreach (ProductFeature feature in productFeaturesOrdered)
            {
                if (feature.DetailInfoToDisplayOnWebsite.IsNullOrWhiteSpace())
                    continue;

                if (feature.Name.IsNullOrWhiteSpace())
                    continue;

                string fStr = string.Format("{0}: {1} ", feature.Name.ToUpper(), feature.DetailInfoToDisplayOnWebsite);
                sb.Append(fStr);
            }

            if (sb.ToString().IsNullOrWhiteSpace())
                return Name;

            return Name + " - " + sb.ToString();

        }

    }
}