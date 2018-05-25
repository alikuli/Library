using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.ProductNS;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeCat3
    {
        private static ProductCat3DAL _dal;
        private static ApplicationDbContext db;
        private string user;
        

        public InitializeCat3(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new ProductCat3DAL(_db, user);
        }

        private void Add(string categoryName)
        {
            ProductCat3 cat = _dal.Factory();
            cat.Name = categoryName;
            try
            {
                _dal.Create(cat);
                _dal.Save();
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
            Add("Plastic");
            Add("Paper");
            Add("Heavy");
            Add("Light");
            Add("Trainer");
            Add("Helper");
            Add("Taxi");
            Add("Rickshaw");
            Add("Jam");
        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.ProductCat3.Remove(item);
            }
            db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.ProductCat3.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<ProductCat3> lst =_dal.FindAll().ToList();

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