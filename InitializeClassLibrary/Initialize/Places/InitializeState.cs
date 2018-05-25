using System;
using System.Linq;
using System.Collections.Generic;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeState
    {
        private static StateDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeState(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new StateDAL(_db, user);
        }

        private void Add(string theName, string abbrev, string country)
        {
            State entity = dal.Factory();
            CountryDAL countryDAL = new CountryDAL(db, user);

            entity.Name = theName;
            entity.StateAbbreviation = abbrev;
            entity.Country = countryDAL.SearchFor(x => x.Name.ToLower() == country.ToLower()).FirstOrDefault();


            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(),DateTime.UtcNow.ToLongDateString());

            try
            {
                dal.Create(entity);
                dal.Save();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }
        
        public void Initialize()
        {
            Add("punjab", "PN", "pakistan");
            Add("pukhtunKhwa", "", "pakistan");
            Add("sind", "", "pakistan");
            Add("balochistan", "", "pakistan");

            Add("New York", "NY", "USA");
            Add("Nevada", "NV", "USA");
            Add("California", "CA", "USA");
            Add("","","UK");
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.States.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.States.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<State> lst = dal.FindAll().ToList();

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