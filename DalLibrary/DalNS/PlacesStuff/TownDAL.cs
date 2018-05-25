using System;
using System.Linq;
using System.Web.Mvc;
using AliKuli.Extentions;
using ApplicationDbContextNS;
using ErrorHandlerLibrary.ExceptionsNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UserModelsLibrary.ModelsNS;


namespace DalLibrary.DalNS
{

    /// <summary>
    /// Town is optional. However, since this is the main field, an empty town will always be created when creating a City.
    /// </summary>
    public class TownDAL : Repositry<Town>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public TownDAL(ApplicationDbContext db, IErrorSet errorsGlobal)
            : base(db, errorsGlobal)
        {
            ErrorsGlobal.ClassName = GetSelfClassName();
        }


        //private long? GetCityId(Town entity)
        //{
        //    if(entity.CityID==0)
        //    {
        //        if(entity.City==null)
        //        {
        //            throw new Exception("There is no city defined! City is required. Try again please.");
        //        }
        //        else
        //        {
        //            return entity.City.Id;
        //        }
        //    }

        //    return entity.CityID;

        //}

        /// <summary>
        /// Sometimes, there is no town, then the city name is used.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public override string GetClassName(Town entity)
        //{
        //    return entity.FullName();

        //}
        public override void ErrorCheck(Town entity)
        {
            entity.SelfErrorCheck();

            //No duplicates allowed
            //search for the name

            Town town = FindByName(entity.Name, entity.CityID);

            if (isCreating)
            {
                if (town != null)
                    if (town.Id != entity.Id)
                        throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException(string.Format("Duplicate. The town: {0} exists already", entity.FullName()));
            }


            //We are allowed to have a city without a state... eg: London, but in that case, one would select the country from the list.
            //The blank state will have the same name as the country.

            //We are also allowed to have cities without towns! So, we will create a blank town for every city. The town will have the
            //same name as the city.

            //So, if a country is not using states, then we can just use a state with the same name as the country.
            //If the city has no town... then we use the same name as the city.


        }

        //public override void Fix(Town entity)
        //{
        //    base.Fix(entity);
        //    Fix_City(entity);

        //}

        #region Fix Methods
        private  void Fix_City(Town entity)
        {
            if (entity.City == null)
            {
                if (entity.CityID.IsNullOrEmpty())
                {
                    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("The City is Missing. TownDAL.Fix");
                }
                else
                {
                    entity.City = new CityDAL(_db, ErrorsGlobal).FindFor(entity.CityID);

                    if (entity.City == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("City Not Found. TownDAL.Fix");
                    }
                }
            }
            else //if (entity.City==null)
            {
                if (entity.CityID.IsNullOrEmpty())
                {
                    entity.CityID = entity.City.Id;
                }

            }

        }

        #endregion


        public Town Factory(string townName, string cityName, string stateName, string countryName, string comment)
        {
            Check_CityName_CountryName(cityName, countryName);
            stateName = If_stateName_Empty_Give_CountryName(stateName, countryName);
            townName = If_TownName_Empty_Give_CityName(townName, cityName);

            City city = FindCIty(cityName, stateName, countryName);
            Town town = Factory();

            town.City = city;
            town.CityID = city.Id;

            town.Name = townName;
            town.MetaData.Comment = comment;

            return town;
        }

        public override Town FindForName(string name)
        {
            throw new Exception("Use another method 'FindByName', this does not work. Not implemented. FindForNameFirstOrDefaultFor. TownDAL.");
        }
        public Town FindByName(string townName, Guid? cityId)
        {
            if (cityId.IsNullOrEmpty())
                throw new Exception("No city information passed. TownDAL.FindByName.");

            townName = If_TownName_Empty_Give_CityName_ForId(townName, cityId);
            Town town = FindTown(townName, cityId);

            return town;
        }

        public Town FindByName(string townName, string cityName, string stateName, string countryName)
        {
            Check_CityName_CountryName(cityName, countryName);
            stateName = If_stateName_Empty_Give_CountryName(stateName, countryName);
            townName = If_TownName_Empty_Give_CityName(townName, cityName);
            City city = FindCIty(cityName, stateName, countryName);

            return FindTown(townName, city.Id);
        }

        #region FindName Methods

        private Town FindTown(string townName, Guid? cityId)
        {
            if (!cityId.HasValue)
                throw new Exception("No city passed. TownDAL.FindTown");

            Town town = this.SearchFor(x =>
                    x.Name.ToLower() == townName.ToLower() &&
                    x.CityID.Equals(cityId))
                    .FirstOrDefault();

            return town;
        }

        private  City FindCIty(string cityName, string stateName, string countryName)
        {
            Check_CityName_CountryName(cityName, countryName);
            stateName = If_stateName_Empty_Give_CountryName(stateName, countryName);

            CityDAL cityDAL = new CityDAL(_db, ErrorsGlobal);
            City city = cityDAL.FindByName(cityName, stateName, countryName);

            if (city == null)
            {
                throw new ErrorHandlerLibrary.ExceptionsNS.NotFoundException(string.Format("No city named '{0}, {1}, {2}' found. FindByName.TownDAL.  ",
                    cityName.ToTitleCase(),
                    stateName.ToTitleCase(),
                    countryName.ToTitleCase()));
            }

            return city;
        }


        private static void Check_CityName_CountryName(string cityName, string countryName)
        {
            if (cityName.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No city name received. TownDAL.FindByName");


            if (countryName.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No country name received. TownDAL.FindByName");
        }

        private string If_TownName_Empty_Give_CityName_ForId(string townName, Guid? cityId)
        {


            if (!cityId.HasValue)
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("City Id missing. TownDal.FindByName");


            City city = new CityDAL(_db, ErrorsGlobal).FindFor(cityId);
            if (city == null)
            {
                throw new Exception("No city found. Check_FindByName_Params.TownDAL");

            }

            if (townName.IsNullOrEmpty())
            {
                townName = city.Name;
            }

            return townName;
        }
        private static string If_TownName_Empty_Give_CityName(string townName, string cityName)
        {
            if (townName.IsNullOrEmpty())
            {
                townName = cityName;
            }
            return townName;
        }

        /// <summary>
        /// Returns statename. If statename is empty, it gives it the country name.
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="stateName"></param>
        /// <param name="countryName"></param>
        /// <returns></returns>
        private static string If_stateName_Empty_Give_CountryName(string stateName, string countryName)
        {
            if (stateName.IsNullOrEmpty())
                stateName = countryName;

            return stateName;
        }

        #endregion




    }
}
