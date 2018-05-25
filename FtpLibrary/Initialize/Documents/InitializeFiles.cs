using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.ExceptionsNS;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;


namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeFiles
    {
        private static FileDocDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeFiles(ApplicationDbContext dbIn, string userIn)
        {
            _db = dbIn;
            _user=userIn;
            _dal = new FileDocDAL(dbIn, _user);
        }

        private void Add(Customer customer, Owner owner, decimal amount, PaymentType paymentType)
        {

            //get list of customers
           FileDoc entity = _dal.Factory();





            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}, {2}", 
                DateTime.UtcNow.ToLongTimeString(),
                DateTime.UtcNow.ToLongDateString(),
                entity.ToString());

            try
            {
                _dal.Create(entity);
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


        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.FileDocs.Remove(item);
            }
            _db.SaveChanges();
            
            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.FileDocs.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Edit()
        {

            List<FileDoc> lst = _dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format ("No '{0}' found to edit.", _dal.GetSelfClassName()));

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