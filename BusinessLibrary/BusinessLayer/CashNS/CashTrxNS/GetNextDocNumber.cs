using AliKuli.Extentions;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using System.Collections.Generic;
using System.Linq;


namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {
        public long GetNextDocNumber()
        {
            List<CashTrx> lst = FindAll().ToList();
            if (lst.IsNullOrEmpty())
                return 1;
            long maxExisting = lst.Max(x => x.DocNumber);
            return maxExisting + 1;

        }
    }
}
