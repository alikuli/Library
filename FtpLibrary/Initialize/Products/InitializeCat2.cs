using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.ProductNS;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeCat2
    {
        private static ProductCat2DAL _dal;
        private static ApplicationDbContext db;
        private string user;


        public InitializeCat2(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new ProductCat2DAL(_db, user);
        }

        private void Add(string categoryName)
        {
            ProductCat2 cat = _dal.Factory();
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
            Add("100");
            Add("1000");
            Add("500");
            Add("1500");
            Add("Blue");
            Add("Red");
            Add("Green");
            Add("White");
            Add("Hello");
        }

        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.ProductCat2.Remove(item);
            }
            db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.ProductCat2.Remove(item);
            }
            db.SaveChanges();
        }


        public void Edit()
        {

            List<ProductCat2> lst =_dal.FindAll().ToList();

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