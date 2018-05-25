using InterfacesLibrary.SharedNS;

namespace InterfacesLibrary.DocumentsNS
{
    public interface ITransactionOrder : ICommonWithId
    {
        //IProduct Product { get; set; }
        double CurrMrsp { get; set; }
        double CurrCost { get; set; }
        double CurrDiscount { get; set; }
        double CurrSoldAt { get; set; }
        double QtyOrderedOriginal { get; set; }
        double QtyRemaining { get; set; }
        double QtyBilled { get; set; }

        void LoadFrom(ITransactionOrder iTransactionOrder);

    }
}
