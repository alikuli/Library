using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId
    {

        /// <summary>
        /// These are all the list of Item Numbers the product can have. You must have at least one
        /// </summary>
        //public virtual ICollection<ProductIdentifier> ItemNos { get; set; }


        /// <summary>
        /// This decides the menus
        /// </summary>
        [Display(Name = "Category")]
        public virtual ICollection<ProductCategoryMain> ProductCategories { get; set; }



        public string ParentId { get; set; }

        [Display(Name = "Parent")]
        public virtual Product Parent { get; set; }


    }
}