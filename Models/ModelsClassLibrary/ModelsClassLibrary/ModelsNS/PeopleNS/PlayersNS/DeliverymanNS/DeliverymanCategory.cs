using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    /// <summary>
    /// This is the deliverymancategory. This also decides what KIND of deliveryman we have so we can apply discounts accorrdingly 
    /// </summary>
    public class DeliverymanCategory : CommonWithId, ICategory
    {
        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.DeliverymanCategory;
        }

    }
}