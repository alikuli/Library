using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    public class SalesmanCategory : CommonWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.SalesmanCategory;
        }

    }
}