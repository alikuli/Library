using System;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class ProgramDataInitialization
    {
        private readonly ApplicationDbContext _db;
        private readonly string _userName;
        private readonly ErrorSet _err;
        private string _adminUserName;
        private readonly SetupDAL _setupDAL;
        private readonly UserDAL _userDAL;
        private readonly GlobalSetUpValues _globalSetUpValues;
         
        //private readonly System.Web.HttpContext _httpCtx;
        
        public ProgramDataInitialization(ApplicationDbContext db, string userName)
        {
            _err = new ErrorSet("InitializeClassLibrary", "ProgramDataInitialization", "");
            _db = db;
            _userName = userName;
            _userDAL = new UserDAL(_db, userName);
            _globalSetUpValues = new GlobalSetUpValues(_db, _userName);

        }

        //private const string CLEAR_ERROR_STRING = "clearerrorstring";
        //private string _errorString = "";
        //private string ErrorString
        //{
        //    get
        //    {
        //        return _errorString;
        //    }
        //    set
        //    {

        //        string valueIn = value;

        //        if (valueIn == CLEAR_ERROR_STRING)
        //        {
        //            _errorString = "";
        //            return;
        //        }

        //        if (!valueIn))
        //        {
        //            _errorString.ConcatStrWithSeperator(valueIn);
        //        }
        //    }
        //}
        private void Initialize_Setup()
        {
            new SetupDAL(_db, _userName).InitializeSetUp();
        }


        public string DoAll()
        {

            bool execute = false;

            Initialize_AdminAndUsersWithRoles();

            //_adminUserName = new UserDAL(_db, _userName).GetAdminUserName();
            //_setupDAL.SendCompanyNameToMemoryFromSetup();

            Initialize_Setup();
            Initialize_DefaultCountryIntoDb(_userName, _db);
            Initialize_NumOfUnitsRequrdToSetUpServiceMen(_db);
            Initialize_NumOfUnitsRequrdToSetUpCustomers(_db);
            Initialize_DefaultLengthOfScratchCardNumber();
            Initialize_CustomerCategoriesFromEnum(_db);
            Initialize_OwnerCategoriesFromEnum(_db);
            Initialize_SalesmenCategoriesFromEnum(_db);
            Initialize_UomLengthFromEnum(_db);
            Initialize_UomQtyFromEnum(_db);
            Initialize_UomWeightFromEnum(_db);
            Initialize_UomVoluneFromEnum(_db);
            Initialize_Cat1FromEnum(_db);
            Initialize_DiscountPrecdencesForStarup(_db);
            Initialize_PaymentTypeEnum(_db);

            Initialize_AddressCategoryType(_db);

            if (execute)
            {
                Initialize_CountriesForStarup(_db);
                Initialize_StatesForStarup(_db);
                Initialize_CitiesForStarup(_db);
                Initialize_DeliveryMethodsForStarup(_db);
                Initialize_TownsForStarup(_db);
                Initialize_LanuagesForStarup(_db);
                Initialize_PaymentMethodsForStarup(_db);
                Initialize_PaymentTermsForStarup(_db);

            }
            return _err.ToString();
        }

        private void Initialize_AddressCategoryType()
        {
            //try
            //{

            //    InitializeAddressCategory ia = new InitializeAddressCategory(_db, _user);
            //    ia.Initialize();
            //}
            //catch(AliKuli.Exceptions.MiscNS.NoDuplicateException)
            //{
            //}
            //catch
            //{
            //    throw;
            //}


        }


        private void Initialize_AdminAndUsersWithRoles()
        {
            new UserDAL(_db, _userName).InitializeDefaultRolesAndAdminUser();
        }
        private void Initialize_DeliveryMethodsForStarup(ApplicationDbContext _db)
        {

            try
            {
                InitializePaymentMethods pm = new InitializePaymentMethods(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_PaymentTermsForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializePaymentTerms pm = new InitializePaymentTerms(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_PaymentMethodsForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializePaymentMethods pm = new InitializePaymentMethods(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_LanuagesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeLanguages pm = new InitializeLanguages(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_DiscountPrecdencesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeDiscountPrecedenceList pm = new InitializeDiscountPrecedenceList(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }

        }

        private void Initialize_TownsForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeTown pm = new InitializeTown(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_CitiesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeCity pm = new InitializeCity(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_StatesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeState pm = new InitializeState(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private void Initialize_CountriesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeCountries pm = new InitializeCountries(_db, _user);
                pm.Initialize();

            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }




        private void Initialize_DefaultCountryIntoDb(string userName, ApplicationDbContext _db)
        {

            string countryName = "";
            string countryAbbreviation = "";
            string lengthOfCountryIdNumber = "";
            int lenIdNumber = 0;

            try
            {

                countryName = _globalSetUpValues.DefaultCountry;
                countryAbbreviation = _globalSetUpValues.DefaultCountryAbbreviation;
                lengthOfCountryIdNumber = _globalSetUpValues.DefaultCountryIdNoLen;

                if (!lengthOfCountryIdNumber.IsNumber())
                    _err.Add("Lenth of Country Id number is not a number or zero. Please fix in setup.", "Initialize_DefaultCountryIntoDb");
                //check to see if default country has been set into the database. If not, set it.
                CountryDAL countryDAL = new CountryDAL(_db, userName);
                //Add the default country

                Country c = countryDAL.Factory();
                c.LengthOfCompleteCnicNumber = lenIdNumber;
                c.Name = countryName;
                c.Abbreviation.Value = countryAbbreviation;

                countryDAL.Create(c);
                countryDAL.Save();

            }
            catch (Exception e)
            {
                _err.Add("Error while entering default country into database.", "Initialize_DefaultCountryIntoDb", e);
            }
        }



        /// <summary>
        /// This is the number of units required to set up service men. Send it to cache.
        /// </summary>
        private string Initialize_NumOfUnitsRequrdToSetUpServiceMen(ApplicationDbContext _db)
        {
            try
            {
                string numOfUnitsToSetupServiceMan = ConfigManager.ScratchCardUnitsToSetupServiceman().ToString();
                //string numOfUnitsToSetupServiceMan = AliKuli.Utilities.AppConfigHelper.GetScratchCardUnitsToSetupServiceman().ToString();
            }

            catch (Exception e)
            {
                _err.Add("Error while initializing number of units required to setup a serviceman.", "Initialize_NumOfUnitsRequrdToSetUpServiceMen", e);
            }

            return null;
        }



        /// <summary>
        /// This is the number of units it will require to setup a customer initially. Thereafter, the customer can refill his account as he pleases.
        /// </summary>
        private string Initialize_NumOfUnitsRequrdToSetUpCustomers(ApplicationDbContext _db)
        {
            try
            {
                string numOfUnitsToSetupSCustomer = AliKuli.Utilities.AppConfigHelper.GetScratchCardUnitsToSetupCustomer();
            }

            catch (Exception e)
            {
                return e.Message;
            }

            return null;

        }


        private string Initialize_DefaultLengthOfScratchCardNumber()
        {

            try
            {
                string defaultLengthOfScratchCardNumber = AliKuli.Utilities.AppConfigHelper.GetScratchCardNumberDefaultLength();
            }

            catch (Exception e)
            {
                return e.Message;
            }

            return null;

        }


        private void Initialize_CustomerCategoriesFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new CustomerCategoryDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }

        }


        /// <summary>
        /// Each one creates entries in the respective tables and saves before it exists
        /// </summary>
        private void Initialize_OwnerCategoriesFromEnum(ApplicationDbContext _db)
        {


            try
            {
                new OwnerCategoryDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...

            }
        }
        private void Initialize_SalesmenCategoriesFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new SalesmanCategoryDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...

            }
        }
        private void Initialize_UomLengthFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new UomLengthDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...

            }
        }
        private void Initialize_UomQtyFromEnum(ApplicationDbContext _db)
        {


            try
            {
                new UomQtyDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }
        private void Initialize_UomWeightFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new UomWeightDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }


        private void Initialize_UomVoluneFromEnum(ApplicationDbContext _db)
        {

            try
            {
                new UomVolumeDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }
        private void Initialize_Cat1FromEnum(ApplicationDbContext _db)
        {


            try
            {
                new ProductCat1DAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }


        private void Initialize_PaymentTypeEnum(ApplicationDbContext _db)
        {


            try
            {
                new PaymentTypeDAL(_db, "").InitializeFromEnumAndSave();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }



    }
}