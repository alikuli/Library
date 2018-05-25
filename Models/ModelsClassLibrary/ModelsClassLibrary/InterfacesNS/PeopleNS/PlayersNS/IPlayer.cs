using System;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS;
using UserModels;
using UserModelsLibrary.ModelsNS;

namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface IPlayer : ICommonWithId
    {
        void LoadFrom(IPlayer p);
        ApplicationUser User { get; set; }
        string UserId { get; set; }

    }
}
