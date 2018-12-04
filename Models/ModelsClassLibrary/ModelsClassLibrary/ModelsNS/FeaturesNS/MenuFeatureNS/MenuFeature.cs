using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public partial class MenuFeature : FeatureAbstract
    {
        [NotMapped]
        public string MenuPath1Id { get; set; }

        [NotMapped]
        public string MenuPath2Id { get; set; }

        [NotMapped]
        public string MenuPath3Id { get; set; }


        /// <summary>
        /// We use IsCreate is used during creation of a new feature. If IsCreate is true
        /// it will hide all the MenuPaths
        /// </summary>
        [NotMapped]
        public bool IsCreate { get; set; }


        public virtual ICollection<MenuPath1> MenuPath1s { get; set; }
        public virtual ICollection<MenuPath2> MenuPath2s { get; set; }
        public virtual ICollection<MenuPath3> MenuPath3s { get; set; }
        //public ICollection<Product> Products { get; set; }
        //public ICollection<ProductChild> ProductChildren { get;set;}

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MenuFeature;
        }


    }
}
