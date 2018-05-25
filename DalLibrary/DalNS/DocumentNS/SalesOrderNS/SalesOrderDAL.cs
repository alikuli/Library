using System;
using System.Linq;
using AliKuli.Extentions;
using DalLibrary.DalNS.DocumentNS;

using UserModels.Models;
using EnumLibrary.EnumNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// Sales order number issued in Fix.
    /// </summary>
    public class SalesOrderDAL : AbstractSaleDocHeaderDAL<SalesOrder>
    {

        public SalesOrderDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }

        public override void Fix(SalesOrder entity)
        {
            //            Fix_Name(entity);

            base.Fix(entity);
            //Fix_Name(entity);


        }


        #region Fix Helpers

        //public override void Fix_TransactionsOfEntity(SalesOrder entity)
        //{
        //    //if the transactions exist... they too need to be fixed.
        //    if (!entity.SalesOrderTrxs.IsNullOrEmpty())
        //    {
        //        SalesOrderTrxDAL soTrxDAL = new SalesOrderTrxDAL(_db, _user);
        //        foreach (var item in entity.SalesOrderTrxs)
        //        {
        //            soTrxDAL.Fix(item);
        //        }
        //    }
        //}




        #endregion



        public override void ErrorCheck(SalesOrder entity)
        {
            base.ErrorCheck(entity);
            entity.SelfErrorCheck();
        }



        /// <summary>
        /// An owner cannot sell to himself. Business Rule.
        /// </summary>
        /// <param name="entity"></param>


        /// <summary>
        /// Note. When an entity is sent, either send the entity, or it's Id or both.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="consignTo"></param>
        /// <param name="consignToId"></param>
        /// <param name="deliveryMethod"></param>
        /// <param name="deliveryMethodId"></param>
        /// <param name="expectedDate"></param>
        /// <param name="informTo"></param>
        /// <param name="informToId"></param>
        /// <param name="miscPaymentAmount"></param>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        /// <param name="ownerId"></param>
        /// <param name="paymentMethod"></param>
        /// <param name="paymentMethodId"></param>
        /// <param name="paymentTerm"></param>
        /// <param name="paymentTermId"></param>
        /// <param name="saleOrderTypeENUM"></param>
        /// <param name="salesman"></param>
        /// <param name="salesmanId"></param>
        /// <param name="shippingAmount"></param>
        /// <param name="shipTo"></param>
        /// <param name="shipToId"></param>
        /// <param name="taxAmount"></param>
        /// <param name="theirPurchaseOrderNumber"></param>
        /// <returns></returns>
        public bool CreateAutomaticly(
            string comment,
            Customer consignTo,
            Guid? consignToId,
            DeliveryMethod deliveryMethod,
            Guid? deliveryMethodId,
            DateTime expectedDate,
            AddressWithId informTo,
            Guid? informToId,
            decimal miscPaymentAmount,
            Owner owner,
            Guid? ownerId,
            PaymentMethod paymentMethod,
            Guid? paymentMethodId,
            PaymentTerm paymentTerm,
            Guid? paymentTermId,
            SaleTypeEnum saleOrderTypeENUM,
            Salesman salesman,
            Guid? salesmanId,
            decimal shippingAmount,
            AddressWithId shipTo,
            Guid? shipToId,
            decimal taxAmount,
            string theirPurchaseOrderNumber)
        {
            //Sale is the default type
            //SalesOrder so = new SalesOrder(
            //    comment,
            //    consignTo,
            //    consignToId,
            //    deliveryMethod,
            //    deliveryMethodId,
            //    expectedDate,
            //    informTo,
            //    informToId,
            //    miscPaymentAmount,
            //    name,
            //    owner,
            //    ownerId,
            //    paymentMethod,
            //    paymentMethodId,
            //    paymentTerm,
            //    paymentTermId,
            //    saleOrderTypeENUM,
            //    salesman,
            //    salesmanId,
            //    shippingAmount,
            //    shipTo,
            //    shipToId,
            //    taxAmount,
            //    theirPurchaseOrderNumber);

            SalesOrder so = Factory();

            //Get parameter values
            so.MetaData.Comment = comment;
            so.ConsignTo = consignTo;
            so.ConsignToId = consignToId;
            so.DeliveryMethod = deliveryMethod;
            so.DeliveryMethodId = deliveryMethodId;
            //so.ExpectedDate = expectedDate;
            //so.InformToAddress = informTo.ToAddressComplex;
            so.InformToId = informToId;
            //so.MiscPaymentAmount = miscPaymentAmount;
            so.Owner = owner;

            if (!(ownerId.IsNullOrEmpty()))
                so.OwnerId = ownerId.GetValueOrDefault();

            so.PaymentMethod = paymentMethod;
            so.PaymentMethodId = paymentMethodId;
            so.PaymentTerm = paymentTerm;
            so.PaymentTermId = paymentTermId;
            so.SaleTypeENUM = saleOrderTypeENUM;
            so.Salesman = salesman;
            so.SalesmanId = salesmanId;
            so.MiscCharges.ShippingAmount = shippingAmount;
            //so.ShipToAddress = shipTo.ToAddressComplex;
            so.ShipToId = shipToId;
            so.MiscCharges.TaxAmount = taxAmount;
            so.TheirPurchaseOrderNumber = theirPurchaseOrderNumber;

            //load the consignToAddress and the OwnersAddress

            //so.ConsignToAddress = ((Address)consignTo.AddressFromUser).ToAddressComplex;
            //so.OwnersAddress = ((Address) owner.AddressFromUser).ToAddressComplex;

            Create(so);

            return true;

        }


        //public Invoice InvoiceThis(SalesOrder so)
        //{

        //    InvoiceDAL invoiceDAL = new InvoiceDAL(_db, _user);
        //    InvoiceTrxDAL invTrxDAL = new InvoiceTrxDAL(_db, _user);
        //    PaymentsAppliedInvoiceDAL paymentsAppliedinvoiceDAL = new PaymentsAppliedInvoiceDAL(_db, _user);

        //    Invoice i = invoiceDAL.Factory();
        //    i.LoadFrom(so); //This loads all the header information


        //    //now transactions.
        //    List<SalesOrderTrx> soTrxs = so.SalesOrderTrxs.ToList();

        //    decimal ttlMiscCharges = i.TotalMiscCharges.Amount;


        //    decimal ttlProductCharges = 0.00M;

        //    if(!soTrxs.IsNullOrEmpty())
        //    {
        //        foreach (var trx in soTrxs)
        //        {
        //            InvoiceTrx invTrx = invTrxDAL.Factory();
        //            invTrx.LoadFrom(trx);
        //            invTrx.Invoice = i;
        //            invTrx.InvoiceId = i.Id;

        //            //update the salesorderTrx to reflext this
        //            trx.InvoiceTrxs.Add(invTrx);

        //            //This gets the total value of the product being shipped.
        //            ttlProductCharges += invTrx.LineTotal_Money_Ship.Amount;
        //        }
        //    }

        //    decimal ttlInvoicedAmount = ttlMiscCharges + ttlProductCharges;
        //    decimal appliedAmountToInvoice = 0.00M;

        //    //Add the payments
        //    List<PaymentAppliedSalesOrder> allPaymentsInSo = so.PaymentsApplied.ToList();

        //    if (!allPaymentsInSo.IsNullOrEmpty())
        //    {

        //        foreach (var paymnt in allPaymentsInSo)
        //        {
        //            //Move the payment to the invoice up to total invoice amount
        //            //First condition: Less than or equal to Invoice Amount

        //            if (so.TotalPaymentAmount.Amount <= ttlInvoicedAmount)
        //            {
        //                //move the whole amount
        //                PaymentAppliedInvoice pai = paymentsAppliedinvoiceDAL.Factory();

        //                //update the navigations.
        //                pai.PaymentsAppliedFromSalesOrders.Add(paymnt);
        //                so.PaymentsApplied.Add(paymnt);

        //                paymnt.PaymentsAppliedToInvoices.Add(pai);
        //                i.PaymentsApplied.Add(pai);

        //            }
        //            else //Second condition: else... Greater than Invoice amount.
        //            {
        //                PaymentAppliedInvoice pai = paymentsAppliedinvoiceDAL.Factory();

        //                //update the navigations.
        //                pai.PaymentsAppliedFromSalesOrders.Add(paymnt);
        //                so.PaymentsApplied.Add(paymnt);

        //                paymnt.PaymentsAppliedToInvoices.Add(pai);
        //                i.PaymentsApplied.Add(pai);

        //            }
        //        }

        //    }
        //}

        public Invoice InvoiceThis(SalesOrder[] sos)
        {
            throw new NotImplementedException();
        }
        public IQueryable<SalesOrder> FindAllSoForCustomer(Guid? customerId)
        {
            if (customerId.IsNullOrEmpty())
                throw new Exception("No customer recieved. SalesOrderDAL.FindAllSoForCustomer");

            var allSalesOrders = SearchFor(x => x.InformToId == customerId)
                .ToList()
                .AsQueryable();

            return allSalesOrders;
        }

        #region Get...

        //Fix is run in AbstracDocHeaderDAL
        public override long GetNextDocNumber()
        {
            if (_db.SalesOrders.IsNullOrEmpty())
                return 0;
            return _db.SalesOrders.Max(x => x.DocNo) + 1;
        }





        public override decimal GetTotalDocAmountShippeded(SalesOrder entity)
        {
            return base.GetTotalDocAmountShippeded(entity);
        }


        //public override decimal GetTotalSalePurchaseAmountOrdered(SalesOrder entity)
        //{
        //    decimal totalSalePurchaseAmountOrdered = 0;

        //    if (entity.IsNull())
        //        throw new Exception("No sale order passed. SalesOrderDAL.GetTotalSalePurchaseAmount");

        //    if (entity.SalesOrderTrxs.IsNullOrEmpty())
        //    {
        //        entity.TotalItemsSalePurchaseOrderedGetSet = 0;
        //        return entity.TotalItemsSalePurchaseOrderedGetSet;
        //    }

        //    if (entity.IsTotalItemsSalePurchaseOrderedCalculated)
        //    {
        //        return entity.TotalItemsSalePurchaseOrderedGetSet;
        //    }

        //    foreach (var item in entity.SalesOrderTrxs.ToList())
        //    {
        //        totalSalePurchaseAmountOrdered += item.LineTotalOrderedCalculator;
        //    }

        //    entity.TotalItemsSalePurchaseOrderedGetSet = totalSalePurchaseAmountOrdered;
        //    return entity.TotalItemsSalePurchaseOrderedGetSet;

        //}

        //public override decimal GetTotalSalePurchaseAmountShipped(SalesOrder entity)
        //{
        //    decimal totalSalePurchaseAmountShipped = 0;

        //    if (entity.IsNull())
        //        throw new Exception("No sale order passed. SalesOrderDAL.GetTotalSalePurchaseAmount");

        //    if (entity.SalesOrderTrxs.IsNullOrEmpty())
        //    {
        //        entity.TotalItemsSalePurchaseOrderedGetSet = 0;
        //        return entity.TotalItemsSalePurchaseOrderedGetSet;
        //    }

        //    if (entity.IsTotalItemsSalePurchaseOrderedCalculated)
        //    {
        //        return entity.TotalItemsSalePurchaseOrderedGetSet;
        //    }

        //    foreach (var item in entity.SalesOrderTrxs.ToList())
        //    {
        //        totalSalePurchaseAmountShipped += item.LineTotalShippedCalculator;
        //    }

        //    entity.TotalItemsSalePurchaseOrderedGetSet = totalSalePurchaseAmountShipped;
        //    return entity.TotalItemsSalePurchaseOrderedGetSet;

        //}


        //public override decimal GetTotalDocAmountOrdered(SalesOrder entity)
        //{
        //    if (!entity.IsTotalItemsSalePurchaseOrderedCalculated)
        //    {
        //        entity.TotalItemsSalePurchaseOrderedGetSet = GetTotalSalePurchaseAmountOrdered(entity);

        //    }
        //    return entity.TotalDocAmountOrderedGet;

        //}

        #endregion
    }
}
