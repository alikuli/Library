using EnumLibrary.EnumNS;
using InterfacesLibrary.PeopleNS.PlayersNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Owner is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    /// </summary>
    public class Owner : PlayerAbstract, IOwner
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.Owner;
        }

    }
}