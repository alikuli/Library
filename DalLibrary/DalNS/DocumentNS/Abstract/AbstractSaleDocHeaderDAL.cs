using System;
using AliKuli.Extentions;

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

using ModelsClassLibrary.ModelsNS.PlayersNS;

using UserModels.Models;

namespace DalLibrary.DalNS.DocumentNS
{
    /// <summary>
    /// This fixes the common items of T. Do not use this for adding, deleting and updating
    ///  <para>Fix_DeliveryMethod(entity);</para>
    ///  <para>Fix_InformTo(entity);</para>
    ///  <para>Fix_ConsignTo(entity);</para>
    ///  <para>Fix_ShipTo(entity);</para>
    ///  <para>Fix_Owner(entity);</para>
    ///  <para>Fix_PaymentTerms(entity);</para>
    ///  <para>Fix_PaymentMethod(entity);</para>
    ///  <para>Fix_DeliveryMethod(entity);</para>
    ///  <para>Fix_Salesman(entity);</para>
    ///  <para>Load_ConsignTo_Complex_Address_If_Empty(entity);</para>
    ///  <para>Load_Owner_Complex_Address_If_Empty(entity);</para>
    ///  <para>Load_ShipTo_Complex_Address_If_Empty(entity);</para>
    ///  <para>Load_InformTo_Complex_Address_If_Empty(entity);</para>
    /// </summary>
    public class AbstractSaleDocHeaderDAL<T> : AbstractDocHeaderDAL<T> where T : AbstractSaleDocumentHeader
    {
        public AbstractSaleDocHeaderDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("AbstractSaleDocHeaderDAL");

        }




        /// <summary>
        /// <para>Fix_DocNo</para>
        /// <para>Fix_DeliveryMethod</para>
        /// <para>Fix_InformTo</para>
        /// <para>Fix_ConsignTo</para>
        /// <para>Fix_ShipTo</para>
        /// <para>Fix_Owner</para>
        /// <para>Fix_PaymentTerms</para>
        /// <para>Fix_PaymentMethod</para>
        /// <para>Fix_DeliveryMethod</para>
        /// <para>Fix_Salesman</para>
        /// <para>Load_ConsignTo_Complex_Address_If_Empty</para>
        /// <para>Load_Owner_Complex_Address_If_Empty</para>
        /// <para>Load_ShipTo_Complex_Address_If_Empty</para>
        /// <para>Load_InformTo_Complex_Address_If_Empty</para>
        /// </summary>
        /// <param name="entity"></param>

        public override void Fix(T entity)
        {
            base.Fix(entity);

            Fix_DocNo(entity);//only if creating

            Fix_DeliveryMethod(entity);
            Fix_InformTo(entity);
            Fix_ConsignTo(entity);
            Fix_ShipTo(entity);
            Fix_Owner(entity);
            Fix_PaymentTerms(entity);
            Fix_PaymentMethod(entity);
            Fix_DeliveryMethod(entity);
            Fix_Salesman(entity);

            //Load_ConsignTo_Complex_Address_If_Empty(entity);
            //Load_Owner_Complex_Address_If_Empty(entity);
            //Load_ShipTo_Complex_Address_If_Empty(entity);
            //Load_InformTo_Complex_Address_If_Empty(entity);

            //we need to have the transactions fixed before we continue.
            //Fix_TransactionsOfEntity(entity);
            Fix_TotalSalePurchaseOrdered(entity);
            Fix_TotalSalePurchaseShip(entity);
            Fix_TotalDocAmount(entity);
            Fix_TotalMiscCharges(entity);
        }


        #region Fix Helpers


        public void Fix_Salesman(T entity)
        {
            //if (entity.Salesman == null)
            //{
            //    if ((entity.SalesmanId.IsNullOrEmpty()))
            //    {
            //        //throw new Exception("No Salesman found. This is required. SaleOrderDAL.Fix_Salesman");
            //        //Note, I am intentionally allowing null Salesman so than we can make empty sales orders.
            //        //We will check this before invoicing, we will not allow invoicing of blank Salesman Sales Orders

            //    }
            //    else
            //    {
            //        entity.Salesman = new SalesmanDAL(_db, _user).FindFor(entity.SalesmanId);
            //        entity.Salesman = GetSalesman(entity.SalesmanId);

            //        if (entity.Salesman == null)
            //        {
            //            throw new Exception("No Salesman found. This is required. SaleOrderDAL.Fix_Salesman");
            //        }
            //    }
            //}
            //else
            //{
            //    if ((entity.SalesmanId.IsNullOrEmpty()))
            //    {
            //        entity.SalesmanId = ((Salesman) entity.Salesman).Id;
            //    }

            //}
        }


