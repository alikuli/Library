using System;
using System.Collections.Generic;
using InterfacesLibrary.PlacesNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using UserModelsLibrary.ModelsNS;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    /// <summary>
    /// This stores the town.  Sometimes cities do not have towns. For those cities, whenever we create a city, we
    /// create a default town. This default town has the same name as the city. When the index is being created, 
    /// then this will not be allowed  to be edited. tHE AutoCreated field is set to be true.
    /// </summary>
    public class Town : TownAbstract
    {
        //public virtual ICollection<Address> Addresses { get; set; }
        //public virtual ICollection<Worker> Workers { get; set; }


        /// <summary>
        /// This lists all the discounts concerning this city
        /// </summary>
        //public virtual ICollection<Discount> Discounts { get; set; }

        //public virtual ICollection<User> Users { get; set; }
        ////public virtual ICollection<Address> Addresses { get; set; }


        
    }
}