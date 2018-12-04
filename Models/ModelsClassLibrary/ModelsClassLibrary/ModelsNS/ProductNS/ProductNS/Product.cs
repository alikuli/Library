using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS.CheckBoxItemNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AliKuli.Extentions;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class Product : ProductAbstract, IProduct
    {

        #region check boxes
        /// <summary>
        /// The check boxes are used for adding the menu path.
        /// Mp1List list helps to print the check baxes as collapseable buttons.
        /// </summary>

        [NotMapped]
        public virtual List<CheckBoxItem> CheckedBoxesList { get; set; }

        [NotMapped]
        public virtual List<CheckBoxListTree> Mp1List { get; set; }

        #endregion


        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        public virtual ICollection<ProductChild> ProductChildren { get; set; }

        public virtual ICollection<GlobalComment> GlobalComments { get; set; }

        public virtual List<ProductFeature> ProductFeatures { get; set; }
        
        //[NotMapped]
        //public List<ProductFeature> ProductFeaturesList
        //{
        //    get
        //    {
        //        if (ProductFeatures.IsNull())
        //            return null;
        //        return ProductFeatures.ToList();
        //    }
        //}
        /// <summary>
        /// A product can have ONE or Many ProductIdentifiers. It must have at least one.
        /// </summary>
        public virtual ICollection<ProductIdentifier> ProductIdentifiers { get; set; }



        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Product;
        }


    }
}