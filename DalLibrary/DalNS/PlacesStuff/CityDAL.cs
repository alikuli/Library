using System.Linq;
using System.Reflection;
using AliKuli.Extentions;
using ApplicationDbContextNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UserModelsLibrary.ModelsNS;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// Required. Whenevever a new city is created, a new "empty"  town will also be created.
    /// </summary>
    public class CityDAL : Repositry<City>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public CityDAL(ApplicationDbContext db, IErrorSet ierrorSet)
            : base(db, ierrorSet)
        {
            ErrorsGlobal.ClassName = GetSelfClassName();
        }



        //public override string GetClassName(City entity)
        //{
        //    return entity.FullName();
        //}



        /// <summary>
        ///We are allowed to have a city without a state... eg: London, but in that case, one would select the country from the list.
        ///The blank state will have the same name as the country.
        ///We are also allowed to have cities without towns! So, we will create a blank town for every city. The town will have the
        ///same name as the city.
        ///So, if a country is not using states, then we can just use a state with the same name as the country.
        ///If the city has no town... then we use the same name as the city.
        /// </summary>
        /// <param name="entity"></param>
        //public override void Fix(City entity)
        //{
        //    base.Fix(entity);

        //    Fix_State(entity);


        //    CreateEmptyTownForCity(entity);
        //}

        //public override void ErrorCheck(City entity)
        //{
        //    base.ErrorCheck(entity);
        //    OnlyOneCityPerProvincePerCountryAllowed(entity);

        //}
        private void Fix_State(City entity)
        {
            if (entity.State == null)
            {
                if (entity.StateId.ToString().IsNullOrEmpty())
                {
                    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException(string.Format("The state for city: '{0}' is missing.", entity.Name));
                }
                else
                {
                    entity.State = new StateDAL(_db, ErrorsGlobal).FindFor(entity.State.Id);

                    if (entity.State == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException(string.Format("The state for city: '{0}' not found. ", entity.Name));
                    }
                }
            }
            else
            {
                if (entity.StateId.ToString().IsNullOrEmpty())
                {
                    entity.StateId = entity.State.Id;
                }

            }

        }


        /// <summary>
        /// No duplicate Cities for the same state and country....
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cityFound"></param>
        /// <param name="cityExists"></param>
        private void OnlyOneCityPerProvincePerCountryAllowed(City entity)
        {
            var cityFound = FindByName(entity.Name, entity.State.Name, entity.State.Country.Name);
            bool cityExists = cityFound != null;

            if (cityExists)
            {
                //taking care of edit
                bool sameCity = cityFound.Id == entity.Id;
                if (!sameCity)
                {
                    throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException(string.Format("A city for state '{0}' already exists. Try again", entity.State.Name));
                }


            }
        }

        private void CreateEmptyTownForCity(City entity)
        {


            //this creates an empty town for the city if it does not exist.
            TownDAL townDAL = new TownDAL(_db,ErrorsGlobal);
            Town town = townDAL.Factory();

            //the blank town has the same name as the city.
            town.Name = entity.Name;
            town.MetaData.IsAutoCreated = true;
            town.MetaData.Comment = "Every city has a blank town.";
            entity.Towns.Add(town);

        }



        /// <summary>
        /// Finds the city for the particular state and country
        /// </summary>
        /// <param name="cityName">The city name</param>
        /// <param name="stateName">the state name</param>
        /// <param name="countryName">the country name</param>
        /// <returns>the city or null if empty</returns>
        /// <exception cref="">AliKuli.Exceptions.MiscNS.NoDataException - if parameters are incomplete.</exception>
        /// <exception cref="">AliKuli.Exceptions.PlacesNS.StateNotFoundException - if state not in db.</exception>
        public City FindByName(string cityName, string stateName, string countryName)
        {

            if (cityName.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No city name received. CityDAL.FindByName.");

            if (countryName.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No country name received. CityDAL.FindByName.");

            if (stateName.IsNullOrEmpty())
            {
                stateName = countryName;
            }


            State state = new StateDAL(_db, ErrorsGlobal).FindByName(stateName, countryName);

            if (state == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.StateNotFoundException("State Not found. CityDAL.FindByName.");

            City city = this.SearchFor(x =>
                x.Name.ToLower() == cityName.ToLower() &&
                x.StateId == state.Id)
                .FirstOrDefault();
            
            return city;

        }

    }
}
