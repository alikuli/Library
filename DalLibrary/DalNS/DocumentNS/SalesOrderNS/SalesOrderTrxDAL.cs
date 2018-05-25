using System;
using System.Collections.Generic;
using AliKuli.Extentions;

using InterfacesLibrary.ProductNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

using UserModels.Models;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// In order for SalesOrderTrx to work properly, we will need to have a saved SalesOrderHeader so that it has a
    /// SalesOrderId. We need that to be able to check for duplicate prodcuts.
    /// </summary>
    public class SalesOrderTrxDAL : AbstractTrxDAL<SalesOrderTrx>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public SalesOrderTrxDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }


        /// <summary>
        /// This is true if we are editing and the product has changed to another product
        /// </summary>
        private bool IsUpdateAndProductChanged { get; set; }
        public override void ErrorCheck(SalesOrderTrx entity)
        {
            entity.SelfErrorCheck();

            //Check_SalesorderTrx_Passed_Is_Not_Null(entity);
            //Check_This_Is_Not_A_Duplicate_Product_In_Sales_Order(entity);
            //Check_If_Total_Quantity_Freshly_Calculated(entity);
            //Check_If_Final_Sales_Price_Freshly_Calculated(entity);
            //Check_If_Line_Total_Freshly_Calculated(entity);
            //Check_If_Is_Qty_Remaining_Freshly_Calculated(entity);
        }



        public override void Fix(SalesOrderTrx entity)
        {
            Fix_Product(entity);
            Fix_SalesOrder(entity);

            Fix_IsUpdateAndProductChanged(entity);

            base.Fix(entity);
            Fix_Description(entity);

            //All this needs to be updated if product is updated.

            Fix_All_Calculated_Values(entity);
            Fix_CurrBuyPrice(entity);
            Fix_ListedSalePrice(entity);


        }


        #region Fix Helpers

        /// <summary>
        /// This loads the CurrBuyPrice
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_CurrBuyPrice(SalesOrderTrx entity)
        {
            entity.CurrBuyPrice = entity.GetProduct_BuyPrice();
        }

        /// <summary>
        /// This fixes the SalesOrder
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_SalesOrder(SalesOrderTrx entity)
        {
            if (entity.SalesOrder == null)
            {
                if (entity.SalesOrderID.IsNullOrEmpty())
                {
                    throw new Exception("No Salesorder passed. SalesOrderTrxDAL.Fix_SalesOrder.");

                }
                else
                {
                    entity.SalesOrder = new SalesOrderDAL(_db, _user).FindFor(entity.SalesOrderID);

                    if (entity.SalesOrder == null)
                        throw new Exception("No Salesorder found. SalesOrderTrxDAL.Fix_SalesOrder.");

                }
            }
            else
            {
                if (entity.SalesOrderID.IsNullOrEmpty())
                {
                    entity.SalesOrderID = entity.SalesOrder.Id;
                }
            }

        }


        /// <summary>
        /// Checks to see if the product has been updated, because if this event happens
        /// it will trigger a lot of changes
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_IsUpdateAndProductChanged(SalesOrderTrx entity)
        {
            if (!isUpdating)
                return;

            if (entity.IsNull())
                throw new Exception("No entity passed. SalesOrderTrxDAL.Fix_Has_Product_Changed");

            if (entity.ProductID.IsNullOrEmpty())
                throw new Exception("No product passed. SalesOrderTrxDAL.Fix_Has_Product_Changed");


            IsUpdateAndProductChanged = false;

            //find the old trx and match with new trx to see if the product has changed
            var oldTrx = FindFor(entity.Id);

            IsUpdateAndProductChanged = entity.ProductID != oldTrx.ProductID;


        }

        private void Fix_ListedSalePrice(SalesOrderTrx entity)
        {
            //we dont want this updated every time we rung FindAll....

            if (isCreating)
                entity.ListedPrice = entity.GetProduct_ListedSellPrice();

            if (IsUpdateAndProductChanged)
            {
                entity.ListedPrice = entity.GetProduct_ListedSellPrice();
            }
        }
        private void Fix_Total_Qty_Shipped(SalesOrderTrx entity)
        {
            //entity.ShippedQty = GetTotalQtyShipped(entity);
            //entity.IsTotalQtyShippedFreshlyCalculated = true;
        }



        /// <summary>
        /// Also updates IsFinalSalesPriceFreshlyCalculated
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_Final_Sales_Price(SalesOrderTrx entity)
        {
            ////
            //entity.FinalSalesPrice = entity.GetFinalSalePrice ();
        }


        /// <summary>
        ///This needs to be checked after giving IsTotalQtyShippedFreshlyCalculated a value
        ///Updates IsQtyRemainingFreshlyCalculated
        ///Updates QtyRemaining
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_Qty_Remaining(SalesOrderTrx entity)
        {
            //entity.QtyRemaining = entity.QtyRemainingCalc;
        }


        ///// <summary>
        ///// This is the name of the transaction.
        ///// </summary>
        ///// <param name="entity"></param>
        //private void Fix_Name(SalesOrderTrx entity)
        //{
        //    //This has to be unique so that duplicate products dont get added
        //    string name = string.Format("{0}{1}", 
        //        entity.SalesOrder.Id, 
        //        entity.ProductID);
        //    entity.Name = name;
        //}


        private void Fix_Description(SalesOrderTrx entity)
        {
            entity.Description = entity.GetProductNameFromProduct();
        }


        private void Fix_Product(SalesOrderTrx entity)
        {
            if (entity.Product.IsNull())
            {
                if (entity.ProductID.IsNullOrEmpty())
                {
                    throw new Exception("The product is null. Required. SalesOrderTrxDAL.Fix_Product");
                }
                else
                {
                    entity.Product = (IProduct)new ProductDAL(_db, _user).FindFor(entity.ProductID);

                    if (entity.Product.IsNull())
                    {
                        throw new Exception("The product not found in Db. Try Again. SalesOrderTrxDAL.Fix_Product");

                    }
                }
            }
            else
            {
                if (entity.ProductID.IsNullOrEmpty())
                {
                    entity.ProductID = entity.Product.Id;
                }
            }
        }


        private void Fix_All_Calculated_Values(SalesOrderTrx item)
        {
            //Fix_Line_Total_Ordered(item);
            Fix_Final_Sales_Price(item);
            Fix_Total_Qty_Shipped(item);
            Fix_Qty_Remaining(item);
        }

        //private void Fix_Buy_Price(SalesOrderTrx entity)
        //{
        //    //This is good if creating or changing the product
        //    //But this will change for every update... we
        //    //only want it to change if the product itself is updated.

        //    //I am adding this IF because otherwise this will change each time we
        //    //edit the record, even if we do not want it to change.

        //    if(IsCreating || IsUpdateAndProductChanged)
        //        entity.CurrBuyPrice = entity.Product.BuyPrice;

        //    //if the product is changed... you will need to update the buy
        //    //price at that time.
        //}

        #endregion

        #region Get...
        //protected override decimal GetTotalQtyShipped(SalesOrderTrx soTrx)
        //{
        //    //This gets the total quantity shipped from invoice trxs
        //    //We get this amount from the invoices
        //    //decimal totalShipped = 0;

        //    //if (!soTrx.InvoiceTrxs.IsNullOrEmpty())
        //    //{
        //    //    foreach (var invTrx in soTrx.InvoiceTrxs.ToList())
        //    //    {
        //    //        totalShipped += invTrx.ShippedQty;
        //    //    }

        //    //}
        //    //return totalShipped;
        //}

        //public decimal GetTotalAllottedFor(Guid productId, out string strSoWithItem)
        //{
        //    //get total for all saleorder items
        //    var openSalesOrderTrx = FindAll().Where(x => x.ProductID == productId);
        //    strSoWithItem = "";

        //    decimal totalAllotted = 0;

        //    if(!openSalesOrderTrx.IsNullOrEmpty())
        //    {
        //        foreach (var item in openSalesOrderTrx.ToList())
        //        {
        //            totalAllotted += item.QtyToShip;
        //            strSoWithItem += string.Format("{0}-{1}; ", item.SalesOrder.DocNo, item.SalesOrder.Date.ToString("dd-mmm-yy"));
        //        }
        //    }

        //    return totalAllotted;

        //}



        #endregion

        /// <summary>
        /// This will get fixed before saving.
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <param name="salesOrder"></param>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <param name="qtyOrdered"></param>
        /// <param name="qtyToShip"></param>
        /// <returns></returns>
        /// 
        public SalesOrderTrx AutoCreate(
            Guid? salesOrderId,
            SalesOrder salesOrder,
            Guid? productId,
            Product product,
            decimal qtyOrdered,
            decimal qtyToShip,
            decimal productSalePrice,
            DateTime dateToShipBegin,
            DateTime dateToShipEnd,
            string comment,
            bool forcedSale = false)
        {
            SalesOrderTrx soTrx = Factory();

            //if null or empty... don't give a value.
            if (!(salesOrderId.IsNullOrEmpty()))
                soTrx.SalesOrderID = (salesOrderId.GetValueOrDefault());

            //if null or empty... don't give a value.
            if (!productId.IsNullOrEmpty())
                soTrx.ProductID = productId.GetValueOrDefault();

            soTrx.SalesOrder = salesOrder;
            soTrx.Product = (IProduct)product;
            soTrx.OrderedQty = qtyOrdered;
            soTrx.ShipQty = qtyToShip;
            soTrx.MetaData.Created.SetToTodaysDateStart();
            soTrx.SalePrice = productSalePrice;

            soTrx.DateToShipBegin = dateToShipBegin;
            soTrx.DateToShipEnd = dateToShipEnd;

            //Note. The purchase price is updated in the Fix.

            soTrx.MetaData.Comment = comment;

            return soTrx;
        }

        public string GetSoNameStringifyOf(ICollection<SalesOrderTrx> soCollection)
        {
            string str = string.Empty;

            if (!soCollection.IsNullOrEmpty())
            {
                foreach (var item in soCollection)
                {
                    string temp = string.Format("{0}, ", item.SalesOrder.DocNo);
                    str += temp;
                }

                str = str.Substring(0, str.Length - 2);
            }

            return str;
        }
    }
}
