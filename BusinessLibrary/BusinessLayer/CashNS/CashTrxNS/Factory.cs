using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using System.Collections.Generic;
using System.Linq;


namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {
        public override ICommonWithId Factory()
        {

            CashTrx paymentTrx = base.Factory() as CashTrx;
            paymentTrx.CashTypeEnum = CashTypeENUM.Unknown;

            return paymentTrx as ICommonWithId;
        }
    }
}
