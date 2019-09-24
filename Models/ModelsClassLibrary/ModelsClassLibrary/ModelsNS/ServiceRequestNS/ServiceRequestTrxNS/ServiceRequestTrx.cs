using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestTrxNS
{
    /// <summary>
    /// This class is used to accept all the service requests from the user.
    /// </summary>
    public class ServiceRequestTrx : CommonWithId
    {

        public ServiceRequestTrx()
        {
            Commission = new CommissionComplex();
        }

        
        public ServiceRequestTrx(string personBeingPaidId, CommissionComplex commission)
            : this()
        {
            PersonBeingPaidId = personBeingPaidId;
            Commission = commission;
        }
        
        
        public ServiceRequestTrx(string personBeingPaidId, decimal amount, decimal percent)
            : this()
        {
            PersonBeingPaidId = personBeingPaidId;
            Commission.Amount = amount;
            Commission.Percent = percent;
        }


        public static ServiceRequestTrx Unbox(ICommonWithId icommonwithid)
        {
            ServiceRequestTrx srt = icommonwithid as ServiceRequestTrx;
            srt.IsNullThrowException();
            return srt;
        }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.ServiceRequestTrx;
        }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            ServiceRequestTrx srt = ServiceRequestTrx.Unbox(icommonWithId);
            PersonBeingPaidId = srt.PersonBeingPaidId;
            Commission = srt.Commission;

        }

        [Display(Name = "Person Being Paid")]
        public string PersonBeingPaidId { get; set; }
        public Person PersonBeingPaid { get; set; }


        public string ServiceRequestHdrId { get; set; }
        public virtual ServiceRequestHdr ServiceRequestHdr { get; set; }

        [NotMapped]
        public SelectList SelectListPersonBeingPaid { get; set; }


        [Display(Name = "Commission")]
        public CommissionComplex Commission { get; set; }


    }

}
