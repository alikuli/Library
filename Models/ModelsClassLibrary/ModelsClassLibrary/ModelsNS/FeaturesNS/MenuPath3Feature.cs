using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;


namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class MenuPath3Feature : CommonWithId
    {

        public MenuPath3Feature()
        {
            FeatureTypeEnum = FeatureTypeENUM.Value;

        }
        [Display(Name = "Menu Path 3")]
        public string MenuPath3Id { get; set; }
        public virtual MenuPath3 MenuPath3 { get; set; }

        [Display(Name = "Feature Type")]
        public FeatureTypeENUM FeatureTypeEnum { get; set; }

        //[Display(Name = "Feature")]
        //public string FeatureId { get; set; }
        //public Feature Feature { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MenuPath3Feature;
        }

        public override void SelfErrorCheck()
        {
            MenuPath3Id.IsNullOrWhiteSpaceThrowArgumentException("MenuPath3Id is null in the model MenuPath3Feature");
            base.SelfErrorCheck();

        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            MenuPath3Feature mp3f = icommonWithId as MenuPath3Feature;
            MenuPath3Id = mp3f.MenuPath3Id;

        }
    }
}
