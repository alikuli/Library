using System;
using AliKuli.Extentions;

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class PaymentsAppliedInvoiceDAL : Repositry<PaymentAppliedInvoice>
    {

        //private ApplicationDbContext db;
        //private string user;

        public PaymentsAppliedInvoiceDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }



        public override void Fix(PaymentAppliedInvoice entity)
        {
            base.Fix(entity);
            //Fix_Name(entity);
            Fix_Invoice(entity);
        }

        private void Fix_Invoice(PaymentAppliedInvoice entity)
        {
            if (entity.Invoice.IsNull())
            {
                if (entity.InvoiceId.IsNullOrEmpty())
                {
                    throw new Exception("Invoice not passed");
                }
                else
                {
                    entity.Invoice = new InvoiceDAL(_db, _user).FindFor(entity.Invoice.Id);

                    if (entity.Invoice.IsNull())
                    {
                        throw new Exception("Invoice not found");
                    }
                }
            }
            else
            {
                if (entity.InvoiceId.IsNullOrEmpty())
                {
                    entity.InvoiceId = entity.Invoice.Id;
                }
            }
        }

        public override void ErrorCheck(PaymentAppliedInvoice entity)
        {
            base.ErrorCheck(entity);
        }

    }
}
