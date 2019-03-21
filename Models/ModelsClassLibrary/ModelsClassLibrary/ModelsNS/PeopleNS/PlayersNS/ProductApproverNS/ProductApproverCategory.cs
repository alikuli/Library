using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS
{
    public class ProductApproverCategory : CommonWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.OwnerCategory;
        }

    }
}
