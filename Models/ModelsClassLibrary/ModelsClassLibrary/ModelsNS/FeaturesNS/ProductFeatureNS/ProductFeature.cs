using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class ProductFeature : ProductFeatureAbstract
    {
        /// <summary>
        /// This holds the value of the feature. The name of the feature is in the name
        /// </summary>
        public string FeatureDescription { get; set; }


        [Display(Name = "Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.ProductFeature;
        }
    }
}
