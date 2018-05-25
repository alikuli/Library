using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ModelsClassLibrary.ModelsNS.ProductNS;

using Microsoft.AspNet.Identity;

using UserModels.Models;
using System.Web.WebPages.Html;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// We are going to put the serial number of the scratchcard in name so that there are no duplicates.
    /// Therefore, we are going to scramble name.
    /// </summary>
    public class ProductDAL : Repositry<Product>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public ProductDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }


        /// <summary>
        /// This is where all the error checking is centralized.
        /// </summary>
        /// <param name="entity"></param>
        public override void ErrorCheck(Product entity)
        {
            base.ErrorCheck(entity);
                            
            Check_SerialNo(entity);
            
        }

        #region Checks
        private void Check_SerialNo(Product entity)
        {
            if (!entity.SerialNo.IsNullOrEmpty())
            {
                var prodFound = this.FindForSerialNumber(entity.SerialNo);
                if (prodFound != null)
                    if (entity.Id != prodFound.Id)
                    {
                        //oops we have a duplicate.... this is not an edit operation
                        throw new ErrorHandlerLibrary.ExceptionsNS.DuplicateScratchCardNumberException("You have entered a duplicate serial number. You cannot do this.");

                    }

            }
        }







        #endregion


        public override void Fix(Product entity)
        {
            base.Fix(entity);

            Fix_Allotted(entity);
            Fix_Available(entity);
            Fix_OnHand(entity);
            Fix_OnOrder(entity);
            Fix_Parent(entity);
            Fix_Product_Category(entity);
            Fix_UomLengthForPackingVol(entity);
            Fix_Uom_Purchase(entity);
            Fix_Uom_ShipWeight(entity);
            Fix_UomStock(entity);
            Fix_Uom_Volume(entity);
            Fix_UomWeightOnProduct(entity);

            Fix_Calculated_Amounts(entity);


        }

        #region Fix Helpers
        private void Fix_Calculated_Amounts(Product entity)
        {
            Fix_Allotted(entity);
            Fix_Available(entity);
            Fix_OnHand(entity);
            Fix_OnOrder(entity);

        }
        /// <summary>
        /// Not Required
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_UomWeightOnProduct(Product entity)
        {
            //Not required
            if (entity.UomWeightOnProduct == null)
            {
                if (entity.UomWeightOnProductID.IsNullOrEmpty())
                {
                    //dont do anything....
                }
                else
                {
                    var x = new UomWeightDAL(_db, _user).FindFor(entity.UomWeightOnProductID);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Weight On Product not found for product: {0}. ProductDAL.Fix_UomWeightOnProduct", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomWeightOnProductID.IsNullOrEmpty())
                {
                    entity.UomWeightOnProductID = entity.UomWeightOnProduct.Id;
                }
            }
        }

        private void Fix_Uom_Volume(Product entity)
        {
            //Not required
            if (entity.UomVolume == null)
            {
                if (entity.UomVolumeId.IsNullOrEmpty())
                {
                    //dont do anything....
                }
                else
                {
                    var x = new UomVolumeDAL(_db, _user).FindFor(entity.UomVolumeId);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Volume On Product not found for product: {0}. ProductDAL.Fix_Uom_Volume", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomVolumeId.IsNullOrEmpty())
                {
                    entity.UomVolumeId = entity.UomVolume.Id;
                }
            }
        }

        private void Fix_UomStock(Product entity)
        {
            if (entity.UomStock == null)
            {
                if (entity.UomStockID.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("Uom Stock On Product not found for product: {0}. 1.ProductDAL.Fix_UomStock", entity.FullName()));
                }
                else
                {
                    var x = new UomQtyDAL(_db, _user).FindFor(entity.UomStockID);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Stock On Product not found for product: {0}. 2.ProductDAL.Fix_UomStock", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomStockID.IsNullOrEmpty())
                {
                    entity.UomStockID = entity.UomStock.Id;
                }
            }
        }

        private void Fix_Uom_ShipWeight(Product entity)
        {
            if (entity.UomShipWeight == null)
            {
                if (entity.UomStockID.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("Uom Ship Weight not found for product: {0}. 1.ProductDAL.Fix_Uom_ShipWeight", entity.FullName()));
                }
                else
                {
                    var x = new UomWeightDAL(_db, _user).FindFor(entity.UomStockID);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Ship Weight On Product not found for product: {0}. 2.ProductDAL.Fix_Uom_ShipWeight", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomShipWeightId.IsNullOrEmpty())
                {
                    entity.UomShipWeightId = entity.UomShipWeight.Id;
                }
            }
        }

        private void Fix_Uom_Purchase(Product entity)
        {
            if (entity.UomPurchase == null)
            {
                if (entity.UomPurchaseID.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("Uom Purchase not found for product: {0}. 1.ProductDAL.Fix_Uom_Purchase", entity.FullName()));
                }
                else
                {
                    var x = new UomQtyDAL(_db, _user).FindFor(entity.UomPurchaseID);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Purchase not found for product: {0}. 2.ProductDAL.Fix_Uom_Purchase", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomPurchaseID.IsNullOrEmpty())
                {
                    entity.UomPurchaseID = entity.UomPurchase.Id;
                }
            }
        }

        private void Fix_UomLengthForPackingVol(Product entity)
        {
            if (entity.UomLengthForPackingVol == null)
            {
                if (entity.UomStockID.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("Uom Uom Length For Packing Vol not found for product: {0}. 1.ProductDAL.Fix_Uom_Purchase", entity.FullName()));
                }
                else
                {
                    var x = new UomWeightDAL(_db, _user).FindFor(entity.UomStockID);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Length For Packing Vol not found for product: {0}. 2.ProductDAL.Fix_Uom_Purchase", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomShipWeightId.IsNullOrEmpty())
                {
                    entity.UomShipWeightId = entity.UomShipWeight.Id;
                }
            }
        }

        private void Fix_Product_Category(Product entity)
        {
            if (entity.ProdCategory == null)
            {
                if (entity.UomStockID.IsNullOrEmpty())
                {
                    //throw new Exception(string.Format("Product Category not found for product: {0}. 1.ProductDAL.Fix_Product_Category", entity.FullName()));
                }
                else
                {
                    var x = new ProductCategoryMainDAL(_db, _user).FindFor(entity.UomStockID);

                    if (x == null)
                    {
                        throw new Exception(string.Format("Uom Purchase not found for product: {0}. 2.ProductDAL.Fix_Product_Category", entity.FullName()));
                    }
                }
            }
            else
            {
                if (entity.UomShipWeightId.IsNullOrEmpty())
                {
                    entity.UomShipWeightId = entity.UomShipWeight.Id;
                }
            }
        }

        private void Fix_Parent(Product entity)
        {
            if (entity.IsChild)
            {
                if (entity.Parent == null)
                {
                    if (entity.ParentId.IsNullOrEmpty())
                    {
                        throw new Exception(string.Format("Parent not found for product: {0}. 1.ProductDAL.Fix_Parent", entity.FullName()));
                    }
                    else
                    {
                        var x = new ProductDAL(_db, _user).FindFor(entity.ParentId);

                        if (x == null)
                        {
                            throw new Exception(string.Format("Parent not found for product: {0}. 2.Fix_Parent.Fix_Product_Category", entity.FullName()));
                        }
                    }
                }
                else
                {
                    if (entity.ParentId.IsNullOrEmpty())
                    {
                        entity.ParentId = entity.Parent.Id;
                    }
                }
            }
            else
            {
                entity.Parent = null;
                entity.ParentId = null;
            }
        }

        private void Fix_OnOrder(Product entity)
        {
            GetOnOrder(entity);
        }


        private void Fix_OnHand(Product entity)
        {
            Get_OnHand(entity);
        }


        private void Fix_Available(Product entity)
        {
            Get_Available(entity);
            //entity.AvailableCalculated();
        }


        private void Fix_Allotted(Product entity)
        {
            Get_Allotted(entity);
        }
        
        #endregion



        #region Get...
        private decimal Get_Allotted(Product entity)
        {
            //Get all items that are on order but not invoiced
            SalesOrderDAL soDAL = new SalesOrderDAL(_db, _user);
            entity.Is_Allotted_FreshlyCalculated = true;

            throw new NotImplementedException();
        }
        private decimal Get_Available(Product entity)
        {
            //OnHand - Allotted
            entity.Is_Available_FreshlyCalculated = true;
            throw new NotImplementedException(GetSelfMethodName());
            
        }
        private decimal Get_OnHand(Product entity)
        {
            //Total Items Received but not shipped.
            entity.Is_OnHand_FreshlyCalculated = true;
            throw new NotImplementedException();
        }
        private decimal GetOnOrder(Product entity)
        {
            //Total items on Order In PO
            entity.Is_OnOrder_FreshlyCalculated = true;

            throw new NotImplementedException();
        }

        #endregion

        public IEnumerable<SelectListItem> SelectListParent(Guid? productId)
        {
            if (productId.IsNullOrEmpty())
                throw new Exception("No product sent. ProductDAL.SelectListParent");

            var data = FindAll().Where(b => b.Id != productId).AsQueryable();
            return SelectListEngine(data);
        }

        
        public Product FindForProuductOwnNumber(string productOwnNumber)
        {
            var products=this.SearchFor(x => x.ProductsOwnNumber == productOwnNumber).ToList().FirstOrDefault();
            return products;
        }

        public Product FindForSerialNumber(string serialNo)
        {
            var product = this.SearchFor(x => x.SerialNo == serialNo).ToList().FirstOrDefault();
            return product;
        }

    }
}
