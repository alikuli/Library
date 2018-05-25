using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSetup
    {
        private static ApplicationDbContext db;
        private static SetupDAL _dal;
        private string user;
        private ErrorSet _err;        

        public InitializeSetup(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new SetupDAL(_db, user);
            _err = new ErrorSet("InitializeClassLibrary", "InitializeSetup", _user);
        }

        

        public void Initialize()
        {
            try
            {
                _dal.InitializeSetUp();
            }
            catch { throw; }
        }

        public void DeleteAll()
        {
            try
            {
                var list = _dal.FindAll();
                foreach (var item in list)
                {
                    db.SetUps.Remove(item);
                }
                db.SaveChanges();

                list = _dal.FindAll(true);
                foreach (var item in list)
                {
                    db.SetUps.Remove(item);
                }
                db.SaveChanges();
            }
            catch { throw; }
        }

        public string Edit()
        {

            List<Setup> lst = _dal.FindAll().ToList();
            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.", _dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Value += "*";
                try
                {
                    _dal.Update(item);
                    _dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                    _err.Add(error, "Edit");
                }
            }

            return _err.ToString(); 
        }


    }
}