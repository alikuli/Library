using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.GeneralLedgerNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeGlAccount
    {
        private static GlAccountDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeGlAccount(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user = _user;
            dal = new GlAccountDAL(_db, user);
        }

        private void Add(string identityCard, string accountName, string accountNumber)
        {

            //try
            //{
            //    GlAccount entity = dal.Factory();

            //    Person person = new PersonDAL(db, user).FindForIdentityCard(identityCard);

            //    Now find Owner for this person
            //    Owner ownerForPerson = new OwnerDAL(db, user).FindForPerson(person.Id);

            //    entity.Name = accountName;
            //    entity.AccountNumber= accountNumber;

            //    entity.Owner = ownerForPerson;
            //    entity.OwnerId = ownerForPerson.Id;
            //    entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

            //    dal.Create(entity);
            //    dal.Save();
            //}
            //catch (AliKuli.Exceptions.MiscNS.NoDuplicateException)
            //{

            //}
            //catch
            //{
            //    throw;
            //}
        }


        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.GlAccounts.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.GlAccounts.Remove(item);
            }
            db.SaveChanges();
        }

        public void Initialize()
        {
            try
            {
                Add("1234567890120", "Sale Account", "786");
                Add("1234567890125", "Sale Account", "");
            }
            catch
            {
                throw;
            }

        }


        public void Edit()
        {

            List<GlAccount> lst = dal.FindAll().ToList();

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