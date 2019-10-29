using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.SharedNS;
using InterfacesLibrary.SharedNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.NonReturnableNS
{
    public class NonReturnableTrx: CommonWithId
    {
        public static NonReturnableTrx UnBox(ICommonWithId ic)
        {
            NonReturnableTrx nrt = ic as NonReturnableTrx;
            nrt.IsNullThrowException();
            return nrt;
        } 


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.NonReturnableTrx;
        }



        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            NonReturnableTrx nrt = NonReturnableTrx.UnBox(icommonWithId);
            Amount = nrt.Amount;


        }

        
        public decimal Amount { get; set; }



        public string BuySellDocId { get; set; }
        public BuySellDoc BuySellDoc { get; set; }
    }
}
