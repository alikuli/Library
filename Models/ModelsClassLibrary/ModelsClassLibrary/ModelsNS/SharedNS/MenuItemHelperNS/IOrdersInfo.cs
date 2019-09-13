using ModelsClassLibrary.ModelsNS.SharedNS;
namespace MenuItemHelperNS
{
    public interface IOrdersInfo
    {
        IMenuItemHelper BackOrdered { get; }
        IMenuItemHelper Canceled { get; }
        IMenuItemHelper Closed { get; }

        //void Initialize(decimal personOpenSaleOrdersInMoney, double personOpenSaleOrdersInQuantity, decimal personClosedSaleOrdersInMoney, double personClosedSaleOrdersInQuantity, decimal personInProccessSaleOrdersInMoney, double personInProccessSaleOrdersInQuantity, decimal personCanceledSaleOrdersInMoney, double personCanceledSaleOrdersInQuantity, decimal personBackSaleOrdersInMoney, double personBackSaleOrdersInQuantity);

        IMenuItemHelper InProccesss { get; }
        //IMenuItemHelper Credit { get; }
        //IMenuItemHelper Quotation { get; }
        IMenuItemHelper Total { get; }

    }
}
