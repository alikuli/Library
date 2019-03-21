using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS.CheckBoxItemNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Product is in active until specifically made active.
    /// </summary>
    public partial class Product : ProductAbstract, IProduct
    {
        public Product()
        {
            IsUnApproved = true;
            ApprovedBy = new DateAndByComplex();
        }

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


        public bool IsUnApproved { get; set; }

        [Display(Name = "Approval Details")]
        public DateAndByComplex ApprovedBy { get; set; }

        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        public virtual ICollection<ProductChild> ProductChildren { get; set; }

        public virtual ICollection<GlobalComment> GlobalComments { get; set; }

        public virtual List<ProductFeature> ProductFeatures { get; set; }
        public virtual ICollection<Message> Messages { get; set; }


        //public virtual List<ProductSetupProblem> ProductSetupProblem { get; set; }

        [Display(Name = "Owner")]
        public string OwnerId { get; set; }

        /// <summary>
        /// This is the Owner who has made the product.
        /// </summary>
        public virtual Owner Owner { get; set; }




        [NotMapped]
        public SelectList SelectListUomPurchase { get; set; }

        [NotMapped]
        public SelectList SelectListUomVolume { get; set; }

        [NotMapped]
        public SelectList SelectListUomShipWeight { get; set; }

        [NotMapped]
        public SelectList SelectListUomWeight { get; set; }

        [NotMapped]
        public SelectList SelectListUomLength { get; set; }

        [NotMapped]
        public SelectList SelectListUomDimensionsLength { get; set; }

        [NotMapped]
        public bool ShowApproveButton { get; set; }
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