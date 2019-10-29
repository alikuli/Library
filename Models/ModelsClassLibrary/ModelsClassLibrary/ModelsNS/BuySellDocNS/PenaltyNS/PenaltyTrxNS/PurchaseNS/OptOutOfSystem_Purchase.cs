
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class OptOutOfSystem_Purchase : PenaltyClassAbstract
    {
        public OptOutOfSystem_Purchase(BuySellDoc buySellDoc)
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


            return ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDoc.Get_Opt_Out_Fee();
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

                string str = string.Format("You will be charged (Rs{0}) to opt out of the system. This amount is not returnable. You are advised not to opt out of the system. The system has been created to protect you. If you opt out, we will not be able to protect you.", GetAmountToBasePenaltyOn());
                return str;
            }

        }
    }
}
