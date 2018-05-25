using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeScratchCard
    {
        private static ScratchCardDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeScratchCard(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new ScratchCardDAL(_db, user);
        }


        

        public void Initialize()
        {
            dal.AddNewScratchNumbersToDb(10, 16, 100);
        }
        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.ScratchCards.Remove(item);
            }
            db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.ScratchCards.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<ScratchCard> lst = dal.FindAll().ToList();

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
                    string error = AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }
    }
}