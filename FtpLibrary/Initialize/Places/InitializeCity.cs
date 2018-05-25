using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.CountryNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeCity
    {
        private static CityDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeCity(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new CityDAL(_db, user);
        }

        private void Add(string theName, string state, string country)
        {
            City entity = dal.Factory();
            StateDAL stateDAL = new StateDAL(db, user);

            entity.Name = theName;
            entity.State = stateDAL.FindByName(state, country);


            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(),DateTime.UtcNow.ToLongDateString());

            try
            {
                dal.Create(entity);
                dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }
        
        public void Initialize()
        {
            try
            {
                Add("Quetta", "Balochistan", "pakistan");
                Add("Lahore", "punjab", "pakistan");
                Add("New York", "New York", "usa");
                Add("London", "", "UK");
                Add("karachi", "sind", "Pakistan");
            }
            catch
            { throw; }
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.Cities.Remove(item);
            }
            db.SaveChanges();


            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Cities.Remove(item);
            }
            db.SaveChanges();
        }


        public void Edit()
        {
            
            List<City> cityLst = dal.FindAll().ToList();

            if (cityLst == null)
                throw new Exception("No cities found to edit.");

            foreach (var item in cityLst)
            {
                item.Name += "*";
                dal.Update(item);
                try
                {
                    dal.Save();
                }
                catch(Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }
    }
}