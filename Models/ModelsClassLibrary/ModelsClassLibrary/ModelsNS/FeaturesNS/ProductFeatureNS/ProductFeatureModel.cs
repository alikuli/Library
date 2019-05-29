using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS.ProductFeatureNS
{
    [NotMapped]
    public class ProductFeatureModel
    {
        public ProductFeatureModel()
        {

        }
        public ProductFeatureModel(string parentId, string parentName, string featureId, string returnUrl, string description)
        {

            ParentId = parentId;
            ParentName = parentName;
            MenuFeatureId = featureId;
            ReturnUrl = returnUrl;
            Description = description;
        }

        public string Description { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        /// <summary>
        /// This is a menu Feature
        /// </summary>
        [Display(Name = "Feature")]
        public string MenuFeatureId { get; set; }
        public SelectList SelectListFeature { get; set; }

        public string ReturnUrl { get; set; }

        public void SelfCheck()
        {
            ParentId.IsNullOrWhiteSpaceThrowException("ParentId");
            MenuFeatureId.IsNullOrWhiteSpaceThrowException("FeatureId");
            ReturnUrl.IsNullOrWhiteSpaceThrowException("ReturnUrl");
        }
    }
}
