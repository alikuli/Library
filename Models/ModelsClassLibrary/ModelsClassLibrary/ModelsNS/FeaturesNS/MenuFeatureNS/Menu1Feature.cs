using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.MenuNS;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS
{
    public class MenuPath1Feature: CommonWithId
    {
        [Display(Name = "Menu Feature")]
        public string MenuFeatureId { get; set; }
        public virtual MenuFeature MenuFeature { get; set; }

        [Display(Name="Menu 1")]
        public string MenuPath1Id { get;set;}
        public MenuPath1 MenuPath1 { get; set; }


        [NotMapped]
        public SelectList SelectListMenuPath1 { get; set; }
        [NotMapped]
        public SelectList SelectListMenuFeature { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MenuPath1Feature;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            MenuPath1Feature menuPath1Feature = icommonWithId as MenuPath1Feature;
            menuPath1Feature.IsNullThrowException("Unable to unbox menuPath1Feature");
            MenuPath1Id = menuPath1Feature.MenuPath1Id;
            MenuFeatureId = menuPath1Feature.MenuFeatureId;

        }
    }
}
