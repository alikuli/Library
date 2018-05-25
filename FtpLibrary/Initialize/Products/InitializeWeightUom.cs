using AliKuli.ExceptionsNS;
using AliKuli.ExceptionsNS;
using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.Inventory;
using ModelsClassLibrary.Models.ProductNS.UOM;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeWeightUom
    {
        private static UomWeightDAL _dal;
        private static ApplicationDbContext db;
        private string user;


        public InitializeWeightUom(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new UomWeightDAL(_db, user);
        }

        private void Add(string name, UomWeightENUM UomWeightEnum )
        {
            UomWeight uomQty = _dal.Factory();
            uomQty.Name = name;
//            uomQty.UomWeightENUM = UomWeightEnum;

            try
            {
                _dal.Create(uomQty);
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
            catch (NoDuplicateException)
            {

            }
            catch { throw; }

            Add("gm", UomWeightENUM.gm);
            Add("kg", UomWeightENUM.kg);
            Add("lb", UomWeightENUM.lb);
            Add("oz", UomWeightENUM.oz);
        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.UomWeights.Remove(item);
            }
            db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.UomWeights.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<UomWeight> lst =_dal.FindAll().ToList();

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
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}