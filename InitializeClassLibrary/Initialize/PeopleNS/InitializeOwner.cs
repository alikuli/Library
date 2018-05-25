

using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeOwner
    {
        private static OwnerDAL dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeOwner(ApplicationDbContext db, string user)
        {
            _db = db;
            _user=user;
            dal = new OwnerDAL(db, _user);
        }

        private void Add(string identityCard)

        {

            try
            {
                Owner entity = dal.Factory();

                entity.User = new UserDAL(_db, _user).FindForIdentityCard(identityCard);

                if (entity.User != null)
                    entity.UserId = entity.User.Id;
                else
                    throw new Exception("No User record found for the Owner");

                entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

                dal.Create(entity);
                dal.Save();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            {

            }
            catch (NotImplementedException)
            {
            }

        }


        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                _db.Owners.Remove(item);
            }
            _db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                _db.Owners.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Initialize()
        {
            try
            {
                Add("1234567890123");
                Add("1234567890120");
            }
            catch
            {
                throw;
            }

        }
        public void Edit()
        {

            List<Owner> lst = dal.FindAll().ToList();

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