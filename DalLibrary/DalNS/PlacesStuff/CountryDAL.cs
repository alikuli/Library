using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using AliKuli.Extentions;
using ApplicationDbContextNS;
using DalLibrary.DAL.PlacesStuff;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using WebLibrary.Programs;

namespace DalLibrary.DalNS
{
    public class CountryDAL : Repositry<Country>, ICountryDAL
    {

        //private ApplicationDbContext _db;
        //private string _user;
        private readonly IMemoryMain _memory;
        public CountryDAL(ApplicationDbContext db, IMemoryMain memory, IErrorSet ErrorsGlobal)
            : base(db, ErrorsGlobal)
        {
            IsDuplicateNameAllowed = false;
            ErrorsGlobal.ClassName = GetSelfClassName();
            _memory = memory;
        }




        #region Fix

        //public override void Fix(Country entity)
        //{
        //    base.Fix(entity);
        //    UpdateFieldsToTitleCaseAndUpperCase(entity);

        //}


        #endregion

        #region Find

        public Country FindForAbbreviation(string abbrev)
        {
            if (abbrev.IsNullOrWhiteSpace())
                return null;
            var c = FindAll();

            if (c.IsNullOrEmpty())
                return null;

            var countries = c.Where(x => x.Abbreviation.ToLower() == abbrev.ToLower());

            if (countries.IsNull() || countries.Count() == 0)
                return null;

            return countries.FirstOrDefault();
        }


        #endregion

        #region Create

        private void Create_BlankStateForCountry(Country entity)
        {
            State state = new StateDAL(_db, ErrorsGlobal).Factory();

            state.Name = entity.Name;
            state.StateAbbreviation = entity.Abbreviation;
            state.MetaData.IsAutoCreated = true;
            state.MetaData.Comment = "Every country also has a default blank state.";

            entity.States.Add(state);
        }


        #endregion

        //#region Update

        //private static void UpdateFieldsToTitleCaseAndUpperCase(Country entity)
        //{
        //    entity.Abbreviation = entity.Abbreviation.ToUpper();
        //    entity.Name = entity.Name.ToTitleCase();
        //}

        //#endregion

        //#region InitializationData and InitializationDataAsync

        //public void InitializationData()
        //{
        //    //get data
        //    string[,] countryDataArray = CountryData.CountryDataArray();
        //    if (!countryDataArray.IsNull() && countryDataArray.Length > 0)
        //    {
        //        for (int i = 0; i < countryDataArray.Length / 3; i++)
        //        {

        //            string countryName = countryDataArray[i, 0];
        //            string abbrev = countryDataArray[i, 1];

        //            Country c = Factory();
        //            c.Abbreviation = (abbrev.IsNull() ? abbrev : abbrev.ToUpper());
        //            c.Name = countryName;

        //            try
        //            {
        //                Create(c);
        //            }
        //            catch (NoDuplicateException e)
        //            {

        //                ErrorsGlobal.AddMessage(string.Format("Duplicate entry: '{0}'", c.ToString()), MethodBase.GetCurrentMethod(), e);
        //            }
        //        }
        //        SaveChanges();

        //    }

        //}

        //public async Task InitializationDataAsync()
        //{

        //    //get data
        //    string[,] countryDataArray = CountryData.CountryDataArray();
        //    if (!countryDataArray.IsNull() && countryDataArray.Length > 0)
        //    {
        //        for (int i = 0; i < countryDataArray.Length / 3; i++)
        //        {

        //            string countryName = countryDataArray[i, 0];
        //            string abbrev = countryDataArray[i, 1];
        //            string phoneAreaCode = countryDataArray[i, 2];

        //            Country c = Factory();
        //            c.Abbreviation = (abbrev.IsNull() ? abbrev : abbrev.ToUpper());
        //            c.Name = countryName;

        //            try
        //            {
        //                Create(c);
        //                await SaveChangesAsync();
        //            }
        //            catch (NoDuplicateException e)
        //            {

        //                ErrorsGlobal.AddMessage(string.Format("Duplicate entry: '{0}'", c.ToString()), MethodBase.GetCurrentMethod(), e);
        //            }
        //        }
        //    }

        //}

        //#endregion

        //#region FixIndexListVM and FixIndexListVMAsync

        //public override void GiveNamesToInputColumnsInIndexList(IndexListVM indexListVM)
        //{
        //    indexListVM.NameInput1 = "Abbreviation";
        //    indexListVM.NameInput2 = "Name";
        //    indexListVM.Heading_Column = "[Abbreviation] - Country Name ";


        //}
        //#endregion

        /// <summary>
        /// This loads the country Data into the select list
        /// </summary>
        /// <returns></returns>
        public override SelectList SelectList()
        {
            SelectList selectList = GetSelectListFromCache();
            if (selectList.IsNull())
            {

                //Load the data
                selectList = LoadDataIntoDbAndThenGetSelectListAndAddToCache();

            }
            return selectList;
        }

        string _key = "CountrySelectListData";
        private SelectList GetSelectListFromCache()
        {

            object obj = _memory.CacheMemory.GetFrom(_key);
            ErrorsGlobal.AddMessage("Country Select List DAL has been Accessed.", MethodBase.GetCurrentMethod());
            return (SelectList)obj;
        }

        private SelectList LoadDataIntoDbAndThenGetSelectListAndAddToCache()
        {
            ErrorsGlobal.AddMessage("Country Select List DAL has been refilled because it was NOT in CACHE.", MethodBase.GetCurrentMethod());
            LoadCountryFromArrayIntoDb();

            var selectListData = base.SelectList();
            if (selectListData.IsNull())
            {
                ErrorsGlobal.Add("No Data loaded. Please load country data.", MethodBase.GetCurrentMethod());
                //return null;
            }

            _memory.CacheMemory.Add(_key, selectListData, new System.TimeSpan(10, 0, 0, 0));
            object obj = _memory.CacheMemory.GetFrom(_key);
            return (SelectList)obj;
        }


        private void LoadCountryFromArrayIntoDb()
        {
            //now make sure that country has been loaded.
            var allCountryDataInDb = FindAll().ToList();


            int noOfCountryRecInArray = CountryData.CountryDataArray().Length;

            if (noOfCountryRecInArray == 0)
            {
                ErrorsGlobal.Add("Country Data not loaded in Array. Please load country data.", MethodBase.GetCurrentMethod());
                return;

            }

            noOfCountryRecInArray = noOfCountryRecInArray / 3;

            //if there is no country data
            if (allCountryDataInDb.IsNullOrEmpty() || allCountryDataInDb.Count() < noOfCountryRecInArray)
            {
                //Load all of the data from array into database
                for (int i = 0; i < noOfCountryRecInArray; i++)
                {
                    try
                    {
                        Country c = Factory();
                        c.Abbreviation = CountryData.CountryDataArray()[i, 1];
                        c.Name = CountryData.CountryDataArray()[i, 0];
                        Create(c);
                        SaveChanges();

                    }
                    catch (NoDuplicateException)
                    {

                        //ignore and continue...
                    }
                    catch (Exception e)
                    {
                        ErrorsGlobal.Add(string.Format("Something happened in record no '{0}' while creating Country records.", i), MethodBase.GetCurrentMethod(), e);
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                }
            }
        }


    }
}
