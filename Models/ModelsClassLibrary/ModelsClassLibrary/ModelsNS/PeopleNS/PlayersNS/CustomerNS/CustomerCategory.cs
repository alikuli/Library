using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    /// <summary>
    /// This is the customer category. This also decides what KIND of customer we have. Basic types are
    ///     CanSell: This one can issue an invoice:
    ///     CanBuy: This customer can buy services.
    /// </summary>
    public class CustomerCategory : CommonWithId, ICategory
    {
        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.CustomerCategory;
        }

    }
}