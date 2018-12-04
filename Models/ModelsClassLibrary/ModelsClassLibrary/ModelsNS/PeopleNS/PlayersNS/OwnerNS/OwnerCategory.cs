using ModelsClassLibrary.ModelsNS;
using System.ComponentModel.DataAnnotations;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace  ModelsClassLibrary.ModelsNS.PlayersNS

{

    public class OwnerCategory:CommonWithId
    {
        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.OwnerCategory;
        }

    }
}