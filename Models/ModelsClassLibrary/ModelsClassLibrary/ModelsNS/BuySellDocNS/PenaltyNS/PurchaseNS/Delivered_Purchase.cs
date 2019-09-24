
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class Delivered_Purchase : PenaltyClassAbstract
    {
        public Delivered_Purchase(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }

        //public override decimal Percent
        //{
        //    get
        //    {
        //        return 0;
        //    }
        //}
        public override decimal GetAmountToBasePenaltyOn()
        {

            return 0;
        }

        //public override WhoPaysWhoENUM WhoPaysWhoEnum
        //{
        //    get
        //    {
        //        return WhoPaysWhoENUM.Unknown;
        //    }
        //}


        public override string Text
        {
            get
            {
                if (BuySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                {
                }

                if (BuySellDoc.BuySellDocViewState.IsNull())
                    return "BuySellDoc.BuySellDocViewState.IsNull";
                return BuySellDoc.BuySellDocViewState.OL_Cancel_Button_ToolTip  + BuySellDoc.GetDateGuaranteeExpires().ToShortDateString();


            }
        }
    }
}
