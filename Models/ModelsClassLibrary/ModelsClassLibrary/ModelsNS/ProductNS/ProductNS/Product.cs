using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductEtcNS;
//using ModelsClassLibrary.Models.DiscountNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class Product : ProductAbstract
    {
        public Product()
        {
            MenuPathMains = new List<MenuPathMain>();
            ProductChildren = new List<ProductChild>();

        }
        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        public virtual ICollection<ProductChild> ProductChildren { get; set; }

        ///// <summary>
        ///// This decides the menus
        ///// </summary>
        //[Display(Name = "Category")]
        //public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Product;
        }
    }
}