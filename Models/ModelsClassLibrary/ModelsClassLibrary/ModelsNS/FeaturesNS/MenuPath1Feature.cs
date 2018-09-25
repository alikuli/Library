using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;


namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class MenuPath1Feature : CommonWithId
    {
        [Display(Name = "Menu Path 1")]
        public string MenuPath1Id { get; set; }
        public virtual MenuPath1 MenuPath1 { get; set; }


        //[Display(Name = "Feature")]
        //public string FeatureId { get; set; }
        //public Feature Feature { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MenuPath1Feature;
        }

        public override void SelfErrorCheck()
        {
            MenuPath1Id.IsNullOrWhiteSpaceThrowArgumentException("MenuPath1Id is null in the model MenuPath1Feature");
            base.SelfErrorCheck();

        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            MenuPath1Feature mp1f = icommonWithId as MenuPath1Feature;
            MenuPath1Id = mp1f.MenuPath1Id;

        }
    }
}
