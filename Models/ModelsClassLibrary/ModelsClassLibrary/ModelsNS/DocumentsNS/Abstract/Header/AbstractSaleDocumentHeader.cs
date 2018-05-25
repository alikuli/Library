using System;
using System.ComponentModel.DataAnnotations;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    public abstract class AbstractSaleDocumentHeader : AbstractDocHeader, IAbstractSaleDocumentHeader
    {

        #region constructors
        public AbstractSaleDocumentHeader()
        {
            ConsignToAddress = new AddressComplex();
            ShipToAddress = new AddressComplex();
            InformToAddress = new AddressComplex();
            OwnersAddress = new AddressComplex();
            Date = DateTime.UtcNow;
            SaleTypeENUM = SaleTypeEnum.Unknown;

            //Initialize
            TotalItems_Ordered_MoneyAmount = new CounterClass();
            TotalItems_Ship_MoneyAmount = new CounterClass();
            TotalDoc_Ordered_MoneyAmount = new CounterClass();
            TotalDoc_Shipped_MoneyAmount = new CounterClass();
            TotalMiscCharges = new CounterClass();


            //Assign delegate
            TotalItems_Ordered_MoneyAmount.Calculator(Calculator_TotalItems_Ordered_MoneyAmount);
            TotalItems_Ship_MoneyAmount.Calculator(Calculator_TotalItems_Ship_MoneyAmount);

            TotalDoc_Ordered_MoneyAmount.Calculator(Calculator_TotalDoc_Ordered_MoneyAmount);
            TotalDoc_Shipped_MoneyAmount.Calculator(Calculator_TotalDoc_Shipped_MoneyAmount);

            TotalMiscCharges.Calculator(Calculator_TotalMiscCharges);

        }


        public AbstractSaleDocumentHeader(AbstractSaleDocumentHeader a)
            : this()
        {
            LoadFrom(a);
        }




        #endregion



        /// <summary>
        /// This tracks if it is an entered SO or a created one for back orders. If it is a FRESH sale order then it will say SaleOrder, otherwise it will be BackOrder
        /// </summary>
        public SaleTypeEnum SaleTypeENUM { get; set; }



        #region Owner FK
        /// <summary>
        /// Only sellers can issue invoices or orders in their name, therefore we will only sellers will be stored. These are different from the salesmen who sell and get commission.
        /// Sellers will be the OWNER of the goods, whereas the salesmen will be selling on behalf of someone and getting commission.
        /// </summary>
        [Display(Name = "Issued By")]
        public Guid OwnerId { get; set; }
        public virtual IOwner Owner { get; set; }

        public virtual AddressComplex OwnersAddress { get; set; }


        //=====================================================================================================
        #endregion
        #region ConsignTo FK
        /// <summary>
        /// In a contract of carriage, the consignee is the person to whom the shipment is to be delivered whether by land, 
        /// sea or air. If a sender dispatches an item to a receiver via a delivery service, the sender is the consignor 
        /// and the recipient is the consignee.
        /// </summary>
        [Display(Name = "Consign To")]
        public Guid? ConsignToId { get; set; }
        /// <summary>
        /// This is the Consign To i.e. customer
        /// </summary>
        public virtual ICustomer ConsignTo { get; set; }
        //=====================================================================================================

        /// <summary>
        /// This holds the string version of the address. This is the version that prints.
        /// </summary>
        public virtual AddressComplex ConsignToAddress { get; set; }

        #endregion
        #region ShipTo FK
        /// <summary>
        /// In a contract of carriage, the consignee is the person to whom the shipment is to be delivered whether by land, 
        /// sea or air. If a sender dispatches an item to a receiver via a delivery service, the sender is the consignor 
        /// and the recipient is the consignee.
        /// </summary>
        [Display(Name = "Ship To")]
        public Guid? ShipToId { get; set; }
        public IAddressWithId ShipTo { get; set; }

        public virtual AddressComplex ShipToString { get; set; }
        [Display(Name = "Ship To")]
        public virtual AddressComplex ShipToAddress { get; set; }

        #endregion
        #region InformTO FK
        [Display(Name = "Inform To")]
        public Guid? InformToId { get; set; }
        public IAddressWithId InformTo { get; set; }

        public virtual AddressComplex InformToString { get; set; }

        public virtual AddressComplex InformToAddress { get; set; }
        #endregion

        #region Salesman FK
        /// <summary>
        /// This person is the one who made the sale or purchase. Possibly we will give commissions.
        /// </summary>
        [Display(Name = "Salesman")]
        public Guid? SalesmanId { get; set; }
        public virtual ISalesman Salesman { get; set; }

        public bool SalesmanExists
        {
            get
            {
                //I am doing this just in case there is an error and one item is true but other is null.
                if (!(SalesmanId.IsNullOrEmpty()))
                {
                    return true;
                }

                if (Salesman != null)
                {
                    return true;
                }

                return false; ;
            }
        }


        #endregion

        #region Payment and Delivery Methods and Terms
        /// <summary>
        /// Method of Delivery. Eg TCS, UPS etc
        /// </summary>
        [Display(Name = "Delivery Method")]
        public Guid? DeliveryMethodId { get; set; }
        public virtual IDeliveryMethod DeliveryMethod { get; set; }



        /// <summary>
        /// This is the method of payment
        /// </summary>
        [Display(Name = "Payment Method")]
        public Guid? PaymentMethodId { get; set; }
        public virtual IPaymentMethod PaymentMethod { get; set; }



        /// <summary>
        /// This is the payment terms i.e. 30 days etc
        /// </summary>
        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public virtual IPaymentTerm PaymentTerm { get; set; }

        #endregion


        [Display(Name = "Their P.O. No.")]
        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
        public string TheirPurchaseOrderNumber { get; set; }

        public MiscChargesAndPayments MiscCharges { get; set; }



        #region CounterClass

        /// <summary>
        /// This holds the total value of the sale/purchase items only. Misc costs are not added eg. shipping
        /// </summary>
        /// 
        public CounterClass TotalItems_Ordered_MoneyAmount { get; set; }
        public CounterClass TotalItems_Ship_MoneyAmount { get; set; }

        public CounterClass TotalDoc_Ordered_MoneyAmount { get; set; }
        /// <summary>
        /// This is the total doc shipped for the document. This is updated through a delegate Calculator_TotalDoc_Ordered_MoneyAmount which is assigned in the constructor. The controller class has a IsCalculated field which is updated once the property is updated.
        /// </summary>
        public CounterClass TotalDoc_Shipped_MoneyAmount { get; set; }

        public CounterClass TotalMiscCharges { get; set; }


        #endregion

        #region delegates for counter class


        protected decimal Calculator_TotalMiscCharges()
        {
            return MiscCharges.TotalMiscCharges;
        }

        /// <summary>
        /// This is the total sale amount calculated from the Ship To. It is equal to or less than the total doc sale amount for orders
        /// </summary>
        protected virtual decimal Calculator_TotalDoc_Shipped_MoneyAmount()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is the total doc sale amount if everything as ordered was delivered.
        /// </summary>
        protected virtual decimal Calculator_TotalDoc_Ordered_MoneyAmount()
        {
            throw new NotImplementedException();
        }

        protected virtual decimal Calculator_TotalItems_Ship_MoneyAmount()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is the value of the total items ordered ... this does not include Misc Costs and is calculated using the total ordered amount.
        /// </summary>
        protected virtual decimal Calculator_TotalItems_Ordered_MoneyAmount()
        {
            throw new NotImplementedException();
        }

        #endregion




        #region SelfErrorCheck
        /// <summary>
        /// Checks the following
        /// <para>Check_Doc_Number_Not_Zero</para>
        /// <para>Check_DeliveryMethod</para>
        /// <para>Check_Owner</para>
        /// <para>Check_PaymentMethod</para>
        /// <para>Check_PaymentTerms</para>
        /// <para>Check_Date</para>
        /// <para>Check_ExpectedDate</para>
        /// <para>Check_SalesMan</para>
        /// <para>Check_Owner_AllowedToShip</para>
        /// <para>Check_Customer_AllowedToShip</para>
        /// <para>Check_Salesman_AllowedToShip</para>
        /// <para>Check_Owner_Cannot_sell_to_Self</para>
        /// <para>Check_Total_Misc_Amount_Calculated</para>
        /// <para>Check_TotalSale_Purchase_Amount_Calculated</para>
        /// <para>Check_Total_Amount_For_Document_Calculated</para>
        /// <para>ErrorCheck_ConsignTo_ShipTo_InformTo_Cannot_Be_Empty_For_Sale (override)</para>
        /// </summary>
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_ConsignTo();
            Check_DeliveryMethod();
            Check_InformTo();
            Check_Owner();
            Check_PaymentMethod();
            Check_PaymentTerms();
            Check_SalesMan();
            Check_SaleTypeEnumIsNotUnknown();
            Check_ShipTo();
            Check_SalesTypeENUM();

            //This has to be here. Blacklisted and suspeneded people have
            //no rights!
            //Check_AllowedToSell();

            Check_TotalDoc_Ordered_MoneyAmount();
            Check_TotalDoc_Shipped_MoneyAmount();
            Check_TotalItems_Ordered_MoneyAmount();
            Check_TotalItems_Shipped_MoneyAmount();
            Check_TotalMiscCharges();

        }






        #region SelfErrorCheck Helpers

        private void Check_SalesTypeENUM()
        {
            if (SaleTypeENUM == SaleTypeEnum.Unknown)
                throw new Exception("Sales Type not set. AbstractDocumentHeader.Check_SalesTypeENUM");
        }

        private void Check_SaleTypeEnumIsNotUnknown()
        {
            if (SaleTypeENUM == SaleTypeEnum.Unknown)
                throw new Exception("saleTypeEnum is unknown. AbstractDocumentHeader.LoadFrom");
        }

        //--- null checks
        public void Check_ShipTo()
        {
            if (ShipToId.IsNull())
                throw new Exception("Ship To Id has no value. AbstractDocumentHeader.ErrorCheck");

            if (ShipToString.IsNull())
                throw new Exception("Ship To has no value. AbstractDocumentHeader.ErrorCheck");

            if (ShipToAddress.IsNull())
                throw new Exception("Ship To printable address has no value. AbstractDocumentHeader.Check_ConsignTo");
        }
        public void Check_InformTo()
        {


            if (InformToId.IsNullOrEmpty())
                throw new Exception("Inform To Id has no value. AbstractDocumentHeader.ErrorCheck");

            if (InformToString.IsNull())
                throw new Exception("Inform To has no value. AbstractDocumentHeader.ErrorCheck");

            if (InformToAddress.IsNull())
                throw new Exception("Inform To printable address has no value. AbstractDocumentHeader.Check_InformTo");


        }

        public void Check_ConsignTo()
        {
            if (ConsignToId.IsNullOrEmpty())
                throw new Exception("Consign To Id has no value. AbstractDocumentHeader.Check_ConsignTo");

            if (ConsignTo.IsNull())
                throw new Exception("Consign To has no value. AbstractDocumentHeader.Check_ConsignTo");

            if (ConsignToAddress.IsNull())
                throw new Exception("Consign To printable address has no value. AbstractDocumentHeader.Check_ConsignTo");

        }

        private void Check_Owner()
        {
            if (OwnerId.IsNullOrEmpty())
                throw new Exception("Owner Id has no value. AbstractDocumentHeader.ErrorCheck");

            if (Owner.IsNull())
                throw new Exception("Owner has no value. AbstractDocumentHeader.ErrorCheck");

            if (OwnersAddress.IsNull())
                throw new Exception("Owner's printable address has no value. AbstractDocumentHeader.Check_ConsignTo");

        }

        private void Check_SalesMan()
        {
            //check to see salesman exists....
            if (SalesmanExists)
            {

                if (SalesmanId.IsNullOrEmpty())
                {
                    throw new Exception("Salesman Id has no value. AbstractDocumentHeader.Check_SalesMan");

                }

                if (Salesman.IsNull())
                {
                    throw new Exception("Salesman has no value. AbstractDocumentHeader.Check_SalesMan");
                }
            }

        }

        private void Check_PaymentTerms()
        {
            if (PaymentTermId.IsNullOrEmpty())
                throw new Exception("Payment Terms Id has no value. AbstractDocumentHeader.ErrorCheck");

            if (PaymentTerm.IsNull())
                throw new Exception("Payment Terms has no value. AbstractDocumentHeader.ErrorCheck");
        }

        private void Check_PaymentMethod()
        {
            if (PaymentMethodId.IsNullOrEmpty())
                throw new Exception("Payment Method Id has no value. AbstractDocumentHeader.ErrorCheck");

            if (PaymentMethod.IsNull())
                throw new Exception("Payment Method has no value. AbstractDocumentHeader.ErrorCheck");
        }

        /// <summary>
        /// This checks the deliver method, both the class and the Id
        /// </summary>
        private void Check_DeliveryMethod()
        {
            if (DeliveryMethodId.IsNullOrEmpty())
                throw new Exception("Delivery Method Id has no value. AbstractDocumentHeader.ErrorCheck");

            if (DeliveryMethod.IsNull())
                throw new Exception("Delivery Method has no value. AbstractDocumentHeader.ErrorCheck");
        }



        public void Check_Salesman_AllowedToShip()
        {
            if (SalesmanExists)
            {
                if (!((Salesman)Salesman).IsAllowedToShip)
                {
                    throw new Exception(string.Format("Salesman '{0}' is not allowed to ship. Either he is suspended or blacklisted. Please check with the administration", ((Salesman)Salesman).FullName()));
                }
            }
        }



        private void Check_Owner_Cannot_sell_to_Self()
        {

            if (ConsignTo == null)
            {
                //do nothing.
            }
            else
            {
                if (ConsignTo.UserId == Owner.UserId)
                {
                    throw new Exception(string.Format("Owner and customer belong to the same user. You cannot sell to self. Owner user: {0}, Consign To User: {1}",
                        ConsignTo.UserId,
                        Owner.UserId));
                }

            }
        }






        private void Check_TotalDoc_Ordered_MoneyAmount()
        {
            if (!TotalDoc_Ordered_MoneyAmount.IsCalculated)
                throw new Exception(string.Format("Total amount (ordered) for the document: '{0}'not calculated", FullName()));

        }
        private void Check_TotalDoc_Shipped_MoneyAmount()
        {
            if (!TotalDoc_Shipped_MoneyAmount.IsCalculated)
                throw new Exception(string.Format("Total amount (shipped) for the document: '{0}'not calculated", FullName()));

        }

        private void Check_TotalMiscCharges()
        {
            if (!TotalMiscCharges.IsCalculated)
                throw new Exception("Total Misc Amount not calculated. AbstractDocumentHeader.Check_Total_Misc_Amount_Calculated");

        }


        private void Check_TotalItems_Ordered_MoneyAmount()
        {
            if (!TotalItems_Ordered_MoneyAmount.IsCalculated)
                throw new Exception("Total items (ordered) Money Amount not calculated. AbstractDocumentHeader.Check_Total_Misc_Amount_Calculated");
        }

        private void Check_TotalItems_Shipped_MoneyAmount()
        {
            if (!TotalItems_Ship_MoneyAmount.IsCalculated)
                throw new Exception("Total items (shipped) Money Amount not calculated. AbstractDocumentHeader.Check_Total_Misc_Amount_Calculated");
        }

        /// <summary>
        /// This is the document date.
        /// <para>This cannot be</para>
        /// <para>   Min Value</para>
        /// <para>   Max Value</para>
        /// <para>   in the future</para>
        /// <para>   too far in the past</para>
        /// </summary>
        //private  void Check_Date()
        //{
        //    if (Date.IsMinOrMax())
        //    {
        //        throw new Exception("Date not set. AbstractDocumentHeader.ErrorCheck");

        //    }
        //    if (IsDateInTheFuture())
        //    {
        //        throw new Exception("Date is in the future. Not allowed. AbstractDocumentHeader.ErrorCheck");

        //    }

        //    DateTime earliestDateAllowed = DateTime.UtcNow.Date.AddDays(-3);
        //    if (IsDateLessThanEarliestDateAllowed(earliestDateAllowed))
        //    {
        //        throw new Exception(string.Format("Date '{0}' is too far in the past. Not allowed. Earliest allowed is '{1}'AbstractDocumentHeader.ErrorCheck", Date.Date, earliestDateAllowed));

        //    }


        //}

        private bool IsDateLessThanEarliestDateAllowed(DateTime earliestDateAllowed)
        {
            return Date.Date < earliestDateAllowed;
        }

        private bool IsDateInTheFuture()
        {
            return Date.Date > DateTime.UtcNow.Date;
        }



        public void Check_Salesman_BlackListed()
        {
            if (((Salesman)Salesman).User.IsBlackListed)
            {
                throw new Exception(string.Format("Salesman is black listed. User: '{0}. AbstractDocumentHeader.Check_Salesman_BlackListed", Salesman.User.PersonInfo.PersonFullName()));
            }
        }

        public void Check_Customer_BlackListed()
        {
            if (Owner.User.IsBlackListed)
            {
                throw new Exception(string.Format("Owner is black listed. User: '{0}. AbstractDocumentHeader.Check_Customer_BlackListed", Owner.User.PersonInfo.PersonFullName()));
            }
        }

        public void Check_Owner_BlackListed()
        {
            if (ConsignTo.User.IsBlackListed)
            {
                throw new Exception(string.Format("Consign To is black listed. User: '{0}. AbstractDocumentHeader.Check_Owner_BlackListed", ConsignTo.User.PersonInfo.PersonFullName()));
            }
        }

        #endregion

        #endregion


        #region Is...




        //private bool IsExpectedDateLessThanOrderDate()
        //{
        //    return ExpectedDate.Date < Date.Date;
        //}


        //public bool IsComplexAddressEmpty(IAddress a)
        //{
        //    bool isEmpty = true;

        //    if (!a.Address2.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    if (!a.Attention.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    //if (!a.CityName.IsNullOrEmpty())
        //    //{
        //    //    isEmpty = false;
        //    //}

        //    //if (!a.ContactPhone.IsNullOrEmpty())
        //    //{
        //    //    isEmpty = false;
        //    //}

        //    //if (!a.CountryName.IsNullOrEmpty())
        //    //{
        //    //    isEmpty = false;
        //    //}

        //    if (!a.Email.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    if (!a.HouseNo.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    if (!a.Road.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    //if (!a.StateName.IsNullOrEmpty())
        //    //{
        //    //    isEmpty = false;
        //    //}

        //    //if (!a.TownName.IsNullOrEmpty())
        //    //{
        //    //    isEmpty = false;
        //    //}
        //    if (!a.WebAddress.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    if (!a.Zip.IsNullOrEmpty())
        //    {
        //        isEmpty = false;
        //    }

        //    return isEmpty;
        //}

        #endregion


        #region Loaders


        public void LoadFrom(AbstractSaleDocumentHeader a)
        {

            base.LoadFrom(a as AbstractDocHeader);

            ConsignTo = a.ConsignTo;
            ConsignToAddress = a.ConsignToAddress;
            ConsignToId = a.ConsignToId;
            DeliveryMethod = a.DeliveryMethod;
            DeliveryMethodId = a.DeliveryMethodId;
            InformToString = a.InformToString;
            InformToAddress = a.InformToAddress;
            //InformToId = a.InformToId;
            MiscCharges.MiscCharges = a.MiscCharges.MiscCharges;
            MiscCharges.ShippingAmount = a.MiscCharges.ShippingAmount;
            MiscCharges.TaxAmount = a.MiscCharges.TaxAmount;
            Owner = a.Owner;
            OwnerId = a.OwnerId;
            OwnersAddress = a.OwnersAddress;
            PaymentMethod = a.PaymentMethod;
            PaymentMethodId = a.PaymentMethodId;
            PaymentTerm = a.PaymentTerm;
            PaymentTermId = a.PaymentTermId;
            Salesman = a.Salesman;
            SalesmanId = a.SalesmanId;
            SaleTypeENUM = a.SaleTypeENUM;
            ShipToString = a.ShipToString;
            ShipToAddress = a.ShipToAddress;
            //ShipToId = a.ShipToId;
            TheirPurchaseOrderNumber = a.TheirPurchaseOrderNumber;


        }


        #endregion









        //public Guid? DeliveryMethodId { get; set; }
        public DateTime DateExpected { get; set; }
        public DateTime DateShipped { get; set; }


    }
}