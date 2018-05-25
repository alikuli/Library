using AliKuli.Utilities;
using System;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.AddressNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeAddress
    {
        private static AddressDAL dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeAddress(ApplicationDbContext db, string user)
        {
            _db = db;
            _user = user;
            dal = new AddressDAL(_db, _user);
        }

        //private void Add(
        //    string name,
        //    string houseNo,
        //    string road,
        //    string address2,
        //    string town,
        //    string city,
        //    string stateName,
        //    string countryName,
        //    string zip,
        //    string fName,
        //    string mName,
        //    string lName,
        //    string personNatnlIdNo)
        //{
        //    //PersonDAL personDAL = new PersonDAL(_db, _user);
        //    //Person person = personDAL.FindForName(fName, mName, lName).FirstOrDefault();

        //    Address address = dal.Factory();
        //    address.Name = name;

        //    address.HouseNo = houseNo;
        //    address.Address2 = address2;
        //    address.Road = road;
        //    address.Zip = zip;

        //    address.Town = new TownDAL(_db, _user).FindByName(town, city, stateName, countryName);
        //    address.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());




        //    try
        //    {
        //        //find the user using his national Id card
        //        User u = new UserDAL(_db, _user).FindForIdentityCard(personNatnlIdNo);

        //        if (u == null)
        //        {
        //            throw new Exception(string.Format("User for National Id card Number '{0}' was not found in intialization. InitializeAddress.Add", personNatnlIdNo));
        //        }
        //        address.UserId = u.Id;
        //        dal.Create(address);
        //        dal.Save();
        //    }
        //    catch (AliKuli.Exceptions.MiscNS.NoDuplicateException)
        //    {

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}


        private void Add(
            string UserCountryId,
            bool isConsignToAddress,
            bool isShipToAddress,
            bool isInformToAddress)
        {
            //get the user
            UserDAL userDAL = new UserDAL(_db, _user);

            User userFound = userDAL.FindForIdentityCard(UserCountryId);

            if (userFound == null)
                throw new Exception(string.Format("User with Countri Id '{0}' not found. InitializeAddress.Add", UserCountryId));
            string addressName = string.Format("Default for {0}", userFound.FullName());

            AddressDAL addyDAL = new AddressDAL(_db, _user);
            
            Address a = addyDAL.Factory();
            a.LoadFrom(userFound, addressName, isConsignToAddress, isShipToAddress, isInformToAddress);

            try
            {
                addyDAL.Create(a);
                addyDAL.Save();
            }
            catch(AliKuli.ExceptionsNS.NoDuplicateException)
            {
            }

        }

        public void Initialize()
        {
            //Add("Ali House", "1", "Main Harbanspura Road", "Gulkali", "", "Lahore", "punjab", "Pakistan", "023493", "Ali", "Kuli", "Aminuddin", "1234567890120");
            //Add("Aila House", "2", "Main Harbanspura Road", "Gulkali", "", "Lahore", "punjab", "Pakistan", "023493", "Aila", "", "azhar", "1234567890125");
            //Add("Selma House", "1", "Tufail Road", "Sector D", "Model Town", "lahore", "punjab", "Pakistan", "34934934", "Ali", "Kuli", "Aminuddin", "1234567890125");


            Add("1234567890123",true, true, true);
            Add("1234567890120", true, true, true);
            Add("1234567890125", true, true, true);
        }

        public void DeleteAll()
        {
            var list = dal.FindAll().ToList();

            foreach (var item in list)
            {
                _db.Addresses.Remove(item);
            }
            _db.SaveChanges();


            list = dal.FindAll(true).ToList();
            foreach (var item in list)
            {
                _db.Addresses.Remove(item);
            }
            _db.SaveChanges();
        }

    }
}