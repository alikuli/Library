using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId
    {






        //#region Properties

        /// <summary>
        /// This is the serial number of the product
        /// </summary>
        [Display(Name = "Serial #")]
        public string SerialNo { get; set; }








    }
}