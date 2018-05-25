using System;
using AliKuli.Extentions;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.ProductNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;
using UserModels.Models;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// In order for SalesOrderTrx to work properly, we will need to have a saved SalesOrderHeader so that it has a
    /// SalesOrderId. We need that to be able to check for duplicate prodcuts.
    /// </summary>
    public class AbstractTrxDAL<T> : Repositry<T> where T : AbstractDocumentTrx
    {


        public AbstractTrxDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
           Errors.ResetLibAndClass("AbstractTrxDAL");

        }


        /// <summary>
        /// This does a selfError check on the class and runs base class checks
        /// </summary>
        public override void ErrorCheck(T entity)
        {

            AbstractDocumentTrx trx = entity as AbstractDocumentTrx;
            trx.SelfErrorCheck();

            Check_Trx_Passed_Is_Not_Null(trx);
            //Check_IsTotalQtyShippedFreshlyCalculated_Freshly_Calculated(trx);
            //Check_IsFinalSalesPriceFreshlyCalculated_Freshly_Calculated(trx);
            //Check_IsLineTotalFreshlyCalculated_Freshly_Calculated(trx);


        }

        #region ErrorCheck Helpers

        /// <summary>
        /// This ensures that  the AbstractTrx passed is not null. This is used as a check for other methods.
        /// </summary>
        private static void Check_Trx_Passed_Is_Not_Null(AbstractDocumentTrx entity)
        {
            if (entity == null)
                throw new Exception("No  Trx passed. AbstractTrxDAL.Check_SalesorderTrx_Passed_Is_Not_Null");
        }

        #endregion

        /// <summary>
        /// This fixes the following:
        ///    Fix_Name
        ///    Fix_Product
        ///    FixDescription
        ///    Fix_CalculatedValues
        /// </summary>
        /// <param name="entity"></param>
        public override void Fix(T entity)
        {
            Fix_Product(entity);

            base.Fix(entity);
            Fix_Description(entity);

        }





        #region Fix Helpers


        /// <summary>
        /// This loads the products listed price.
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_ListedSalePrice(InvoiceTrx entity)
        {
            if (isCreating)
                entity.ListedPrice = entity.GetProduct_ListedSellPrice();
            //if product is updated you will need to change the price manually
        }


        /// <summary>
        /// This adds the product name from the product to the description
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_Description(T entity)
        {
            IAbstractDocumentTrx a = entity as AbstractDocumentTrx;
            entity.Description = a.GetProductNameFromProduct();
        }


        /// <summary>
        /// This ensure that the Product and ProductId have values
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_Product(T entity)
        {
            if (entity.Product.IsNull())
            {
                if (entity.ProductID.IsNullOrEmpty())
                {
                    throw new Exception("The product is null. Required. AbstractTrxDAL.Fix_Product");
                }
                else
                {
                    entity.Product = new ProductDAL(_db, _user).FindFor(entity.ProductID) as IProduct;

                    if (entity.Product.IsNull())
                    {
                        throw new Exception("The product not found in Db. Try Again. AbstractTrxDAL.Fix_Product");

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



        #endregion


        #region Get...

        /// <summary>
        /// This is overridden in the derived classes. Salesorder will need to count how much has been
        /// shipped in child invoice trx, while invoice trx will get the total qty shipped.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual decimal GetTotalQtyShipped(T item)
        {
            //This gets the total quantity shipped from invoice trxs
            throw new NotImplementedException();
        }

        #endregion



    }
}
