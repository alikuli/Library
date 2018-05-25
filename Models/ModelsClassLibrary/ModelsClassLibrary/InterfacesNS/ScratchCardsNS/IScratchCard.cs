using System;
using System.Collections.Generic;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.ProductNS;

namespace InterfacesLibrary.ProductNS

{
    public interface IScratchCard:ICommonWithId
    {
        int AvailableUnits { get; set; }
        long BatchNumberCreation { get; set; }
        long BatchNumberPrinting { get; set; }
        DateTime ExpiryDate { get; set; }
        //string FullName();
        //void Initialize();
        DateTime IssueDate { get; set; }
        void LoadFrom(IScratchCard s);
        string Number16DigitFormat { get; }
        DateTime PrintDate { get; set; }
        ICollection<IScratchCardTrx> ScratchCardTrxs { get; set; }
        //void SelfErrorCheck();
        ScratchCardStatesENUM State { get; set; }
        string ToString();
        int TotalNumberOfUnits { get; set; }
    }
}
