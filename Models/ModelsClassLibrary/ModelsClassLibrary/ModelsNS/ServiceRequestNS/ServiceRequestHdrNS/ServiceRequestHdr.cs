using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestTrxNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS
{
    public class ServiceRequestHdr : CommonWithId, IServiceRequestHdr
    {
        public ServiceRequestHdr()
        {
            RequestTypeEnum = RequestTypeENUM.Unknown;
            ServiceRequestStatusEnum = ServiceRequestStatusENUM.Open;
        }



        public ServiceRequestHdr(string personFromId, string personToId, decimal amount, RequestTypeENUM requestTypeEnum)
            : this()
        {
            PersonFromId = personFromId;
            PersonToId = personToId;
            Amount = amount;
            RequestTypeEnum = requestTypeEnum;
            ServiceRequestStatusEnum = ServiceRequestStatusENUM.Open;
        }
        public static ServiceRequestHdr Unbox(ICommonWithId icommonWithId)
        {
            ServiceRequestHdr srh = icommonWithId as ServiceRequestHdr;
            srh.IsNullThrowException();
            return srh;
        }


        [Display(Name = "From")]
        public string PersonFromId { get; set; }

        [Display(Name = "From")]
        public Person PersonFrom { get; set; }


        [NotMapped]
        public SelectList SelectListPersonFrom { get; set; }


        [Display(Name = "Request Type")]
        public RequestTypeENUM RequestTypeEnum { get; set; }

        [NotMapped]
        public SelectList SelectListRequestTypeEnum { get { return EnumExtention.ToSelectListSorted<RequestTypeENUM>(RequestTypeENUM.Unknown); } }



        [Display(Name = "Request Status")]
        public ServiceRequestStatusENUM ServiceRequestStatusEnum { get; set; }

        [NotMapped]
        public SelectList SelectListServiceRequestStatusEnum { get { return EnumExtention.ToSelectListSorted<ServiceRequestStatusENUM>(ServiceRequestStatusENUM.Unknown); } }


        [Display(Name = "To")]
        public string PersonToId { get; set; }

        [Display(Name = "To")]
        public Person PersonTo { get; set; }

        [NotMapped]
        public SelectList SelectListPersonTo { get; set; }


        public decimal Amount { get; set; }



        public virtual ICollection<ServiceRequestTrx> ServiceRequestTrxs { get; set; }

        [NotMapped]
        public List<ServiceRequestTrx> ServiceRequestTrxs_Fixed
        {
            get
            {
                if (ServiceRequestTrxs.IsNull())
                    return null;

                List<ServiceRequestTrx> lst = ServiceRequestTrxs
                    .Where(x => x.MetaData.IsDeleted == false)
                    .OrderByDescending(x => x.Name)
                    .ToList();

                return lst;
            }
        }




        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.ServiceRequestHdr;

        }

        public override string MakeUniqueName()
        {
            string name = string.Format("Doc No. {0:0000} ", DocumentNo);

            if (RequestTypeEnum != RequestTypeENUM.Unknown)
                name = RequestTypeEnum.ToString().ToTitleSentance();
            return name;

        }
        public long DocumentNo { get; set; }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            ServiceRequestHdr srh = ServiceRequestHdr.Unbox(icommonWithId);
            PersonFromId = srh.PersonFromId;
            PersonToId = srh.PersonToId;
            RequestTypeEnum = srh.RequestTypeEnum;
            Amount = srh.Amount;
            ServiceRequestStatusEnum = srh.ServiceRequestStatusEnum;
        }

        public StringStringAndBool ToStringStringBool()
        {
            PersonFrom.IsNullThrowException();
            StringStringAndBool ssb = new StringStringAndBool(PersonFrom.FullName(), RequestTypeEnum.ToString().ToTitleSentance(), false);
            return ssb;
        }

    }
}
