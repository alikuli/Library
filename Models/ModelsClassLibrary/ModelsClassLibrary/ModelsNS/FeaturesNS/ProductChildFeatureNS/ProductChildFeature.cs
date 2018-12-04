
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class ProductChildFeature : ProductFeatureAbstract
    {
        public ICollection<ProductChild> ProductChildren { get; set; }
    }
}
