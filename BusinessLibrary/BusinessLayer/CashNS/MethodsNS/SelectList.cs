using System;
using System.Reflection;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ViewModels;
using WebLibrary.Programs;

namespace UowLibrary.PaymentMethodNS
{
    public partial class PaymentMethodBiz : BusinessLayer<PaymentMethod>
    {
        public override string SelectListCacheKey
        {
            get { return "PaymentMethodsSelectListData"; }
        }
    }
}
