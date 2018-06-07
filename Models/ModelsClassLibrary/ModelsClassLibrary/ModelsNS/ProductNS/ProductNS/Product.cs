﻿using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
//using ModelsClassLibrary.Models.DiscountNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class Product : ProductAbstract, IAmMenu
    {
        public Product()
        {
            MenuPathMains = new List<MenuPathMain>();
            ProductChildren = new List<ProductChild>();
            ProductIdentifiers = new List<ProductIdentifier>();

        }
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

    }
}