        public void Fix_ShipTo(T entity)
        {
            //if (entity.ShipTo == null)
            //{
            //    if (entity.ShipToId.IsNullOrEmpty())
            //    {
            //        //                    throw new Exception("No Ship To Address. This is required. SaleOrderDAL.Fix_ShipTo");
            //        //dont do anything... no shipTo selected
            //    }
            //    else
            //    {
            //        entity.ShipToString = (AddressComplex) GetShipToAddress(entity.ShipToId);

            //        if (entity.ShipToString == null)
            //        {
            //            throw new Exception("No Ship To Address found. This is required. SaleOrderDAL.Fix_ShipTo");
            //        }
            //    }
            //}
            //else
            //{
            //    if (entity.ShipToId.IsNullOrEmpty())
            //    {
            //        entity.ShipToId = entity.ShipTo.Id;
            //    }

            //}
        }

        public void Fix_PaymentMethod(T entity)
        {
            if (entity.PaymentMethod == null)
            {
                if (entity.PaymentMethodId.IsNullOrEmpty())
                {
                    throw new Exception("No Payment method. This is required. SaleOrderDAL.Fix_PaymentMethod");
                }
                else
                {
                    entity.PaymentMethod = GetPaymentMethod(entity.PaymentMethodId);

                    if (entity.PaymentMethod == null)
                    {
                        throw new Exception("No Payment method found. This is required. SaleOrderDAL.Fix_PaymentMethod");
                    }
                }
            }
            else
            {
                if (entity.PaymentMethodId.IsNullOrEmpty())
                {
                    entity.PaymentMethodId = ((PaymentMethod)entity.PaymentMethod).Id;
                }

            }
        }

        public void Fix_ConsignTo(T entity)
        {
            //if (entity.ConsignTo == null)
            //{
            //    if (entity.ConsignToId.IsNullOrEmpty())
            //    {
            //        //throw new Exception("No Consign To found. This is required. SaleOrderDAL.Fix_ConsignTo");
            //        //Note, I am intentionally allowing null consignto so than we can make empty sales orders.
            //        //We will check this before invoicing, we will not allow invoicing of blank consignto Sales Orders
            //    }
            //    else
            //    {
            //        entity.ConsignTo = GetCustomer(entity.ConsignToId);

            //        if (entity.ConsignTo.IsNull())
            //        {
            //            throw new Exception("No Consign To found. This is required. SaleOrderDAL.Fix_ConsignTo");
            //        }
            //    }
            //}
            //else
            //{
            //    if (entity.ConsignToId.IsNullOrEmpty())
            //    {
            //        entity.ConsignToId = ((Customer) entity.ConsignTo).Id;
            //    }

            //}
        }

        public void Fix_InformTo(T entity)
        {


            //if (entity.InformToString.IsNull())
            //{
            //    if (entity.InformToId.IsNullOrEmpty())
            //    {
            //        //throw new Exception("No Inform To found. This is required. SaleOrderDAL.Fix_InformTo");
            //        //Note, I am intentionally allowing null inform to so than we can make empty sales orders.
            //        //We will check this before invoicing, we will not allow invoicing of blank InformTo Sales Orders
            //    }
            //    else
            //    {
            //        entity.InformToString = (AddressComplex) GetInformToAddress(entity.InformToId);

            //        if (entity.InformToString.IsNull())
            //        {
            //            throw new Exception("No Inform To found. This is required. SaleOrderDAL.Fix_InformTo");
            //        }
            //    }
            //}
            //else
            //{
            //    if (entity.InformToId.IsNullOrEmpty())
            //    {
            //        entity.InformToId = entity.InformToString.Id;
            //    }

            //}
        }

