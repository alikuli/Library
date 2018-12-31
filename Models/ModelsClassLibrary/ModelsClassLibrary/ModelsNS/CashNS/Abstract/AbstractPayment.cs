using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using InterfacesLibrary.DocumentsNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentNS
{
    public abstract class AbstractPayment : AbstractDocHeader, IAbstractDocHeader
    {


        #region Properties

        #region To Owner
        public Guid? ToOwnerId { get; set; }
        public Owner ToOwner { get; set; }

        #endregion


        #region From Customer
        public Guid? FromCustomerId { get; set; }
        public Customer FromCustomer { get; set; }

        #endregion


        public decimal Amount { get; set; }
        public Guid PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        
        #endregion
        #region CounterClass
        /// <summary>
        /// This keeps track of the total payment applied.
        /// </summary>
        public CounterClass TotalPaymentApplied { get; set; }
        
        #endregion

        #region CounterClass Calculator/Delegate
        public virtual decimal Calculator_TotalPaymentApplied()
        {
            throw new NotImplementedException("NotImplementedException. " + MetaData.GetSelfMethodName());
        }
        
        #endregion

        #region SelferrorCheck
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            //You could also check the amount here.

            Check_FromCustomer();
            Check_ToOwner();
            Check_TotalPaymentApplied();

        }

        #region SelferrorCheck
        private void Check_TotalPaymentApplied()
        {
            if (!TotalPaymentApplied.IsCalculated)
                throw new Exception("Total Payment Applied has not been calculated. AbstractPayment.Check_TotalPaymentApplied");

        }

        private void Check_ToOwner()
        {
            if (ToOwner.IsNull())
                throw new Exception("The To Owner in Payments is empty. AbstractPayment.Check_ToOwner");

            if (ToOwnerId.IsNullOrEmpty())
                throw new Exception("The To Owner Id in Payments is empty. AbstractPayment.Check_ToOwner");

        }

        private void Check_FromCustomer()
        {
            if (FromCustomer.IsNull())
                throw new Exception("The Customer in Payments is empty. AbstractPayment.Check_ToCustomer");

            if (FromCustomerId.IsNullOrEmpty())
                throw new Exception("The Customer Id in Payments is empty. AbstractPayment.Check_ToCustomer");
        }


        #endregion
        
        #endregion

        #region LoadFrom
        public void LoadFrom(AbstractPayment a)
        {

            LoadFrom(a as AbstractDocHeader);

            Amount = a.Amount;
            FromCustomer = a.FromCustomer;
            FromCustomerId = a.FromCustomerId;
            PaymentType = a.PaymentType;
            ToOwner = a.ToOwner;
            ToOwnerId = a.ToOwnerId;
        } 
        #endregion
    }
}