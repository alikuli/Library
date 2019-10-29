using EnumLibrary.EnumNS.VerificationNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequest_StatePatternNS.ServiceRequest_StatesNS;

namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequest_StatePatternNS
{
    public class ServiceRequestController
    {
        public static IServiceRequestDetail GetServiceRequestDetail(ServiceRequestTypeENUM serviceRequestTypeEnum)
        {
            switch (serviceRequestTypeEnum)
            {
                case ServiceRequestTypeENUM.GetPhoneNumber:
                    return new GetPhoneNumberState();

                case ServiceRequestTypeENUM.BecomeSalesman:
                    return new BecomeSalesmanState();

                case ServiceRequestTypeENUM.BecomeSeller:
                    return new BecomeSellerState();

                case ServiceRequestTypeENUM.BecomeMailer:
                    return new BecomeMailerState();

                case ServiceRequestTypeENUM.BecomeCustomer:
                    break;
                case ServiceRequestTypeENUM.BecomeSuperSalesman:
                    break;
                case ServiceRequestTypeENUM.Unknown:
                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
