using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;

namespace InitializeClassLibrary.InitializeNS
{
    public static class ProgramDataInitialization
    {
        private static string _user = "";

        //private const string CLEAR_ERROR_STRING = "clearerrorstring";
        //private static string _errorString = "";
        //private static string ErrorString
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
        private static void Initialize_Setup(string userName, ApplicationDbContext _db)
        {
            new SetupDAL(_db, userName).InitializeSetUp();
        }


        public static string DoAll(ApplicationDbContext _db,out string userName)
        {
            userName = "";
            ErrorStringBuilder.ClearString();
            bool execute = false;

            Initialize_AdminAndUsersWithRoles(_db);

            _user = new UserDAL(_db, _user).GetAdminUserName();
            new SetupDAL(_db, "").SendCompanyNameToMemoryFromSetup();

            Initialize_Setup(userName, _db);
            Initialize_DefaultCountryIntoDb(userName, _db);
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

            //Initialize_AddressCategoryType(_db);

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
            return ErrorStringBuilder.ErrorString;
        }

        //private static void Initialize_AddressCategoryType(ApplicationDbContext _db)
        //{
        //    //try
        //    //{

        //    //    InitializeAddressCategory ia = new InitializeAddressCategory(_db, _user);
        //    //    ia.Initialize();
        //    //}
        //    //catch(AliKuli.Exceptions.MiscNS.NoDuplicateException)
        //    //{
        //    //}
        //    //catch
        //    //{
        //    //    throw;
        //    //}


        //}


        private static void Initialize_AdminAndUsersWithRoles(ApplicationDbContext _db)
        {
            new UserDAL(_db, _user).InitializeDefaultRolesAndAdminUser(_db);
        }
        private static void Initialize_DeliveryMethodsForStarup(ApplicationDbContext _db)
        {

            try
            {
                InitializePaymentMethods pm = new InitializePaymentMethods(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_PaymentTermsForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializePaymentTerms pm = new InitializePaymentTerms(_db, _user);
                pm.Initialize();

            }
            catch(AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_PaymentMethodsForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializePaymentMethods pm = new InitializePaymentMethods(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_LanuagesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeLanguages pm = new InitializeLanguages(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_DiscountPrecdencesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeDiscountPrecedenceList pm = new InitializeDiscountPrecedenceList(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
                    
        }

        private static void Initialize_TownsForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeTown pm = new InitializeTown(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_CitiesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeCity pm = new InitializeCity(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_StatesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeState pm = new InitializeState(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }

        private static void Initialize_CountriesForStarup(ApplicationDbContext _db)
        {
            try
            {
                InitializeCountries pm = new InitializeCountries(_db, _user);
                pm.Initialize();

            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothingg
            }
        }
        
        


        private static string Initialize_DefaultCountryIntoDb(string userName, ApplicationDbContext _db)
        {

            string countryName="";
            string countryAbbreviation="";
            string lengthOfCountryIdNumber="";
            int lenIdNumber = 0;

            try
            {
                countryName = AliKuli.Utilities.AppConfigHelper.GetDefaultCountryName();
                countryAbbreviation = AliKuli.Utilities.AppConfigHelper.GetDefaultCountryAbbreviation();
                lengthOfCountryIdNumber = AliKuli.Utilities.AppConfigHelper.GetDefaultCountryIdentificationNoLength(out lenIdNumber);

                //check to see if default country has been set into the database. If not, set it.
                CountryDAL countryDAL = new CountryDAL(_db, userName);
                //Add the default country

                Country c = countryDAL.Factory();
                c.LengthOfCompleteCnicNumber = lenIdNumber;
                c.Name = countryName;
                c.Abbreviation = countryAbbreviation;

                countryDAL.Create(c);
                countryDAL.Save();
                
                return null; //No error!
            }
            catch (Exception e)
            {
                return AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e);
            }
        }



        /// <summary>
        /// This is the number of units required to set up service men.
        /// </summary>
        private static string  Initialize_NumOfUnitsRequrdToSetUpServiceMen(ApplicationDbContext _db)
        {
            try
            {
                string numOfUnitsToSetupServiceMan = AliKuli.Utilities.AppConfigHelper.GetScratchCardUnitsToSetupServiceman().ToString();
            }
            
            catch(Exception e)
            {
                return e.Message;
            }

            return null;
        }



        /// <summary>
        /// This is the number of units it will require to setup a customer initially. Thereafter, the customer can refill his account as he pleases.
        /// </summary>
        private static string Initialize_NumOfUnitsRequrdToSetUpCustomers(ApplicationDbContext _db)
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


        private static string Initialize_DefaultLengthOfScratchCardNumber()
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


        private static void Initialize_CustomerCategoriesFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new CustomerCategoryDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }

        }

        
        /// <summary>
        /// Each one creates entries in the respective tables and saves before it exists
        /// </summary>
        private static void Initialize_OwnerCategoriesFromEnum(ApplicationDbContext _db)
        {


            try
            {
                new OwnerCategoryDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...

            }
        }
        private static void Initialize_SalesmenCategoriesFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new SalesmanCategoryDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...

            }
        }
        private static void Initialize_UomLengthFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new UomLengthDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...

            }
        }
        private static void  Initialize_UomQtyFromEnum(ApplicationDbContext _db)
        {


            try
            {
                new UomQtyDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }
        private static void Initialize_UomWeightFromEnum(ApplicationDbContext _db)
        {
            try
            {
                new UomWeightDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }


        private static void Initialize_UomVoluneFromEnum(ApplicationDbContext _db)
        {

            try
            {
                new UomVolumeDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }
        private static void Initialize_Cat1FromEnum(ApplicationDbContext _db)
        {


            try
            {
                new ProductCat1DAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }


        private static void Initialize_PaymentTypeEnum(ApplicationDbContext _db)
        {


            try
            {
                new PaymentTypeDAL(_db, "").InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //Do nothing...
            }
        }
        


    }
}