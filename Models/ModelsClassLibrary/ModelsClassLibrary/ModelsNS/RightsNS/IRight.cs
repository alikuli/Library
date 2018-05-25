using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using System;
using UserModels;

namespace ModelsClassLibrary.RightsNS
{
    public interface IRight: ICommonWithId
    {
        bool Create { get; set; }
        bool CreateChildren { get; set; }
        bool Delete { get; set; }
        bool DeleteActually { get; set; }
        bool Retrieve { get; set; }
        ClassesWithRightsENUM RightsFor { get; set; }
        bool Update { get; set; }
        ApplicationUser User { get; set; }
        string UserId { get; set; }
    }
}
