
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass
{
    public class MoneyType
    {

        public MoneyType()
        {
            Refundable = new MoneyCountItemClass();
            Non_Refundable = new MoneyCountItemClass();
        }
        public MoneyCountItemClass Refundable { get; set; }
        public MoneyCountItemClass Non_Refundable { get; set; }
    }
}
