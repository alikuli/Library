using System;
using System.Collections.Generic;
using System.Linq;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeWarehouses
    {
        private static WarehouseDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeWarehouses(ApplicationDbContext db, string user)
        {
            _db = db;
            _user=user;
            _dal = new WarehouseDAL(db, _user);
        }

        private void Add(string theName, string nationalIdCard)
        {

            try
            {

                //PersonDAL personDAL = new PersonDAL(_db, _user);
                //Person person = personDAL.FindForIdentityCard(nationalIdCard);//If null it will throw exception

                OwnerDAL ownerDAL = new OwnerDAL(_db, _user);
                Owner owner = ownerDAL.FindForNationalIdentificationNo(nationalIdCard);

                Warehouse entity = _dal.Factory();
                entity.Name = theName;
                entity.Owner = owner;
                _dal.Create(entity);
                _dal.Save();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }
        

        public void Initialize()
        {
            Add("First Warehouse", "1234567890123");
            Add("My Warehouse", "1234567890120");

        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.Warehouses.Remove(item);
            }
            _db.SaveChanges();
            
            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.Warehouses.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Edit()
        {

            List<Warehouse> lst = _dal.FindAll().ToList();

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