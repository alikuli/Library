using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    
    public abstract class StateAbstract : CommonWithId, IState
    {

        /// <summary>
        /// The name can be empty for states because there are some cities that are NOT in a state, like London
        /// Everytime a country is created an empty state (with no name) will be created. This will be used in the cases
        /// where a city has no state. We can't leave the State empty completely because we will not have a connection to the country.
        /// I do not want to put a name of the country in the name because then the data will not be normalized and if someone changes
        /// the name of the country, the name of the state will also have to be managed. Therefore, every country will have at least ONE
        /// state/province... an empty one.
        /// </summary>


        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.State;
        }

        [Display(Name = "Abrreviation")]
        [MaxLength(10, ErrorMessage = "Max length allowed is {0} charecters")]
        public string Abbreviation { get; set; }

        public virtual string CountryId { get; set; }
        public virtual Country Country { get; set; }



        /// <summary>
        /// Provides name for select lists and for Index method. Throws AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        /// </summary>
        /// <returns></returns>
        public override string FullName()
        {
            return this.ToString();
        }


        /// <summary>
        /// Does a self error check. Checks the following. Throws AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        ///     Name not empty
        ///     Country not null
        /// </summary>
        public override void SelfErrorCheck()
        {
            bool isCountryEmpty = Country == null || CountryId.IsNullOrEmpty();
            if (isCountryEmpty)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException(string.Format("No country found for state: {0}. StateAbstract.MakeFullStateName", this.FullName()));
        }



        /// <summary>
        /// Provides an address format string. Throws AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            bool isStateAbbrevEmpty = Abbreviation.IsNullOrWhiteSpace();
            bool isStateNameEmpty = Name.IsNullOrWhiteSpace();

            if (Country.IsNull())
            {
                throw new Exception("Country name is null.");
            }

            if (isStateAbbrevEmpty && isStateNameEmpty)
                return Country.FullName() + " (No State)";

            if (isStateAbbrevEmpty)
                return string.Format("{0}, {1} ", Country.FullName(), Name);

            return string.Format("{0}, {1} [{2}]", Country.FullName(), Name, Abbreviation);
        }
    }
}