using AliKuli.Utilities.ExceptionNS;
using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.ProductNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSetup
    {
        private static ApplicationDbContext db;
        private static SetupDAL _dal;
        private string user;
        

        public InitializeSetup(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new SetupDAL(_db, user);
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
            ErrorStringBuilder.ClearString();
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
                }
            }

            return ErrorStringBuilder.ErrorString; 
        }


    }
}