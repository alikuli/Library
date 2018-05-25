using System;
using InterfacesLibrary.PeopleNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.PeopleNS.PlayersNS;

namespace InterfacesLibrary.DocumentsNS
{
    public interface IAbstractPayment : IAbstractDocHeader
    {
        decimal Amount { get; set; }
        decimal Calculator_TotalPaymentApplied();
        ICustomer FromCustomer { get; set; }
        Guid? FromCustomerId { get; set; }
        IPaymentType PaymentType { get; set; }
        IOwner ToOwner { get; set; }
        Guid? ToOwnerId { get; set; }
        ICounterClass TotalPaymentApplied { get; set; }
        void LoadFrom(IAbstractPayment iAbstractPayment);

    }
}
