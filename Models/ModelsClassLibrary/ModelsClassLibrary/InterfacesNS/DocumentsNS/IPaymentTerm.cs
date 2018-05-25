namespace InterfacesLibrary.DocumentsNS
{
    public interface IPaymentTerm
    {
        decimal EarlyPaymentDiscount { get; set; }
        string FullName();
        void LoadFrom(IPaymentTerm p);
        int NoOfDaysCredit { get; set; }
        int NoOfDaysEarlyPayment { get; set; }
    }
}
