using System;
using System.Linq;
using AliKuli.Extentions;
using InterfacesLibrary.AddressNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// Email is not presised. You need to move it to the db.
    /// Contact Phone is not presisted. Move it to the Db.
    /// </summary>
    public class AddressDAL : Repositry<AddressWithId>
    {

        public AddressDAL(ApplicationDbContext db, IUser user)
            : base(db)
        {
            Errors.ResetLibAndClass("AddressDAL");
        }



        public override void ErrorCheck(AddressWithId entity)
        {


            //Do this only if this is a NEW record....
            //check to see if this AddressWithId exists... if it exists, throw exist error...
            if (isCreating)
                Check_CheckForDuplicateAddress(entity);

            Check_User(entity);
            Check_Town(entity);
            Check_Must_Be_ConsignTo_Or_ShipTo_Or_InformToAddress(entity);

            base.ErrorCheck(entity);
        }

        #region ErrorCheck Helpers
        private void Check_Must_Be_ConsignTo_Or_ShipTo_Or_InformToAddress(AddressWithId entity)
        {
            //bool allFalse = entity.Is == entity.IsInformToAddressWithId == entity.IsShipToAddressWithId == false;

            //if (entity.AllAddressEmpty)
            //{
            //    string e = ("You have not selected the type of address. Consign to, Ship To, or Inform To, or a multiple any. AddressDAL.Check_Must_Be_ConsignTo_Or_ShipTo_Or_InformToAddress.");
            //    Errors.Add(e, "Check_Must_Be_ConsignTo_Or_ShipTo_Or_InformToAddress");
            //}
        }

        private void Check_CheckForDuplicateAddress(AddressWithId entity)
        {
            //var addressInDb = FindSameAddressInDb(entity);

            //if (addressInDb != null)
            //{
            //    throw new AliKuli.Exceptions.MiscNS.NoDuplicateException("AddressWithId exists in db. AddressDAL.ErrorCheck");
            //}
        }

        /// <summary>
        /// By the time this is invoked, everything should be fixed in the AddressWithId record
        /// </summary>
        /// <param name="entity"></param>
        private  void Check_Town(AddressWithId entity)
        {
            if (entity.Town.IsNull())
            {
                string e = ("No Town for address. AddressWithId is required. AddressDAL.ErrorCheck.ErrorCheck_Town");
                Errors.Add(e, "Check_Town");

            }

            if (entity.TownId.IsNullOrEmpty())
            {
                string e = ("No Town Id for address. AddressWithId is required. AddressDAL.ErrorCheck. ErrorCheck_Town");
                Errors.Add(e, "Check_Town");

            }
        }

        private  void Check_User(AddressWithId entity)
        {
            if (entity.User.IsNull())
            {
                string e = ("No user found. User is required. AddressDAL.ErrorCheck.Fix_UserField");
                Errors.Add(e, "Check_User");

            }

            if (entity.UserId.IsNullOrEmpty()) //Defensive programming...
            {
                string e = ("No user. User is required. AddressDAL.ErrorCheck.Fix_UserField");
                Errors.Add(e, "Check_User");

            }
        }

        #endregion

        public override void Fix(AddressWithId entity)
        {
            base.Fix(entity);

            //check to see if AddressWithId has a name...
            //Fix_Name(entity);

            Fix_UserField(entity);
            Fix_TownField(entity);
            //Fix_City(entity);
            //Fix_State(entity);
            //Fix_Country(entity);

            //Load the town...
            //SelectCityStateCountryAutomatically(entity);


        }

        //private void Fix_Country(AddressWithId entity)
        //{
        //    if (entity.Address.Country.IsNull())
        //    {
        //        if (entity.Address.CountryId.IsNullOrEmpty())
        //            throw new Exception("No Country for address. AddressWithId is required. 0.AddressDAL.ErrorCheck.Fix_Country");

        //        entity.Address.Town.City.State.Country = new CountryDAL(_db, _user).FindFor(entity.Town.City.State.CountryId);

        //        if (entity.Address.Country.IsNull())
        //            throw new Exception("No Country found for address. AddressWithId is required. 1.AddressDAL.ErrorCheck.Fix_Country");

        //    }
        //    else
        //    {
        //        if (entity.CountryId.IsNullOrEmpty())
        //            entity.Town.City.State.CountryId = entity.Town.City.State.Country.Id;
        //    }

        //    entity.CountryName = entity.Town.City.State.Country.Name;
        //}

        //private static bool IsCountryNull(AddressWithId entity)
        //{
        //    return entity.Town.City.State.Country == null;
        //}

        //private void Fix_State(AddressWithId entity)
        //{
        //    if (entity.State.IsNull())
        //    {
        //        if (entity.StateId.IsNullOrEmpty())
        //            throw new Exception("No State for address. AddressWithId is required. 0.AddressDAL.ErrorCheck");

        //        entity.Town.City.State = new StateDAL(_db, _user).FindFor(entity.Town.City.StateId);

        //        if (entity.StateId.IsNullOrEmpty())
        //            throw new Exception("No State found for address. AddressWithId is required. 1.AddressDAL.ErrorCheck");

        //    }
        //    else
        //    {
        //        if (entity.StateId.IsNullOrEmpty())
        //            entity.Town.City.StateId = entity.Town.City.State.Id;
        //    }

        //    entity.StateName = entity.Town.City.State.Name;
        //}

        //private void Fix_City(AddressWithId entity)
        //{
        //    if (entity.City.IsNull())
        //    {
        //        if (entity.CityId.IsNullOrEmpty())
        //            throw new Exception("No City for address. AddressWithId is required. 0.AddressDAL.ErrorCheck");

        //        entity.Town.City = new CityDAL(_db, _user).FindFor(entity.Town.CityID);

        //        if (entity.City.IsNull())
        //            throw new Exception("No City found for address. AddressWithId is required. 1.AddressDAL.ErrorCheck");

        //    }
        //    else
        //    {
        //        if (entity.CityId.IsNullOrEmpty())
        //            entity.Town.CityID = entity.Town.City.Id;
        //    }

        //    entity.CityName = entity.Town.City.Name;
        //}



        #region Fix Helpers
        private static void Fix_TownField(AddressWithId entity)
        {
            if (entity.AddressComplex.TownName.IsNull())
            {
                if (entity.TownId.IsNullOrEmpty())
                    throw new Exception("No Town for address. AddressWithId is required. AddressDAL.ErrorCheck");

                entity.Town = new TownDAL(_db, _user).FindFor(entity.TownId);

                if (entity.Town.IsNull())
                    throw new Exception("No Town for address. AddressWithId is required. AddressDAL.ErrorCheck");
            }
            else
            {
                if (entity.TownId.IsNullOrEmpty())
                    entity.TownId = entity.Town.Id;
            }

        }

        private static void Fix_UserField(AddressWithId entity)
        {
            if (entity.User.IsNull())
            {
                if (entity.UserId.IsNullOrEmpty()) //Defensive programming...
                    throw new Exception("No user. User is required. AddressDAL.ErrorCheck.Fix_UserField");
                else
                    entity.User = new UserDAL(_db, _user).FindUserById(entity.UserId??Guid.Empty);
            }
            else
            {
                if (entity.UserId.IsNullOrEmpty()) //Defensive programming...
                    entity.UserId = entity.User.Id;
            }


        }

        public AddressWithId SelectCityStateCountryAutomatically(AddressWithId entity)
        {
            if (entity == null)
                return null;



            Check_Town_Exists(entity);
            Check_Town_Has_A_City(entity);
            Check_There_Is_A_State_In_City_Record(entity);
            Check_There_Is_A_Country_In_State_Record(entity);

            return entity;
        }



        #region SelectCityStateCountryAutomaticly Helpers
        private static void Check_There_Is_A_Country_In_State_Record(IAddressWithId entity)
        {

            //CountryDAL countryDAL = new CountryDAL(_db, _user);

            ////Now we have a state in the city....
            ////Do we have a country in the state?
            //if (entity.Detail.Town.City.State.Country == null)
            //{
            //    //There was no country found in the state
            //    if (GuidHelper.IsNullOrEmpty(entity.Detail.Town.City.State.CountryId))
            //    {
            //        //There is no country for this state
            //        throw new AliKuli.Exceptions.MiscNS.RequiredDataMissingException(string.Format("No country in state named: '{3}' in city named: '{2}' in AddressWithId Name '{0}' FOR  Town: '{1}'. AddressDAL.SelectCityStateCountryAutomatically. AddressDAL.SelectCityStateAuto",
            //            entity.Name,
            //            entity.Detail.Town,
            //            entity.Detail.State,
            //            entity.Detail.Country));

            //    }
            //    else
            //    {
            //        //Check if there is an id for the country available
            //        var country = countryDAL.FindFor(entity.Detail.Town.City.State.CountryId);

            //        bool countryNotFound = country == null;

            //        if (countryNotFound)
            //        {
            //            //There is no country for this state
            //            throw new AliKuli.Exceptions.MiscNS.RequiredDataMissingException(string.Format("No country with id = '{4}' exists in db for State '{3}' in city named: '{2}' in AddressWithId Name '{0}' FOR  Town: '{1}'. AddressDAL.SelectCityStateCountryAutomatically. AddressDAL.SelectCityStateAuto",
            //            entity.Name,
            //            entity.Detail.Town,
            //            entity.Detail.City,
            //            entity.Detail.State,
            //            entity.Detail.Country));

            //        }

            //        entity.Detail.Town.City.State.Country = country;

            //    }
            //}
            //else
            //{
            //    if (GuidHelper.IsNullOrEmpty(entity.Detail.Town.City.State.CountryId))
            //    {
            //        entity.Detail.Town.City.State.CountryId = entity.Detail.Town.City.State.Country.Id;
            //    }
            //}
        }

        private static void Check_There_Is_A_State_In_City_Record(IAddressWithId entity)
        {
            //StateDAL stateDAL = new StateDAL(_db, _user);

            ////Now we have a city INSIDE the Town Entity...
            ////Do we have a state inside the City entity?
            //if (entity.Detail.Town.City.State == null)
            //{
            //    //There was no state found in the city
            //    if (GuidHelper.IsNullOrEmpty(entity.Detail.Town.City.StateId))
            //    {
            //        //There is no city for this town
            //        throw new AliKuli.Exceptions.MiscNS.RequiredDataMissingException(string.Format("No state in city named: '{2}' in AddressWithId Name '{0}' FOR  Town: '{1}'. AddressDAL.SelectCityStateCountryAutomatically. AddressDAL.SelectCityStateAuto",
            //            entity.Name,
            //            entity.Detail.Town.Name,
            //            entity.Detail.Town.City.Name));

            //    }
            //    else
            //    {
            //        //Check if there is an id for the state available
            //        var state = stateDAL.FindFor(entity.Detail.Town.City.StateId);

            //        bool stateNotFound = state == null;
            //        if (stateNotFound)
            //        {
            //            //There is no city for this town
            //            throw new AliKuli.Exceptions.MiscNS.RequiredDataMissingException(string.Format("No state exists in db for stateId Num {3} in city named: '{2}' in AddressWithId Name '{0}' FOR  Town: '{1}'. AddressDAL.SelectCityStateCountryAutomatically. AddressDAL.SelectCityStateAuto",
            //            entity.Name,
            //            entity.Detail.Town.Name,
            //            entity.Detail.Town.City.Name,
            //            entity.Detail.Town.City.StateId));

            //        }

            //        entity.Detail.Town.City.State = state;

            //    }



            //}
            //else
            //{
            //    if (GuidHelper.IsNullOrEmpty(entity.Detail.Town.City.StateId))
            //    {
            //        entity.Detail.Town.City.StateId = entity.Detail.Town.City.State.Id;
            //    }
            //}
        }

        private static void Check_Town_Has_A_City(IAddressWithId entity)
        {
            //CityDAL cityDAL = new CityDAL(_db, _user);

            ////Now we have the town... Check if the town has a city.
            //if (entity.Town.City == null)
            //{
            //    //There was no city found in the Town
            //    if (GuidHelper.IsNullOrEmpty(entity.Town.CityID))
            //    {
            //        //There is no city for this town
            //        throw new AliKuli.Exceptions.MiscNS.RequiredDataMissingException(string.Format("No city found in AddressWithId Name '{0}' FOR  Town: '{1}'. AddressDAL.SelectCityStateCountryAutomatically. AddressDAL.SelectCityStateAuto", entity.Name, entity.Town.Name));

            //    }
            //    else
            //    {
            //        //There is an id for the city available
            //        var city = cityDAL.FindFor(entity.Detail.Town.CityID);

            //        bool cityNotFound = city == null;
            //        if (cityNotFound)
            //        {
            //            //There is no city for this town
            //            throw new AliKuli.Exceptions.MiscNS.RequiredDataMissingException(string.Format("No city with Id = '{2}' exists in db for town '{1}' in AddressWithId Name '{0}' . AddressDAL.SelectCityStateCountryAutomatically. AddressDAL.SelectCityStateAuto",
            //                entity.Name,
            //                entity.Detail.Town.Name,
            //                entity.Detail.Town.CityID));

            //        }

            //        entity.Detail.Town.City = city;

            //    }
            //}
            //else
            //{
            //    if (GuidHelper.IsNullOrEmpty(entity.Detail.Town.CityID))
            //    {
            //        entity.Detail.Town.CityID = entity.Detail.Town.City.Id;
            //    }
            //}

        }

        private static void Check_Town_Exists(AddressWithId entity)
        {
            TownDAL townDAL = new TownDAL(_db, _user);
            //Make sure Town Entity is there.
            if (entity.Town == null)
            {
                if (!(entity.TownId.IsNullOrEmpty()))
                {
                    entity.Town = townDAL.FindFor(entity.TownId);
                }

                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException(string.Format("No Town exists. AddressDAL.Check_Town_Exists"));

            }
            else
            {
                if (entity.TownId.IsNullOrEmpty())
                {
                    entity.TownId = ((Town)entity.Town).Id; ;
                }

            }
        }

        #endregion SelectCityStateCountryAutomaticly Helpers

        #endregion Fix Helpers




        /// <summary>
        /// Adds the Town, City, State and Country records automaticly into AddressWithId record if they are missing.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>




        //public override IQueryable<Address> FindAll(bool deleted = false)
        //{
        //    //var listOfAddresses = base.FindAll(deleted).OrderBy(x=>x.Name).ToList();

        //    var listOfAddresses = base.FindAll(deleted).ToList();

        //    if (listOfAddresses != null)
        //    {
        //        listOfAddresses = listOfAddresses.ToList();

        //        foreach (var item in listOfAddresses)
        //        {
        //            SelectCityStateCountryAutomatically(item);
        //        }
        //    }
        //    return listOfAddresses.OrderBy(x => x.NameDecrypted).AsQueryable();
        //}




        public override AddressWithId FindFor(Guid? id, bool deleted = false)
        {
            var entity = base.FindFor(id, deleted);
            SelectCityStateCountryAutomatically(entity);
            return entity;

        }

        /// <summary>
        /// This checks to see if the same AddressWithId exists in the db by checking every field against the db.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public AddressWithId FindSameAddressInDb(IAddressWithId entity)
        {
            if (entity.IsNull())
                throw new Exception("No AddressWithId passed. AddressDAL,FindSameAddressInDb");

            var addressesInSameTown = SearchFor(x => x.TownId == entity.TownId);

            //now you have all the addresses for the town, city, state, and country for the
            //AddressWithId you are searching for.

            //Now get all the same road addresses
            var addressWithSameRoad = addressesInSameTown.Where(x => x.AddressComplex.Road.ToLower() == entity.AddressComplex.Road.ToLower()).ToList().AsQueryable();

            //if we get a result ... the road is the same and the town is the same.
            //Check for AddressWithId 2 ...
            if (!addressWithSameRoad.IsNull())
            {
                if (entity.AddressComplex.Address2.IsNullOrEmpty())
                {
                    //they are the same

                    //now check for the house number
                    if (entity.AddressComplex.HouseNo.IsNullOrEmpty())
                    {
                        //look got empty house numbers....

                        var addressWithSameTownRdNoHouseNo = addressWithSameRoad.Where(x => x.AddressComplex.HouseNo == null || x.AddressComplex.HouseNo.Trim() == "").FirstOrDefault();

                        if (!addressWithSameTownRdNoHouseNo.IsNull())
                        {
                            return addressWithSameTownRdNoHouseNo;
                        }
                    }
                    else
                    {
                        //look for the same house number
                        var addressWithSameTown_AND_Rd_And_HouseNo = addressWithSameRoad.Where(x => x.AddressComplex.HouseNo.ToLower().Trim() == entity.AddressComplex.HouseNo.ToLower().Trim())
                            .FirstOrDefault();

                        if (!addressWithSameTown_AND_Rd_And_HouseNo.IsNull())
                        {
                            return addressWithSameTown_AND_Rd_And_HouseNo;
                        }

                    }
                }
                else
                {
                    //there is a 2nd road AddressWithId as well...
                    //is there any AddressWithId in the list of addresses where even the 2nd road is the same?
                    var addressWithSameSecondAddressLine = addressWithSameRoad
                        .Where(x => x.AddressComplex.Address2.ToLower() == entity.AddressComplex.Address2.ToLower())
                        .ToList()
                        .AsQueryable();

                    if (!addressWithSameSecondAddressLine.IsNullOrEmpty())
                    {
                        //we have results....
                        if (entity.AddressComplex.HouseNo.IsNullOrEmpty())
                        {

                            //look for empty house numbers....
                            var addressWithSameRdNoAndHouseNo = addressWithSameSecondAddressLine.Where(x => x.AddressComplex.HouseNo == null || x.AddressComplex.HouseNo.Trim() == "").FirstOrDefault();
                            if (addressWithSameRdNoAndHouseNo != null)
                                return addressWithSameRdNoAndHouseNo;
                        }
                        else
                        {
                            //look for the same house number
                            var addressWithSameRdSameHouseNo = addressWithSameSecondAddressLine.Where(x => x.AddressComplex.HouseNo.ToLower().Trim() == entity.AddressComplex.HouseNo.ToLower().Trim())
                                .FirstOrDefault();

                            if (!addressWithSameRdSameHouseNo.IsNull())
                            {
                                return addressWithSameRdSameHouseNo;
                            }
                        }

                    }

                }

            }

            return null;
        }




        ///// <summary>
        ///// This returns the data for a ConsignTo addesses along with default addresses
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public IEnumerable<SelectListItem> SelectList_ConsignToAddressFor(Guid userId)
        //{
        //    var consigntToAddressesForPerson = SearchFor(x => x.UserId == userId && x.ConsigneeAddressWithId == true);
        //    return SelectListEngine(consigntToAddressesForPerson);
        //}

        ///// <summary>
        ///// This returns the data for a Ship To addesses along with default addresses
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //public IEnumerable<SelectListItem> SelectList_ShipToAddressFor(Guid userId)
        //{
        //    var consigntToAddressesForPerson = SearchFor(x => x.UserId == userId && x.ShipToAddressWithId == true);
        //    return SelectListEngine(consigntToAddressesForPerson);
        //}

        ///// <summary>
        ///// This returns the data for a Inform To addesses along with default addresses
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <returns></returns>

        //public IEnumerable<SelectListItem> SelectList_InformToAddressFor(Guid userId)
        //{
        //    var consigntToAddressesForPerson = SearchFor(x => x.UserId == userId && x.InformToAddressWithId == true);
        //    return SelectListEngine(consigntToAddressesForPerson);

        //}

        public override void InitializationData()
        {
            throw new NotImplementedException();
        }
    }
}

