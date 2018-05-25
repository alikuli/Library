using System.Collections.Generic;
using InterfacesLibrary.SharedNS;
namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface IVendor
    {
        ICategory Category { get; set; }
        long CategoryId { get; set; }
        //ICollection<IDiscount> Discounts { get; set; }
        void LoadFrom(IVendor p);
    }
}
