
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class BuySellOpt_Sale : PenaltyClassAbstract
    {
        public BuySellOpt_Sale(BuySellDoc buySellDoc)
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

                return WhoPaysWhoENUM.CustomerPaysSystem;

            }
        }


        public override string Text
        {
            get
            {

                string str = string.Format("You will be charged a fee of (Rs{0}) to Opt out of the system. This amount is not returnable. Note, you are advised NOT to opt out of the sysatem. We will not be able to protect your sale. However, decision is yours.", GetAmountToBasePenaltyOn());
                return str;
            }

        }
    }
}