        public void Fix_DeliveryMethod(T entity)
        {
            //if (entity.DeliveryMethod == null)
            //{
            //    if (entity.DeliveryMethodId.IsNullOrEmpty())
            //    {
            //        throw new Exception("No delivery method. This is required. SaleOrderDAL.Fix_DeliveryMethod");
            //    }
            //    else
            //    {
            //        entity.DeliveryMethod = GetDeliveryMethod(entity.DeliveryMethodId);

            //        if (entity.DeliveryMethod.IsNull())
            //        {
            //            throw new Exception("No delivery method found. This is required. SaleOrderDAL.Fix_DeliveryMethod");
            //        }
            //    }
            //}
            //else
            //{
            //    if (entity.DeliveryMethodId.IsNullOrEmpty())
            //    {
            //        entity.DeliveryMethodId = ((DeliveryMethod)entity.DeliveryMethod).Id;
            //    }

            //}
        }
        public void Fix_Owner(T entity)
        {
            if (entity.Owner == null)
            {
                if (entity.OwnerId.IsNullOrEmpty())
                {
                    //throw new Exception("No Owner found. This is required. SaleOrderDAL.Fix_Owner");
                    //Note, I am intentionally allowing null Owner so than we can make empty sales orders.
                    //We will check this before invoicing, we will not allow invoicing of blank owner Sales Orders

                }
                else
                {
                    entity.Owner = new OwnerDAL(_db, _user).FindFor(entity.OwnerId);
                    entity.Owner = GetOwner(entity.OwnerId);

                    if (entity.Owner == null)
                    {
                        throw new Exception("No Owner found. This is required. SaleOrderDAL.Fix_Owner");
                    }
                }
            }
            else
            {
                if (entity.OwnerId.IsNullOrEmpty())
                {
                    entity.OwnerId = ((Owner)entity.Owner).Id;
                }

            }
        }

        private void Fix_TotalMiscCharges(T entity)
        {
            //var temp = entity.GetTotalMiscCharges();
        }

        private void Fix_TotalDocAmount(T entity)
        {
            //var temp = entity.TotalDocAmountOrderedGet;
        }


        private void Fix_TotalSalePurchaseOrdered(T entity)
        {
            //entity.TotalItemsSalePurchaseOrderedGetSet = GetTotalSalePurchaseAmountOrdered(entity);
        }

        private void Fix_TotalSalePurchaseShip(T entity)
        {
            //entity.TotalItemsSalePurchaseShipGetSet = GetTotalSalePurchaseAmountShipped(entity);
        }

        /// <summary>
        /// The GetNextDocNumber is overridden in the various derived classes
        /// </summary>
        /// <param name="entity"></param>
        private void Fix_DocNo(T entity)
        {
            if (isCreating)
            {
                entity.DocNo = GetNextDocNumber();
            }
        }

        public void Fix_PaymentTerms(T entity)
        {
            if (entity.PaymentTerm == null)
            {
                if (entity.PaymentTermId.IsNullOrEmpty())
                {
                    throw new Exception("No Payment Terms. This is required. SaleOrderDAL.Fix_PaymentTerms");
                }
                else
                {
                    entity.PaymentTerm = GetPaymentTerms(entity.PaymentTermId);

                    if (entity.PaymentTerm == null)
                    {
                        throw new Exception("No Payment Terms found. This is required. SaleOrderDAL.Fix_PaymentTerms");
                    }
                }
            }
            else
            {
                if (entity.PaymentTermId.IsNullOrEmpty())
                {
                    entity.PaymentTermId = ((PaymentTerm)entity.PaymentTerm).Id;
                }

            }
        }


        #endregion


        #region Get...


        public virtual decimal GetTotalSalePurchaseAmountOrdered(T entity)
        {
            throw new NotImplementedException(GetSelfMethodName());

        }
        public virtual decimal GetTotalSalePurchaseAmountShipped(T entity)
        {
            throw new NotImplementedException(GetSelfMethodName());

        }

        public virtual decimal GetTotalDocAmountOrdered(T entity)
        {
            throw new NotImplementedException(GetSelfMethodName());

        }
        public virtual decimal GetTotalDocAmountShippeded(T entity)
        {
            throw new NotImplementedException(GetSelfMethodName());

        }

        private PaymentMethod GetPaymentMethod(Guid? paymentMethodId)
        {
            if (paymentMethodId.IsNullOrEmpty())
                return null;

            return new PaymentMethodDAL(_db, _user).FindFor(paymentMethodId);
        }


        private PaymentTerm GetPaymentTerms(Guid? paymentTermsId)
        {
            if (paymentTermsId.IsNullOrEmpty())
                return null;

            return new PaymentTermDAL(_db, _user).FindFor(paymentTermsId);

        }

        private static Owner GetOwner(Guid? ownerId)
        {
            if (ownerId.IsNullOrEmpty())
                return null;


            return new OwnerDAL(_db, _user).FindFor(ownerId);
        }

