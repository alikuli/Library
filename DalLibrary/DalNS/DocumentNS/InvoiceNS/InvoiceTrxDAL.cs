using System;
using AliKuli.Extentions;

using InterfacesLibrary.ProductNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

using ModelsClassLibrary.ModelsNS.ProductNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// In order for InvoiceTrx to work properly, we will need to have a saved SalesOrderHeader so that it has a
    /// SalesOrderId. We need that to be able to check for duplicate prodcuts.
    /// </summary>
    public class InvoiceTrxDAL : AbstractTrxDAL<InvoiceTrx>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public InvoiceTrxDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("InvoiceTrxDAL");

        }


        /// <summary>
        /// Does the following checks
        /// </summary>
        /// <param name="entity"></param>
        public override void ErrorCheck(InvoiceTrx entity)
        {
            entity.SelfErrorCheck();

            Check_InvoiceTrx_Passed_Is_Not_Null(entity);
            Check_This_Is_Not_A_Duplicate_Product_In_Sales_Order(entity);
            Check_If_Total_Quantity_Freshly_Calculated(entity);
            Check_If_Final_Sales_Price_Freshly_Calculated(entity);
            Check_If_Line_Total_Freshly_Calculated(entity);
            //Check_If_Is_Qty_Remaining_Freshly_Calculated(entity);
        }

        #region ErrorCheck Helpers

        //private void Check_If_Is_Qty_Remaining_Freshly_Calculated(InvoiceTrx entity)
        //{
        //    InvoiceTrx soTrx = entity as InvoiceTrx;
        //    if (!soTrx.IsQtyRemainingFreshlyCalculated)
        //        throw new Exception("The Qty Remaining NOT Freshly Calculated. InvoiceTrxDAL.Check_If_Is_Qty_Remaining_Freshly_Calculated.");
        //}


        private void Check_If_Line_Total_Freshly_Calculated(InvoiceTrx entity)
        {
            //InvoiceTrx soTrx = entity as InvoiceTrx;
            //if (!soTrx.IsLineTotalOrderedFreshlyCalculated)
            //    throw new Exception("The Line Total NOT Freshly Calculated. InvoiceTrxDAL.Check_If_Line_Total_Freshly_Calculated.");
        }


        private void Check_If_Final_Sales_Price_Freshly_Calculated(InvoiceTrx entity)
        {
            //InvoiceTrx soTrx = entity as InvoiceTrx;
            //if (!soTrx.IsFinalSalesPriceFreshlyCalculated)
            //    throw new Exception("The Final Sales Price NOT Freshly Calculated. InvoiceTrxDAL.Check_If_Final_Sales_Price_Freshly_Calculated.");

        }


        private void Check_If_Total_Quantity_Freshly_Calculated(InvoiceTrx entity)
        {
            //InvoiceTrx soTrx = entity as InvoiceTrx;
            //if (!soTrx.IsTotalQtyShippedFreshlyCalculated)
            //    throw new Exception("The Total Quantity NOT Freshly Calculated. InvoiceTrxDAL.Check_If_Total_Quantity_Freshly_Calculated.");

        }


        private void Check_This_Is_Not_A_Duplicate_Product_In_Sales_Order(InvoiceTrx entity)
        {
            //Not required because we get this check done through the unique name
        }

        private static void Check_InvoiceTrx_Passed_Is_Not_Null(InvoiceTrx entity)
        {
            if (entity == null)
                throw new Exception("No sales order Trx passed");
        }

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
        public InvoiceTrx AutoCreate(
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
            InvoiceTrx soTrx = Factory();

            //if null or empty... don't give a value.
            //if(!GuidHelper.IsNullOrEmpty(salesOrderId))
            //    soTrx.SalesOrderID = GuidHelper.GetGuid_NonNullable_ValueOfNullable(salesOrderId);

            //if null or empty... don't give a value.
            if (!(productId.IsNullOrEmpty()))
                soTrx.ProductID = productId.GetValueOrDefault();

            //soTrx.SalesOrder = salesOrder;
            soTrx.Product = (IProduct)product;
            soTrx.OrderedQty = qtyOrdered;
            //soTrx.QtyToShip = qtyToShip;
            soTrx.MetaData.Created.SetToTodaysDateStart();
            soTrx.SalePrice = productSalePrice;

            soTrx.DateToShipBegin = dateToShipBegin;
            soTrx.DateToShipEnd = dateToShipEnd;
            //Note. The purchase price is updated in the Fix.

            soTrx.MetaData.Comment = comment;

            return soTrx;
        }

        public InvoiceTrx CreateFrom(SalesOrderTrx soTrx)
        {
            InvoiceTrx iTrx = Factory();
            iTrx.LoadFrom(soTrx);

            iTrx.SalesOrderTrxs.Add(soTrx);

            return iTrx;
        }
    }
}
