using Bearer.DAL;
using Bearer.Models;
using Bearer6.DAL;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Models.PlayersNS;
using System;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSalesman
    {
        private static SalesmanDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeSalesman(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user = _user;
            dal = new SalesmanDAL(_db, user);
        }

        private void Add(string identityCard)
        {

            try
            {
                Salesman entity = dal.Factory();

                entity.User = new UserDAL(db, user).FindForIdentityCard(identityCard);

                if (entity.User != null)
                    entity.UserId = entity.User.Id;
                else
                    throw new Exception("No User record found. initializeSalesman.Add ");


                entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

                dal.Create(entity);
                dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
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
                db.Salesmen.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Salesmen.Remove(item);
            }
            db.SaveChanges();
        }

        public void Initialize()
        {
            try
            {
                Add("1234567890123");
                Add("1234567890125");
            }
            catch
            {
                throw;
            }

        }

    }
}