using System;
using System.Linq;
using System.Collections.Generic;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeVolumeUom
    {
        private static UomVolumeDAL _dal;
        private static ApplicationDbContext db;
        private string user;


        public InitializeVolumeUom(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new UomVolumeDAL(_db, user);
        }

        private void Add(string name, double multiplier)
        {
            UomVolume uomVolume = _dal.Factory();
            uomVolume.Name = name;
            uomVolume.Multiplier = multiplier;

            try
            {
                _dal.Create(uomVolume);
                _dal.Save();
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
            try
            {
                _dal.InitializeFromEnum();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }
            catch
            {
                throw;
            }

            Add("Test",2);
        }

        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.UomVolumes.Remove(item);
            }
            db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.UomVolumes.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<UomVolume> lst =_dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.",_dal.GetSelfClassName()));

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
                    string error = AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}