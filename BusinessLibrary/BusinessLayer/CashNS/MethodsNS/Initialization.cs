using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ViewModels;
using WebLibrary.Programs;
using DatastoreNS;

namespace UowLibrary.PaymentMethodNS
{
    public partial class PaymentMethodBiz : BusinessLayer<PaymentMethod>
    {


        #region InitializationData and InitializationDataAsync
        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return PaymentMethodData.DataArray();
            }
        }

        #endregion
    }
}
