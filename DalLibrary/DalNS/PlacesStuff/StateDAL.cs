using System.Linq;
using AliKuli.Extentions;
using ApplicationDbContextNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;


namespace DalLibrary.DalNS
{
    public class StateDAL : Repositry<State>
    {


        public StateDAL(ApplicationDbContext db, IErrorSet errorsGlobal)
            : base(db, errorsGlobal)
        {
            ErrorsGlobal.ClassName = GetSelfClassName();
        }

        public override void ErrorCheck(State state)
        {
            //No duplicates allowed for state
            //the key is state + country
            //search for the name

            //if (state.Country == null)
            //{
            //    if (state.CountryId == null)
            //        throw new Exception(string.Format("The Country is null for the state: '{0}'. It is a required field. Please try again",
            //            state.Name));

            //    //Load the country
            //    state.Country = new CountryDAL(_db, _userGuid).FindFor(state.CountryId);
            //}
            //else
            //{
            //    if (state.CountryId == null)
            //        state.CountryId = state.Country.Id;
            //}



            //Now make sure there are no duplicates
            var nameFound = this.FindByName(state.Name, state.Country.Name);

            if (nameFound != null)
                if (nameFound.Id != state.Id)
                {
                    if (state.Name.IsNullOrEmpty())
                        throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException(string.Format("A blank state already exists for country: '{0}'. Try again",
                            state.Country.Name));
                    else
                        throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException(string.Format("The state '{0}' already exists for country: '{1}'. Try again",
                            state.Name,
                            state.Country.Name));
                }

            UpdateFieldsToTitleCaseAndUpperCase(state);

        }


        //public override string GetClassName(State entity)
        //{
        //    return entity.FullName();
        //}

        /// <summary>
        /// Finds the state by name. 
        /// </summary>
        /// <param name="stateName">Receives the States Name</param>
        /// <param name="countryName">Receives the countries name</param>
        /// <returns>Returns state or null if not found</returns>
        /// <exception cref="">AliKuli.Exceptions.MiscNS.NoDataException - If any of the parameters is blank</exception>
        public State FindByName(string stateName, string countryName)
        {
            //we are allowing empty states
            if (stateName.IsNullOrEmpty())
            {
                stateName = countryName;
            }


            if (countryName.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException(" No country name received. StateDAL.FindByName.");

            string stateNameLowercase = stateName.ToLower();
            string countryNameLowercase = countryName.ToLower();

            //if this is a dummy entry for the state
            //some states will not have an abrreviation...its possible...
            //therefore

            var state = this.SearchFor(x =>
                x.Name.ToLower() == stateNameLowercase &&
                x.Country.Name.ToLower() == countryNameLowercase)
                .FirstOrDefault();


            return state;


        }


        private static void UpdateFieldsToTitleCaseAndUpperCase(State entity)
        {
            entity.StateAbbreviation = entity.StateAbbreviation.IsNullOrEmpty() ? "" : entity.StateAbbreviation.ToUpper();
            entity.Name = entity.Name.ToTitleCase();
        }


    }
}
