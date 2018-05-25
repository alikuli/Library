using Bearer.DAL;
using Bearer.Models;
using Bearer6.Models.PlayersNS;
using ModelsClassLibrary.Models.DeliveryMethodNS;
using ModelsClassLibrary.Models.Documents.PaymentsNS;
using ModelsClassLibrary.Models.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;


namespace InitializeClassLibrary.InitializeNS
{
    public class InitializePayment
    {
        private static PaymentDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializePayment(ApplicationDbContext dbIn, string userIn)
        {
            _db = dbIn;
            _user=userIn;
            _dal = new PaymentDAL(dbIn, _user);
        }

        private void Add(Customer customer, Owner owner, decimal amount, PaymentType paymentType)
        {

            //get list of customers
            Payment entity = _dal.Factory();






            entity.FromCustomer = null;
            entity.FromCustomerId = customer.Id;

            entity.ToOwner = null;
            entity.ToOwnerId = owner.Id;
            
            entity.PaymentType = null;
            entity.PaymentTypeId = paymentType.Id;

            entity.Date = DateTime.UtcNow;
            entity.Amount = amount;

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
            CustomerDAL custDAL = new CustomerDAL(_db, _user);
            var custArray = custDAL.FindAll().ToArray();

            OwnerDAL ownerDAL = new OwnerDAL(_db, _user);
            var ownerArray = ownerDAL.FindAll().ToArray();

            PaymentTypeDAL paymentTypeDAL = new PaymentTypeDAL(_db, _user);
            var paymentTypeArray = paymentTypeDAL.FindAll().ToArray();


            Add(custArray[0], ownerArray[0], 100.00M, paymentTypeArray[0]);


        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.Payments.Remove(item);
            }
            _db.SaveChanges();
            
            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.Payments.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Edit()
        {

            List<Payment> lst = _dal.FindAll().ToList();

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