
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class Opted_Out_Of_System_Sale : PenaltyClassAbstract
    {
        public Opted_Out_Of_System_Sale(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }

        public override decimal Percent
        {
            get
            {

                return 1;
            }
        }
        public override decimal GetAmountToBasePenaltyOn()
        {

            return PenaltyClassAbstract.Get_Opt_Out_Fee();
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

                string str = string.Format("You are opting out of the system. You will be charged a NON-RETURNABLE fee of {0}. Later, if you decide to join the system again, it is possible,but the fee will not be returned, furthermore, you will be charged an additional amount of commission payable for using the system. We advise you not to opt out.",
                    GetAmountToBasePenaltyOn());
                return str;
            }
        }

    }
}
