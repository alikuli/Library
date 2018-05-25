using System;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
namespace InterfacesLibrary.GeneralLedgerNS
{
    public interface IGlTrx: ICommonWithId
    {
        decimal Amount { get; set; }
        DebitCreditENUM DebitCreditEnum { get; set; }
        IGlAccount GlAccount { get; set; }
        long GlAccountId { get; set; }
        //void Initialize();
        void LoadFrom(IGlTrx g);
    }
}
