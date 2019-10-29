using EnumLibrary.EnumNS.VerificationNS;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS
{
    /// <summary>
    /// Only ServiceRequestHdrs that are open will come here.
    /// </summary>
    [NotMapped]
    public class ServiceRequestVM
    {

        public ServiceRequestVM()
        {

        }

        public ServiceRequestVM(string serviceRequestHdrId, DateTime date, string name, ServiceRequestTypeENUM serviceRequestTypeEnum)
        {
            ServiceRequestHdrId = serviceRequestHdrId;
            Date = date;
            Name = name;
            ServiceRequestTypeEnum = serviceRequestTypeEnum;
        }
        public string ServiceRequestHdrId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public ServiceRequestTypeENUM ServiceRequestTypeEnum { get; set; }
    }
}
