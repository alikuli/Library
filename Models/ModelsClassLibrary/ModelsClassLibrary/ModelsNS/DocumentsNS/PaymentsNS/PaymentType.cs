
using InterfacesLibrary.DocumentsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS
{
    /// <summary>
    ///Unknown,
    ///Cash,
    /// Cheque,
    ///WireTransfer,
    ///Paypal,
    ///WesternUnion, etc

    /// </summary>
    public class PaymentType : CommonWithId, IPaymentType
    {

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.PaymentTerm;
        }
    }
}