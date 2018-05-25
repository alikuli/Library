using System;
using System.Collections.Generic;
using InterfacesLibrary.PlacesNS;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    
    public class City : CityAbstract
    {


        public City()
        {
            //We need to initialize this because every city MUST have one blank town, and that needs to be added.
            //when we add that blank town, we need to initialize this field, thereofore it is being done here.
            Towns = new List<ITown>();
        }



        #region Country

        public ICountry Country
        {
            get
            {
                return State.Country;
            }
        }
        public string CountryId
        {
            get
            {
                return State.CountryId;
            }
        }

        #endregion

        /// <summary>
        /// This lists all the discounts concerning this city
        /// </summary>
        //public virtual ICollection<IDiscount> CityDiscounts { get; set; }


    }
}