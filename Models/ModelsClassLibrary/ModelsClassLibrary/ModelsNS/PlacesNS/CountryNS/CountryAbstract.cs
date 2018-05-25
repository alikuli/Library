using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    public abstract class CountryAbstract : CommonWithId, ICountry
    {
        public CountryAbstract()
        {
        }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.Country;
        }
        
        #region Properties
        [Display(Name = "Country Abbreviation")]
        [MaxLength(10)]
        public string Abbreviation { get; set; }




        #endregion


        #region FullName()
        /// <summary>
        /// Provides name for select lists and for Indexes. Displays the country name with abbreviation if available. Throws AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        /// </summary>
        /// <returns></returns>
        public override string FullName()
        {
            SelfErrorCheck();
            bool abbreviationEmpty = Abbreviation.IsNullOrEmpty();

            if (abbreviationEmpty)
                return Name;
            else
                return string.Format("{0}  [{1}]", Name, Abbreviation);

        }

        #endregion

        #region ToString()
        /// <summary>
        /// Displays the country name with abbreviation if available. Throws AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FullName();
        }

        #endregion


        #region ErrorCheck
        /// <summary>
        /// Self error check. Throws AliKuli.Exceptions.MiscNS.RequiredDataMissingException
        /// Checks:
        ///     Name is not empty.
        /// </summary>
        public override void SelfErrorCheck()
        {
            bool countryNameEmpty = Name.IsNullOrEmpty();
            if (countryNameEmpty)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Country name is empty. CountryAbstract.Fullname. ");
        }

        #endregion

        #region Input for Index description creation

        public override string Input1SortString
        {
            get
            {
                return "[" + Abbreviation.ToUpper() + "]";
            }
        }

        public override string Input2SortString
        {
            get
            {
                return Name;
            }
        }

        public override string NameInput1
        {
            get
            {
                return "Abbreviation";
            }
        }

        public override string NameInput2
        {
            get
            {
                return "Name";
            }
        }

        #endregion

    }
}