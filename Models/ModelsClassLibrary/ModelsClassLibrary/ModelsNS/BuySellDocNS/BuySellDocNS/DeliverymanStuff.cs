using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    /// <summary>
    /// During sale the vendor / seller cannot update the CustomerId or the SellerId
    /// What will happen if the seller and the delivery man are the same. They could cheat the customer.
    /// The customer can always create a problem.
    /// </summary>
    public partial class BuySellDoc
    {
        public bool IsShowFreightMessage
        {
            get
            {
                switch (BuySellDocStateEnum)
                {
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.RequestUnconfirmed:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.RequestConfirmed:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.OptedOutOfSystem:
                        return false;
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.ReadyForPickup:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.CourierComingToPickUp:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.PickedUp:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.Enroute:
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.Delivered:
                        return true;
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.Rejected:
                        return false;
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.Problem:
                        return true;
                    case EnumLibrary.EnumNS.BuySellDocStateENUM.Unknown:
                    default:
                        throw new Exception(string.Format("Unknown Document Type: {0}",BuySellDocStateEnum.ToString().ToTitleSentance()));
                }
            }
        }

        /// <summary>
        /// This is used in orderList
        /// </summary>
        /// <param name="currUser_DeliverymanId"></param>
        /// <returns></returns>
        public string CurrentUsers_FreightOfferTrx_String(string currUser_DeliverymanId)
        {
            if (currUser_DeliverymanId.IsNullOrWhiteSpace())
                return null;

            if (FreightOfferTrxs.IsNullOrEmpty())
                return null;
            //return " <<< NO OFFER >>";
            FreightOfferTrx frtTrx = FreightOfferTrxs.FirstOrDefault(x => x.DeliverymanId == currUser_DeliverymanId);
            if (frtTrx.IsNull())
                return null;

            string date = frtTrx.MetaData.Created.Date_NotNull_Min.ToShortDateString();
            string offerAmount = frtTrx.OfferAmount.ToString("N0");
            string transport = frtTrx.VehicalType.IsNull() ? "Unknown" : frtTrx.VehicalType.FullName();
            string insurance = InsuranceRequired_ToSring;
            string freightOffer = string.Format("On {0} You offered Rs{1} to pick. Vehical Type: {2}, Insurance {3}",
                date,
                offerAmount,
                transport,
                insurance);

            if (frtTrx.OfferAcceptedByOwner.IsSelected)
            {
                freightOffer += string.Format(" *** Accepted By Shipper ({0}) ***", frtTrx.OfferAcceptedByOwner.Date_NotNull_Min.ToShortDateString());
            }

            if (FreightOfferTrxAcceptedId == frtTrx.Id)
            {
                freightOffer += string.Format(" *** Accepted By By You ***");

            }

            return freightOffer;

        }


        #region Freight Offer



        [Display(Name = "Freight Offers")]
        public virtual ICollection<FreightOfferTrx> FreightOfferTrxs { get; set; }


        [NotMapped]
        public List<FreightOfferTrx> FreightOfferTrxs_Fixed
        {
            get
            {
                if (FreightOfferTrxs.IsNullOrEmpty())
                    return new List<FreightOfferTrx>();
                return FreightOfferTrxs.Where(x => x.MetaData.IsDeleted == false).OrderByDescending(x => x.OfferAcceptedByOwner.IsSelected == true).OrderBy(x => x.OfferAmount).ToList();
            }
        }



        [Display(Name = "Accepted Trx")]
        public string FreightOfferTrxAcceptedId { get; set; }
        public virtual FreightOfferTrx FreightOfferTrxAccepted { get; set; }


        /// <summary>
        /// This is the offer made by the delivery man to pick and drop the item
        /// </summary>
        /// 
        [NotMapped]
        [Display(Name = "Offer")]
        public string FreightOffer { get; set; }

        [NotMapped]
        [Display(Name = "Freight Offer")]
        public decimal FreightOfferDecimal
        {
            get
            {
                decimal offer;
                bool success = decimal.TryParse(FreightOffer, out offer);
                if (success)
                    return offer;
                return 0;
            }
        }

        /// <summary>
        /// This it the amount the customer has budgeted for freight. Not neccassry he will
        /// get it but it will be the starting value for the deliveryman
        /// </summary>
        [Display(Name = "Customer Freight Budget")]

        public decimal FreightCustomerBudget { get; set; }

        [NotMapped]
        [Display(Name = "Customer Freight Budget")]

        public string FreightCustomerBudget_String { get; set; }



        [NotMapped]
        [Display(Name = "delivery man comments")]
        public string CommentByDeliveryman { get; set; }
















        [Display(Name = "Offered Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime OfferedPickupOnDate { get; set; }



        [Display(Name = "Offered Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedDeliveryDate { get; set; }













        [NotMapped]
        [Display(Name = "Agreed Pick up Date")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AgreedPickupDateByDeliveryman
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return DateTime.MinValue;

                return FreightOfferTrxAccepted.PickupDate;
            }
        }



        /// <summary>
        /// This is the date that pick up is requested.
        /// </summary>
        [Display(Name = "Please pick up on ")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime PleasePickupOnDate_Start { get; set; }



        [Display(Name = "Last Day for Pick up")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PleasePickupOnDate_End { get; set; }


        #endregion


        #region Vehical Type Requested
        [Display(Name = "Vehical Type Requested")]
        public string VehicalTypeRequestedId { get; set; }

        [Display(Name = "Vehical Type Requested")]
        public virtual VehicalType VehicalTypeRequested { get; set; }

        [NotMapped]
        public SelectList SelectListVehicalTypeRequested { get; set; }
        #endregion

        #region Vehical Type Offered

        [NotMapped]
        [Display(Name = "Vehical Type Offered")]
        public string VehicalTypeOfferedId { get; set; }

        [Display(Name = "Vehical Type Offered")]
        [NotMapped]
        public virtual VehicalType VehicalTypeOffered { get; set; }

        [NotMapped]
        public SelectList SelectListVehicalTypeOffered { get; set; }

        #endregion

        #region Vehical Type Accepted

        [Display(Name = "Vehical Type Accepted")]
        [NotMapped]
        public string VehicalTypeAcceptedId
        {
            get
            {
                if (VehicalTypeAccepted.IsNull())
                    return null;

                return VehicalTypeAccepted.Id;
            }
        }


        [NotMapped]
        [Display(Name = "Vehical Type Accepted")]
        public virtual VehicalType VehicalTypeAccepted
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return null;
                if (FreightOfferTrxAccepted.VehicalType.IsNull())
                    return null;
                return FreightOfferTrxAccepted.VehicalType;
            }
        }


        [NotMapped]
        public SelectList SelectListVehicalTypeAccepted { get; set; }

        #endregion



        [NotMapped]
        public Deliveryman Deliveryman
        {
            get
            {
                if (FreightOfferTrxAccepted.IsNull())
                    return null;

                FreightOfferTrxAccepted.Deliveryman.IsNullThrowException();
                return FreightOfferTrxAccepted.Deliveryman;
            }
        }

        [NotMapped]
        public string DeliverymanId
        {
            get
            {
                if (Deliveryman.IsNull())
                    return "";
                return Deliveryman.Id;
            }
        }


        ///// <summary>
        ///// If true then the deliveryman has accepted to deliver the products
        ///// </summary>
        //[NotMapped]
        //public bool IsDeliveryman_Accepted_By_Courier
        //{
        //    get
        //    {
        //        if (FreightOfferTrxAccepted.IsNull())
        //            return false;

        //        if (FreightOfferTrxAccepted.IsOfferAccepted)
        //            return true;
        //        return false;
        //    }
        //}

        public string GetDeliverymanId()
        {
            if (Deliveryman.IsNull())
                return "";
            return Deliveryman.Id;
        }


        public void UpdateFreightBidVars(FreightOfferTrx earlierOfferByUser)
        {
            FreightOffer = string.Format("{0:N0}", earlierOfferByUser.OfferAmount);
            CommentByDeliveryman = earlierOfferByUser.Comment;
            OfferedPickupOnDate = earlierOfferByUser.PickupDate;
            VehicalTypeOfferedId = earlierOfferByUser.VehicalTypeId;

        }
        private string parse_DeliveryCode_Deliveryman(string DeliveryCode_Deliveryman_AsEntered)
        {

            string minimumNumberStr = BuySellDoc.GetRandomGeneratorMinimumNumber();
            minimumNumberStr.IsNullOrWhiteSpaceThrowException("The RandomNumberGenerator.MinimumNumber is null or empty");
            //get the digits from the right which are relevant
            string code = DeliveryCode_Deliveryman_AsEntered
                .Substring(DeliveryCode_Deliveryman_AsEntered.Length - minimumNumberStr.Length);


            return code;
            //DeliveryCode_Deliveryman = DeliveryCode_Deliveryman_AsEntered
            //    .Substring(DeliveryCode_Deliveryman_AsEntered.Length - minimumNumberStr.Length);


        }

        private void update_freightOfferAccepted(BuySellDoc buySellDoc)
        {
            FreightOfferTrxAcceptedId = buySellDoc.FreightOfferTrxAcceptedId;
        }




        private void update_Freight_Request_Variables(BuySellDoc buySellDoc)
        {
            PleasePickupOnDate_Start = buySellDoc.PleasePickupOnDate_Start;
            PleasePickupOnDate_End = buySellDoc.PleasePickupOnDate_End;
            VehicalTypeRequestedId = buySellDoc.VehicalTypeRequestedId;
            FreightCustomerBudget_String = buySellDoc.FreightCustomerBudget_String;
        }
        private void update_VehicalType(BuySellDoc buySellDoc)
        {
            VehicalTypeRequestedId = buySellDoc.VehicalTypeRequestedId;
        }



    }
}
