using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    public class CashierCategory : CommonWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.CashierCategory;
        }


        public virtual ICollection<Cashier> Cashiers { get; set; }

    }
}