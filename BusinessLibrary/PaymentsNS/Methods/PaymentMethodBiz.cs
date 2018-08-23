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
using UowLibrary.MyWorkClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PaymentMethodNS
{
    public partial class PaymentMethodBiz : BusinessLayer<PaymentMethod>
    {
        public PaymentMethodBiz(IRepositry<PaymentMethod> entityDal, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {

        }

    }
}
