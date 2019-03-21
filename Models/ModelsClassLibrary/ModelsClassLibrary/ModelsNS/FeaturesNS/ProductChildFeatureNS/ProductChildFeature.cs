
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class ProductChildFeature : FeatureAbstract
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.ProductChildFeature;
        }

        [Display(Name = "Product")]
        public string ProductChildId { get; set; }
        public virtual ProductChild ProductChild { get; set; }

        //the name and the comment will store the name and the value
        //public ProductChildFeature ToProductChildFeatureFrom(MenuFeature menuFeature)
        //{
        //    menuFeature.IsNullThrowExceptionArgument("menuFeature");
        //    ProductChildFeature pcf = new ProductChildFeature();
        //    pcf.Name = menuFeature.Name;
        //    pcf.Comment = menuFeature.Comment;
        //    return pcf;
        //}
    }
}
