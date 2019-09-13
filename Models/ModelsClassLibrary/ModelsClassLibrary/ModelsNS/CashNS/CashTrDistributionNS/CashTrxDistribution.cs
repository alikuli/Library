using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrDistributionNS
{
    //this holds how a cash trx is dirtributed.
    public class CashTrxDistribution : CommonWithId
    {

        public virtual string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }

        public decimal Amount { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.CashTrxDistribution;
        }
    }
}
