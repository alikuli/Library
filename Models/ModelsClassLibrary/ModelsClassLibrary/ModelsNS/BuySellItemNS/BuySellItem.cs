using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS
{
    public class BuySellItem : CommonWithId
    {
        public BuySellItem()
        {
            initialize();
        }



        public BuySellItem(string buySellDocId, string productChildId, double ordered, /*double shipped,*/ decimal salePrice, string name)
            : this()
        {
            BuySellDocId = buySellDocId;
            ProductChildId = productChildId;
            Quantity = new Quantity(ordered);
            SalePrice = salePrice;
            OriginalPrice = salePrice;//this is the original listed price.
            Name = name;
        }

        void initialize()
        {
            Quantity = new Quantity();
            //LastOffer_Buyer = new DecimalWithDateComplex();
            //LastOffer_Seller = new DecimalWithDateComplex();


        }

        [NotMapped]
        public decimal CustomerBalanceNonRefundable { get; set; }

        [NotMapped]
        public decimal CustomerBalanceRefundable { get; set; }


        [NotMapped]
        public Customer Customer
        {
            get
            {
                if (BuySellDoc.IsNull())
                    return null;
                return BuySellDoc.Customer;
            }
        }

        [NotMapped]
        public Owner Owner
        {
            get
            {
                if (BuySellDoc.IsNull())
                    return null;
                return BuySellDoc.Owner;
            }
        }

        public override string FullName()
        {
            string fullName = string.Format("{0}", Name);

            if (BuySellDocStateEnum != BuySellDocStateENUM.Unknown)
            {
                if (BuySellDoc.IsNull())
                {
                }
                else
                {
                    BuySellDocStateEnum = BuySellDoc.BuySellDocStateEnum;
                    BuySellDocumentTypeEnum = BuySellDoc.BuySellDocumentTypeEnum;
                    fullName = string.Format("{0} - {1} ", Name, BuySellDocStateEnum.ToString().ToTitleSentance());

                }
            }


            return fullName;
        }
        [NotMapped]
        public bool IsUserOwned { get; set; }

        [NotMapped]
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }

        [NotMapped]
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }

        public string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }

        [Display(Name = "Guaranteed Amount Required")]
        public decimal GuaranteeRequiredInRs { get; set; }
        public string ProductChildId { get; set; }
        public virtual ProductChild ProductChild { get; set; }

        public Quantity Quantity { get; set; }


        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

        
        
        [NotMapped]
        [Display(Name = "Sale Price")]
        public string SalePriceStr { get; set; }

        
        
        /// <summary>
        /// This is the original price the item was listed at
        /// </summary>
        [Display(Name = "Original Price")]
        public decimal OriginalPrice { get; set; }
        public string OriginalPrice_Formatted
        {
            get
            {
                return string.Format("{0:N2}", OriginalPrice);
            }
        }



        [Display(Name = "Difference")]
        public string Difference_Formatted
        {
            get
            {
                return string.Format("{0:N2}", Difference);
            }
        }
        public decimal Difference
        {
            get
            {
                return SalePrice - OriginalPrice;
            }
        }


        public bool IsSalePriceSame
        {
            get
            {
                return Difference == 0;
            }
        }

        [Display(Name = "Ordered (Rs)")]
        public decimal OrderedRs
        {
            get
            {
                return SalePrice * Quantity.OrderedAsDecimal;
            }
        }


        public string OrderedRs_Formatted
        {
            get
            {
                return string.Format("{0:N2}", OrderedRs);
            }
        }

        ////if true, the product will be counted in the money calculation
        ////and will be deemed as part of the shipment.                                 
        //[Display(Name = "Is Shipping")]
        //public bool IsShipping { get; set; }
        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDoc;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            BuySellItem buySellItem = icommonWithId as BuySellItem;
            buySellItem.IsNullThrowException();

            BuySellDocStateEnum = buySellItem.BuySellDocStateEnum;
            BuySellDocumentTypeEnum = buySellItem.BuySellDocumentTypeEnum;
            Quantity.OrderStr = buySellItem.Quantity.OrderStr;
            SalePriceStr = buySellItem.SalePriceStr;

            ////if the sale price being saved is different then update the last offers
            //switch (buySellItem.BuySellDocumentTypeEnum)
            //{
            //    case BuySellDocumentTypeENUM.Unknown:
            //        break;
            //    case BuySellDocumentTypeENUM.Delivery:
            //        switch (buySellItem.BuySellDocStateEnum)
            //        {
            //            case BuySellDocStateENUM.RequestConfirmed:
            //            case BuySellDocStateENUM.RequestUnconfirmed:
            //            case BuySellDocStateENUM.Unknown:
            //            case BuySellDocStateENUM.InProccess:
            //            case BuySellDocStateENUM.BackOrdered:
            //            case BuySellDocStateENUM.All:
            //            case BuySellDocStateENUM.ConfirmedBySeller:
            //            case BuySellDocStateENUM.ReadyForShipment:
            //            case BuySellDocStateENUM.CourierAccepted:
            //            case BuySellDocStateENUM.ConfirmedByCourier:
            //            case BuySellDocStateENUM.PickedUp:
            //            case BuySellDocStateENUM.Delivered:
            //            case BuySellDocStateENUM.Rejected:
            //            case BuySellDocStateENUM.Problem:
            //            default:
            //                break;
            //        }
            //        break;

            //    case BuySellDocumentTypeENUM.Sale:    //SALE
            //        switch (buySellItem.BuySellDocStateEnum)
            //        {
            //            case BuySellDocStateENUM.RequestConfirmed:

            //                Quantity.Order = getOrderAmount(buySellItem);
            //                SalePrice = getSalePrice(buySellItem);
            //                break;

            //            case BuySellDocStateENUM.RequestUnconfirmed:
            //            case BuySellDocStateENUM.Unknown:
            //            case BuySellDocStateENUM.InProccess:
            //            case BuySellDocStateENUM.BackOrdered:
            //            case BuySellDocStateENUM.All:
            //            case BuySellDocStateENUM.ConfirmedBySeller:
            //            case BuySellDocStateENUM.ReadyForShipment:
            //            case BuySellDocStateENUM.ConfirmedByCourier:
            //            case BuySellDocStateENUM.PickedUp:
            //            case BuySellDocStateENUM.Delivered:
            //            case BuySellDocStateENUM.Rejected:
            //            case BuySellDocStateENUM.Problem:
            //            default:
            //                break;
            //        }
            //        break;

            //    case BuySellDocumentTypeENUM.Purchase:   //PURCHASE

            //        switch (buySellItem.BuySellDocStateEnum)
            //        {
            //            case BuySellDocStateENUM.RequestUnconfirmed:
            //                Quantity.Order = getOrderAmount(buySellItem);
            //                Quantity.Order_Original = Quantity.Order;
            //                SalePrice = getSalePrice(buySellItem);
            //                break;

            //            case BuySellDocStateENUM.Unknown:
            //            case BuySellDocStateENUM.InProccess:
            //            case BuySellDocStateENUM.BackOrdered:
            //            case BuySellDocStateENUM.All:
            //            case BuySellDocStateENUM.RequestConfirmed:
            //            case BuySellDocStateENUM.ConfirmedBySeller:
            //            case BuySellDocStateENUM.ReadyForShipment:
            //            case BuySellDocStateENUM.ConfirmedByCourier:
            //            case BuySellDocStateENUM.PickedUp:
            //            case BuySellDocStateENUM.Delivered:
            //            case BuySellDocStateENUM.Rejected:
            //            case BuySellDocStateENUM.Problem:
            //            default:
            //                break;
            //        }

            //        break;

            //    default:
            //        break;
            //}

        }



        ///// <summary>
        ///// The last offer of the buyer is saved here for reference purposes.
        ///// </summary>
        //[Display(Name = "Last offer to buy at")]
        //public DecimalWithDateComplex LastOffer_Buyer { get; set; }

        ////the last offer of the seller is saved here for reference purposes
        //[Display(Name = "Last offer to sell at")]
        //public DecimalWithDateComplex LastOffer_Seller { get; set; }

        //[Display(Name = "Number of Offers")]
        //public int NumberOfOffers { get; set; }

        //[Display(Name = "Shipped (Rs)")]
        //public decimal ShipRs
        //{
        //    get
        //    {
        //        return SalePrice * Quantity.ShipAsDecimal;
        //    }
        //}
        //public string ShippedRs_Formatted
        //{
        //    get
        //    {
        //        return string.Format("{0:N2}", ShipRs);
        //    }
        //}



        //public decimal TotalBackOrderedMoney
        //{
        //    get
        //    {
        //        return SalePrice * Quantity.BackOrderedAsDecimal;
        //    }
        //}

        //public bool HasBackOrder
        //{
        //    get
        //    {
        //        return Quantity.Remaining > 0;
        //    }
        //}

        //[Display(Name = "Remaining (Rs)")]
        //public decimal RemainingRs
        //{
        //    get
        //    {
        //        return Quantity.RemainingDecimal * SalePrice;
        //    }
        //}
        //public string RemainingRs_Formatted
        //{
        //    get
        //    {
        //        return string.Format("{0:N2}", RemainingRs);
        //    }
        //}

        //public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        //{
        //    base.UpdatePropertiesDuringModify(icommonWithId);
        //    BuySellDoc buySellDoc = icommonWithId as BuySellDoc;

        //    CourierSelected = buySellDoc.CourierSelected;
        //    CourierAccepts = buySellDoc.CourierAccepts;
        //    VendorAccepts = buySellDoc.VendorAccepts;
        //    BuySellDocStateEnum = buySellDoc.BuySellDocStateEnum;
        //    BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;
        //    AcceptRejectOrEmpty = buySellDoc.AcceptRejectOrEmpty;

        //    //During sale, the Vendor cannot update the Buyer and seller.
        //    switch (buySellDoc.BuySellDocumentTypeEnum)
        //    {
        //        case BuySellDocumentTypeENUM.Unknown:
        //        case BuySellDocumentTypeENUM.Delivery:
        //            break;
        //        case BuySellDocumentTypeENUM.Sale:
        //            break;
        //        case BuySellDocumentTypeENUM.Purchase:
        //            {

        //                //only these will be updated for purchases
        //                //the state comes from the saved document because the user might fiddle with
        //                //it in the view.
        //                switch (BuySellDocStateEnum)
        //                {
        //                    case BuySellDocStateENUM.Unknown:
        //                    case BuySellDocStateENUM.RequestUnconfirmed:
        //                        {
        //                            //if we change the status here, then in the business rules it follows
        //                            //the rules of the new status.
        //                            //better to change the status in the business rules
        //                            //BuySellDocStateEnum = BuySellDocStateENUM.RequestConfirmed;

        //                            //Buyer should be able to update their address.
        //                            ShipToAddress = buySellDoc.ShipToAddress;
        //                            BillToAddress = buySellDoc.BillToAddress;

        //                            //note we have drop downs at this point. I am considering
        //                            //not saving them for the future. They will just be used to add
        //                            //addressess
        //                            AddressShipToId = buySellDoc.AddressShipToId;
        //                            AddressInformToId = buySellDoc.AddressInformToId;

        //                            DeliverymanId = buySellDoc.DeliverymanId;
        //                            Freight = buySellDoc.Freight;





        //                        }
        //                        break;
        //                    case BuySellDocStateENUM.InProccess:
        //                    case BuySellDocStateENUM.BackOrdered:
        //                    case BuySellDocStateENUM.All:
        //                    case BuySellDocStateENUM.RequestConfirmed:
        //                    case BuySellDocStateENUM.ConfirmedBySeller:
        //                    case BuySellDocStateENUM.ReadyForShipment:
        //                    case BuySellDocStateENUM.ConfirmedByCourier:
        //                    case BuySellDocStateENUM.PickedUp:
        //                    case BuySellDocStateENUM.Delivered:
        //                    case BuySellDocStateENUM.Rejected:
        //                    case BuySellDocStateENUM.Problem:
        //                    default:
        //                        break;
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }


        //}

        public IBuySellDocViewState BuySellDocViewState
        {
            get
            {
                BuySellDocViewStateController buySellDocViewStateController = new BuySellDocViewStateController(BuySellDocStateEnum, BuySellDocumentTypeEnum, Customer, Owner, CustomerBalanceRefundable, CustomerBalanceNonRefundable);
                return buySellDocViewStateController.GetBuySellDocStateController();
            }
        }

        public IBuySellDocViewState GetBuySellDocViewState(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        {
            BuySellDocViewStateController buySellDocViewStateController = new BuySellDocViewStateController(buySellDocStateEnum, buySellDocumentTypeEnum, Customer, Owner, CustomerBalanceRefundable, CustomerBalanceNonRefundable);
            return buySellDocViewStateController.GetBuySellDocStateController();

        }

    }
}
