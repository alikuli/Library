using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract
    {


        #region Error Checks

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            Check_Short_Description();
            Check_Long_Description();
            Check_MlpPrice();
            Check_Sell_Price();
            Check_Is_Not_Child_Of_Self();
            //Check_UomStock();
            Check_Uom_Weight_On_Product();
            Check_Uom_Volume();
            Check_Ship_Weight();
            Check_Uom_ShipWeight();
            //Check_At_Least_One_ItemNos();

            //Check_MSRP();
            //Check_Parent();
            //Check_Product_Category();
            //Check_UomLengthForPackingVolId();
            //Check_Last_Order_Date_Not_Greater_Than_Today();
            //Check_Picture_Loaded_If_Displaying_On_Website();
            //Check_Calculated_Fields_Updated();
        }

        /// <summary>
        /// Checks to see all the calculated fields have been updated.
        /// </summary>
        //private void Check_Calculated_Fields_Updated()
        //{
        //    if (!Is_Allotted_FreshlyCalculated)
        //        throw new Exception("The allotted field has not been calculated. ProductAbstract.Check_Calculated_Fields_Updated");
        //    if (!Is_Available_FreshlyCalculated)
        //        throw new Exception("The available field has not been calculated. ProductAbstract.Check_Calculated_Fields_Updated");
        //    if (!Is_OnHand_FreshlyCalculated)
        //        throw new Exception("The On Hand field has not been calculated. ProductAbstract.Check_Calculated_Fields_Updated");
        //    if (!Is_OnOrder_FreshlyCalculated)
        //        throw new Exception("The On Order field has not been calculated. ProductAbstract.Check_Calculated_Fields_Updated");
        //}

        //private void Check_Picture_Loaded_If_Displaying_On_Website()
        //{
        //    if (IsDisplayedOnWebsite)
        //    {
        //        if (IsBigPictureLoaded || IsSmallPictureLoaded || IsMediumPictureLoaded)
        //        {
        //            //do nothing...
        //        }
        //        else
        //        {
        //            throw new Exception("No picture has been uploaded. At least load one picture. ProductAbstract.Check_Picture_Loaded_If_Displaying_On_Website");
        //        }
        //    }
        //}
        /// <summary>
        /// Makes sure if the Last Order Date is not null then it
        /// is not greater than today.
        /// </summary>
        //private void Check_Last_Order_Date_Not_Greater_Than_Today()
        //{
        //    if (Date.LastOrderedDate== null)
        //        return;

        //    if (!Date.LastOrderedDate.HasValue)
        //        return;

        //    DateTime lastDate = Date.LastOrderedDate ?? DateTime.MinValue;
        //    if (lastDate.Date > DateTime.Now)
        //        throw new Exception("Last Order Date cannot be greater than today.");
        //}

        //private void Check_At_Least_One_ItemNos()
        //{
        //    if (ItemNos == null || ItemNos.Count == 0)
        //    {
        //        throw new Exception(string.Format("There is no Item No for this product: '{0}'. At least one is required 3.ProductAbstract.Check_UomLengthForPackingVolId", FullName()));
        //    }
        //}

        //private void Check_UomLengthForPackingVolId()
        //{
        //    if (IsUomForPackingVolRequired)
        //    {
        //        if (UomPackageLength == null)
        //        {
        //            throw new Exception(string.Format("The Uom Length For Packing Vol is required for this product: '{0}' but is missing. 1.ProductAbstract.Check_UomLengthForPackingVolId", FullName()));
        //        }

        //        if (UomPackageLengthId.IsNullOrEmpty())
        //        {
        //            throw new Exception(string.Format("The Uom Length For Packing Vol is required for this product: '{0}' but is missing. 2.ProductAbstract.Check_UomLengthForPackingVolId", FullName()));
        //        }

        //    }
        //    else
        //    {
        //        //Even if not required...
        //        //if UomLengthForPackingVolId has a value, then so must its Id.
        //        if (!UomPackageLength.IsNull())
        //        {
        //            if (UomPackageLengthId.IsNullOrEmpty())
        //            {
        //                throw new Exception(string.Format("The Uom Length For Packing Vol is required for this product: '{0}' but is missing. 3.ProductAbstract.Check_UomLengthForPackingVolId", FullName()));
        //            }

        //        }

        //        if (!UomPackageLengthId.IsNullOrEmpty())
        //        {
        //            if (UomPackageLength.IsNull())
        //            {
        //                throw new Exception(string.Format("The Uom Length For Packing Vol is required for this product: '{0}' but is missing. 3.ProductAbstract.Check_UomLengthForPackingVolId", FullName()));
        //            }

        //        }
        //    }
        //}

        private void Check_Uom_ShipWeight()
        {
            //if (UomWeightActual.IsNull())
            //{
            //    throw new Exception(string.Format("The Uom of Ship Weight On Product '{0}' has no is missing. 1.ProductAbstract.Check_Uom_ShipWeight", FullName()));

            //}
            //if (UomWeightActualId.IsNullOrEmpty())
            //{
            //    throw new Exception(string.Format("The Uom of Ship Weight On Product '{0}' has no is missing. 2.ProductAbstract.Check_Uom_ShipWeight", FullName()));

            //}
        }

        private void Check_Ship_Weight()
        {
            //if (WeightActual == 0)
            //{
            //    throw new Exception(string.Format("The Uom of Ship Weight On Product '{0}' is zero. 1.ProductAbstract.Check_Ship_Weight", FullName()));

            //}
        }

        private void Check_Uom_Volume()
        {
            //if (UomVolume.IsNull())
            //{
            //    throw new Exception(string.Format("The Uom of Ship volume On Product '{0}' has no is missing. 1.ProductAbstract.Check_Uom_Volume", FullName()));

            //}
            if (UomVolumeId.IsNullOrEmpty())
            {
                throw new Exception(string.Format("The Uom of Ship volume On Product '{0}' has no is missing. 2.ProductAbstract.Check_Uom_Volume", FullName()));

            }
        }

        private void Check_Uom_Weight_On_Product()
        {
            //not required....

            //Either both there... or none there.

            //if (UomWeightListed == null)
            //{
            //    if (UomWeightListedId.IsNullOrEmpty())
            //    {
            //        //Both are null... dont do anything
            //        return;
            //    }

            //    throw new Exception(string.Format("The Uom Weight On Product '{0}' has no Id. ProductAbstract.Check_Uom_Weight_On_Product", FullName()));
            //}
            //else
            //{
            //    if (UomWeightListedId.IsNullOrEmpty())
            //    {
            //        throw new Exception(string.Format("The Uom Weight On Product '{0}' has no Product. ProductAbstract.Check_Uom_Weight_On_Product", FullName()));
            //    }
            //}
        }

        //private void Check_UomStock()
        //{
        //    //this is required
        //    if (UomStock == null)
        //    {
        //        throw new Exception(string.Format("UOM Stock Is Required for product '{0}'. 1.ProductAbstract.Check_UomStock", FullName()));
        //    }
        //    if (UomStockID.IsNullOrEmpty())
        //    {
        //        throw new Exception(string.Format("UOM Stock Is Required for product '{0}'. 2.ProductAbstract.Check_UomStock", FullName()));
        //    }
        //}

        //private void Check_Parent()
        //{
        //    if (IsChild)
        //    {
        //        //Parent it required
        //        if (Parent == null)
        //        {
        //            throw new Exception(string.Format("This is a child item but has no parent. 1.Check_Parent.ProductAbstract."));
        //        }

        //        if (ParentId.IsNullOrEmpty())
        //        {
        //            throw new Exception(string.Format("This is a child item but has no parent. 2.Check_Parent.ProductAbstract."));
        //        }
        //    }
        //    else
        //    {
        //        if (Parent != null)
        //        {
        //            if (ParentId.IsNullOrEmpty())
        //            {
        //                throw new Exception(string.Format("This is a child item but has no parent. 3.Check_Parent.ProductAbstract."));
        //            }
        //        }
        //        else
        //        {

        //            if (!ParentId.IsNullOrEmpty())
        //            {
        //                throw new Exception(string.Format("This is a child item but has no parent. 4.Check_Parent.ProductAbstract."));
        //            }
        //        }
        //    }

        //}

        private void Check_Is_Not_Child_Of_Self()
        {
            if (IsChild)
            {
                if (ParentId == Id)
                {
                    throw new Exception(string.Format("This product '{0}' is a child of itself! Not allowed. ProductAbstract.Check_Is_Not_Child_Of_Self", FullName()));
                }
            }
        }

        private void Check_Sell_Price()
        {
            if (Sell.SellPrice < Buy.Cost)
            {
                throw new Exception(string.Format("Sell price '{0}' cannot be less than buy price '{1}' for product {2}. ProductAbstract.Check_Sell_Price",
                    Sell.SellPrice,
                    Buy.Cost,
                    FullName()));
            }

        }

        /// <summary>
        /// Manufacturers lowest price
        /// </summary>
        private void Check_MlpPrice()
        {
            if (Sell.MlpPrice == 0)
            {
                //No checks
                return;
            }

            if (Sell.MlpPrice > Sell.SellPrice)
            {
                throw new Exception(string.Format("The MLP Price {0}, cannot be higher than the Sell Price {1} for product '{2}' ProductAbstract.Check_MlpPrice.",
                    Sell.MlpPrice,
                    Sell.SellPrice,
                    FullName()));
            }

        }

        //private void Check_MSRP()
        //{
        //    if (Sell.MSRP == 0)
        //    {
        //        if (IsAllowd_Zero_MRSP)
        //        {
        //            //dont do anything.
        //        }
        //        else
        //        {
        //            throw new Exception(string.Format("MSRP is zero for product '{0}'. This is not allowed for this product. If you want a zero MSRP then mark IsAllowd_Zero_MRSP to true from Product Edits. ProductAbstract.Check_MSRP", FullName()));
        //        }
        //    }
        //}

        private void Check_Long_Description()
        {
            //this is allowed for now...
        }

        private void Check_Short_Description()
        {
            //if (ShortDescription.IsNullOrEmpty())
            //{
            //    throw new Exception(string.Format("Short Description is required for product '{0}'. ProductAbstract.Check_Short_Description", FullName()));
            //}
        }

        #endregion


    }
}