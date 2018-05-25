//using Bearer.DAL;
//using Bearer.Models;
//using Bearer6.Models.PlayersNS;
//using System;
//using AliKuli.Extentions;
//using ModelsClassLibrary.Models.ProductNS;

//namespace Bearer6.Initialize
//{
//    public class InitializeAddressCategory
//    {
//        private static AddressCategoryDAL dal;
//        private static ApplicationDbContext db;
//        private string user;

//        public InitializeAddressCategory(ApplicationDbContext _db, string _user)
//        {
//            db = _db;
//            user=_user;
//            dal = new AddressCategoryDAL(_db, user);
//        }


//        public void DeleteAll()
//        {
//            var list = dal.FindAll();
//            foreach (var item in list)
//            {
//                db.AddressCategories.Remove(item);
//            }
//            db.SaveChanges();

//            list = dal.FindAll(true);
//            foreach (var item in list)
//            {
//                db.AddressCategories.Remove(item);
//            }
//            db.SaveChanges();
//        }

//        public void Initialize()
//        {
//            try
//            {
//                dal.InitializeFromEnum();
//                dal.Save();
//            }
//            catch
//            {
//                throw;
//            }

//        }

//    }
//}