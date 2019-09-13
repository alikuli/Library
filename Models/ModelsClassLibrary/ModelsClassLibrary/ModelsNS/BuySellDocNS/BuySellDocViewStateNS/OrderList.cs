
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySeelDocViewStateNS
{
    /// <summary>
    /// This contains all the varibles for the BuySellDoc.ListOrders and control it's state
    /// </summary>
    public class OrderList
    {
        public virtual string IconForEditView { get; set; }
        public virtual bool IsDeleteEnabled { get { return true; } }
        public virtual string BadgeClass { get { return ""; } }
    }
}
