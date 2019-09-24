using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestTrxNS;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS
{
    public interface IServiceRequestHdr
    {
        decimal Amount { get; set; }
        ClassesWithRightsENUM ClassNameForRights();
        Person PersonFrom { get; set; }
        string PersonFromId { get; set; }
        Person PersonTo { get; set; }
        string PersonToId { get; set; }
        RequestTypeENUM RequestTypeEnum { get; set; }
        SelectList SelectListPersonFrom { get; set; }
        SelectList SelectListPersonTo { get; set; }
        SelectList SelectListRequestTypeEnum { get; }
        //ICollection<ServiceRequestTrx> ServiceRequestTrxs { get; set; }
        void UpdatePropertiesDuringModify(ICommonWithId icommonWithId);
    }
}
