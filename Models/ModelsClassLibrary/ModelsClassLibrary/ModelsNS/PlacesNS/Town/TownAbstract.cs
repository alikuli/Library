using System;
using System.ComponentModel.DataAnnotations.Schema;
using InterfacesLibrary.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    public class TownAbstract : CommonWithId, ITown
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.Town;
        }
        #region City

        public string CityID { get; set; }
        public virtual ICity City { get; set; }


        #endregion


        /// <summary>
        /// Displays the name for Index.
        /// </summary>
        /// <returns></returns>
        public override string FullName()
        {
            SelfErrorCheck();
            return string.Format("{0} - {1}", City.FullName(), Name);
        }




        /// <summary>
        /// Self Error Check. Checks:
        ///     City not null
        ///     State not null
        ///     country not null
        /// </summary>
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            if (City == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("City is null. TownAbstract.Fullname. ");

            if (City.State == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("State is null. TownAbstract.Fullname. ");

            if (City.State.Country == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Country is null. TownAbstract.Fullname. ");
        }

        //#region Country

        public string StateId
        {
            get
            {
                return City.StateId;
            }
        }

        public State State
        {
            get
            {
                return (State)City.State;
            }
        }

        public string StateName
        {
            get
            {
                return City.State.Name;
            }
        }

        public string CountryId
        {
            get
            {
                SelfErrorCheck();
                return City.State.CountryId;
            }
        }


        /// <summary>
        /// This is the country in which the town is...
        /// </summary>
        [NotMapped]
        public ICountry Country
        {
            get
            {
                //SelfErrorCheck();
                return City.State.Country;
            }
        }

        //this is the country abbreviation.
        [NotMapped]
        public string CountryAbbreviaton
        {
            get
            {
                return Country.Abbreviation;
            }
        }


        //#endregion




        /// <summary>
        /// Displays the name for address format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            SelfErrorCheck();
            return string.Format("{0}, {1}", Name, City.ToString());

        }



        #region ITown Members


        public string Abbreviation { get; set; }

        #endregion
    }
}