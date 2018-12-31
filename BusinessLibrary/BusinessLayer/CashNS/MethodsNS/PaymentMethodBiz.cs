using System;
using System.Reflection;
using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ViewModels;
using WebLibrary.Programs;
using ApplicationDbContextNS;
using UserModels;
using AliKuli.UtilitiesNS;
using UowLibrary.UploadFileNS;
using BreadCrumbsLibraryNS.Programs;
using ModelsClassLibrary.RightsNS;
using UowLibrary.PlayersNS;
using UowLibrary.ParametersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PaymentMethodNS
{
    public partial class PaymentMethodBiz : BusinessLayer<PaymentMethod>
    {
        public PaymentMethodBiz(IRepositry<PaymentMethod> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }

    }
}
