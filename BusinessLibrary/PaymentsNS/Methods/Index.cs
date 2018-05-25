using System;
using System.Reflection;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ViewModels;
using WebLibrary.Programs;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PaymentMethodNS
{
    public partial class PaymentMethodBiz : BusinessLayer<PaymentMethod>
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Payment Methods";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
