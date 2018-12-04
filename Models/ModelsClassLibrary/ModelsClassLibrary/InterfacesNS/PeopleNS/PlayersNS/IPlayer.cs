using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface IPlayer : ICommonWithId
    {
        void LoadFrom(IPlayer p);
        Person Person { get; set; }
        string PersonId { get; set; }

    }
}