        //public DeliveryMethod GetDeliveryMethod(Guid? deliverMethodId)
        //{
        //    if (deliverMethodId.IsNullOrEmpty())
        //        return null;

        //    return new DeliveryMethodDAL(_db, _user).FindFor(deliverMethodId);
        //}


        //private AddressComplex GetInformToAddress(Guid? informToId)
        //{
        //    if (informToId.IsNullOrEmpty())
        //        return null;

        //    return (IAddressComplex) ((IAddress)new AddressDAL(_db, _user).FindFor(informToId));
        //}


        //private static Customer GetCustomer(Guid? customerId)
        //{
        //    if (customerId.IsNullOrEmpty())
        //        return null;

        //    return new CustomerDAL(_db, _user).FindFor(customerId);
        //}


        //private AddressComplex GetShipToAddress(Guid? shipToId)
        //{
        //    if (shipToId.IsNullOrEmpty())
        //        return null;

        //    return (IAddressComplex) ((IAddress)new AddressDAL(_db, _user).FindFor(shipToId));
        //}

        //private Salesman GetSalesman(Guid? salesmanId)
        //{
        //    //if (GuidHelper.IsNullOrEmpty(salesmanId))
        //    //    return null;

        //    //return new SalesmanIdDAL(_db, _user).FindFor(paymentTermsId);
        //    throw new NotImplementedException("Not Implemented. SalesorderDAL.GetSalesman");

        //}


        #endregion


        #region Load....


        //private void Load_InformTo_Complex_Address_If_Empty(T entity)
        //{
        //    if (((Address) entity.InformToAddress).IsEmpty)
        //    {
        //        Load_InformTo_Complex_Address(entity);
        //    }
        //}
        //public static void Load_InformTo_Complex_Address(T entity)
        //{
        //    ((AddressComplex )entity.InformToAddress)
        //        .LoadFrom(entity.ConsignTo.User);
        //}

        //public void Load_ConsignTo_Complex_Address_If_Empty(T entity)
        //{

        //    if (((AddressComplex )entity.ConsignToAddress).IsEmpty)
        //    {
        //        Load_ConsignTo_Complex_Address(entity);
        //    }


        //}

        //public static void Load_ConsignTo_Complex_Address(T entity)
        //{
        //    if (!entity.ConsignTo.IsNull())
        //        ((AddressComplex ) entity.ConsignToAddress).LoadFrom(entity.ConsignTo.User);
        //}


        //public void Load_Owner_Complex_Address_If_Empty(T entity)
        //{
        //    if (((AddressComplex) entity.OwnersAddress).IsEmpty)
        //    {
        //        Load_Owner_Complex_Address(entity);
        //    }


        //}

        //public static void Load_Owner_Complex_Address(T entity)
        //{
        //    ((AddressComplex) entity.OwnersAddress).LoadFrom(entity.Owner.User);
        //}



        //private void Load_ShipTo_Complex_Address_If_Empty(T entity)
        //{
        //    if (((AddressComplex) entity.ShipToAddress).IsEmpty)
        //    {
        //        Load_ShipTo_Complex_Address(entity);
        //    }
        //}

        //public static void Load_ShipTo_Complex_Address(T entity)
        //{
        //    ((AddressComplex) entity.ShipToAddress).LoadFrom(entity.ConsignTo.User);
        //}

        #endregion


        /// <summary>
        /// <para>SelfErrorCheck</para>
        /// <para>Check_Customer_BlackListed</para>
        /// <para>Check_Owner_BlackListed</para>
        /// <para>Check_Salesman_BlackListed</para>
        /// </summary>
        /// <param name="entity"></param>
        public override void ErrorCheck(T entity)
        {
            base.ErrorCheck(entity);

            entity.Check_Customer_BlackListed();
            entity.Check_Owner_BlackListed();
            entity.Check_Salesman_BlackListed();
        }

        #region ErrorCheck Helpers

        public virtual void ErrorCheck_ConsignTo_ShipTo_InformTo_Cannot_Be_Empty_For_Sale()
        {
            Check_ConsignToAreNotNullOrEmpty();
            Check_ShipToAreNotNullOrEmpty();
            Check_InformToAreNotNullOrEmpty();

        }

        private void Check_InformToAreNotNullOrEmpty()
        {
            throw new NotImplementedException();
        }

        private void Check_ShipToAreNotNullOrEmpty()
        {
            throw new NotImplementedException();
        }

        private void Check_ConsignToAreNotNullOrEmpty()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}