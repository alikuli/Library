using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.ExceptionsNS;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeTown
    {
        private static TownDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeTown(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user = _user;
            dal = new TownDAL(_db, user);
        }

        private void Add(string townName, string cityName, string stateName, string countryName)
        {
            string comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());
            Town entity = dal.Factory(townName, cityName, stateName, countryName, comment);


            try
            {
                dal.Create(entity);
                dal.Save();
            }

            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {
                //do nothing
            }

            catch
            {
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                Add("Gulberg", "Quetta", "Balochistan", "pakistan");
                Add("Model Town", "Quetta", "Balochistan", "pakistan");
                Add("", "Lahore", "punjab", "pakistan");
                Add("Model Town", "Lahore", "punjab", "pakistan");
                Add("Gulberg", "Lahore", "punjab", "pakistan");
                Add("Village", "New York", "New York", "usa");
                Add("Soho", "London", "", "UK");
                Add("Malir", "karachi", "sind", "Pakistan");
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }
            catch
            { throw; }
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.Towns.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Towns.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<Town> lst = dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.", dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Name += "*";
                dal.Update(item);
                try
                {
                    dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}