using Bearer.DAL;
using Bearer.Models;
using Bearer6.DAL;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.People;
using ModelsClassLibrary.Models.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSalepoints
    {
        private static SalepointDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeSalepoints(ApplicationDbContext db, string user)
        {
            _db = db;
            _user=user;
            _dal = new SalepointDAL(db, _user);
        }

        private void Add(string theName, string nationalIdCard)
        {

            try
            {

                OwnerDAL ownerDAL = new OwnerDAL(_db, _user);
                Owner owner = ownerDAL.FindForNationalIdentificationNo(nationalIdCard);

                Salepoint entity = _dal.Factory();
                entity.Name = theName;
                entity.Owner = owner;
                _dal.Create(entity);
                _dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {}

            catch
            {
                throw;
            }
        }
        

        public void Initialize()
        {
            Add("First Salepoint", "1234567890120");
            Add("My Salepoint", "1234567890123");

        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.Salepoints.Remove(item);
            }
            _db.SaveChanges();
            
            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.Salepoints.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Edit()
        {

            List<Salepoint> lst = _dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.", _dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Name += "*";
                _dal.Update(item);
                try
                {
                    _dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}