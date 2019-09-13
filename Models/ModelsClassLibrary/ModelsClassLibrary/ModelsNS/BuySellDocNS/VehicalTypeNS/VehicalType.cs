using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS
{
    public class VehicalType : CommonWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.VehicalType;
        }

        public virtual ICollection<BuySellDoc> BuySellDocs { get; set; }
        public virtual ICollection<FreightOfferTrx> FreightOfferTrxs { get; set; }
    }
}
