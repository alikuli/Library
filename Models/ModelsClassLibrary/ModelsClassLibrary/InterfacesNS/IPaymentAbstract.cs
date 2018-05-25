using ModelsClassLibrary.Interfaces;
using ModelsClassLibrary.Models.Documents.Abstract;
using ModelsClassLibrary.Models.Documents.Abstract.Header;
using System;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;
namespace ModelsClassLibrary.Models.Documents.PaymentsNS
{
    public interface IAbstractPayment : IAbstractDocHeader
    {
        decimal Amount { get; set; }
        decimal Calculator_TotalPaymentApplied();
        Customer FromCustomer { get; set; }
        Guid? FromCustomerId { get; set; }
        PaymentType PaymentType { get; set; }
        Owner ToOwner { get; set; }
        Guid? ToOwnerId { get; set; }
        CounterClass TotalPaymentApplied { get; set; }
        void LoadFrom(IAbstractPayment iAbstractPayment);
        
    }
}
