

using System;
using InterfacesLibrary.PeopleNS.PlayersNS;
using Microsoft.AspNet.Identity;


namespace InterfacesLibrary.PeopleNS
{
    public interface IPersonAbstract : IPerson
    {
        void LoadAllUserFields(IUser user);
        IUser User { get; set; }
        Guid UserId { get; set; }
    }
}
