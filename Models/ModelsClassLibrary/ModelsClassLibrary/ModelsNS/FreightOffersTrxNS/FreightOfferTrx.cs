using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
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

        }

        /// <summary>
        /// If this is true then the insurance is payable
        /// </summary>
        [Display(Name = "Pay Insurance")]
        public bool IsPayInsurance { get; set; }

        
        //Everyobdy must guarantee pickup.
        ///// <summary>
        ///// If this is true then the guarantee is payable
        ///// </summary>
        //[Display(Name = "Pay Pickup Guarantee")]
        //public bool IsPayGuaranteePickUp { get; set; }


        //[Display(Name = "Pickup Code")]
        //public string PickupCode { get; set; }


        //[Display(Name = "Delivert Code")]
        //public string DeliveryCode { get; set; }

        public string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }


        [Display(Name = "Insurance Amount")]
        public decimal InsuranceAmount { get; set; }

        public string DeliverymanId { get; set; }
        public virtual Deliveryman Deliveryman { get; set; }


        public decimal OfferAmount { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }


        ///// <summary>
        ///// This is the day the item is picked up.
        ///// </summary>
        //public DateAndByComplex PickedUpOn { get; set; }


        public bool IsOfferAccepted { get; set; }


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
