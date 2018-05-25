//using AliKuli.Utilities;
//using Bearer.DAL;
//using Bearer.Models;
//using ModelsClassLibrary.Models.AddressNS;
//using ModelsClassLibrary.Models.People;
//using System;
//using System.Linq;

//namespace Bearer6.Initialize
//{
//    public class InitializePersonAddress
//    {
//        private static PersonAddressDAL dal;
//        private static ApplicationDbContext _db;
//        private string _user;

//        public InitializePersonAddress (ApplicationDbContext db, string user)
//        {
//            _db = db;
//            _user= user;
//            dal = new PersonAddressDAL(_db, _user);
//        }

//        private void Add(
//            string addressName, string fname, string mName, string lName)
//        {
//            //UserDAL UserDAL = new UserDAL(_db, _user);
//            //Person person = UserDAL.FindForName(fName, mName, lName).FirstOrDefault();


//            try
//            {
//                UserDAL UserDAL = new UserDAL(_db, _user);
//                AddressDAL addressDAL = new AddressDAL(_db, _user);

//                PersonAddress personAddress = dal.Factory();

//                personAddress.Address = addressDAL.FindByName(addressName);
//                personAddress.AddressId = personAddress.Id;


//                personAddress.Person = UserDAL.FindForName(fname, mName, lName).FirstOrDefault();

//                personAddress.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

//                dal.Create(personAddress);
//                dal.Save();
//            }
//            catch(AliKuli.Exceptions.MiscNS.NoDuplicateException)
//            {
                
//            }
//            catch
//            {
//                throw;
//            }
//        }
        

//        public void Initialize()
//        {
//            Add("Ali House","Ali","Kuli","Aminuddin");
//            Add("Aila House", "Aila","","azhar");
//            Add("Selma House","Ali", "Kuli", "Aminuddin");

//        }

//        public void DeleteAll()
//        {
//            var list = dal.FindAll().ToList();
            
//            foreach (var item in list)
//            {
//                _db.PersonAddresses.Remove(item);
//            }
//            _db.SaveChanges();


//            list = dal.FindAll(true).ToList();
//            foreach (var item in list)
//            {
//                _db.PersonAddresses.Remove(item);
//            }
//            _db.SaveChanges();
//        }

//    }
//}