using System;
using System.Reflection;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ViewModels;
using WebLibrary.Programs;
using ApplicationDbContextNS;
using DalNS;
using UserModels;
using AliKuli.UtilitiesNS;
using UowLibrary.UploadFileNS;

namespace UowLibrary.PaymentMethodNS
{
    public partial class PaymentMethodBiz : BusinessLayer<PaymentMethod>
    {
        public PaymentMethodBiz(IRepositry<ApplicationUser> userDal, IRepositry<PaymentMethod> iPaymentMethodDAL, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, iPaymentMethodDAL, db, configManager, uploadedFileBiz)
        {

        }

    }
}
