using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;


namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class MenuPath2Feature : CommonWithId
    {
        [Display(Name = "Menu Path 2")]
        public string MenuPath2Id { get; set; }
        public virtual MenuPath2 MenuPath2 { get; set; }


        //[Display(Name = "Feature")]
        //public string FeatureId { get; set; }
        //public Feature Feature { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MenuPath2Feature;
        }

        public override void SelfErrorCheck()
        {
            MenuPath2Id.IsNullOrWhiteSpaceThrowArgumentException("MenuPath2Id is null in the model MenuPath2Feature");
            base.SelfErrorCheck();

        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            MenuPath2Feature mp2f = icommonWithId as MenuPath2Feature;
            MenuPath2Id = mp2f.MenuPath2Id;

        }
    }
}
