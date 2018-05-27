using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId
    {

        #region UomPurchase
        public string UomPurchaseId { get; set; }

        [Display(Name = "Purchase UOM")]
        public virtual  UomQty UomPurchase { get; set; }

        #endregion


        #region UomStock


        //=========================================================================================================
        /// <summary>
        /// This is the UOM we stock in. 
        /// </summary>
        [Display(Name = "Stock UOM")]
        public string UomStockID { get; set; }


        public virtual UomQty UomStock { get; set; }


        ///// <summary>
        ///// This is the UOM we stock in. Note, we dont need the sale UOM because we can convert this to any type. Note, the quantity stored here will be calculated 
        ///// on the fly and stored
        ///// </summary>
        [NotMapped]
        [Display(Name = "Stock")]
        public Quantity Qty { get; set; }

        #endregion


        #region Listed Weight UOM, WeightOnProduct
        [Display(Name = "Listed Weight UOM")]
        public string UomWeightId { get; set; }
        public virtual UomWeight UomUomWeight { get; set; }

        [Display(Name = "Listed Weight")]
        public double Weight { get; set; }
        #endregion


        #region Listed Volume


        [Display(Name = "Listed Volume UOM")]
        public string UomVolumeId { get; set; }
        public virtual UomVolume UomVolume { get; set; }

        public double Volume { get; set; }

        #endregion


        #region Ship Weight

        [Display(Name = "Ship Wt. UOM")]
        public string UomShipWeightId { get; set; }
        public virtual UomWeight UomShipWeight { get; set; }

        [Display(Name = "Ship Wt.")]
        public double ShipWeight { get; set; }

        #endregion


        #region Diemensions

        [Display(Name = "UOM Dimensions")]
        public string UomDimensionsId { get; set; }
        public virtual UomLength UomDimensions { get; set; }


        public virtual Dimensions Dimensions { get; set; }

        #endregion





    }
}