using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS
{
    [NotMapped]
    public class MenuFeatureDeleteModel
    {
        public MenuFeatureDeleteModel()
        {

        }

        public MenuFeatureDeleteModel(string menuPathId, string menuFeatureId, string returnUrl)
        {
            MenuPathId = menuPathId;
            MenuFeatureId = menuFeatureId;
            ReturnUrl = returnUrl;

        }
        public string MenuPathId { get; set; }
        public IMenuPath MenuPath { get; set; }

        [Display(Name = "Feature")]
        public string MenuFeatureId { get; set; }
        public MenuFeature MenuFeature { get; set; }
        public string ReturnUrl { get; set; }

        public void SelfCheck()
        {
            MenuPathId.IsNullOrWhiteSpaceThrowException("MenuPath1Id");
            MenuFeatureId.IsNullOrWhiteSpaceThrowException("FeatureId");
            ReturnUrl.IsNullOrWhiteSpaceThrowException("ReturnUrl");
            MenuPath.IsNullThrowException("MenuPath");
            MenuFeature.IsNullThrowException("MenuFeature");
        }
        public void SelfCheckIdsAndReturnOnly()
        {
            MenuPathId.IsNullOrWhiteSpaceThrowException("MenuPathId");
            MenuFeatureId.IsNullOrWhiteSpaceThrowException("FeatureId");
            ReturnUrl.IsNullOrWhiteSpaceThrowException("ReturnUrl");
        }
    }
}
