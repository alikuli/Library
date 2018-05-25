using System;
using InterfacesLibrary.SharedNS;

namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface ICustomer : IPlayer
    {
        ICategory CustomerCategory { get; set; }
        Guid CustomerCategoryId { get; set; }
        //ICollection<IDiscount> CustomerDiscounts { get; set; }
        //ICollection<IGeoLocation> ListOfGeoLocationsToWork { get; set; }
        //void LoadFrom(ICustomer c);
        //void SelfErrorCheck();
    }
}
