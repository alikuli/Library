using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS
{
    [NotMapped]
    public class MenuFeatureModel
    {
        public MenuFeatureModel()
        {

        }
        public MenuFeatureModel(string parentId, string parentName, string featureId, string returnUrl)
        {
            
            ParentId = parentId;
            ParentName = parentName;
            FeatureId = featureId;
            ReturnUrl = returnUrl;

        }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        [Display(Name = "Feature")]
        public string FeatureId { get; set; }
        public SelectList SelectListFeature { get; set; }
        public string ReturnUrl { get; set; }

        public void SelfCheck()
        {
            ParentId.IsNullOrWhiteSpaceThrowException("ParentId");
            FeatureId.IsNullOrWhiteSpaceThrowException("FeatureId");
            ReturnUrl.IsNullOrWhiteSpaceThrowException("ReturnUrl");
        }
    }
}
