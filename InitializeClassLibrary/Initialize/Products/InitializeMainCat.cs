using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using DbContextLibrary.ModelsNS;
using DalLibrary.DalNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using InterfacesLibrary.SharedNS;
using ErrorHandlerLibrary.ExceptionsNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeMainCat
    {
        private static ApplicationDbContext db;
        private static ProductCategoryMainDAL _dal;

        private static ProductCat1DAL cat1Dal;
        private static ProductCat2DAL cat2Dal;
        private static ProductCat3DAL cat3Dal;
        
        private string user;


        public InitializeMainCat(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new ProductCategoryMainDAL(_db, user);

            cat1Dal = new ProductCat1DAL(_db, _user);
            cat2Dal = new ProductCat2DAL(_db, _user);
            cat3Dal = new ProductCat3DAL(_db, _user);
        }

        private void Add(string cat1, string cat2, string cat3)
        {
            try
            {
                ProductCategoryMain cat = _dal.Factory();
                if (!cat1.IsNullOrEmpty())
                {
                    ProductCat1 cat1Found = cat1Dal.SearchFor(x=>x.Name==cat1).FirstOrDefault();
                    cat.ProductCat1 = (ICategory) cat1Found;
                }

                
                if (!cat2.IsNullOrEmpty())
                {
                    ProductCat2 cat2Found = cat2Dal.SearchFor(x => x.Name == cat2).FirstOrDefault();
                    cat.ProductCat2 = (ICategory)cat2Found;
                }

                if (!cat3.IsNullOrEmpty())
                {
                    ProductCat3 cat3Found = cat3Dal.SearchFor(x => x.Name == cat3).FirstOrDefault();
                    cat.ProductCat3 = (ICategory)cat3Found;
                }

                _dal.Create(cat);
                _dal.Save();
            }

            catch (ErrorHandlerLibrary.ExceptionsNS.ProductCategoryException)
            { }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            { }
            
        }
        

        public void Initialize()
        {
            Add("car", "1000", "");
            Add("card", "1500", "jam");
            Add("computer", "500","light");
            Add("man", "blue", "paper");
            Add("servant", "green", "plastic");
            Add("taxi", "red", "rickshaw");
            Add("woman", "white", "taxi");
            Add("woman", "red", "taxi");
            Add("woman", "", "taxi");
            Add("", "100", "heavy");
            Add("ball", "", "helper");
        }

        public void DeleteAll()
        {
            var list = _dal.FindAll().AsQueryable(); ;
            foreach (var item in list)
            {
                db.ProductCategoryMains.Remove(item);
            }
            db.SaveChanges();

            list = _dal.FindAll(true).AsQueryable();
            foreach (var item in list)
            {
                db.ProductCategoryMains.Remove(item);
            }
            db.SaveChanges();
        }


        public void Edit()
        {

            List<ProductCategoryMain> lst =_dal.FindAll().ToList();

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