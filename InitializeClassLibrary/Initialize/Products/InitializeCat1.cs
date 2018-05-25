using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeCat1
    {
        private static ProductCat1DAL _dal;
        private static ApplicationDbContext db;
        private string user;
        

        public InitializeCat1(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new ProductCat1DAL(_db, user);
        }

        private void Add(string categoryName)
        {
            ProductCat1 cat = _dal.Factory();
            cat.Name = categoryName;
            try
            {
                _dal.Create(cat);
                _dal.Save();
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
            try
            {
                _dal.InitializeFromEnumAndSave();
            }
            catch(ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {
                //do nothing
            }

            Add("Card");
            Add("Ball");
            Add("Car");
            Add("Taxi");
            Add("Computer");
            Add("Man");
            Add("Woman");
            Add("Servant");
            Add("Worker");
        }

        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.ProductCat1s.Remove(item);
            }
            db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.ProductCat1s.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<ProductCat1> lst =_dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.",_dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Name += "*";
               _dal.Update(item);
                try
                {
                   _dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    
    }
}