using DelegatesLibrary.DelegateNS;

namespace InterfacesLibrary.SharedNS
{
    public interface ICounterClass
    {
        decimal Amount { get; }
        void Calculator(CalculatorDelegate calculator);
        bool IsCalculated { get; set; }

    }
}
