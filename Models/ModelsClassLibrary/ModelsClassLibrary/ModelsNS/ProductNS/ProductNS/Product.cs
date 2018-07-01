using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class Product : ProductAbstract, IProduct
    {

        public Product()
        {
            MenuManager = new MenuManager(Id, null, this, null, MenuLevelENUM.unknown, string.Empty, false, "", "", "", SortOrderENUM.Item1_Asc, ActionNameENUM.Unknown);

        }

        [NotMapped]
        public virtual List<CheckBoxItem> CheckedBoxesList { get; set; }

        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        public virtual ICollection<ProductChild> ProductChildren { get; set; }


        /// <summary>
        /// A product can have ONE or Many ProductIdentifiers. It must have at least one.
        /// For automobiles, this will be automatically created using the brand, year model etc.
        /// </summary>
        public virtual ICollection<ProductIdentifier> ProductIdentifiers { get; set; }

        ///// <summary>
        ///// This decides the menus
        ///// </summary>
        //[Display(Name = "Category")]
        //public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Product;
        }

        [NotMapped]
        public IMenuManager MenuManager { get; set; }


        public bool IsAutomobile
        {
            get
            {
                return !(this as ProductAutomobileVM).IsNull();
            }
        }

    }
}