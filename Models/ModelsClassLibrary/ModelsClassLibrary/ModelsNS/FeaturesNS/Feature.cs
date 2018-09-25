using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS
{
    /// <summary>
    /// This stores extra features about the product.
    /// The Name is constructed from the parent name and feature name.
    /// </summary>
    public class Feature : CommonWithId
    {
        public Feature()
        {
            FeaturesTypeEnum = FeatureTypeENUM.Unknown;
        }

        ///// <summary>
        ///// This is the name given to the feature. Name will be formed from this. 
        ///// Name + FeatureName
        ///// </summary>
        //public string FeatureName { get; set; }
        public string Value { get; set; }
        public FeatureTypeENUM FeaturesTypeEnum { get; set; }


        [Display(Name = "Menu Path 1")]
        public virtual ICollection<MenuPath1> MenuPath1s { get; set; }

        //[NotMapped]
        //public System.Web.Mvc.SelectList SelectList { get; set; }

        //[Display(Name = "MenuPath 2")]
        //public string MenuPath2Id { get; set; }
        //public virtual MenuPath2 MenuPath2 { get; set; }


        //[Display(Name = "MenuPath 3")]
        //public string MenuPath3Id { get; set; }
        //public virtual MenuPath3 MenuPath3 { get; set; }


        //[Display(Name = "Product")]
        //public string ProductId { get; set; }
        //public virtual Product Product { get; set; }


        //[Display(Name = "Product Child")]
        //public string ProductChildId { get; set; }
        //public virtual ProductChild ProductChild { get; set; }

        public FeatureTypeENUM FeatureTypeEnum { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Feature;
        }
    }
}
