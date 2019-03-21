using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.AbstractNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Complex;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using ModelsClassLibrary.ModelsNS.ProductChildNS;


namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    public class BuySellDoc : DocumentAbstract
    {
        public BuySellDoc()
        {
            CourierSelected = new BoolDateAndByComplex();
            CourierAccepts = new BoolDateAndByComplex();
            VendorAccepts = new BoolDateAndByComplex();
            OrderShipped = new BoolDateAndByComplexWithConfirmationCode();
            OrderDelivered = new BoolDateAndByComplexWithConfirmationCode();
        }

        public BuySellDoc(
            string ownerId,
            string customerId,
            string addressInformToId,
            string addressShipToId,
            string poNumber,
            DateTime poDate,
            SelectList selectListOwner,
            SelectList selectListCustomer,
            SelectList selectListAddressInformTo,
            SelectList selectListAddressShipTo)
            :
            base(
                ownerId,
                customerId,
                addressInformToId,
                addressShipToId,
                poNumber,
                poDate,
                selectListOwner,
                selectListCustomer,
                selectListAddressInformTo,
                selectListAddressShipTo)
        {
        }



        public void Initialize(string ownerId, string customerId, string addressInformToId, string addressShipToId, string poNumber, DateTime poDate, SelectList selectListOwner, SelectList selectListCustomer, SelectList selectListAddressInformTo, SelectList selectListAddressShipTo)
        {
            base.InitializeAbstract(ownerId, customerId, addressInformToId, addressShipToId, poNumber, poDate, selectListOwner, selectListCustomer, selectListAddressInformTo, selectListAddressShipTo);
            BuySellDocStateEnum = BuySellDocStateENUM.New;
        }

        public void Add(BuySellItem buySellItem)
        {
            if (buySellItem.IsNull())
                return;

            if (BuySellItems.IsNull())
            {
                BuySellItems = new List<BuySellItem>();
                BuySellItems.Add(buySellItem);
                return;
            }

            //check to see if the item already exists.
            BuySellItem bsItemFound = BuySellItems.FirstOrDefault(x => x.ProductChildId == buySellItem.Id);
            if(bsItemFound.IsNull())
            {
                BuySellItems.Add(buySellItem);
                return;

            }
            //item was found already ordered... just its quantity is increased
            bsItemFound.Quantity.Ordered = buySellItem.Quantity.Ordered;
        }

        public void Add(List<BuySellItem> buySellItems)
        {
            if (buySellItems.IsNullOrEmpty())
                return;
            
            if (BuySellItems.IsNull())
                BuySellItems = new List<BuySellItem>();

            foreach (var item in buySellItems)
            {
                BuySellItems.Add(item);
            }
        }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDoc;
        }



        public override bool HideNameInView()
        {
            return true;
        }

        public override string FullName()
        {
            //Owner.IsNullThrowException("Owner");
            //Customer.IsNullThrowException("Customer");
            string fullName = Name;
            if (!Owner.IsNull())
            {
                fullName = string.Format("[{0}] {1} By: {2} To: {3}", DocumentNumber, MetaData.Created.Date_NotNull_Min.ToString("dd-MMM-yyyy"), Owner.Name, Customer.Name);

            }
            return fullName;
        }

        public virtual ICollection<BuySellItem> BuySellItems { get; set; }
        private ICollection<BuySellItem> buySellItemFixed
        {
            get
            {
                if (BuySellItems.IsNullOrEmpty())
                    return null;

                List<BuySellItem> withoutDeleted = BuySellItems.Where(x => x.MetaData.IsDeleted == false).ToList();
                if (withoutDeleted.IsNullOrEmpty())
                    return null;
                
                return withoutDeleted;
            }
        }
        public decimal TotalSale
        {
            get
            {
                if (buySellItemFixed.IsNullOrEmpty())
                    return 0;

                decimal ttlSale = 0;
                foreach (var item in buySellItemFixed)
                {
                    ttlSale += item.Ordered;
                }
                return ttlSale;
            }
        }

        public decimal TotalBackOrdered
        {
            get
            {
                if (buySellItemFixed.IsNullOrEmpty())
                    return 0;

                decimal ttlSale = 0;
                foreach (var item in buySellItemFixed)
                {
                    ttlSale += item.TotalBackOrderedMoney;
                }
                return ttlSale;
            }
        }

        public bool HasBackOrders
        {
            get
            {
                if (buySellItemFixed.IsNullOrEmpty())
                    return false;

                foreach (var item in buySellItemFixed)
                {
                    if (item.HasBackOrder)
                        return true;
                }
                return false;

            }
        }

        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }

        public BoolDateAndByComplex CourierSelected { get; set; }
        public BoolDateAndByComplex CourierAccepts { get; set; }
        public BoolDateAndByComplex VendorAccepts { get; set; }

        public BoolDateAndByComplexWithConfirmationCode OrderShipped { get; set; }
        public BoolDateAndByComplexWithConfirmationCode OrderDelivered { get; set; }

    }
}
