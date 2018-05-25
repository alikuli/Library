


using System;
using AliKuli.Extentions;
using System.Linq;
using System.Collections.Generic;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeCustomer
    {
        private static CustomerDAL dal;
        private static ApplicationDbContext db;
        private string user;


        public InitializeCustomer(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new CustomerDAL(_db, user);
        }

        private void Add(string categoryName, string identityCard)

        {

            try
            {
                Customer entity = dal.Factory();

                entity.User = new UserDAL(db, user).FindForIdentityCard(identityCard);

                if (entity.User == null)
                    throw new Exception("No User record found. initializeCustomer.Add ");

                entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

                entity.CustomerCategory = new CustomerCategoryDAL(db, user).FindForName(categoryName);
                entity.CustomerCategoryId = entity.CustomerCategory.Id;


                entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());


                dal.Create(entity);
                dal.Save();
            }
            catch ( ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {

            }
            catch (NotImplementedException)
            {
            }
            catch
            {
                throw;
            }
        }


        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.Customers.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Customers.Remove(item);
            }
            db.SaveChanges();
        }

        public void Initialize()
        {
            try
            {

                Add(CustomerCategoryENUM.EndCustomer.ToString().ToSentence().ToTitleCase(), "1234567890123");
                Add(CustomerCategoryENUM.Serviceman.ToString().ToSentence().ToTitleCase(), "1234567890120");
                Add(CustomerCategoryENUM.EndCustomer.ToString().ToSentence().ToTitleCase(), "1234567890125");
            }
            catch
            {
                throw;
            }

        }

        public void Edit()
        {

            List<Customer> lst = dal.FindAll().ToList();

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