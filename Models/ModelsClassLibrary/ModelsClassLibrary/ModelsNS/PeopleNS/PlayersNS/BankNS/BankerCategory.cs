using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    public class BankCategory : CommonWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BankCategory;
        }


        public virtual ICollection<Bank> Banks { get; set; }

    }
}