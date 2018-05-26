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

        #region UomPurchase
        public string UomPurchaseId { get; set; }

        [Display(Name = "Purchase UOM")]
        public UomQty UomPurchase { get; set; }

        #endregion


        #region UomStock


        //=========================================================================================================
        /// <summary>
        /// This is the UOM we stock in. 
        /// </summary>
        [Display(Name = "Stock UOM")]
        public string UomStockID { get; set; }


        public UomQty UomStock { get; set; }


        ///// <summary>
        ///// This is the UOM we stock in. Note, we dont need the sale UOM because we can convert this to any type. Note, the quantity stored here will be calculated 
        ///// on the fly and stored
        ///// </summary>
        [Display(Name = "Stock")]
        public Quantity Qty { get; set; }

        #endregion


        #region Listed Weight UOM, WeightOnProduct
        [Display(Name = "Listed Weight UOM")]
        public string UomWeightId { get; set; }
        public UomWeight UomUomWeight { get; set; }

        [Display(Name = "Listed Weight")]
        public double Weight { get; set; }
        #endregion


        #region Listed Volume


        [Display(Name = "Listed Volume UOM")]
        public string UomVolumeId { get; set; }
        public UomVolume UomVolume { get; set; }

        public double Volume { get; set; }

        #endregion


        #region Ship Weight

        [Display(Name = "Ship Wt. UOM")]
        public string UomShipWeightId { get; set; }
        public UomWeight UomShipWeight { get; set; }

        [Display(Name = "Ship Wt.")]
        public double ShipWeight { get; set; }

        #endregion


        #region Diemensions

        [Display(Name = "UOM Length")]
        public string UomPackageLengthId { get; set; }
        public UomLength UomPackageLength { get; set; }


        public Dimensions Dimensions { get; set; }

        #endregion





    }
}