
using InterfacesLibrary.DocumentsNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS
{
    public class PaymentType : CommonWithId, IPaymentType
    {

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.PaymentType;
        }

        public virtual ICollection<CashEncashmentTrx> CashEncashmentTrxs { get; set; }
    }
}