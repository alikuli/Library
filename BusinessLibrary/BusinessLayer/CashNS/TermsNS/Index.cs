using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using WebLibrary.Programs;

namespace UowLibrary.PaymentTermNS
{
    public partial class PaymentTermBiz : BusinessLayer<PaymentTerm>
    {
        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Payment Terms";
            indexListVM.Show.EditDeleteAndCreate = true;

        }

    }
}
