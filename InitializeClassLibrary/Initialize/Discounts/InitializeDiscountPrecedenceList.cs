using System;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeDiscountPrecedenceList : InitalizeAbstract
    {
        private static DiscountPrecedenceDAL dal;

        public InitializeDiscountPrecedenceList(ApplicationDbContext db, string user)
            : base(db, user)
        {
            dal = new DiscountPrecedenceDAL(db, user);
        }



        public override void Add()
        {

            var values = Enum.GetNames(typeof(DiscountENUM));
            int counter = 0;
            foreach (var item in values)
            {
                var discountEnum = (DiscountENUM)Enum.Parse(typeof(DiscountENUM), item);
                counter += 5;

                DiscountPrecedence entity = dal.Factory();
                entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());
                entity.DiscountEnum = discountEnum;
                entity.Rank = counter;

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

        }

        public override void Edit()
        {
               throw new NotImplementedException();
        }

        public override void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                _db.DiscountPrecedences.Remove(item);
            }
            _db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                _db.DiscountPrecedences.Remove(item);
            }
            _db.SaveChanges();
        }

    }
}