using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeLanguages
    {
        private static LanguageDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeLanguages(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new LanguageDAL(_db, user);
        }

        private void Add(string language)
        {
            Language lang = dal.Factory();
            lang.Name = language;
            try
            {
                dal.Create(lang);
                dal.Save();
            }
            catch ( AliKuli.ExceptionsNS.NoDuplicateException)
            { }

        }
        

        public void Initialize()
        {
            Add("english");
            Add("urdU");
            Add("englIsh");
            Add("Hindi");
            Add("swahili");
            Add("pUnjabi");
            Add("sindhi");
            Add("arabic");
            Add("French");
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.Languages.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Languages.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<Language> lst = dal.FindAll().ToList();

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