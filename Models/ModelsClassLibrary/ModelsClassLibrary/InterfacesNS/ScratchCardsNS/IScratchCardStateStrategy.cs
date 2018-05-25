using System;
namespace InterfacesLibrary.ProductNS

{
    public interface IScratchCardState_Strategy
    {
        bool CanBuy { get; }
        bool CanSell { get;}

        bool CanUse { get; }
    }
}
