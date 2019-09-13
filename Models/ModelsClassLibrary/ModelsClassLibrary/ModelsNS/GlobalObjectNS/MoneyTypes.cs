
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS
{
    public class MoneyInfo
    {

        public MoneyInfo()
        {
            Refundable = new MoneyCountItemClass();
            Non_Refundable = new MoneyCountItemClass();
            TotalCash = new MoneyCountItemClass();
        }
        public MoneyCountItemClass Refundable { get; set; }
        public MoneyCountItemClass Non_Refundable { get; set; }
        public MoneyCountItemClass TotalCash { get; set; }
    }
}
