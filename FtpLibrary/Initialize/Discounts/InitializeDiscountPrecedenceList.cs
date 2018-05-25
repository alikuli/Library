using System;
using DalLibrary.DalNS;
using DbContextLibrary;
using DbContextLibrary.ModelsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeDiscountPrecedenceList
    {
        private static DiscountPrecedenceDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeDiscountPrecedenceList(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new DiscountPrecedenceDAL(_db, user);
        }

        

        private void Add(DiscountENUM discountENUM, int counter)
        {
            DiscountPrecedence entity = dal.Factory();
            

            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(),DateTime.UtcNow.ToLongDateString());
            entity.DiscountEnum = discountENUM;
            entity.Rank = counter;

            try
            {
                dal.Create(entity);
                dal.Save();
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
            var values = Enum.GetNames(typeof(DiscountENUM));
            int counter = 0;
            foreach (var item in values)
            {
                var discountEnum=  Enum.Parse(typeof(DiscountENUM),item);
                Add((DiscountENUM)discountEnum, counter);
                counter += 5;
            }
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.DiscountPrecedences.Remove(item);
            }
            db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.DiscountPrecedences.Remove(item);
            }
            db.SaveChanges();
        }

    }
}