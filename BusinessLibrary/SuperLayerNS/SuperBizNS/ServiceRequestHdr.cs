using UowLibrary.MenuNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequest_StatePatternNS;

namespace UowLibrary.SuperLayerNS
{
    public partial class SuperBiz
    {
        public bool AssignUserToServiceRequestHdr(string serviceRequestHdrId)
        {
            UserId.IsNullOrWhiteSpaceThrowException("Not logged in");
            serviceRequestHdrId.IsNullOrWhiteSpaceThrowArgumentException();
            ServiceRequestHdr srh = ServiceRequestHdrBiz.Find(serviceRequestHdrId);
            srh.IsNullThrowException();

            if(srh.ServiceRequestStatusEnum == ServiceRequestStatusENUM.Open)
            {
                //Person userPerson = PersonBiz.GetPersonForUserId(UserId);
                //userPerson.IsNullThrowException();
                srh.PersonToId= CurrentUserParameter.PersonId;
                srh.ServiceRequestStatusEnum = ServiceRequestStatusENUM.Closed;
                ServiceRequestHdrBiz.Update(srh);

                PenaltyHeader ph = setupPenaltyHeaderForServiceRequestDetail(srh);
                
            }
            else
            {
                throw new Exception("This item is not open");
            }

            return true;
        }

        private static PenaltyHeader setupPenaltyHeaderForServiceRequestDetail(ServiceRequestHdr srh)
        {
            IServiceRequestDetail srd = ServiceRequestController.GetServiceRequestDetail(srh.RequestTypeEnum);
            PenaltyHeader ph = new PenaltyHeader();
            ph.Amount = srd.Amount;
            ph.FromPersonId = srh.PersonToId; //the person who bought the name is paying
            ph.Comment = srd.Text;
            return ph;
        }


    }
}
