using System;
using InterfacesLibrary.SharedNS;

namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface ICustomer : IPlayer
    {
        ICategory CustomerCategory { get; set; }
        string CustomerCategoryId { get; set; }
    }
}
