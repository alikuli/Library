using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using System.Collections.Generic;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {

        public override ICommonWithId Factory()
        {
            ICommonWithId iCommonWithId = base.Factory();
            BuySellDoc buySellDoc = BuySellDoc.UnBox(iCommonWithId)
                ;
            buySellDoc.BuySellItems = new List<BuySellItem>();

            //int noOfDays = GetMoneyBackGuaranteeNumberOfDays();
            //buySellDoc.MoneyBackGuarantee.MarkTrue(noOfDays, UserName, UserId);

            return iCommonWithId;

        }


    }
}
