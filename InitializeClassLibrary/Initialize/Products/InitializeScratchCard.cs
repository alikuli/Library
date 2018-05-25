using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeScratchCard : InitalizeAbstract
    {
        private static ScratchCardDAL dal;

        public InitializeScratchCard(ApplicationDbContext _db, string _user)
            : base(_db, _user)
        {
            dal = new ScratchCardDAL(_db, _user);
            _err.ResetLibAndClass("InitializeScratchCard");
        }




        //public void Initialize()
        //{
        //    Add();
        //}


        public override void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                _db.ScratchCards.Remove(item);
            }
            _db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                _db.ScratchCards.Remove(item);
            }
            _db.SaveChanges();
        }

        public override void Edit()
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
                    _err.Add("Error in Initialization", "Edit", e);
                    throw new Exception(_err.ToString());
                }
            }

        }

        public override void Add()
        {
            dal.AddNewScratchNumbersToDb(10, 16, 100);
        }

    }
}