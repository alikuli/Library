using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Complex;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS
{
    public class FreightOfferTrx : CommonWithId
    {
        public FreightOfferTrx()
        {
            OfferAcceptedInfo = new DateAndByComplex();
            //PickedUpOn = new DateAndByComplex();
        }
        public FreightOfferTrx(string buySellDocId, string deliverymanId, decimal offerAmount, DateTime pickupDate, DateTime expectedDeliveryDate, string comment, string vehicalTypeId)
            : this()
        {
            BuySellDocId = buySellDocId;
            DeliverymanId = deliverymanId;
            OfferAmount = offerAmount;
            PickupDate = pickupDate;
            ExpectedDeliveryDate = expectedDeliveryDate;
            Comment = comment;
            VehicalTypeId = vehicalTypeId;
            OfferAcceptedByOwner = new BoolDateAndByComplex();
            //InsuranceAmount = insuranceAmount;

        }

        /// <summary>
        /// If this is true then the insurance is payable
        /// </summary>
        [Display(Name = "Pay Insurance")]
        public bool IsPayInsurance { get; set; }




        public string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }

        /// <summary>
        /// if his is true, then the owner has accepted the offer
        /// </summary>
        [Display(Name = "Offer Accepted By Owner")]
        public BoolDateAndByComplex OfferAcceptedByOwner { get; set; }

        public bool IsOfferAcceptedByDeliveryman()
        {
            if (BuySellDoc.FreightOfferTrxAcceptedId.IsNullOrWhiteSpace())
                return false;

            if (BuySellDoc.FreightOfferTrxAcceptedId == Id)
                return true;

            return false;
        }


        [Display(Name = "Deliveryman")]
        public string DeliverymanId { get; set; }
        public virtual Deliveryman Deliveryman { get; set; }


        public decimal OfferAmount { get; set; }

        public decimal MaxPossibleLiabilityToDeliverParcel()
        {
            BuySellDoc.IsNullThrowException();
            decimal ttlLiability = OfferAmount + BuySellDoc.InsuranceRequired;
            return ttlLiability;
        }

        [Display(Name = "Pick up (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime PickupDate { get; set; }




        [Display(Name = "Expected Delivery Date (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedDeliveryDate { get; set; }


        ///// <summary>
        ///// This is the day the item is picked up.
        ///// </summary>
        //public DateAndByComplex PickedUpOn { get; set; }




        [Display(Name = "Vehical Type")]
        public string VehicalTypeId { get; set; }

        [Display(Name = "Vehical Type")]
        public virtual VehicalType VehicalType { get; set; }

        
        [NotMapped]
        public SelectList SelectListVehicalType { get; set; }




        public virtual DateAndByComplex OfferAcceptedInfo { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.FreightOfferTrx;
        }

        public override string MakeUniqueName()
        {
            string str = string.Format("{0} - Deliveryman: {1}", BuySellDoc.FullName(), Deliveryman.FullName());
            return str;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            FreightOfferTrx frtOff = icommonWithId as FreightOfferTrx;

            BuySellDocId = frtOff.BuySellDocId;
            DeliverymanId = frtOff.DeliverymanId;
            OfferAmount = frtOff.OfferAmount;
            PickupDate = frtOff.PickupDate;
            ExpectedDeliveryDate = frtOff.ExpectedDeliveryDate;
            Comment = frtOff.Comment;
            VehicalTypeId = frtOff.VehicalTypeId;

        }

    }
}
