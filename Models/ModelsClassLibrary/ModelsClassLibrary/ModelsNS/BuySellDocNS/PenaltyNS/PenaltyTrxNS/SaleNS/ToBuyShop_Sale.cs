
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class ToBuyShop_Sale : PenaltyClassAbstract
    {
        public ToBuyShop_Sale(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }

        public override decimal Percent 
        {
            get
            {

                return 100;

            }
        }
        public override decimal GetAmountToBasePenaltyOn()
        {


            return ModelsClassLibrary.MenuNS.MenuPathMain.Payment_To_Buy_Shop();
        }

        public override WhoPaysWhoENUM WhoPaysWhoEnum
        {
            get
            {

                return WhoPaysWhoENUM.OwnerPaysSystem;

            }
        }


        public override string Text
        {
            get
            {

                string str = string.Format("You will be charged (Rs{0}) to purchase the shop for {1} months. This amount is not returnable. The shop will show all your products in one spot. It is your choice where to open your shop, or shops.", GetAmountToBasePenaltyOn());
                return str;
            }

        }
    }
}
