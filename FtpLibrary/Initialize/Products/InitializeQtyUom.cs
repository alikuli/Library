using AliKuli.ExceptionsNS;
using AliKuli.ExceptionsNS;
using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.Inventory;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeQtyUom
    {
        private static UomQtyDAL dal;
        private static ApplicationDbContext db;
        private string user;


        public InitializeQtyUom(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new UomQtyDAL(_db, user);
        }

        private void Add(string name, double qty)
        {
            UomQty uomQty = dal.Factory();
            uomQty.Name = name;
            uomQty.Multiplier = qty;

            try
            {
                dal.Create(uomQty);
                dal.Save();
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
                dal.InitializeFromEnum();
            }
            catch (NoDuplicateException)
            {

            }
            catch { throw; }

            Add("Gross",100);
            Add("Dozen",12);
            Add("Dz",12);
            Add("Ea", 1);
            Add("Pair",2);
            Add("Count",1);
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.UomQties.Remove(item);
            }
            db.SaveChanges();


            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.UomQties.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<UomQty> lst = dal.FindAll().ToList();

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
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}