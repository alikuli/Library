using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AliKuli.Extentions;
using DalLibrary.DalNS.DocumentNS;

using EnumLibrary.EnumNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class InvoiceDAL : AbstractSaleDocHeaderDAL<Invoice>
    {


        #region Constructors
        public InvoiceDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
              Errors.ResetLibAndClass("InvoiceDAL");

        }



        #endregion



        public override void Fix(Invoice entity)
        {
            base.Fix(entity);

            //Fix_SalesOrder(entity); This is a list
            Fix_AliasDocNo(entity); //Only in Invoice
            Fix_HasAlias(entity);   //Only in Invoice
            Fix_OrderDate(entity);
        }

        /// <summary>
        /// If the Invoice Number and Alias are not the same... then Invoice has an alias
        /// </summary>
        /// <param name="entity"></param>


        #region Fix Helpers

        //public override void Fix_TransactionsOfEntity(Invoice  entity)
        //{
        //    //if the transactions exist... they too need to be fixed.
        //    if (!entity.InvoiceTrxs.IsNullOrEmpty())
        //    {
        //        InvoiceTrxDAL invTrxDAL = new InvoiceTrxDAL(_db, _user);
        //        foreach (var item in entity.InvoiceTrxs)
        //        {
        //            invTrxDAL.Fix(item);
        //        }
        //    }
        //}

        private void Fix_HasAlias(Invoice entity)
        {
            entity.HasAlias = entity.DocNo.ToString().Trim() != entity.DocAliasID.Trim();
        }


        //private void Fix_TotalAmountForDoc(Invoice entity)
        //{
        //    throw new System.NotImplementedException();
        //}


        private void Fix_OrderDate(Invoice entity)
        {
            //only do this during creation.
            if (!isCreating)
                return;

            if (entity.SalesOrders.IsNullOrEmpty())
            {
                return;
            }


            StringBuilder sb = new StringBuilder();
            foreach (var item in entity.SalesOrders)
            {
                string dateString = string.Format("{0}-{1:}; ", item.DocNo, item.Date.ToString("MMMM dd, yyyy"));
                sb.Append(dateString);
            }

            entity.OrderDateAndNumber = sb.ToString();


        }

        /// <summary>
        /// This only works if AliasDocNo is null or empty.
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_AliasDocNo(Invoice entity)
        {
            if (entity.DocAliasID.IsNullOrEmpty())
            {
                entity.DocAliasID = entity.DocNo.ToString();
            }



        }





        #endregion

        #region Get...

        public override long GetNextDocNumber()
        {
            return _db.Invoices.Max(x => x.DocNo) + 1;
        }
        //public override long Get()
        //{
        //    return _db.Invoices.Max(x => x.InvoiceNo) + 1;
        //}

        #endregion


        public override void ErrorCheck(Invoice entity)
        {
            base.ErrorCheck(entity);

            entity.SelfErrorCheck();
            Check_For_Duplicate_Alias(entity);


        }

        #region Error Check Helpers
        private void Check_For_Duplicate_Alias(Invoice entity)
        {

            var duplicatAlias = FindForAlias(entity.DocAliasID, entity.Id);

            if (duplicatAlias != null)
                throw new Exception(string.Format("This alias already exists for Invoice No: {0}, Alias {1}. Try another. InvoiceDAL.FindForAlias",
                    entity.FullName(),
                    entity.DocAliasID));

        }


        public Invoice FindForAlias(string alias, Guid? exceptThisInvoiceId)
        {
            if (alias.IsNullOrEmpty())
                throw new Exception("No alias passed. InvoiceDAL.FindForAlias");

            if (exceptThisInvoiceId == null)
            {
                var invoice = SearchFor(x => x.DocAliasID.ToLower() == alias.ToLower())
                    .FirstOrDefault();
                return invoice;

            }
            else
            {
                var invoice = SearchFor(x => x.DocAliasID.ToLower() == alias.ToLower() && x.Id != exceptThisInvoiceId)
                    .FirstOrDefault();
                return invoice;

            }
        }

        #endregion

        #region Overrides

        public override decimal GetTotalDocAmountOrdered(Invoice entity)
        {
            return base.GetTotalDocAmountOrdered(entity);
        }

        public override decimal GetTotalDocAmountShippeded(Invoice entity)
        {
            return base.GetTotalDocAmountShippeded(entity);
        }

        public override decimal GetTotalSalePurchaseAmountOrdered(Invoice entity)
        {
            return base.GetTotalSalePurchaseAmountOrdered(entity);
        }

        public override decimal GetTotalSalePurchaseAmountShipped(Invoice entity)
        {
            return base.GetTotalSalePurchaseAmountShipped(entity);
        }

        #endregion

        public Invoice CreateInvoiceFromManySalesOrders(List<SalesOrder> so)
        {
            if (!so.IsNullOrEmpty())
            {
                //Get all the totals of the products in case there are duplicates
                List<SalesOrderTrx> lstSalesOrderTrx = new List<SalesOrderTrx>();
                foreach (var item in so)
                {
                    if (item.SalesOrderTrxs.IsNullOrEmpty())
                    {
                        foreach (var salesOrderTrx in item.SalesOrderTrxs)
                        {
                            lstSalesOrderTrx.Add(salesOrderTrx);
                        }
                    }
                }

                //now lstSalesOrderTrx contains all the SalesOrderTrx of All the sales orders

                var lstSoProductsSummed = (from s in lstSalesOrderTrx
                                           group s by new { s.ProductID, s.FinalSalesPrice } into strGrp
                                           select new
                                           {
                                               Key = strGrp.Key,
                                               TotalOrdered = strGrp.Sum(x => x.OrderedQty),
                                               TotalToShip = strGrp.Sum(x => x.ShipQty),
                                               FinalSalesPrice = strGrp.Key.FinalSalesPrice
                                           })
                                                            .ToList();

                if (!lstSoProductsSummed.IsNullOrEmpty())
                {
                    InvoiceTrxDAL invTrxDAL = new InvoiceTrxDAL(_db, _user);
                    SalesOrderTrxDAL soTrxDAL = new SalesOrderTrxDAL(_db, _user);

                    foreach (var item in lstSoProductsSummed)
                    {
                        InvoiceTrx i = invTrxDAL.Factory();
                        i.ProductID = item.Key.ProductID;

                        //Adds only those trxs where the product is the same.
                        i.SalesOrderTrxs = lstSalesOrderTrx.Where(x => x.ProductID == i.ProductID).ToList();
                        i.MetaData.Comment = "Summed " + soTrxDAL.GetSoNameStringifyOf(i.SalesOrderTrxs);
                        i.FinalSalesPrice = item.FinalSalesPrice;
                        i.OrderedQty = item.TotalOrdered;
                        //i.ShippedQty = item.TotalToShip;

                    }
                }

                //Now we have the totals for each product. Now make 1 Salesorder Trxs got each and add 
                //the So Trx that contains the product




            }
            return null;
        }
        public Invoice CreateInvoiceFromSalesOrder(SalesOrder so)
        {
            if (so.SaleTypeENUM != SaleTypeEnum.Unknown || so.SaleTypeENUM == SaleTypeEnum.Quotation)
            {
                Invoice i = Factory();
                i.LoadFrom(so);
                i.SalesOrders.Add(so);


                if (!so.SalesOrderTrxs.IsNullOrEmpty())
                {
                    InvoiceTrxDAL iTrxDAL = new InvoiceTrxDAL(_db, _user);
                    foreach (var item in so.SalesOrderTrxs)
                    {
                        InvoiceTrx iTrx = iTrxDAL.CreateFrom(item);

                        iTrx.SalesOrderTrxs.Add(item);
                        i.InvoiceTrxs.Add(iTrx);
                    }
                }
                return i;
            }

            return null;
        }



    }
}
