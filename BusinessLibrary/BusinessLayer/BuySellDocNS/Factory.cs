using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;


namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {

        public override ICommonWithId Factory()
        {
            ICommonWithId iCommonWithId = base.Factory();
            BuySellDoc buySellDoc = iCommonWithId as BuySellDoc;
            buySellDoc.IsNullThrowException("Boxing buySellDoc");
            buySellDoc.BuySellItems = new List<BuySellItem>();
            return iCommonWithId;

        }

    }
}
