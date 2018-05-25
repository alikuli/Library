using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AliKuli.Extentions;
using InterfacesLibrary.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    /// <summary>
    /// This holds the City information. Throws exception: AliKuli.Exceptions.MiscNS.RequiredDataMissingException
    /// </summary>
    public abstract class CityAbstract : CommonWithId, ICity
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.City;
        }
        [Display(Name = "State")]
        public string StateId { get; set; }

        public virtual IState State { get; set; }




        public virtual ICollection<ITown> Towns { get; set; }

        /// <summary>
        /// This does an error check if the record is properly filled. Throws exception: AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        /// Checks the following
        ///     Name not empty
        ///     State not null
        ///     Country not null
        ///     
        /// </summary>
        public override void SelfErrorCheck()
        {
            bool cityNameEmpty = Name.IsNullOrEmpty();

            if (cityNameEmpty)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("City does not have a name. CityAbstract.Fullname. ");

            //check for state
            if (State == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("State is null. CityAbstract.Fullname. ");


            if (State.Country == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Country is null. CityAbstract.Fullname. ");


        }

        /// <summary>
        /// Provides the full name of the state with country first for select lists and Index
        /// </summary>
        /// <returns></returns>
        public override string FullName()
        {

            SelfErrorCheck();
            return string.Format("{0} - {1}", State.FullName(), Name);
        }

        /// <summary>
        /// Displays name for Address format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            SelfErrorCheck();
            return string.Format("{0}, {1}", Name, State.ToString());
        }

        public string Abbreviation { get; set; }

    }


}