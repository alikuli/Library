using System;
using System.Linq;
using System.Collections.Generic;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeProductIdentifierType
    {
        private static ProductIdentifierTypeDAL _dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeProductIdentifierType(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new ProductIdentifierTypeDAL(_db, user);
        }

        private void Add(string ident)
        {
            ProductIdentifierType prodIdentType = _dal.Factory();
            prodIdentType.Name = ident;
            try
            {
                _dal.Create(prodIdentType);
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
            Add("ASIN");
            Add("ItemNo");
            Add("StapleNo");
            Add("CardNo");
        }

        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.ProductIdentifierTypes.Remove(item);
            }
            db.SaveChanges();

            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.ProductIdentifierTypes.Remove(item);
            }
            db.SaveChanges();

        }

        public void Edit()
        {

            List<ProductIdentifierType> lst = _dal.FindAll().ToList();

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
                    string error = AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}