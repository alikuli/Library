using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.CountryNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeCountries
    {
        private static CountryDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeCountries(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new CountryDAL(_db, user);
        }

        private void Add(string theName, string acro, string phoneCode, int phoneNumberLength, int cnicNumberLength)
        {
            Country entity = dal.Factory();
            
            entity.Name = theName;
            entity.Abbreviation = acro;
            entity.LengthOfCompleteCnicNumber = cnicNumberLength;
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
            Add("pakistan","pk","92",12,13);
            Add("india","IN","91",14,9);
            Add("bAngladesh","BN","90",13,11);
            Add("USA","us","01",15,16);
            Add("Uk","Uk","44",14,13);
            Add("englanD","EN","44",16,11);
            Add("France","FR","46",12,9);
            Add("germany","GR","48",10,7);
            Add("Holland","HO","47",9,8);
        }
        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.Countries.Remove(item);
            }
            db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Countries.Remove(item);
            }
            db.SaveChanges();
        }


        public void Edit()
        {

            List<Country> lst = dal.FindAll().ToList();

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