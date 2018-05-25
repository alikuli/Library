using System;
using System.Collections.Generic;
using System.Linq;
using ErrorHandlerLibrary.ExceptionsNS;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeDeliveryMethods
    {
        private static DeliveryMethodDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeDeliveryMethods(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new DeliveryMethodDAL(_db, user);
        }

        private void Add(string theName)
        {
            DeliveryMethod entity = dal.Factory();
            
            entity.Name = theName;
            entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

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
            Add("TCS");
            Add("RCS");
            Add("UPS");
        }
        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.DeliveryMethods.Remove(item);
            }
            db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.DeliveryMethods.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<DeliveryMethod> lst = dal.FindAll().ToList();

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