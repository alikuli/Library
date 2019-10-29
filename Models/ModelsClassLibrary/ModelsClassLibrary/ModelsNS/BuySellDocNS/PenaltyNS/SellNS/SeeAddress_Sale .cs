
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class SeeAddress_Sale : PenaltyClassAbstract
    {
        public SeeAddress_Sale(BuySellDoc buySellDoc)
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


            return ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDoc.Payment_For_Full_Address();
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

                string str = string.Format("You will be charged (Rs{0}) to see the address. This amount is not returnable.", ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDoc.Payment_For_Full_Address());
                return str;
            }

        }
    }
}
