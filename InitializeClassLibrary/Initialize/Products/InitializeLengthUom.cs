using System;
using System.Linq;
using System.Collections.Generic;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeLengthUom
    {
        private static UomLengthDAL _dal;
        private static ApplicationDbContext db;
        private string user;


        public InitializeLengthUom(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            _dal = new UomLengthDAL(_db, user);
        }

        private void Add(string name)
        {
            UomLength uomQty = _dal.Factory();
            uomQty.Name = name;
            //uomQty.UomLenthENUM = uomLengthEnum;

            try
            {
                _dal.Create(uomQty);
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
            Add("Inch");
            Add("mm");
            Add("m");
            Add("ft");
            Add("yd");
        }

        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                db.UomLengths.Remove(item);
            }
            db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                db.UomLengths.Remove(item);
            }
            db.SaveChanges();
        }


        public void Edit()
        {

            List<UomLength> lst =_dal.FindAll().ToList();

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