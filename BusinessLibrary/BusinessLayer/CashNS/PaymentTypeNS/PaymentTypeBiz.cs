using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.PaymentTypeNS
{
    public partial class PaymentTypeBiz : BusinessLayer<PaymentType>
    {
        public PaymentTypeBiz(IRepositry<PaymentType> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }

    }
}

