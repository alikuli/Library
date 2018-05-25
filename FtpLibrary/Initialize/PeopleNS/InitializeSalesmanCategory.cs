using Bearer.DAL;
using Bearer.Models;
using Bearer6.DAL;
using Bearer6.Models.PlayersNS;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSalesmanCategory
    {
        private static SalesmanCategoryDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeSalesmanCategory(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new SalesmanCategoryDAL(_db, user);
        }

        private void Add(string categoryName)
        {

            SalesmanCategory entity = dal.Factory();
            entity.Name = categoryName;
            
            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(),DateTime.UtcNow.ToLongDateString());

            try
            {
                dal.Create(entity);
                dal.Save();
            }
            catch
            {

            }
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.SalesmanCategories.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.SalesmanCategories.Remove(item);
            }
            db.SaveChanges();
        }

        public void Initialize()
        {
            dal.InitializeFromEnum();
            Add("Just a test");
        }

        public void Edit()
        {

            List<SalesmanCategory> lst = dal.FindAll().ToList();

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