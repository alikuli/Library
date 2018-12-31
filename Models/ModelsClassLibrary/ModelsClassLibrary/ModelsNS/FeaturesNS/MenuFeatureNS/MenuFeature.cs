using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using System.Collections.Generic;
//using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public partial class MenuFeature : FeatureAbstract
    {



        public virtual ICollection<MenuPath1> MenuPath1s { get; set; }
        public virtual ICollection<MenuPath2> MenuPath2s { get; set; }
        public virtual ICollection<MenuPath3> MenuPath3s { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MenuFeature;
        }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            MenuFeature menuFeature = icommonWithId as MenuFeature;
            menuFeature.IsNullThrowException("Unable to unbox MenuFeature");


        }


    }
}
