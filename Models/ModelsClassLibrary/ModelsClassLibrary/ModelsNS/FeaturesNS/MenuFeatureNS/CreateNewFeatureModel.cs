using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS
{
    [NotMapped]
    public class CreateNewFeatureModel
    {
        public string MenuPathId { get; set; }

        [Display(Name = "Feature Name")]
        public string FeatureName { get; set; }
        public string ReturnUrl { get; set; }

        public void SelfCheck()
        {
            FeatureName.IsNullOrWhiteSpaceThrowException("FeatureName");
            ReturnUrl.IsNullOrWhiteSpaceThrowException("ReturnUrl");
            MenuPathId.IsNullOrWhiteSpaceThrowException("FeatureName");
        }

    }
}
