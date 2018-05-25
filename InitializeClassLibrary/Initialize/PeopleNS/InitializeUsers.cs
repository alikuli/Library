using System;
using System.Collections.Generic;
using AliKuli.UtilitiesNS;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeUser
    {
        private static ApplicationDbContext _db;
        private static string _user;
        //private static TownDAL townDAL;

        public InitializeUser(ApplicationDbContext db, string user)
        {
            _db = db;
            _user = user;
        }

        private void Add(
            string userName,
            string password,
            string role,
            string countryAbbrev,
            string nationIdCard,
            string fName,
            string houseNo,
            string road,
            string townName,
            string cityName,
            string stateName,
            string countryName,
            bool isAdmin,
            string mName,
            string lName,
            string address2,
            string zip,
            string contactPhone,
            string webAddress,
            string email,
            SexENUM sex,
            SonOfWifeOfDotOfENUM sonOfWifeOfDotOfENUM,
            string nameOfFatherOrHusband,
            string createdByUser,
            string attention)
        {
            try
            {
                DateTime beginCreateDate = DateTime.UtcNow;
                TownDAL townDAL = new TownDAL(_db, _user);

                Town town = townDAL.FindByName(townName, cityName, stateName, countryName);

                if (town == null)
                    throw new Exception("Town not found. InitializeUsers.Add");

                Guid townId = town.Id;

                string comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());
                UserDAL userDAL = new UserDAL(_db, _user);

                User userCreated = userDAL.CreateUser(
                   userName,
                   password,
                   countryAbbrev,
                   nationIdCard,
                   fName,
                   houseNo,
                   road,
                   beginCreateDate,
                   isAdmin,   //Is Administrator
                   mName,
                   lName,
                   address2,
                   zip,
                   contactPhone,
                   webAddress,
                   email,
                   townId,
                   town,
                   sex,
                   sonOfWifeOfDotOfENUM,
                   nameOfFatherOrHusband,
                   comment,
                   attention);

                userDAL.AddUserToRole(userCreated.Id, role);
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.UserExistsException)
            {

            }
        }


        public void DeleteAll()
        {
            new UserDAL(_db, _user).RemoveAllUsers(_db);
        }

        public void Initialize()
        {

            string errorStr = "";
            List<string> roles = new List<string>();

            new UserDAL(_db, _user).InitializeDefaultRolesAndAdminUser(_db);

            if (errorStr.Trim().Length != 0)
                throw new Exception(errorStr);

            string countryAbbreviation = ConfigManager.DefaultCountryAbbreviation;
            string serviceManRole = new UserDAL(_db, _user).GetRoleNameServicemen();
            string hiringManRole = new UserDAL(_db, _user).GetRoleNameHiringman();
            string salesManRole = new UserDAL(_db, _user).GetRoleNameManager();



            Add("923314474121", "@liKuli786!", serviceManRole, countryAbbreviation, "1234567890123", "Ali", "1", "Main Harbanspura Road", "Lahore", "Lahore", "Punjab", "Pakistan", false, "Kuli", "Aminuddin", "Gulkali", "12345678", "3314474999", "Kokopala.com", "alikuli62@gmail.com", SexENUM.Male, SonOfWifeOfDotOfENUM.SonOf, "Parvez Amin", "Initial User", "Owner of House");

            Add("3334416272", "@liKuli786!", hiringManRole, countryAbbreviation, "1234567890120", "Aila", "2", "Gulberg", "Lahore", "Lahore", "Punjab", "Pakistan", false, null, "Azhar", "Gulkali", null, "3314474998", "Aila.com", "alikuli@yahoo.com", SexENUM.Female, SonOfWifeOfDotOfENUM.DaughterOf, "Parvez Amin", "Initial User", "Accounbt Receivable");

            Add("03314474123", "@liKuli786!", salesManRole, countryAbbreviation, "1234567890125", "Khursheed", "1", "Abbot Road", "Lahore", "Lahore", "Punjab", "Pakistan", false, "Mughal", "Khan", "Mountain Pass", "0987654321", "3314474997", "arco.com", "alikuli62@gmail.com", SexENUM.Male, SonOfWifeOfDotOfENUM.SonOf, "Mughal", "Initial User", "Account payable");

        }

    }
}